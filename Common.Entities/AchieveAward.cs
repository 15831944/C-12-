using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class AchieveAward  //成果报奖
    {
        public int AchieveAwardID { get; set; } //ID
         [Required]
        public int? AchievementID { get; set; }  //成果ID
         [Required]
        [StringLength(40)]
        public string AwardUnit { get; set; }   //评奖单位
         [Required]
        [StringLength(100)]
        public string AwardName { get; set; }   //评奖名称
         [Required]
        [StringLength(10)]
        public string AwardGrade { get; set; }  //评奖等级
         [Required]
        [StringLength(20)]
        public string AwardType { get; set; }//报奖类别
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [Required ]
        [StringLength (300)]
        public string AwardPeople { get; set; }//报奖人

        [StringLength (255)]
        public string Member { get; set; }//成员
    }
}
