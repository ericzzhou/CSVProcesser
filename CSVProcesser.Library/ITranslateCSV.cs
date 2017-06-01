using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVProcesser.Library
{
    public interface ITranslateCSV<T> where T : class
    {
        /// <summary>
        /// 解析csv文件为自定义的类型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">csv文件路径</param>
        /// <returns>解析到的数据数量</returns>
        long Translate(string fileName);

        /// <summary>
        /// 解析到每行数据时的回调时间
        /// </summary>
        event TranslateRow OnTranslateRow;
    }


    /// <summary>
    /// 解析每一行数据
    /// </summary>
    /// <param name="csvString">原始csv字符串</param>
    /// <param name="result">解析到的数据模型</param>
    /// <returns></returns>
    public delegate void TranslateRow(string csvString, object result);
}
