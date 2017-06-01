using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSVProcesser.Library
{
    /// <summary>
    /// 
    /// </summary>
    internal class ThreadAsyncStreamReader : StreamReader,_Reader
    {
        public ThreadAsyncStreamReader(string fileName)
            : base(fileName)
        {
        }

        public override int Peek()
        {
            Monitor.Enter(this);
            try
            {
                return base.Peek();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public override int Read()
        {
            Monitor.Enter(this);
            try
            {
                return base.Read();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public override int Read(char[] buffer, int index, int count)
        {
            Monitor.Enter(this);
            try
            {
                return base.Read(buffer, index, count);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            Monitor.Enter(this);
            try
            {
                return base.ReadBlock(buffer, index, count);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public override string ReadLine()
        {
            Monitor.Enter(this);
            try
            {
                return base.ReadLine();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        public override string ReadToEnd()
        {
            Monitor.Enter(this);
            try
            {
                return base.ReadToEnd();
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
    }
}
