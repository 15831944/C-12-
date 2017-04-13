using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class AchivementApply
    {
        public int? AchivementApplyID{get;set;}
         [Required]
        [StringLength(20)]
        public string ApplyUnit { get; set; }//应用单位
         [Required]
        public DateTime? StartTime{get;set;}//开始时间
        public DateTime? EndTime { get; set; }//结束时间
        [Required]
        public int? AchievementID { get; set; }//成果ID
         [Required]
        [StringLength(50)]
        public string Use { get; set; }//用途
        public int? AttachmentID { get; set; }	//	附件ID(附件表的外键)照片
        [StringLength(20)]
        public string EconomicBenefit { get; set; }//经济效益       
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength(255)]
        public string Member { get; set; }   //成员
    }
}
