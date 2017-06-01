using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSVProcesser.Library
{
    public class CSVTranslater<T> : CSVTranslaterBase<T>
        where T : class
    {
        private int m_MaxThreadCount;//最大线程数
        private ThreadManager m_ThreadManager = null;
        private Dictionary<DataMappingAttribute, PropertyInfo> m_Mapping = null;

        public CSVTranslater()
            : this(0)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxThreadCount">最大开启的子线程数量</param>
        public CSVTranslater(int maxThreadCount)
        {
            if (maxThreadCount > 64)
            {
                throw new ArgumentOutOfRangeException("maxThreadCount", maxThreadCount, "最大子线程数不能超过64");
            }
            this.m_MaxThreadCount = maxThreadCount;

            //this.m_ThreadManager = new ThreadManager();
            //获取数据模型的类型
            Type type = typeof(T);
            //获取数据模型的属性集合
            Dictionary<DataMappingAttribute, PropertyInfo> mapping = DataMappingCache.Current[type];
            this.m_Mapping = mapping;
        }

        /// <summary>
        /// 解析csv文件为自定义的类型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">csv文件路径</param>
        /// <returns>解析到的数据数量</returns>
        public override long Translate(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format("文件{0}不存在", fileName), fileName);
            }

           // InitThreads(this.m_MaxThreadCount);

            long count = 0L;
            
            //表头
            List<CSVHeader> headers;
            ThreadAsyncStreamReader reader = new ThreadAsyncStreamReader(fileName);
            //解析表头
            headers = GetCSVHeaders<T>(reader).OrderBy(item => item.Index).ToList();

            using (reader)
            {
                Context context = null;
                if (this.m_MaxThreadCount <= 0)
                {
                    context = new Context(reader, headers);
                    Process<T>(context);// Process<T>(context);
                    count = context.Count;
                }
                else
                {
                    WaitHandle[] handles = new WaitHandle[m_MaxThreadCount];
                    List<Context> contextList = new List<Context>(m_MaxThreadCount * 2);
                    for (int i = 0; i < this.m_MaxThreadCount; i++)
                    {
                        EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.ManualReset);
                        handles[i] = handle;
                        context = new Context(reader, headers);
                        context.Handle = handle;
                        contextList.Add(context);

                        //string name = string.Format("thread-{0}",i);
                        //Thread thread = this.m_ThreadManager[name];
                        //thread.Start(context);


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
                    context = new Context(reader, headers);
                    Process<T>(context);
                    count = context.Count;
                    WaitHandle.WaitAll(handles);
                    count += contextList.Sum(item => item.Count);
                }
            }

            return count;
        }

        private void InitThreads(int threadCount)
        {
            if (threadCount > 0)
            {
                for (int i = 0; i < threadCount; i++)
                {
                    Thread thread = new Thread(Dowork);
                    this.m_ThreadManager.Add(string.Format("thread-{0}", i), thread);
                }
            }
        }

        private void Dowork(object obj)
        {
            Context t = (Context)obj;
            if (t == null)
            {
                return;
            }
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
                EventWaitHandle handle = t.Handle as EventWaitHandle;
                if (handle != null)
                {
                    handle.Set();
                }
            }
        }

       

    }
}
