using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class FutherStudy
    {
        public int FutherStudyID { get; set; }	//	ID
         [Required]
        [StringLength(10)]
        public string Name { get; set; }	//	姓名
         [Required]
        public bool? Sex { get; set; }	//	性别
        [StringLength(10)]
        public string Hometown { get; set; }	//	籍贯
        public DateTime? Birthday { get; set; }	//	出生年月
        [StringLength(15)]
        public string PhoneNum { get; set; }	//	联系电话
        [StringLength(20)]
        public string Email { get; set; }	//	电子邮箱
        [StringLength(10)]
        public string DocuType { get; set; }	//	证件类型
        [StringLength(20)]
        public string IDNum { get; set; }	//	证件号码
        [StringLength(2000)]
        public string Profile { get; set; }	//	个人简介
         [Required]
        [StringLength(50)]
        public string LearnPlace { get; set; }	//	进修地点
         [Required]
        [StringLength(20)]
        public string LearnSchool { get; set; }	//	进修学校
         [Required]
        public DateTime? LearnBeginTime { get; set; }	//	进修开始时间
        public DateTime? LearnEndTime { get; set; }	//	进修结束时间
         [Required]
        [StringLength(30)]
        public string LearnContent { get; set; }	//	进修内容
        [StringLength(200)]
        public string Remark { get; set; }	//	备注
         [Required]
        public int? AgencyID { get; set; }	//机构ID	
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
