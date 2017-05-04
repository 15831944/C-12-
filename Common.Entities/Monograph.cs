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
    public class Monograph
    {
        public int MonographID { get; set; }	//ID	
        //[Required]
        [StringLength(100)]
        public string MonographName { get; set; }	//专著名称	
        [StringLength(20)]
        public string Publisher { get; set; }	//出版单位	
        [StringLength(20)]
        public string IssueRegin { get; set; }	//出版地		
        public DateTime? PUblicationTime { get; set; }	//出版时间
        public int? FAttachmentID { get; set; }//附件ID	（封面）
        public int? BAttachmentID { get; set; }//附件ID	（版权页）
        [StringLength(20)]
        public string BookNuber { get; set; }	//图书编号	
        [StringLength(20)]
        public string Revision { get; set; }	//版次
        [StringLength(200)]
        public string Remark { get; set; }	//备注	
        [StringLength(10)]
        public string Sort { get; set; }  //类别
        public int? AchievementID { get; set; }
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        //[Required]
        [StringLength(200)]
        public string MonographPeople { get; set; }//专著作者
        //[Required]
        [StringLength(10)]
        public string FirstWriter { get; set; }//第一作者
        [StringLength(10)]
        public string WriterIdentity { get; set; }//第一作者身份
        //[Required]
        [StringLength(30)]
        public string CIPNum { get; set; }//CIP号
        //[Required]
        [StringLength(30)]
        public string ISBNNum { get; set; }//ISBN号
        //[Required]
        [StringLength(10)]
        public string MonographType { get; set; }//专著类型
         [StringLength(10)]
        public string PaperUnit { get; set; }	//所属机构	
    }
}
