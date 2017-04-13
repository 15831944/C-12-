using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Files
    {
        public int FilesID { get; set; }	//	ID
         [Required]
        [StringLength(100)]
        public string FileName { get; set; }	//	文件名
         [Required]
        public int? AttachmentID { get; set; }	//	附件ID(附件表的外键)
         [Required]
        [StringLength (10)]
        public string DocumentCategoryID { get; set; }	//	文件分类编号(文件分类表的外键)
         [Required]
        public int? AgencyID { get; set; }	//发放部门
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [Required]
        [StringLength(50)]
        public string LevinUnit { get; set; }//来文单位 
        [Required]
        public DateTime? LevinTime { get; set; }//来文单位
        [Required]
        [StringLength(20)]
        public string FileRecipient { get; set; }//来文单位
    }
}
