using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Pact
    {
        public int PactID { get; set; }//		ID 
         [Required]
        [StringLength(5)]
        public string PactType { get; set; }//		合同类别->任务来源
        public int? ProjectID { get; set; }//		项目ID
        public int? AttachmentID { get; set; }//		附件ID
         [Required]
        [StringLength(20)]
        public string PactNum{ get; set; }//		合同编号
         [Required]
        public DateTime?  StartTime { get; set; }//		合同开始时间
        public DateTime?  EndTime { get; set; }//		合同结束时间
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength(100)]
        public string PactName { get; set; }//合同名称
        [StringLength(20)]
        public string ChargePerson { get; set; }//合同负责人
        [StringLength(20)]
        public string PactMoney { get; set; }//合同经费
        [StringLength(20)]
        public string RealMoney { get; set; }//实到经费
        [StringLength(20)]
        public string PactCompletion { get; set; }//合同完成情况
        [StringLength(20)]
        public string IsExistingFile { get; set; }//是否有现存文件
        [StringLength(20)]
        public string FileNum { get; set; }//文件编号
    }
}
