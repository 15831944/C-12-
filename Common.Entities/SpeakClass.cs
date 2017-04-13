using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class SpeakClass
    {
        public int SpeakClassID { get; set; }
         [Required]
        public int? UserInfoID { get; set; }   //人员ID
         [Required]
          [StringLength(100)]
        public string ClassName { get; set; }	//课程名称	
          [StringLength(20)]
        public string Specialty { get; set; }	//专业
         [Required]
          [StringLength(10)]
        public string TeachingDegree { get; set; }//		教学对象（学历）
        public DateTime? TeachingTime { get; set; }//		教学时间
          [StringLength(10)]
        public string Grade { get; set; }//		年级
          [Required]
          public int? SecrecyLevel { get; set; }//涉密级别
          [Required]
          [StringLength(20)]
          public string EntryPerson { get; set; }//录入人
          [Required]
          public bool? IsPass { get; set; } //是否审核
    }
}
