using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSVProcesser.Library
{
    public class CSVThreadTranslater<T> : CSVTranslaterBase<T>, ITranslateCSV<T>
        where T : class
    {
        private int m_MaxThreadCount;

        public CSVThreadTranslater() : this(64) { }

        public CSVThreadTranslater(int maxThreadCount)
        {
            if (maxThreadCount > 64)
            {
                throw new ArgumentOutOfRangeException("maxThreadCount", maxThreadCount, "最大子线程数不能超过64");
            }
            this.m_MaxThreadCount = maxThreadCount;
        }

        public override long Translate(string fileName)
        {
            long count = 0L;
            //表头
            List<CSVHeader> headers;
            using (StreamReader reader = new StreamReader(fileName))
            {
                //解析表头
                headers = GetCSVHeaders<T>(reader).OrderBy(item => item.Index).ToList();
            }

            PartialStreamReader[] readers = BuildReaders(fileName, m_MaxThreadCount + 1);
            WaitHandle[] handles = null;
            PartialStreamReader mainReader = readers[0];//主线程需要使用的文件流
            Context context = null;
            List<Context> contextList = new List<Context>(readers.Length * 2);
            if (readers.Length > 1)
            {
                handles = new WaitHandle[readers.Length - 1];
                PartialStreamReader pr = null;
                for (int i = 1; i < readers.Length; i++)
                {
                    pr = readers[i];
                    EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                    handles[i - 1] = handle;
                    context = new Context(pr, headers);
                    context.Handle = handle;
                    contextList.Add(context);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(obj =>
                    {
                        Context t = (Context)obj;
                        try
                        {
                            Process<T>(t);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                        finally
                        {
                            ((EventWaitHandle)t.Handle).Set();
                        }
                    }), context);
                    //Action<Context, EventWaitHandle> action = new Action<Context, EventWaitHandle>((t, d) =>
                    //{
                    //    try
                    //    {
                    //        Process<T>(t);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Debug.WriteLine(ex.ToString());
                    //    }
                    //    finally
                    //    {
                    //        d.Set();
                    //    }
                    //});

                    //action.BeginInvoke(context, handle, null, null);


                }
            }

            context = new Context(mainReader, headers);
            Process<T>(context);
            count = context.Count;
            if (handles != null
                && handles.Length > 0)
            {
                WaitHandle.WaitAll(handles);
                count += contextList.Sum(item => item.Count);
            }

            return count;
        }

        /// <summary>
        /// 生成部分读取数据流对象集合
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="maxCount">最大对象数量</param>
        /// <returns></returns>
        private PartialStreamReader[] BuildReaders(string fileName, int maxCount)
        {
#if DEBUG
            Stopwatch watch = new Stopwatch();
            watch.Start();
#endif
            List<PartialStreamReader> readers = new List<PartialStreamReader>(maxCount * 2);
            for (int i = 0; i < maxCount; i++)
            {
                readers.Add(new PartialStreamReader(fileName, 0L, 0L));
            }
            long max = 0L;
            using (StreamReader reader = new StreamReader(fileName))
            {
                long startPosition;
                startPosition = reader.BaseStream.Position;
                long index = 0L;
                PartialStreamReader pr = null;
                int len = 100000;

                List<Dictionary<long, long>> linePositionMapping = new List<Dictionary<long, long>>(10000);

                Dictionary<long, long> m = new Dictionary<long, long>(len);
                string l;
                while ((l = reader.ReadLine()) != null)
                {
                    if (m.Count == len)
                    {
                        linePositionMapping.Add(m);
                        m = new Dictionary<long, long>(len);
                    }
                    m[index] = reader.BaseStream.Position;
                    index++;
                }
                if (m.Count > 0)
                {
                    linePositionMapping.Add(m);
                }
                max = linePositionMapping.Sum(item => item.Count);//总行数
                max -= 1;//去除表头
                long lines = max / maxCount;//每个Stream要处理的行数
                int lines1 = (int)(max % maxCount);//剩余的行数，这些行将被附加到Stream集合的前排
                index = 0;
                for (int i = 0; i < maxCount; i++)
                {
                    pr = readers[i];
                    pr.MaxReadLine = lines;
                    if (lines1 > 0)
                    {
                        pr.MaxReadLine += 1;
                        lines1--;
                    }
                    pr.Position = linePositionMapping[(int)(index / (long)len)][index];
                    index += pr.MaxReadLine;
                }
            }

            PartialStreamReader[] result = readers.Where(item => item.MaxReadLine > 0L).ToArray();

#if DEBUG
            watch.Stop();

            Console.WriteLine("总行数:{0}", max);
            Console.WriteLine("预读文件共计耗时:{0}", watch.ElapsedMilliseconds);

            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine("reader-position:{0},line-count:{1}", result[i].Position, result[i].MaxReadLine);
            }
#endif
            return result;
        }

    }
}
