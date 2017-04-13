using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class UnitLectures
    {
        public int UnitLecturesID { get; set; }	//	ID
         [Required]
        [StringLength(10)]
        public string LecturesName { get; set; }	//	姓名
         [Required]
        [StringLength(40)]
        public string WorkUnit { get; set; }	//	工作单位
         [Required]
        public int? AgencyID { get; set; }	//机构ID	
        [StringLength(100)]
        public string UReportName { get; set; }	//	报告名称
        public int? AttachmentID { get; set; }	//	附件表ID(附件表的外键)
        public DateTime? LecturesTime { get; set; }	//	时间
        [StringLength(40)]
        public string LecturesPlace { get; set; }	//	地点
        public int? listenerNumber { get; set; }	//	听众人数
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength (20)]
        public string WorkTitle { get; set; }//职称

        public string Identity { get; set; }//身份证号
        [StringLength (20)]
        public string Telephone{get;set;}//手机号
        [StringLength (200)]
        public string Remark{get;set;}//备注
    }
}
