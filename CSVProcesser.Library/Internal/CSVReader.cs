using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVProcesser.Library
{
    internal class CSVReader : IDisposable
    {
        private Stream m_Reader = null;//读取器
        private Stream m_Writer = null;//写入器
        private Stack<byte> m_Stack = null;//需要做特殊处理的字节栈
        private int m_Index;//当前第几列
        internal CSVReader(string csvString)
        {
            if (string.IsNullOrEmpty(csvString))
            {
                throw new ArgumentNullException("csvString");
            }
            byte[] buffer = Encoding.Default.GetBytes(csvString);
            m_Reader = new MemoryStream(buffer);
            m_Writer = new MemoryStream();
            m_Stack = new Stack<byte>(buffer.Length);
        }

        /// <summary>
        /// 每次解读一列
        /// </summary>
        /// <param name="result">解读到的一列的值</param>
        /// <returns>当前第N列，当N=-1时，解析完成</returns>
        internal int Read(out string result)
        {
            int c = -1;
            int leng = 0;
            byte b;
            byte? lastStackByte = null;
            m_Writer.Position = 0L;
            bool ignore = false;//是否忽略下一个字节，当读取到转义符时，需要忽略下一个字节
            while ((c = m_Reader.ReadByte()) > -1)
            {
                b = (byte)c;
                if (b == CONSTFORMATTER.SPALITOR    //每列的分隔符
                    && m_Stack.Count == 0           //栈当中没有需要匹配的结束符
                    )
                {
                    break;
                }

                m_Writer.WriteByte(b);
                leng++;
                if (b == CONSTFORMATTER.TRANSLATION) //判断是否为转义符
                {
                    ignore = !ignore;
                }
                else
                {
                    if (!ignore //当前没有转义符存在
                        && CONSTFORMATTER.NEEDSTACK.Contains(b))//检测是否需要入栈操作
                    {
                        //如果最后一个入栈的字节和本次检测到的字节相等，则出栈
                        //否则即入栈
                        if (m_Stack.Count == 0)
                        {
                            m_Stack.Push(b);
                        }
                        else
                        {
                            lastStackByte = m_Stack.Peek();
                            if (lastStackByte == b)
                            {
                                m_Stack.Pop();
                            }
                            else
                            {
                                m_Stack.Push(b);
                            }
                        }
                    }

                    if (ignore)
                    {
                        ignore = false;
                    }
                }
            }
            byte[] buffer = new byte[leng];
            m_Writer.Position = 0;
            m_Writer.Read(buffer, 0, leng);

            result = Encoding.Default.GetString(buffer);

            return c == -1 && leng == 0 ? -1 : m_Index++;
        }

        public void Dispose()
        {
            if (m_Writer != null)
            {
                m_Writer.Dispose();
            }
            if (m_Reader != null)
            {
                m_Reader.Dispose();
            }
            m_Writer = null;
            m_Reader = null;
            m_Stack = null;
        }
    }
}
