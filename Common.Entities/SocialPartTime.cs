using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class SocialPartTime
    {
        public int SocialPartTimeID { get; set; }//		ID
        [Required]
         [StringLength(5)]
        public string LevelName { get; set; }//		级别名称
        [Required]
        [StringLength(20)]
        public string PartTimeName { get; set; }//		兼职职位名称
        [Required]
         [StringLength(40)]
        public string AwardDepartments { get; set; }//		授予部门
         [StringLength(30)]
        public string Terms { get; set; }//		任期
        public DateTime? ApproveTime { get; set; }//		批准时间
         [StringLength(200)]
        public string Remark { get; set; }//		备注
        [Required]
         [StringLength(50)]
        public string PartUnitName { get; set; }//兼职单位名称
        [Required]
        public int? UserInfoID { get; set; }   //人员ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [Required]
        [StringLength(50)]
        public string primaryUnit { get; set; }//兼职人员原单位
        [StringLength(15)]
        public string Sort { get; set; }//分类（社会兼职或学术兼职）
    }
}
