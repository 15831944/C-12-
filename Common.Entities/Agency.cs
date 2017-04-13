using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
namespace Common.Entities
{
    public class Agency
    {
        public int AgencyID { get; set; }	//机构ID	
        [Required]
        [StringLength(40)]
        public string AgencyName { get; set; }	//机构名称		
        public int? ParentID { get; set; }	//ParentID	
        [Required]
        [StringLength(20)]
        public string AgencyHeads { get; set; }	//机构负责人	
        [Required]
        public string  AgencyNumber { get; set; }	//机构分类编号	
        [StringLength(100)]
        public string Research { get; set; }	//研究方向
        [Required]	
        public int? FullTimeNumbers { get; set; }	//专职人数	
        public int? PartTimeNumbers { get; set; }	//兼职人数
        [StringLength(10)]
        public string Area { get; set; }	//面积
        [StringLength(50)]
        public string Location { get; set; }	//地点	
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength (20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [Required]
        public string IsGlobal { get; set; }//总体机构，内部结构
       // public virtual ICollection<FutherStudy> FutherStudys { get; set; }
        //public virtual ICollection<Files> Files { get; set; }
        //public virtual ICollection<ScienceReport> ScienceReports { get; set; }
        //public virtual ICollection<UnitInspect> UnitInspects { get; set; }
        //public virtual ICollection<Achievement> Achievements { get; set; }
        //public virtual ICollection<UnitLectures> UnitLectures { get; set; }
        //public virtual ICollection<WorkPlanSummary> AWorkPlanSummarys { get; set; }       
        //public virtual ICollection<Announcement> Announcements { get; set; }
        //public virtual ICollection<Project> Projects { get; set; }
        //public virtual ICollection<UserInfo> UserInfos { get; set; }
        //public virtual ICollection<Contract> Contracts { get; set; }
    }
}
