using CSVProcesser.Library;
using DowloadHelper;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CSVProcesser
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = null;
            string mes = "请选择：1、单线程读取解析，2、多线程读取解析，3、解压缩，0、退出";
            CSVTranslaterBase translater = null;
            do
            {
                Console.WriteLine(mes);
                if ((key = Console.ReadLine()) == "0")
                {
                    break;
                }
                string fileName = @"test.gz.csv";
                long count = 0, count1 = 0;
                translater = null;
                if (key == "1")
                {
                    translater = new CSVTranslater(64);
                }
                else if (key == "2")
                {
                    translater = new CSVThreadTranslater();
                }
                else
                {
                    GZipTest();
                }
                int index = 1;
                //List<CSVEntity> list = new List<CSVEntity>(200000);
                //translater.OnTranslateRow += (s, o) =>
                //{
                //    count1++;
                //    CSVEntity entity = (CSVEntity)o;
                //    //list.Add(entity);
                //    Console.WriteLine(string.Format("{0}:{1}", index++, entity.UserAgent));
                //};
                if (translater != null)
                {
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    count = translater.Translate<CSVEntity>(fileName);
                    watch.Stop();
                    Console.WriteLine("共解析数据：{0}条，耗时：{1}ms", count, watch.ElapsedMilliseconds);
                }
            } while (true);

            //Console.ReadLine();

            //DownloadTest();
            //Console.ReadLine();
        }

        private static void DownloadTest()
        {
            string time = DateTime.Now.ToString("yyyy.MM.dd");
            string dowloadUrl = string.Format("https://172.20.8.49/getexport?usr=mis&pwd=newegg@123&export=true&start={0}.0.0&end={1}.23.50&pages=true&objects=false&errors=false&sessions=true", time, time);

            Downloader loader = new Downloader(dowloadUrl);
            using (Stream stream = loader.Download())
            {
                using (FileStream writer = new FileStream("test.zip", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[1024];
                    int readSize = 0;
                    while ((readSize = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writer.Write(buffer, 0, readSize);
                    }
                }
            }
        }

        private static void GZipTest()
        {
            string sourceFileName = "test.gz";
            string targetFileName = "test.gz.csv";
            using (GZipInputStream reader = new GZipInputStream(new FileStream(sourceFileName, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                //reader.Available
                //ZipEntry entry = null;
                //while ((entry = reader.GetNextEntry()) != null)
                //{
                //    if (!entry.IsDirectory)
                //    {
                using (FileStream writer = new FileStream(targetFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] buffer = new byte[1024 * 4];
                    int readSize = -1;
                    while ((readSize = reader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        writer.Write(buffer, 0, readSize);
                    }
                }
                //    }
                //}                
            }
        }
    }
}
