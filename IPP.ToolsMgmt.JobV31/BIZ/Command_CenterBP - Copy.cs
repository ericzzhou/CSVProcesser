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

namespace IPP.ToolsMgmt.JobV31.BIZ
{
    public class Command_CenterBP
    {
        /// <summary>
        /// Excel路径：默认根目录下
        /// </summary>
        public string excelPath { get; set; }

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
                List<string> Columns = new List<string>();
                List<EntityItem> ItemList = GetInfo(sheetName, ref Columns);
                //Console.WriteLine("  品牌个数：{0},Item个数：{1}", Columns.Count, ItemList.Count);
                //写入大类
                //int categoryId = Command_CenterDA.InsertCategory(sheetName, 1);

                //if (Columns != null & Columns.Count > 0)
                //{
                //    foreach (string brandName in Columns)
                //    {
                //        var bName = brandName.Trim();
                //        //写入品牌
                //        if (string.IsNullOrWhiteSpace(bName))
                //        {
                //            continue;
                //        }
                //        int newBrandID = Command_CenterDA.InsertItem(bName, 2, categoryId);

                //        List<EntityItem> itemByBrand = ItemList.Where(x => x.BrandName == bName).ToList();
                //        if (itemByBrand != null && itemByBrand.Count > 0)
                //        {
                //            //写入型号
                //            foreach (var item in itemByBrand)
                //            {
                //                int r = Command_CenterDA.InsertItem(item.ItemName, 3, newBrandID);
                //            }
                //        }


                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }


        /// <summary>
        /// 读取Excel返回结算商品信息实体
        /// </summary>
        /// <returns></returns>
        private List<EntityItem> GetInfo(string sheetName, ref List<string> Columns)
        {
            List<EntityItem> requestList = new List<EntityItem>();
            //string excelPath = ".\\ItemInfo.xls";
            DataTable dt = GetExcleInfo(sheetName);
            if (dt != null && dt.Rows.Count > 0)
            {
                //foreach (DataColumn dc in dt.Columns)
                //{
                //    string brandName = dc.ColumnName;
                //    Columns.Add(brandName);

                //    foreach (DataRow drItem in dt.Rows)
                //    {

                //        var itemName = drItem[brandName];
                //        if (itemName != null && !string.IsNullOrWhiteSpace(itemName.ToString()) && !string.IsNullOrWhiteSpace(itemName.ToString().Trim()))
                //        {
                //            EntityItem requestEntity = new EntityItem();
                //            requestEntity.BrandName = brandName.Trim();
                //            requestEntity.ItemLevel = 3;
                //            requestEntity.ItemName = itemName.ToString().Trim();
                //            requestList.Add(requestEntity);
                //        }
                //    }
                //}

            }
            return requestList;
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
                //excelData = ReadExcelFileToDataTable(filePath, sheetName);
                //excelData = ReadCSVFileToDataTable(filePath, sheetName);
                // excelData = ReadCSVFileToDataTableByODBC(filePath, sheetName);
                excelData = ReadCSVFileToDataTableByStream(filePath, sheetName);
                if (string.IsNullOrEmpty(excelData.Rows[0][0].ToString().Trim()))
                    throw new Exception("数据不能为空");

            }
            return excelData;
        }


        public DataTable ReadCSVFileToDataTableByStream(string fileName, string sheetName)
        {
            int intColCount = 0;
            DataTable mydt = new DataTable("myTableName");

            DataColumn mydc;
            DataRow mydr;
            SortedList sl = new SortedList();
            string strline;
            string[] aryline;
            string src = string.Empty;
            StreamReader mysr = new System.IO.StreamReader(fileName);
            string headLine = mysr.ReadLine();
            if (headLine != null)
            {
                string[] heads = headLine.Split(new char[] { ',' });
                intColCount = heads.Length;
                for (int i = 0; i < heads.Length; i++)
                {
                    mydc = new DataColumn(heads[i]);
                    mydt.Columns.Add(mydc);
                }

                while ((strline = mysr.ReadLine()) != null)
                {
                    src = strline.Replace("\"\"", "'");
                    //正则适配  ,"Chengdu, China",
                    string reg = ",\"[^\",]+?,[^\",]+?\",";
                    MatchCollection col = Regex.Matches(strline, reg, RegexOptions.ExplicitCapture);
                    //获取所有适配项，以索引为止保存在list 中，然后替换当前适配项为“,, ”即清空中间内容
                    IEnumerator ie = col.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        string patn = ie.Current.ToString();
                        if (!string.IsNullOrWhiteSpace(src))
                        {
                            int key = src.Substring(0, src.IndexOf(patn)).Split(',').Length;
                            if (!sl.ContainsKey(key))
                            {
                                sl.Add(key, patn.Trim(new char[] { ',', '"' }).Replace("'", "\""));
                                src = src.Replace(patn, ",,");
                            }
                        }

                    }

                    src = Replace(sl, src, reg);



                    //继续 拆分其他数据，保存在 list 中，避开之前已经添加的索引数据
                    string[] arr = src.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (!sl.ContainsKey(i))
                            sl.Add(i, arr[i]);
                    }

                    mydr = mydt.NewRow();
                    for (int i = 0; i < sl.Count; i++)
                    {
                        mydr[i] = sl[i];
                    }

                    mydt.Rows.Add(mydr);
                    sl.Clear();
                    src = string.Empty;
                }
            }
            return mydt;
        }

        private static string Replace(SortedList sl, string src, string reg)
        {
            MatchCollection col2 = Regex.Matches(src, reg, RegexOptions.ExplicitCapture);
            //获取所有适配项，以索引为止保存在list 中，然后替换当前适配项为“,, ”即清空中间内容
            IEnumerator ie2 = col2.GetEnumerator();
            while (ie2.MoveNext())
            {
                string patn = ie2.Current.ToString();
                if (!string.IsNullOrWhiteSpace(src))
                {
                    int key = src.Substring(0, src.IndexOf(patn)).Split(',').Length;
                    if (!sl.ContainsKey(key))
                    {
                        sl.Add(key, patn.Trim(new char[] { ',', '"' }).Replace("'", "\""));
                        src = src.Replace(patn, ",,");
                    }
                }

            }
            return src;
        }
        public DataTable ReadCSVFileToDataTableByODBC(string fileName, string sheetName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                DataTable dt = new DataTable();
                string strConn = "Driver={Microsoft Text Driver (*.txt; *.csv)};Dbq=" + fileName + "';Extensions=asc,csv,tab,txt;";//IMEX=1数据以文本读取
                string SQL = string.Format("select * from [{0}$]", sheetName);
                OdbcConnection odbccon = new OdbcConnection(strConn);
                OdbcDataAdapter adapter = new OdbcDataAdapter(SQL, strConn);
                try
                {
                    dt.Clear();
                    dt.Columns.Clear();
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    if (odbccon != null)
                    {
                        odbccon.Close();
                    }
                    if (adapter != null)
                    {
                        adapter.Dispose();
                    }
                }
                return dt;
            }
            return null;

        }
        private DataTable ReadCSVFileToDataTable(string fileName, string sheetName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                string strconn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Text;", fileName);
                string sql = string.Format("SELECT * FROM [{0}]", sheetName);
                using (OleDbConnection conn = new OleDbConnection(strconn))
                {
                    DataTable dtTable = new DataTable();
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);
                    try
                    {
                        adapter.Fill(dtTable);
                    }
                    catch (Exception ex)
                    {
                        dtTable = new DataTable();
                        throw ex;
                    }
                    return dtTable;
                }
            }
            return null;
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

        #endregion

    }
}
