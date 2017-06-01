using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPP.ToolsMgmt.JobV31.library
{
    public class GZipHelper
    {
        private string _filePath;
        private string _targetDirectory;
        private string _targetFileName;

        public string TargetFileName
        {
            get { return _targetFileName; }
            private set { _targetFileName = value; }
        }
        public string TargetDirectory
        {
            get { return _targetDirectory; }
            private set { _targetDirectory = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="targetDirectory">解压文件存放路径，为空时与压缩文件同目录</param>
        public GZipHelper(string filePath, string targetDirectory)
        {
            this._targetFileName = "test.csv";
            this._filePath = filePath;
            this._targetDirectory = targetDirectory;
        }

        public void UnZip()
        {
            if (string.IsNullOrWhiteSpace(this._filePath))
            {
                throw new InvalidOperationException(string.Format("无效的gzip文件路径路径，FileName:{0}", this._filePath));
            }

            if (!File.Exists(this._filePath))
            {
                throw new InvalidOperationException(string.Format("gzip文件不存在，FileName:{0}", this._filePath));

            }

            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹
            if (string.IsNullOrWhiteSpace(this._targetDirectory))
                this._targetDirectory = this._filePath.Replace(Path.GetFileName(this._filePath)
                    , Path.GetFileNameWithoutExtension(this._filePath));
            if (!this._targetDirectory.EndsWith("\\"))
                this._targetDirectory += "\\";
            if (Directory.Exists(this._targetDirectory))
            {
                Directory.Delete(this._targetDirectory, true);
            }
            Directory.CreateDirectory(this._targetDirectory);

            try
            {
                //using (GZipInputStream zipStream = new GZipInputStream(File.OpenRead(this._filePath)))
                using (GZipInputStream zipStream = new GZipInputStream(new FileStream(this._filePath, FileMode.Open, FileAccess.Read, FileShare.None)))
                {
                    using (FileStream stream = new FileStream(this._targetDirectory + this._targetFileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    {

                        int size = 1024;
                        byte[] buffer = new byte[size];
                        while ((size = zipStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            stream.Write(buffer, 0, size);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
