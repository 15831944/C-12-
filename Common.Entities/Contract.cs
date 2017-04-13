using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Contract
    {
        public int ContractID { get; set; }//		资料编号
         [Required]
        [StringLength(100)]
        public string ContractHeadLine { get; set; }//		资料题目
         [Required]
        [StringLength(10)]
        public string ContractAuthors { get; set; }//		资料著作人
         [StringLength(100)]
        public string ContractOriginal { get; set; }//       原始文件保存人
        public int? AttachmentID { get; set; }//		附件ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核

    }
}
