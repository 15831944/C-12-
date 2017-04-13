using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class NewAcademicReporting
    {
        public int NewAcademicReportingID { get; set; }  //报告ID

        [Required]
        [StringLength(10)]
        public string ReportPeople { get; set; } //报告人

        [StringLength(20)]
        public string JobName { get; set; } //职称

        [StringLength(20)]
        public string JobMission { get; set; } //职务

        [StringLength(50)]
        public string ReportUnit { get; set; } //报告人单位

        [StringLength(20)]
        public string Report { get; set; } //报告人身份证号

        [StringLength(20)]
        public string ReportTele { get; set; } //报告人手机号

        [StringLength(200)]
        public string Remark { get; set; } //备注

        [StringLength(50)]
        public string AcademicTitle { get; set; } //学术兼职及荣誉称号

        [StringLength(100)]
        public string ReportName { get; set; } //学术报告名称


        public DateTime? ReportTime { get; set; } //报告时间

        [StringLength(50)]
        public string ReportPlace { get; set; } //报告地点

        [StringLength(20)]
        public string ApplyFund { get; set; } //申请经费

        public int? PeopleCount { get; set; } //参与人数

        [StringLength(50)]
        public string Organizers { get; set; } //主办单位

        [StringLength(100)]
        public string Coorganizer { get; set; } //协办单位（3个）

        public int? AttachmentID { get; set; } //附件ID（外键）

        [StringLength(20)]
        public string ReportType { get; set; } //报告类别

        [StringLength(100)]
        public string MajorPeople { get; set; } //主要参与人（20个）

        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别

        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人

        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
