using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Attachment
    {
        public int AttachmentID { get; set; }//		附件ID
        [Required]
        [StringLength(50)]
        public string FileName { get; set; }//		文件名
        [Required]
        public string FilePath { get; set; }//		文件路径
    }
}
