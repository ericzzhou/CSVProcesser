using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newegg.Oversea.Framework.JobConsole.Client;
using IPP.ToolsMgmt.JobV31.BIZ;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using IPP.ToolsMgmt.JobV31.library;
using System.Threading;
using System.Diagnostics;
using CSVProcesser.Library;
using IPP.ToolsMgmt.JobV31.Entity;

namespace IPP.ToolsMgmt.JobV31
{

    public class JobAutoRun : IJobAction
    {
        private static readonly object LockObj = new object();

        Dictionary<string, int> domains = new Dictionary<string, int>();
        string[] domainArr = ConfigurationManager.AppSettings["domains"].Split(',');
        string time = "";
        string dowloadUrl = "";
        public JobAutoRun()
        {
            //2013.12.12
            time = DateTime.Now.AddDays(-1).ToString("yyyy.MM.dd");
            var downloadUrl = GetMappingConfig().Mappings.Where(x => x.Key == "downloadUrl").Select(x => x.Value).FirstOrDefault();


            bool deployTime = Convert.ToBoolean(ConfigurationManager.AppSettings["deployTime"]);
            string deploy_begin = ConfigurationManager.AppSettings["deploy_begin"];
            string deploy_end = ConfigurationManager.AppSettings["deploy_end"];

            if (deployTime && !string.IsNullOrWhiteSpace(deploy_begin) && string.IsNullOrWhiteSpace(deploy_end))
            {
                dowloadUrl = string.Format(downloadUrl, deploy_begin.Trim(), deploy_end.Trim());
            }
            else
            {
                dowloadUrl = string.Format(downloadUrl, time, time);
            }
        }

        public void Run(JobContext context)
        {

            PrintLog("---begin----------------------");
            try
            {
                DownloadHelper downer = new DownloadHelper(dowloadUrl);
                PrintLog("正在下载...");
                downer.Download();
                //downer.FilePath = @"E:\Work\WorkSpace\SourceCode\Domain name to access statistics\IPP.ToolsMgmt.JobHost\bin\Debug\tem\test.zip";

                PrintLog(string.Format("下载完成，文件保存路径：{0}", downer.FilePath));
                PrintLog("正在解压缩...");
                ZipHelper ziper = new ZipHelper(downer.FilePath, "");
                ziper.UnZip();

                PrintLog("解压缩完成.");
                PrintLog(string.Format("文件保存路径：{0}", ziper.TargetDirectory));

                string[] files = Directory.GetFiles(ziper.TargetDirectory);

                PrintLog("正在检索是否包含 page-开头的 .csv.gz 文件...");
                string zipFile2 = "";
                string zipFile2Path = "";
                foreach (string file in files)
                {
                    string f = Path.GetFileName(file);
                    if (f.StartsWith("page-") && f.EndsWith(".csv.gz"))
                    {
                        PrintLog(string.Format("检索到文件：{0}", f));
                        zipFile2 = f;
                        zipFile2Path = file;
                        break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(zipFile2Path))
                {
                    PrintLog(string.Format("正在解压缩：{0}...", zipFile2));
                    GZipHelper gziper = new GZipHelper(zipFile2Path, "");
                    gziper.UnZip();

                    PrintLog(string.Format("解压缩完成：{0}", zipFile2));
                    PrintLog(string.Format("保存路径：{0}", gziper.TargetDirectory));
                    PrintLog(string.Format("文件{0}", gziper.TargetFileName));


                    string dataCsv = gziper.TargetDirectory + gziper.TargetFileName;
                    // string dataCsv = @"E:\Work\WorkSpace\SourceCode\Domain name to access statistics\IPP.ToolsMgmt.JobHost\bin\Debug\page-full.csv";
                    PrintLog("正在解析CSV...");
                    long CsvCount = 0;

                    CSVTranslaterBase<TruesightStatistics> translater = new CSVTranslater<TruesightStatistics>(10);
                    translater.OnTranslateRow += translater_OnTranslateRow;
                    CsvCount = translater.Translate(dataCsv);
                    PrintLog(string.Format("CSV文件解析完毕,共{0}条数据", CsvCount));
                    PrintLog("统计结果如下：");

                    FormatEmail();

                }
                else
                {
                    PrintLog("未检索到包含 page-开头的 zip 文件");
                }

                //删除 压缩包 和 解压后的文件
                string tempDirectory = AppDomain.CurrentDomain.BaseDirectory + "temp\\";
                bool autoDeleteDownloadFile = Convert.ToBoolean(ConfigurationManager.AppSettings["autoDeleteDownloadFile"]);
                if (autoDeleteDownloadFile)
                {
                    Delete(tempDirectory);
                }
            }
            catch (Exception ex)
            {
                PrintLog(ex.Message);
            }
            PrintLog("---end----------------------");
            PrintLog("");
            PrintLog("");
            PrintLog("");
            PrintLog("");
            PrintLog("");
            //Console.ReadLine();


        }

        private void Delete(string tempDirectory)
        {
            if (Directory.Exists(tempDirectory))
            {
                string[] files = Directory.GetFiles(tempDirectory);
                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                }

                string[] dirs = Directory.GetDirectories(tempDirectory);
                if (dirs.Count() > 0)
                {
                    foreach (var dir in dirs)
                    {
                        Delete(dir);
                    }
                }

                Directory.Delete(tempDirectory);
            }
        }

