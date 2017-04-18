﻿/*
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
    public class StaffDevote   //人员投入
    {
        public int StaffDevoteID { get; set; }  //ID    
         //[Required]
        public int? UserInfoID { get; set; }   //人员ID
        public DateTime? DevoteTime { get; set; }    //投入时间
        public DateTime? ExitTime { get; set; }  //退出时间
         //[Required]
        public int?  ProjectID { get; set; }  //项目ID
         //[Required]
        public int? Sort { get; set; }//排序
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength(20)]
        public string ProjectCompletion { get; set; }//项目完成情况
    }
}