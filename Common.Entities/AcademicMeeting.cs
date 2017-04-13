/**编写人：未知
 * 时间：  未知
 * 功能：  学术会议基础类
 * 修改履历：       1、修改人：吕博杨
 *                    修改时间：2015年11月29日
 *                    修改内容：字段MajorPeople更名为AttendMeetingPeople
 *                             增加照片ID字段（PhotoID)
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class AcademicMeeting
    {
        public int AcademicMeetingID { get; set; }	//	会议ID
        [Required]
        [StringLength(100)]
        public string MeetingName { get; set; }	//	会议名称
        [Required]
        [StringLength(40)]
        public string Organizers { get; set; }	//	主办方
        [StringLength(40)]
        public string Coorganizers { get; set; }	//	协办方
        public DateTime? StratTime { get; set; }	//	会议开始时间
        public DateTime? EndTime { get; set; }	//	会议结束时间
        [Required]
        [StringLength(50)]
        public string MeetingPlace { get; set; }	//	会议地点
        [StringLength(20)]
        public string ProceedingsofTitle { get; set; }	//	论文集名称
        public int? AttachmentID { get; set; }//   附件ID（通知会议） 
        //lby ↓
        public int? PhotoID { get; set; } //照片ID
        [StringLength(10)]
        public string MeetingSortName { get; set; }//会议分类名称
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        public string MeetingCount { get; set; }//会议参加人数
        [StringLength(20)]
        public string MeetingMajorPerson { get; set; }//会议主席
        [StringLength(100)]
        public string MeetingMajorTheme { get; set; }//会议主题
        [Required]
        [StringLength(20)]
        public string MeetingHost { get; set; }//会议主持人
        [StringLength(200)]
        public string MeetingContent { get; set; }//会议内容简介
        [StringLength(100)]
        public string AttendMeetingPeople { get; set; }//会议参加人员（20个左右）
    }
}
