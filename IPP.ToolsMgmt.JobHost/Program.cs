using System;
using Newegg.Oversea.Framework.JobConsole.Client;
using IPP.ToolsMgmt.JobV31;

namespace IPP.ToolsMgmt.JobHost
{
    public class Program
    {
        static JobAutoRun job = new JobAutoRun();
        static void Main(string[] args)
        {

            JobContext context = new JobContext();
            Console.WriteLine("。。。。。。。。开始。。。。。。。。");

            Console.WriteLine("1:all ");
            Console.WriteLine("2:download");
            Console.WriteLine("3:unzip");
            Console.WriteLine("4:analysis csv");
            Console.WriteLine("5:delete temp files");
            Console.WriteLine("10:exit");

            int readNum = 0;
            do
            {
                Console.WriteLine("请选择要进行的操作:");
                string read = Console.ReadLine();

                int.TryParse(read, out readNum);
                switch (readNum)
                {
                    case 1:
                        ExecuteALL();
                        break;
                    case 2:
                        Download();
                        break;
                    case 3:
                        UnZIP();
                        break;
                    case 4:
                        AnalysisCsv();
                        break;
                    case 5:
                        DeleteTempFiles();
                        break;
                    default:
                        break;
                }

            } while (readNum != 10);

            //job.Run(context);

            Console.WriteLine("                      ");
            Console.WriteLine("。。。。。。。。结束。。。。。。。。");
            Console.ReadLine().Trim();


        }





        private static void ExecuteALL()
        {
            Download();
            UnZIP();
            AnalysisCsv();
            DeleteTempFiles();
        }

        private static void Download()
        {
            job.Download();
        }
        private static void UnZIP()
        {
            job.UNZip();
        }

        private static void AnalysisCsv()
        {
            job.AnalysisCSV();
        }
        private static void DeleteTempFiles()
        {
            job.DeleteTemp();
        }
    }
}
