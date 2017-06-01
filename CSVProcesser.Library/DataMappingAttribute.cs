using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CSVProcesser.Library
{
    /// <summary>
    /// 数据字典，定义属性的别名
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class DataMappingAttribute : Attribute, ICloneable
    {
        public static bool operator ==(DataMappingAttribute a, DataMappingAttribute b)
        {
            if (object.Equals(a, null)
                && object.Equals(b, null))
            {
                return true;
            }
            if (!object.Equals(a, b))
            {
                return false;
            }
            return object.Equals(a.Name, b.Name);
        }

        public static bool operator !=(DataMappingAttribute a, DataMappingAttribute b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            DataMappingAttribute attr = obj as DataMappingAttribute;
            return this == attr;
        }

        private string m_Name;
        private TypeCode m_TypeCode;

        public DataMappingAttribute(string name, TypeCode typeCode)
        {
            this.m_Name = name;
            this.m_TypeCode = typeCode;
        }

        public DataMappingAttribute() : this(null, default(TypeCode)) { }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public TypeCode TypeCode
        {
            get
            {
                return this.m_TypeCode;
            }
            set
            {
                this.m_TypeCode = value;
            }
        }

        public object Clone()
        {
            DataMappingAttribute attr = new DataMappingAttribute(this.m_Name, this.m_TypeCode);
            return attr;
        }
    }
}
