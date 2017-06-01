using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IPP.ToolsMgmt.JobV31.Entity
{
    [XmlRoot("mapping")]
    [Serializable]
    public class MappingConfiguration
    {
        [XmlElement("map")]
        public List<MapConfiguration> Mappings { get; set; }
    }

    [Serializable]
    [XmlRoot("map")]
    public class MapConfiguration
    {
        [XmlAttribute("key")]
        public string Key { get; set; }

        [XmlText()]
        public string Value { get; set; }
    }
}
