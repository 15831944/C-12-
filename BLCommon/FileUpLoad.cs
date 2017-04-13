using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLCommon
{
    public class FileUpLoad
    {
        public FileUpLoad()
        { }
        /**/
        /// 
        /// 上传文件名称 
        /// 
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
        private string fileName;

        /**/
        /// 
        /// 上传文件路径 
        /// 
        public string FilePath
        {
            get
            {
                return filepath;
            }
            set
            {
                filepath = value;
            }
        }
        private string filepath;

        /**/
        /// 
        /// 文件扩展名 
        /// 
        public string FileExtension
        {
            get
            {
                return fileExtension;
            }
            set
            {
                fileExtension = value;
            }
        }
        private string fileExtension;

        //附件id
        private int attachid;
        public int Attachid
        {
            get
            {
                return attachid;
            }
            set
            {
                attachid = value;
            }
        }
    }
}
