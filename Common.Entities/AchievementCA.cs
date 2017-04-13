using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class AchievementCA //成果验收
    {
        public int AchievementCAID { get; set; }    //ID
        [Required]
        public int? AchievementID { get; set; }  //成果ID
        [Required]
        public DateTime? CATime { get; set; }    //验收时间
        [Required]
        [StringLength(40)]
        public string CAUnit { get; set; }  //验收部门
        [StringLength(255)]
        public string ProjectMember { get; set; }  //成员
        [Required]
        [StringLength(255)]
        public string CACommnetLevel { get; set; }  //验收评语级别
        public int? AttachmentID { get; set; }//附件表ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
