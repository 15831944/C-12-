using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Education
    {
        public int EducationID { get; set; }	//学历ID	
        [Required]
        [StringLength(30)]
        public string SchoolName { get; set; }	//学校名称
        [Required]
        [StringLength(5)]
        public string Degree { get; set; }	//学位	
        public DateTime? EduTime { get; set; }	//学历取得时间	
        [StringLength(20)]
        public string College { get; set; }//学院
        [StringLength(20)]
        public string Series { get; set; } //系
        [StringLength(20)]
        public string Major { get; set; }//专业
        [Required]
        public int? UserInfoID { get; set; }   //人员ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        public string DegreeNumber { get; set; }//学位证书号
        public string GraduateNumber { get; set; }//毕业证书号
    }
}
