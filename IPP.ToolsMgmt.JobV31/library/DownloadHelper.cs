using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace IPP.ToolsMgmt.JobV31.library
{
    public class DownloadHelper
    {
        private string _dowloadUrl;
        private int _timeOut = 180000;//默认最大超时时间不能超过30分钟，超过30分钟的设置将被忽略，单位：毫秒
        private string _savePath = "temp";
        private string _fileName = "download" + DateTime.Now.ToString("yyyyMMdd") + ".zip";
        private string _ContentType = "";
        public string FilePath { get; set; }
        public DownloadHelper(string dowloadUrl)
        {
            this._dowloadUrl = dowloadUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dowloadUrl"></param>
        /// <param name="timeOut">毫秒</param>
        public DownloadHelper(string dowloadUrl, int timeOut)
        {
            this._dowloadUrl = dowloadUrl;
            this._timeOut = timeOut;
        }

        public void Download()
        {
            string _full = Path.Combine(this._savePath, this._fileName);
            //if (File.Exists(_full))
            //{
            //    this._fileName = _full.TrimEnd('.', 'z', 'i', 'p') + "-2" + ".zip";
            //    _full = _full.TrimEnd('.', 'z', 'i', 'p') + "-2" + ".zip";
            //}
            GenerateInit();

            if (string.IsNullOrWhiteSpace(_dowloadUrl))
            {
                throw new InvalidOperationException(string.Format("无效的下载路径，FileName:{0}", _dowloadUrl));
            }

           
            using (HttpWebResponse response = Connect())
            {
                this._ContentType = response.ContentType;
                //response.GetResponseStream();
                using (Stream stream = response.GetResponseStream())
                {
                    using (FileStream fs = new FileStream(_full, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[1024];
                        int readSize = 0;
                        while ((readSize = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, readSize);
                        }
                    }
                }
            }
            this.FilePath = _full;

        }

        private void GenerateInit()
        {
            GetFileSavePath();

            if (!Directory.Exists(this._savePath))
            {
                Directory.CreateDirectory(this._savePath);
            }

            if (string.IsNullOrWhiteSpace(this._fileName))
            {
                throw new Exception("文件名不能为空");
            }

            string fullPath = Path.Combine(this._savePath, this._fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        private void GetFileSavePath()
        {
            //if (!Path.IsPathRooted(this._savePath))
            //{
            //    this._savePath = this._savePath.TrimStart('/', ' ').Replace("/", @"\");
            this._savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this._savePath);
            //}
        }

        private HttpWebResponse Connect()
        {
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.CreateDefault(new Uri(this._dowloadUrl));
            request.KeepAlive = true;
            request.Method = "GET";
            request.Timeout = this._timeOut;
            //request.Proxy = new

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //return stream;
            return response;
        }
    }
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
    {
        public TrustAllCertificatePolicy()
        { }
        public bool CheckValidationResult(ServicePoint sp,
            System.Security.Cryptography.X509Certificates.X509Certificate cert,
            WebRequest req, int problem)
        {
            return true;
        }

    }
}
