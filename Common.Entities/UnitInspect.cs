using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class UnitInspect
    {
        public int UnitInspectID { get; set; }	//	ID
        [Required]
        [StringLength(10)]
        public string InspectName { get; set; }	//	姓名
        [Required]
        [StringLength(40)]
        public string WorkPlace { get; set; }	//	工作单位
        [Required]
        public int? AgencyID { get; set; }	//机构ID	

        [StringLength(20)]
        public string Duty { get; set; }	//	职称/职务
        public DateTime? InspectTime { get; set; }	//	时间
        [Required]
        [StringLength(200)]
        public string VisitContent { get; set; }	//	参观内容
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
