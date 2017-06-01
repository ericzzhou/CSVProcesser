using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newegg.Oversea.Framework.JobConsole.Client;
using System.Xml.Serialization;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;
using Newegg.Oversea.Framework.ServiceConsole.Client;
using IPP.Oversea.CN.OrderMgmt.ServiceInterfaces.ServiceContracts;
using IPP.Oversea.CN.OrderMgmt.ServiceInterfaces.DataContracts;
using IPP.ToolsMgmt.JobV31.Entity;
using IPP.ToolsMgmt.JobV31.DataAccess;
using IPP.CN.IM.Core.Interface.Item;
using System.Collections;
using System.Text.RegularExpressions;
using IPP.ToolsMgmt.JobV31.CSV;

namespace IPP.ToolsMgmt.JobV31.BIZ
{
    public class Command_CenterBP
    {
        /// <summary>
        /// Excel路径：默认根目录下
        /// </summary>
        public string excelPath { get; set; }

        string emailTO = ConfigurationManager.AppSettings["emailTO"];
        string emailBCC = ConfigurationManager.AppSettings["emailBCC"];
        string emailCC = ConfigurationManager.AppSettings["emailCC"];
        string emailTemplate = ConfigurationManager.AppSettings["emailTemplate"];
        #region

        /// <summary>
        /// 添加或者更新
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sheetName"></param>
        /// <param name="excelPath"></param>
        public void Process(JobContext context, string sheetName)
        {
            try
            {
                string emailSubject = DateTime.Now.ToString();
                List<string> Columns = new List<string>();
                List<TruesightStatistics> items = GetInfo(sheetName, ref Columns) ?? new List<TruesightStatistics>(); ;
                List<string> hosts = new List<string>();
                foreach (TruesightStatistics item in items)
                {
                    if (!hosts.Contains(item.cs_Host))
                    {
                        hosts.Add(item.cs_Host);
                    }
                }

                StringBuilder sbrData = new StringBuilder();
                foreach (string host in hosts)
                {
                    List<TruesightStatistics> domainDetail = items.Where(x => x.cs_Host == host).ToList();
                    sbrData.AppendFormat("<h2>domain:{0}</h2>", host);
                    sbrData.AppendFormat("<h3>count:{0}</h3>", domainDetail.Count());
                    sbrData.Append("<table>");
                    sbrData.Append(@"<tr>
                                        <th>cs(Host)</th>
                                        <th>x-page-name</th>
                                        <th>cs-uri-stem</th>
                                        <th>x-start-time</th>
                                        <th>x-end-time</th>
                                    </tr>
                                    <tr>");
                    for (int i = 0; i < domainDetail.Count; i++)
                    {
                        if (i > 0)
                        {
                            sbrData.Append("</tr><tr>");
                        }
                        TruesightStatistics dom = domainDetail[i];
                        if (dom == null)
                        {
                            continue;
                        }
                        sbrData.AppendFormat("<td>{0}</td>", dom.cs_Host);
                        sbrData.AppendFormat("<td>{0}</td>", dom.x_page_name);
                        sbrData.AppendFormat("<td>{0}</td>", dom.cs_uri_stem);
                        sbrData.AppendFormat("<td>{0}</td>", dom.x_start_time);
                        sbrData.AppendFormat("<td>{0}</td>", dom.x_end_time);
                    }
                    sbrData.Append("</tr></table>");

                }
                string emailBody = LoadFileStream(this.emailTemplate);
                emailBody = emailBody.Replace("#TIME#", emailSubject);
                emailBody = emailBody.Replace("#DATAS#", sbrData.ToString());

                SendMail(emailSubject, emailBody);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }

        public void SendMail(string emailSubject, string emailBody)
        {
            string to = this.emailTO;
            string bcc = this.emailBCC;
            string cc = this.emailCC;
            Command_CenterDA.SendMail(to, cc, bcc, emailSubject, emailBody);
            MailManager.SendEmailClient sendMail = new MailManager.SendEmailClient();
            sendMail.SendMail_Fully(new MailManager.MailContract()
            {
                BCC = bcc,
                Body = emailBody,
                CC = cc,
                To = to,
                IsBodyHtml = true,
                From = ConfigurationManager.AppSettings["emailFrom"],
                Subject = emailSubject,
                BodyEncoding = MailManager.MailEncoding.UTF8,
                SubjectEncoding = MailManager.MailEncoding.UTF8
            }, false);

            //("eric.z.zhou@newegg.com", to, emailSubject, emailBody);
        }


        /// <summary>
        /// 读取Excel返回结算商品信息实体
        /// </summary>
        /// <returns></returns>
        private List<TruesightStatistics> GetInfo(string sheetName, ref List<string> Columns)
        {
            List<TruesightStatistics> requestList = new List<TruesightStatistics>();
            DataTable dt = GetCsvInfo(sheetName);
            if (dt != null && dt.Rows.Count > 0)
            {
                TruesightStatistics truesight;
                foreach (DataRow dr in dt.Rows)
                {
                    truesight = new TruesightStatistics()
                    {
                        x_page_name = dr["x-page-name"].ToString() ?? "",
                        cs_Host = dr["cs(Host)"].ToString() ?? "",
                        cs_uri_stem = dr["cs-uri-stem"].ToString() ?? "",
                        //location = dr["location"].ToString(),
                        x_start_time = dr["x-start-time"].ToString() ?? "",
                        x_end_time = dr["x-end-time"].ToString() ?? ""
                    };
                    requestList.Add(truesight);
                }
            }
            return requestList;
        }

        private DataTable GetCsvInfo(string sheetName)
        {
            string fileName = Path.GetFileName(excelPath);
            using (Stream streamFile = new FileStream(excelPath, FileMode.Open))
            {
                int fileLength = (int)streamFile.Length;
                byte[] fileData = new Byte[fileLength];
                streamFile.Read(fileData, 0, fileLength);
                streamFile.Close();
                streamFile.Dispose();
                return GetToImportedData_CSV(fileData, fileName, sheetName);
            }


        }

        private DataTable GetToImportedData_CSV(byte[] fileData, string fileName, string sheetName)
        {
            DataTable dt = null;
            string filePath = string.Empty;
            filePath = SaveFile(fileData, fileName);
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                CsvStreamReader cr = new CsvStreamReader(fileName);
                dt = cr[1, cr.RowCount, 1, 23];
                //dt = cr[1, cr.RowCount, 1, cr.ColCount];
                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString().Trim()))
                    throw new Exception("数据不能为空");
            }

