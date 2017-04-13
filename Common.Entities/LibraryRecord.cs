using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class LibraryRecord //借阅记录表
    {
        public int LibraryRecordID { get; set; }
         [Required]
        public int? UserInfoID { get; set; }   //人员ID
         [Required]
        public int? ContractID { get; set; } //合同资料编号
         [Required]
        public DateTime? BorrowTime { get; set; }    //借阅时间
        public DateTime? ReturnTime { get; set; }    //归还时间
        [Required]
        [StringLength (20)]
        public string Sort { get; set; }// 分类 合同和资料
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
