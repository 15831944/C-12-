using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class WithinPost
    {
        public int WithinPostID { get; set; }	  //	ID

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }	  //	文件名

        [Required]
        [StringLength(20)]
        public string FileType { get; set; }      //文件分类

        [StringLength(50)]
        public string AndUnit { get; set; }       //文件收放单位或部门

        [StringLength(20)]
        public string recipient { get; set; }     //收放接收人

        [Required]
        public DateTime? Time { get; set; }          //文件收放时间

        [Required]
        public int? AttachmentID { get; set; }	  //	附件ID

        [Required]
        public int? SecrecyLevel { get; set; }    //保密级别

        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }   //录入人

        [Required]
        public bool? IsPass { get; set; }         //是否审核
    }
}
