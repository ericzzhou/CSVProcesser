
using CSVProcesser.Library;
namespace IPP.ToolsMgmt.JobV31.Entity
{
    /// <summary>
    /// Truesight系统用户访问统计
    /// </summary>
    public class TruesightStatistics
    {

       [DataMapping("x-page-name", System.TypeCode.String)]               
       public string x_page_name { get; set; }
       [DataMapping("cs(Host)", System.TypeCode.String)]                   
       public string cs_Host { get; set; }
       [DataMapping("cs-uri-stem", System.TypeCode.String)]               
       public string cs_uri_stem { get; set; }
       [DataMapping("x-start-time", System.TypeCode.String)]              
       public string x_start_time { get; set; }
       [DataMapping("x-end-time", System.TypeCode.String)]                
       public string x_end_time { get; set; }                             
    }
}