            return dt;
        }

        #endregion

        #region 公用 读取Excel方法

        /// <summary>
        /// 获取Excel信息返回DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetExcleInfo(string sheetName)
        {
            DataTable dtInfo = new DataTable();
            string fileName = Path.GetFileName(excelPath);
            using (Stream streamFile = new FileStream(excelPath, FileMode.Open))
            {
                int fileLength = (int)streamFile.Length;
                byte[] fileData = new Byte[fileLength];
                streamFile.Read(fileData, 0, fileLength);
                streamFile.Close();
                streamFile.Dispose();
                return GetToImportedData(fileData, fileName, sheetName);
            }
        }

        public DataTable GetToImportedData(byte[] fileData, string fileName, string sheetName)
        {
            DataTable excelData = null;
            string filePath = string.Empty;
            filePath = SaveFile(fileData, fileName);
            if (!string.IsNullOrEmpty(filePath))
            {
                excelData = ReadExcelFileToDataTable(filePath, sheetName);
                if (string.IsNullOrEmpty(excelData.Rows[0][0].ToString().Trim()))
                    throw new Exception("数据不能为空");

            }
            return excelData;
        }

        /// <summary>
        /// 读取Excel到Table
        /// </summary>
        /// <param name="fileName">Excel路径</param>
        /// <returns></returns>
        private DataTable ReadExcelFileToDataTable(string fileName, string sheetName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                string strConn = "Provider=Microsoft.Ace.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;";
                string SQL = string.Format("select * from [{0}$]", sheetName);
                OleDbConnection cn = new OleDbConnection(string.Format(strConn, fileName));
                OleDbDataAdapter da = new OleDbDataAdapter(SQL, string.Format(strConn, fileName));
                try
                {
                    DataTable dt = new DataTable();
                    cn.Open();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (cn != null)
                    {
                        cn.Close();
                    }
                    if (da != null)
                    {
                        da.Dispose();
                    }
                }
            }
            return null;
        }

        private string SaveFile(byte[] buffer, string fileName)
        {
            string filePath = string.Empty;

            filePath = GetAbsoluteFilePath(fileName);

            FileStream fileStream = null;
            BinaryWriter binaryWriter = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Create);
                binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(buffer);
                binaryWriter.Flush();
                return filePath;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (binaryWriter != null)
                {
                    binaryWriter.Close();
                }
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        /// <summary>
        /// 获取Excel 绝度路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static string GetAbsoluteFilePath(string filePath)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath.Replace('/', '\\').TrimStart(new char[] { '\\' }));
        }

        private static string LoadFileStream(string fileName)
        {
            string fileContent;
            if (fileName == null)
            {
                throw new Exception("请指定要载入的文件名");
            }
            string filePant = GetAbsoluteFilePath(fileName);


            if (!File.Exists(filePant))
            {
                throw new Exception("指定的文件不存在");
            }
            StreamReader sr = new StreamReader(filePant, Encoding.Default);
            fileContent = sr.ReadToEnd();
            sr.Close();
            return fileContent;
        }
        #endregion

    }
}
