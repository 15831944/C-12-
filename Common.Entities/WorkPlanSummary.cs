using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class WorkPlanSummary
    {
        public int WorkPlanSummaryID { get; set; }	//	ID
        [Required]
        [StringLength (10)]
        public string  Sort { get; set; }	//	分类 0:个人 1:部门
        public int? AgencyID { get; set; }//机构ID
        public int? UserInfoID { get; set; }//人员ID
        [Required]
        public int? Attachment { get; set; }//附件ID
        [Required]
        [StringLength (100)]
        public string PlanWork { get; set; }//计划名称
        [Required]
        public DateTime? Time { get; set; }//时间
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
