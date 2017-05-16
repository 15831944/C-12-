/*
 * 作者：未知
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：设置所有属性可以为空
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Patent
    {
        public int PatentID { get; set; }	//ID	
        //[Required]
        [StringLength(100)]
        public string PatentName { get; set; }	//专利名称
        // [Required]
        [StringLength(20)]
        public string PatentNumber { get; set; }	//专利号	
        //[StringLength(5)]
       // public string PatentType { get; set; }	//类别	
        public DateTime? ApplicationTime { get; set; }	//申请时间	
        [StringLength(50)]
        public string CertificateNumber { get; set; }	//证书号	
        [StringLength(120)]
        public string PatentDepartment { get; set; }	//单位
        [StringLength(200)]
        public string Comment { get; set; }	//备注	
        public DateTime? AccreditTime{get;set;}//授权时间
        //[Required]
        [StringLength(20)]
        public string GivenUnit { get; set; }//授予部门
        //[Required]
        [StringLength(10)]
        public string State { get; set; }//状态
        //[Required ]
        [StringLength(10) ]
        public string PatentForm { get; set; }//专利类型

        [StringLength(50)]
        public string AchievementID { get; set; }//成果ID
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        //[Required]
        [StringLength(200)]
        public string PatentPeople { get; set; }//全部发明人
        //[Required]
        [StringLength(20)]
        public string FirstPeople { get; set; }//第一发明人
        [StringLength(200)]
        public string AgencyID { get; set; }//所属部门
        //[Required]
        [StringLength(20)]
        public string Fund { get; set; }//资助经费
        //[Required]
        [StringLength(20)]
        public string PatentCondition { get; set; }//专利情况
        //[Required]
        [StringLength(50)]
        public string ApplyNum { get; set; }//申请号
        [StringLength(255)]
        public string Member { get; set; }//成员
        //lby ↓
        public int? Attachment_Patent { get; set; }//附件ID，专利证书
        //lby ↓
        public int? Attachment_Application { get; set; }//附件ID，申请书
        //lby ↓
        public int? PatentAuthorization { get; set; } //专利授权号
        //lby ↓
        public int? PatentCertificate { get; set; } //专利证书号
        
    }
}
