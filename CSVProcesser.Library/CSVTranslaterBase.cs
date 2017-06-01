using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSVProcesser.Library
{
    public abstract class CSVTranslaterBase<T> : ITranslateCSV<T>
        where T : class
    {
        public abstract long Translate(string fileName);
        /// <summary>
        /// 解析csv文件的表头
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected List<CSVHeader> GetCSVHeaders<T>(StreamReader stream)
        {
            //获取数据模型的类型
            Type type = typeof(T);
            //获取数据模型的属性集合
            Dictionary<DataMappingAttribute, PropertyInfo> mapping = DataMappingCache.Current[type];
            //设置文件流指针指向文件头
            stream.BaseStream.Position = 0L;
            string str = stream.ReadLine();
            int index = 0;
            string name;
            List<CSVHeader> headers = new List<CSVHeader>(50);
            using (CSVReader reader = new CSVReader(str))
            {
                while ((index = reader.Read(out name)) > -1)
                {
                    headers.Add(new CSVHeader
                    {
                        Index = index,
                        Name = name
                    });
                }
            }

            //做Mapping映射，将csv的列名、列索引和要解析的数据模型属性做映射关系
            Mapping(headers, mapping);
            return headers;
        }

        private void Mapping(List<CSVHeader> headers, Dictionary<DataMappingAttribute, PropertyInfo> properties)
        {
            foreach (CSVHeader header in headers)
            {
                foreach (DataMappingAttribute item in properties.Keys)
                {
                    if (item.Name == header.Name)
                    {
                        header.Property = properties[item];
                        header.DataMapping = item;
                        break;
                    }
                }
            }
        }

        protected void Process<T>(Context context)
        {
#if DEBUG
            Stopwatch watch = new Stopwatch();
            watch.Start();
#endif
            string csvString = null;
            long count = 0L;
            while ((csvString = context.Reader.ReadLine()) != null)
            {
                T entity = TranslateEntity<T>(context.Headers, csvString);
                count++;

                if (OnTranslateRow != null)
                {
                    OnTranslateRow.Invoke(csvString, entity);
                }
            }
            context.Count += count;
#if DEBUG
            watch.Stop();
            Console.WriteLine("线程：{0},共计耗时：{1}", Thread.CurrentThread.ManagedThreadId, watch.ElapsedMilliseconds);
#endif
        }

        protected T TranslateEntity<T>(List<CSVHeader> headers, string csvString)
        {
            if (string.IsNullOrEmpty(csvString))
            {
                return default(T);
            }
            T result = Activator.CreateInstance<T>();
            using (CSVReader reader = new CSVReader(csvString))
            {
                int index = -1;
                string tmp;
                CSVHeader header;
                while ((index = reader.Read(out tmp)) > -1)
                {
                    try
                    {
                        header = headers[index];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        break;
                    }
                    catch (IndexOutOfRangeException)
                    {
                        break;
                    }
                    if (header != null
                        && header.Property != null
                        && header.DataMapping != null)
                    {
                        if (header.Property.CanWrite)
                        {
                            if (header.DataMapping.TypeCode == TypeCode.String)
                            {
                                header.Property.SetValue(result, tmp, null);
                            }
                            else
                            {
                                object value = Convert.ChangeType(tmp, header.DataMapping.TypeCode);
                                header.Property.SetValue(result, value, null);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public event TranslateRow OnTranslateRow;

        protected class Context
        {
            public Context() { }

            public Context(_Reader reader, List<CSVHeader> headers) : this(reader, headers, 0L) { }

            public Context(_Reader reader, List<CSVHeader> headers, long count)
            {
                this.Count = count;
                this.Reader = reader;
                this.Headers = headers;
            }
            /// <summary>
            /// 解析到的数据条数
            /// </summary>
            public long Count { get; set; }

            /// <summary>
            /// 文件读取器
            /// </summary>
            public _Reader Reader { get; set; }

            /// <summary>
            /// 表头
            /// </summary>
            public List<CSVHeader> Headers { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public WaitHandle Handle { get; set; }
        }

    }
}