        private void FormatEmail()
        {
            //domains.Clear();
            //foreach (int id in Mapping.Keys)
            //{
            //    Dictionary<string, int> mapping = Mapping[id];
            //    foreach (string host in mapping.Keys)
            //    {
            //        if (!domains.ContainsKey(host))
            //        {
            //            domains[host] = mapping[host];
            //        }
            //        else
            //        {
            //            domains[host] += mapping[host];
            //        }
            //    }
            //}

            StringBuilder sbrData = new StringBuilder();
            sbrData.AppendFormat("<h1>{0}</h1>", time);
            sbrData.Append("<table><tr><th>Domain</th><th>Count</th></tr><tr>");
            for (int i = 0; i < domainArr.Length; i++)
            {
                string item = domainArr[i];
                int currentCount = domains.Where(x => x.Key == item).Select(x => x.Value).FirstOrDefault();
                if (i > 0)
                {
                    sbrData.Append("</tr><tr>");
                }

                sbrData.AppendFormat("<td>{0}</td><td>{1}</td>", item, currentCount);
                PrintLog(string.Format("domain:{0} count:{1}", item, currentCount));
            }
            sbrData.Append("</tr></table>");
            Command_CenterBP bp = new Command_CenterBP();
            bp.SendMail(string.Format("(Info)Truesight系统域名访问情况 - {0}", time), sbrData.ToString());
        }

        void translater_OnTranslateRow(string csvString, object result)
        {
            TruesightStatistics entity = (TruesightStatistics)result;
            if (entity != null && !string.IsNullOrWhiteSpace(entity.cs_Host))
            {
                string host = entity.cs_Host.ToLower().Trim().Trim('"').Trim('\'');
                if (domainArr.Contains(host))
                {

                    lock (LockObj)
                    {
                        if (!domains.ContainsKey(host))
                        {
                            domains[host] = 1;
                        }
                        else
                        {
                            domains[host] += 1;
                        }
                    }
                }
            }
        }

        private void PrintLog(string msg)
        {
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss --> ") + msg;
#if DEBUG
            Console.WriteLine(msg);
#else
            Console.WriteLine(msg);
#endif
            string logPath = AppDomain.CurrentDomain.BaseDirectory + "log\\";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            using (StreamWriter sw = new StreamWriter(string.Format("{0}{1}.log", logPath, time), true, Encoding.Default))
            {
                sw.WriteLine(msg);
            }
        }

        private MappingConfiguration GetMappingConfig()
        {
            MappingConfiguration config = new MappingConfiguration();
            string fileName = ConfigurationManager.AppSettings["mapping"];
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(fileName);
            string xml = doc.OuterXml;
            System.Xml.Serialization.XmlSerializer ser = null;
            byte[] buffer = Encoding.UTF8.GetBytes(xml);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer))
            {
                ser = new System.Xml.Serialization.XmlSerializer(typeof(MappingConfiguration));
                object o = ser.Deserialize(stream);
                config = o as MappingConfiguration;
            }
            return config;
        }

        private Dictionary<int, Dictionary<string, int>> Mapping = new Dictionary<int, Dictionary<string, int>>();



