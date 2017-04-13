/**编写人：未知
 * 时间：未知
 * 功能: 平台类
 * 修改履历： 1、修改时间：2015年11月25日
 *              修改人：吕博杨
 *              修改内容：增加批复文号、平台负责人、平台成员、批复经费、平台管理（时间、人员、业务、经费）、上传文件等字段
 **/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Common.Entities
{
    public class Platform
    {
        public int PlatformID { get; set; }//	ID
        [Required]
        [StringLength(100)]
        public string PlatformName { get; set; }//	平台名称
        [Required]
        [StringLength(30)]
        public string PlatformRank { get; set; }//	平台级别
        [Required]
        [StringLength(50)]
        public string AgreeUnit { get; set; }//	批复部门
        [Required]
        public DateTime? AgreeTime { get; set; }//	批复日期
        //[Required]
        [StringLength(20)]
        public string AgreeNumber { get; set; }//批复文号
        //[Required]
        [StringLength(20)]
        public string PlatformPrincipal { get; set; }//平台负责人
        //[Required]
        [StringLength(200)]
        public string PlatformMember { get; set; }//平台成员
        //[Required]
        [StringLength(20)]
        public string AgreeExpenditure { get; set; }//批复经费
        //[Required]
        [StringLength(200)]
        public string PlatformManagement { get; set; }//平台管理
        public int? AttachmentID { get; set; }//附件ID
        [Required]
        [StringLength(20)]
        public string PlatformType { get; set; }//	平台类别
        [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
    }
}
