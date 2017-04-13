using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class DFurtherStudy
    {
        public int DFurtherStudyID { get; set; }	//	ID
         [Required]
        public int? UserInfoID { get; set; }   //人员ID(人员表的外键)
         [Required]
        [StringLength(50)]
        public string StudyPlace { get; set; }	//	进修地点
         [Required]
        [StringLength(30)]
        public string StudySchool { get; set; }	//	进修学校
         [Required]
        [StringLength(20)]
        public string StudyContent { get; set; }	//	进修内容
         [Required]
        public DateTime? DBegainTime { get; set; }	//	进修开始时间
        public DateTime? DEndTime { get; set; }	//	进修结束时间
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
