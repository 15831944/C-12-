using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class FundInformation   //经费基本信息
    {
        public int FundInformationID { get; set; }  //ID
        public int? UserInfoID { get; set; }   //人员ID（提取人）
        [Required]
        public DateTime? Time { get; set; }  //时间
        [StringLength (10)]
        public string FundingPurposeSortName { get; set; }   //经费用途
        [Required]
        public int? ProjectID { get; set; }       //所属项目ID
        [Required]
        [StringLength(20)]
        public string EveItemUseMoney { get; set; } //每项用途所用金额
        [StringLength(10)]
        public string Proportion { get; set; }  //比例
        [Required]
        [StringLength(5)]
        public string OperateType { get; set; } //操作类型
        [StringLength(10)]
        public string BudgetDirector { get; set; } //经费负责人
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
