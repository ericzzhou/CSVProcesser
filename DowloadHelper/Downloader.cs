using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace DowloadHelper
{
    public class Downloader
    {
        private string m_Url = null;
        public Downloader(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentNullException("url");
            }
            m_Url = url;
        }

        public Stream Download()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(this.m_Url));
            request.Method = "GET";
            request.KeepAlive = true;
            System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy(); 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();

            return stream;
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
