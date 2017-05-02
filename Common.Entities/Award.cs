/*
 * 作者：未知
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：设置所有属性可以为空
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Award
    {
        public int AwardID { get; set; }	//ID 	
        [StringLength(20)]
        public string AwardwSpecies { get; set; }	//获奖类别	
        [StringLength(20)]
        public string Grade { get; set; }	//获奖等级	
        // [Required]
        [StringLength(100)]
        public string AwardName { get; set; }	//获奖名称	
        [StringLength(200)]
        public string Remark { get; set; }	//备注	
        public DateTime? AwardTime { get; set; }   //获奖时间
        // [Required]
        [StringLength(20)]
        public string GivAgency { get; set; } //赋予机构    
        [StringLength(200)]
        public string Unit { get; set; }//单位
        public int? AttachmentID { get; set; }//附件ID
        [StringLength (500)]
        public string Acheivement { get; set; }//成果
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        // [Required]
        public bool? IsPass { get; set; } //是否审核
        //[Required]
        [StringLength(250)]
        public string AwardPeople { get; set; }//获奖人
        [StringLength(10)]
        public string FirstAward { get; set; }//第一获奖人
        [StringLength(50)]
        public string AwardNum { get; set; }//获奖证书号
        [StringLength(30)]
        public string AwardForm { get; set; }//获奖类型
        [StringLength(15)]
        public string Sort { get; set; }//分类（成果获奖或教学获奖）
        [StringLength(255)]
        public string Member { get; set; }//成员
    }
}
