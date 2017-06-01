using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVProcesser.Library
{
    /// <summary>
    /// 定义csv文件的表头
    /// </summary>
    public class CSVHeader
    {
        /// <summary>
        /// 标记第几列，从0开始
        /// </summary>
        internal int Index { get; set; }

        /// <summary>
        /// csv文件的列头名称
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 对应的属性
        /// </summary>
        internal PropertyInfo Property { get; set; }

        /// <summary>
        /// 映射的属性类
        /// </summary>
        internal DataMappingAttribute DataMapping { get; set; }
    }
}
