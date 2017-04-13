using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class WorkExperience
    {
        public	int	WorkExperienceID	{get;set;}	//ID	
        public	DateTime?	StartTime	{get;set;}	//开始时间	
        public	DateTime?	EndTime	{get;set;}	//结束时间
         [Required]
        [StringLength(20)]
        public	string	Post	{get;set;}	//职务
         [Required]
        [StringLength(20)]
        public	string	JobTitle	{get;set;}	//职称
         [Required]
        [StringLength(40)]
        public string WorkUnit { get; set; }//工作单位
         [Required]
        public int? UserInfoID { get; set; }   //人员ID
         [Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        [Required]
        public bool? IsPass { get; set; } //是否审核
        [StringLength(200)]
        public string Remark { get; set; }//备注
        public string PartTimeUnit { get; set; }//兼职工作单位
    }
}
