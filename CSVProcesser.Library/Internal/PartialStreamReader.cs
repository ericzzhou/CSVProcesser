using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVProcesser.Library
{
    internal class PartialStreamReader : IDisposable,_Reader
    {
        /// <summary>
        /// 要读取的行数
        /// </summary>
        private long m_LineCount;
        
        private StreamReader m_Reader;

        /// <summary>
        /// 当前读取了多少行
        /// </summary>
        private long m_CurrentReadLine;

        /// <summary>
        /// 最大读取的数据行数
        /// </summary>
        public long MaxReadLine
        {
            get
            {
                return m_LineCount;
            }
            set
            {
                m_LineCount = value;
            }
        }

        /// <summary>
        /// 指针位置
        /// </summary>
        public long Position
        {
            get
            {
                if (m_Reader != null)
                {
                    return m_Reader.BaseStream.Position;
                }
                return 0L;
            }
            set
            {
                if (m_Reader != null)
                {
                    m_Reader.BaseStream.Position = value;
                    m_CurrentReadLine = 0L;
                }
            }
        }

        public PartialStreamReader(string fileName)
            : this(fileName, 0L, 0L)
        { 
        }
        
        public PartialStreamReader(string fileName, long position, long lineCount)
        {
            m_Reader = new StreamReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
            m_Reader.BaseStream.Position = position;
            this.m_LineCount = lineCount;
            this.m_CurrentReadLine = 0L;
        }

        public string ReadLine()
        {
            if (m_LineCount > 0L    //设置了最大读取行数
                && m_CurrentReadLine >= m_LineCount//并且当前未读完目标行数
                )
            {
                return null;
            }
            string str = m_Reader.ReadLine();
            m_CurrentReadLine++;
            return str;
        }

        public void Dispose()
        {
            if (m_Reader != null)
            {
                m_Reader.Dispose();
                m_Reader = null;
            }
        }
    }
}
