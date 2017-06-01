using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CSVProcesser.Library
{
    internal class ThreadManager
    {
        private Dictionary<string, Thread> m_ThreadMapping;
        private Dictionary<int, string> m_ThreadIdNameMapping;

        public string CurrentThreadName
        {
            get
            {
                int id = Thread.CurrentThread.ManagedThreadId;
                if (m_ThreadIdNameMapping.ContainsKey(id))
                {
                    return m_ThreadIdNameMapping[id];
                }
                return null;
            }
        }

        public int Count
        {
            get
            {
                return this.m_ThreadMapping.Count;
            }
        }

        public Thread this[string name]
        {
            get
            {
                return m_ThreadMapping[name];
            }
        }

        public Thread[] AllThreads
        {
            get
            {
                return m_ThreadMapping.Values.ToArray();
            }
        }

        public ThreadManager()
        {
            m_ThreadMapping = new Dictionary<string, Thread>();
            m_ThreadIdNameMapping = new Dictionary<int, string>();
        }

        public void Add(string name, Thread thread)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (thread == null)
            {
                throw new ArgumentNullException("thread");
            }
            CheckThreadExists(name);
            int id = thread.ManagedThreadId;
            m_ThreadIdNameMapping[id] = name;
            m_ThreadMapping[name] = thread;
        }

        private void Remove(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (m_ThreadMapping.ContainsKey(name))
            {
                Thread thread = null;
                thread = m_ThreadMapping[name];
                //如果线程还处于存活状态，不允许创建新的同名线程
                if (thread != null
                    && !thread.ThreadState.HasFlag(ThreadState.Stopped)
                    && !thread.ThreadState.HasFlag(ThreadState.Aborted)
                    )
                {
                    throw new ThreadStateException(string.Format("线程{0}存活中，不允许移除", name));
                }
                m_ThreadMapping.Remove(name);
                int id = thread.ManagedThreadId;
                m_ThreadIdNameMapping.Remove(id);
            }
        }

        private void CheckThreadExists(string name)
        {
            Thread thread = null;
            if (m_ThreadMapping.ContainsKey(name))
            {
                thread = m_ThreadMapping[name];
                //如果线程还处于存活状态，不允许创建新的同名线程
                if (thread != null
                    && !thread.ThreadState.HasFlag(ThreadState.Stopped)
                    && !thread.ThreadState.HasFlag(ThreadState.Aborted)
                    )
                {
                    throw new ThreadStateException(string.Format("线程{0}存活中，不允许创建新的同名线程", name));
                }
            }
        }
    }
}