#if DEBUG
        string resultDownloadFile = @"E:\Work\WorkSpace\SourceCode\Domain name to access statistics\IPP.ToolsMgmt.JobHost\bin\Debug\temp\download20140207.zip";
#else
         string resultDownloadFile = string.Empty;
#endif

        public void Download()
        {
            DownloadHelper downer = new DownloadHelper(dowloadUrl);
            PrintLog("正在下载...");
            downer.Download();
            //downer.FilePath = @"E:\Work\WorkSpace\SourceCode\Domain name to access statistics\IPP.ToolsMgmt.JobHost\bin\Debug\tem\test.zip";
            resultDownloadFile = downer.FilePath;
            if (string.IsNullOrWhiteSpace(downer.FilePath))
            {
                PrintLog("文件下载失败");
                return;
            }
            PrintLog(string.Format("下载完成，文件保存路径：{0}", resultDownloadFile));
        }

        string resultGZipTargetDirectory = string.Empty;
        string resultGZipTargetFileName = string.Empty;
        public void UNZip()
        {
            string gzipFile2 = string.Empty;
            string gzipFile2Path = string.Empty;
            bool hasGzip = false;

            if (string.IsNullOrWhiteSpace(resultDownloadFile))
            {
                PrintLog("未找到要解析的文件");
                return;
            }
            ZipHelper ziper = new ZipHelper(resultDownloadFile, "");
            ziper.UnZip();
            string zipTargetDirectory = ziper.TargetDirectory;
            PrintLog("解压缩完成.");
            PrintLog(string.Format("文件保存路径：{0}", zipTargetDirectory));

            string[] files = Directory.GetFiles(zipTargetDirectory);


            PrintLog("正在检索是否包含 page-开头的 .csv.gz 文件...");
            foreach (string file in files)
            {
                string f = Path.GetFileName(file);
                if (f.StartsWith("page-") && f.EndsWith(".csv.gz"))
                {
                    PrintLog(string.Format("检索到文件：{0}", f));
                    gzipFile2 = f;
                    gzipFile2Path = file;
                    break;
                }
            }

            if (!string.IsNullOrWhiteSpace(gzipFile2Path))
            {
                hasGzip = true;
            }

            if (!hasGzip)
            {
                PrintLog("未检索到包含 page-开头的 zip 文件");
                return;

            }
            PrintLog(string.Format("正在解压缩：{0}...", gzipFile2));
            GZipHelper gziper = new GZipHelper(gzipFile2Path, "");
            gziper.UnZip();
            resultGZipTargetDirectory = gziper.TargetDirectory;
            resultGZipTargetFileName = gziper.TargetFileName;
            PrintLog(string.Format("解压缩完成：{0}", gzipFile2));
            PrintLog(string.Format("保存路径：{0}", gziper.TargetDirectory));
            PrintLog(string.Format("文件{0}", gziper.TargetFileName));
        }

        public void AnalysisCSV()
        {
            domains.Clear();
            string dataCsv = resultGZipTargetDirectory + resultGZipTargetFileName;
            if (string.IsNullOrWhiteSpace(dataCsv))
            {
                PrintLog("CSV文件不存在。");
                return;
            }
            // string dataCsv = @"E:\Work\WorkSpace\SourceCode\Domain name to access statistics\IPP.ToolsMgmt.JobHost\bin\Debug\page-full.csv";
            PrintLog("正在解析CSV...");
            long CsvCount = 0;

            CSVTranslaterBase<TruesightStatistics> translater = new CSVTranslater<TruesightStatistics>(10);
            translater.OnTranslateRow += translater_OnTranslateRow;
            CsvCount = translater.Translate(dataCsv);
            PrintLog(string.Format("CSV文件解析完毕,共{0}条数据", CsvCount));
            PrintLog("统计结果如下：");

            FormatEmail();
        }

        public void DeleteTemp()
        {
            //删除 压缩包 和 解压后的文件
            string tempDirectory = AppDomain.CurrentDomain.BaseDirectory + "temp\\";
            bool autoDeleteDownloadFile = Convert.ToBoolean(ConfigurationManager.AppSettings["autoDeleteDownloadFile"]);
            if (autoDeleteDownloadFile)
            {
                Delete(tempDirectory);
            }
        }
    }
}
