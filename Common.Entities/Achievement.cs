using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Common.Entities
{
    public class Achievement
    {
        public int AchievementID { get; set; }	//	ID
        [Required]
        [StringLength(100)]
       
        public string AchievementName { get; set; }	//	成果名称
        //[StringLength(10)]
        //public string AchievementRank { get; set; }	//	级别
        //public DateTime? AchievementTime { get; set; }	//	时间
        [Required]
        public int?  AgencyID { get; set; }	//机构ID	
        [StringLength(60)]
        public string ProjectName { get; set; }	//	所属项目
        [Required]
        public DateTime? AppraisalTime { get; set; }	//	鉴定时间
        [Required]
        [StringLength(40)]
        public string AppraisalUnit { get; set; }	//	鉴定组织部门
        [Required]
        [StringLength(10)]
        public string ApRemarkRank { get; set; }	//	鉴定评语级别
        public int? AttachmentID { get; set; }	//	附件ID(附件表的外键)
        public int? MemberPage { get; set; }	//	课题组成员页ID
        public int? OpinionPage { get; set; }//鉴定意见页ID
        public int? SealPage { get; set; }	//	组织单位盖章页ID
        public string ApprovalNum { get; set; }	//	鉴定批文号
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength(30)]
        public string ProjectInNum { get; set; }//项目内部编号
        [StringLength(20)]
        public string ProjectRank { get; set; }//鉴定级别
        [StringLength(20)]
        public string ProjectForm { get; set; }//鉴定形式
        [StringLength(20)]
      public string ProjectLevel { get; set; }//鉴定水平
        [StringLength(255)]
        public string ProjectPeople { get; set; }//成员
        public string FirstFinishedPeople { get; set; }//成果第一完成人
	
	

    }
}
