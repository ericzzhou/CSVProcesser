using System;

namespace IPP.ToolsMgmt.JobV31.Entity
{
    public class EntityItem
    {
        public string BrandName { get; set; }
        public string ItemName { get; set; }
        public int ItemLevel { get; set; }
        public int? ParentSysNo { get; set; }
    }
}
