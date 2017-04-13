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
    public class ProjectImportantNode
    {
        public int ProjectImportantNodeID { get; set; }	//	ID
        //[Required]
         [StringLength(500)]
        public string MissionName { get; set; }	//	节点名称
        //[Required]
        //public DateTime? Time { get; set; }	//	时间
        //[Required]
        public int? ProjectID { get; set; }	//	项目ID
         [StringLength(200)]
        public string Remark { get; set; }	//	备注
         //[Required]
         public int? SecrecyLevel { get; set; }//涉密级别
         //[Required]
         [StringLength(20)]
         public string EntryPerson { get; set; }//录入人
         //[Required]
         public bool? IsPass { get; set; } //是否审核
        //[Required]
         public DateTime? StartTime { get; set; }	//开始时间
         public DateTime? EndTime { get; set; }	//结束时间
         [StringLength(50)]
         public string PersonCharge { get; set; }//负责人
         [StringLength(50)]
         public string ResearchCharge { get; set; }//负责研究室
         [StringLength(50)]
         public string CompleteSpecificPerson { get; set; }//具体完成人
         [StringLength(50)]
         public string ProjectCompletion { get; set; }//项目完成情况
         [StringLength(50)]
         public string ActualComleption { get; set; }//实际完成

    }
}
