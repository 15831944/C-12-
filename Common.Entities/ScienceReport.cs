using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class ScienceReport
    {
        public int ScienceReportID { get; set; }	//	ID
        [Required]
         [StringLength(100)]
        public string SReportName { get; set; }	//	报告名称
        [Required]
          [StringLength(10)]
        public string SReportPeople { get; set; }	//	报告人
        public DateTime? SReportTime { get; set; }	//	报告时间
         [StringLength(40)]
        public string SReportPlace { get; set; }	//	报告地点

         public int? MeetingID { get; set; }	//	所属会议ID(学术会议表的外键)不可为空

        public int? AgencyID { get; set; }	//机构ID	
        public int? AccessoryID { get; set; }	//	附件ID(附件表的外键)
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
