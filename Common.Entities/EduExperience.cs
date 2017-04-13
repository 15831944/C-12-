using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;


namespace Common.Entities
{
    public class EduExperience
    {
        public int EduExperienceID { get; set; }	//	ID
        public DateTime? StartTime { get; set; }	//	开始时间
        public DateTime? EndTime { get; set; }	//	结束时间
         [Required]
        [StringLength(20)]
        public string Major { get; set; }	//	所学专业
         [Required]
        [StringLength(20)]
        public string EHoldOffice { get; set; }	//	担任职位
         [Required]
        public int? UserInfoID { get; set; }   //人员ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
         [StringLength(200)]
        public string Remark { get; set; }//备注
    }
}
