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
    public class Project
    {
        public int ProjectID { get; set; }//		项目ID
         //[Required]
        [StringLength(1000)]
        public string ProjectName { get; set; }//		项目名称
         //[Required]
         public int AgencyID { get; set; }//		项目所属单位
         //[Required]
         [StringLength(200)]
        public string SourceUnit { get; set; }//		来源单位
        [StringLength (30)]
        public string ProjectSortName { get; set; }//		项目分类名称

         //[Required]
         [StringLength(10)]
        public string ProjectState { get; set; }//		项目状态

        [StringLength(15)]
        public string ApprovedMoney { get; set; }//		项目经费
         [StringLength(15)]
        public string GetMoney { get; set; }//		到账金额
         [StringLength(10)]
        public string CooperationForms { get; set; }//		合作形式
         [StringLength(10)]
        public string ProjectLevel { get; set; }//		项目级别,类型
         //[Required]
         [StringLength(20)]
        public string ProjectHeads { get; set; }//		实际负责人
        public DateTime? StartTime { get; set; }//		开始时间
        public DateTime? EndTime { get; set; }//		结束时间
        public DateTime? ExpectEndTime { get; set; } //    预期结束时间
        public int? BenefitAttachment { get; set; }//		经济效益附件ID
        public int? BudgetAttachment { get; set; }//经费预算附件
         [StringLength(10)]
        public string ExpecteResults { get; set; }//		预期成果
         //[Required]
         [StringLength(50)]
        public string GivenMoneyUnits { get; set; }//		来款单位
        
         [StringLength(200)]
        public string Remark { get; set; }//		备注
         //[Required]
         [StringLength(200)]
        public string AcceptUnit { get; set; }//		承接单位
        // [Required]
        // [StringLength(20)]
        //public string ApproveUnit { get; set; }//       批准部门
        //[Required]
         [StringLength(10)]
        public string ProjectNature { get; set; }//   项目性质
         [StringLength(200)]
        public string PactNum { get; set; }//合同编号
         [StringLength(200)]
        public string TaskNum { get; set; }//课题编号
        //[StringLength (20)]
        //public string SubjectSortName { get; set; } //学科分类外键
        //public virtual ICollection<Pact> Pact { get; set; }
        //public virtual ICollection<FundInformation> FundInformations { get; set; }
        //public virtual ICollection<ProjectImportantNode> ProjectImportantNode { get; set; }
        //[Required]
         public int? SecrecyLevel { get; set; }//涉密级别
         //[Required]
         [StringLength(20)]
         public string EntryPerson { get; set; }//录入人
         //[Required]
         public bool? IsPass { get; set; } //是否审核
        [StringLength (10)]
         public string ManageMoney { get; set; }//管理费比例
         //[Required]
         [StringLength(40)]
         public string ProjectManager { get; set; }//项目负责人（三个）
         //[Required]
         [StringLength(50)]
         public string ProjectInNum { get; set; }//项目内部编号

        [StringLength(255)]
         public string ProjectMember { get; set; }//项目成员
    }
}
