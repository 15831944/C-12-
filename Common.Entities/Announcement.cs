using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Announcement
    {
        public int AnnouncementID { get; set; }	//	通知公告ID
         [Required]
        [StringLength(100)]
        public string HeadLine { get; set; }	//	标题
        
        [StringLength (50)]
         public string SourceAgency { get; set; }//公告来源单位
        [StringLength (20)]
        public string AnnouncementSortName { get; set; }	//	分类（通知公告）
         [Required]
        public DateTime? Time { get; set; }	//	时间
        public int? AttachmentID { get; set; }	//	附件表ID
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
