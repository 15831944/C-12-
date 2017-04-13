/*
 * 作者：未知
 * 修改履历：  1、修改人：吕博扬
 *               修改时间：2015年9月23日
 *               修改内容：设置所有属性可以为空
 *            2、修改人：吕博杨
 *               修改时间：2015年11月30日
 *               修改内容：新增附件ID字段
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Paper
    {
        public int PaperID { get; set; }	//ID	
        //[Required]
        [StringLength(1000)]
        public string Subject { get; set; }	//题目
        //[Required]  
        [StringLength(200)]
        public string PublicJournalName { get; set; }	//论文发表刊物名称	
         [StringLength(5)]
        public string ImpactFactor { get; set; }	//影响因子	
        public int? HQuoteNum { get; set; }	//他引次数	
        //[Required]
         [StringLength(50)]
        public string PaperUnit { get; set; }	//论文所属单位	
        public DateTime? PublicDate { get; set; }	//发表日期	
         [StringLength(10)]
        public string VolumesNum { get; set; }	//卷号	
         [StringLength(10)]
        public string JournalNum { get; set; }	//期号	
         [StringLength(20)]
        public string SerialNum { get; set; }	//刊号	
        public int? StartPageNum { get; set; }	//起始页码	
        public int? EndPageNum { get; set; }	//结束页码
         [StringLength(50)]
        public string RetrieveSituation { get; set; }	//收录情况
         [StringLength(10)]
        //public string RetrieveNum { get; set; }	//检索号	
        public string  PaperRank{get;set;}//刊物级别
        // [StringLength(5)]
        //public string PaperForm { get; set; }//论文形式
         [StringLength(200)]
        public string IncludeNum { get; set; }//收录号
        public int? QuoteNum { get; set; }//引用次数
         [StringLength(500)]
        public string Remark { get; set; }//备注
        public int? AchievementID { get; set; } //成果ID
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        //[Required]
        [StringLength(2000)]
        public string PaperPeople { get; set; }//全部作者
        //[Required]
        [StringLength(100)]
        public string FirstWriter { get; set; }//第一作者
        //[Required]
        [StringLength(100)]
        public string MessageWriter { get; set; }//通讯作者
        [StringLength(50)]
        public string MWAgency { get; set; }//通讯作者部门
        [StringLength(10)]
        public string WriterIdentity { get; set; }//论文作者身份
        [StringLength(15)]
        public string Sort { get; set; }//分类（教研论文或论文）

        public int? AttachmentID { get; set; }//附件ID
    }
}
