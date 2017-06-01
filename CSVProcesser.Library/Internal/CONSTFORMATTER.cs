using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVProcesser.Library
{
    internal class CONSTFORMATTER
    {
        /// <summary>
        /// 需要做入栈特殊处理的字节
        /// </summary>
        public static readonly byte[] NEEDSTACK = new byte[] 
        { 
            (byte)'"',
            (byte)'\''
        };

        /// <summary>
        /// 每一列的分隔符
        /// </summary>
        public static readonly byte SPALITOR = (byte)',';

        /// <summary>
        /// 转义符
        /// </summary>
        public static readonly byte TRANSLATION = (byte)'\\';

        /// <summary>
        /// 换行符
        /// </summary>
        public static readonly byte LINEBREAK = (byte)'\n';
    }
}
