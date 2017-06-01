using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

using System;
using System.IO;
using System.IO.Compression;

namespace IPP.ToolsMgmt.JobV31.library
{
    public class ZipHelper
    {
        private string _filePath;
        private string _targetDirectory;

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
        public ZipHelper(string filePath, string targetDirectory)
        {
            this._filePath = filePath;
            this._targetDirectory = targetDirectory;


        }
        
        public void UnZip()
        {
            if (string.IsNullOrWhiteSpace(this._filePath))
            {
                throw new InvalidOperationException(string.Format("无效的zip文件路径路径，FileName:{0}", this._filePath));
            }

            if (!File.Exists(this._filePath))
            {
                throw new InvalidOperationException(string.Format("zip文件不存在，FileName:{0}", this._filePath));

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
                using (ZipInputStream zipStream = new ZipInputStream(File.OpenRead(this._filePath)))
                {
                    ZipEntry entity;
                    while ((entity = zipStream.GetNextEntry()) != null)
                    {
                        string directoryName = Path.GetDirectoryName(entity.Name);
                        string fileName = Path.GetFileName(entity.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(this._targetDirectory + directoryName);
                        }
                        if (!directoryName.EndsWith("\\"))
                        {
                            directoryName += "\\";
                        }

                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            using (FileStream stream = File.Create(this._targetDirectory + entity.Name))
                            {
                                int size = 1024;
                                byte[] buffer = new byte[size];
                                while (true)
                                {
                                    size = zipStream.Read(buffer, 0, buffer.Length);
                                    if (size > 0)
                                    {
                                        stream.Write(buffer, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
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
