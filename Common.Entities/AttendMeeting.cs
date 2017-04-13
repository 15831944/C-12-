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
    public class AttendMeeting //会议参加人员表
    {
        public int AttendMeetingID { get; set; }
         //[Required]
        public int?  UserInfoID { get; set; }   //人员ID
        // [Required]
        public int? AcademicMeetingID { get; set; }  //会议ID
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
