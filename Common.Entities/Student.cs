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
    public class Student
    {
        public int StudentID { get; set; }	//	ID	
        //[Required]
        [StringLength(20)]
        public string Sno { get; set; }	//	学号	
        //[Required]
        [StringLength(10)]
        public string Sname { get; set; }	//	姓名	
        //[Required]
        public bool? Sex { get; set; }	//	性别	0:女 1：男
        [StringLength(10)]
        public string DocumentType { get; set; }	//	证件类型	

        public string DocumentNumber { get; set; }	//	证件号码	
        [StringLength(20)]
        public string Contact { get; set; }	//	联系方式	
        //[Required]
        public bool? IsGraduation { get; set; }	//	是否毕业	0:未毕业 1：已毕业
        //[Required]
        [StringLength(20)]
        public string Specialty { get; set; }	//	专业	
        //[Required]
        [StringLength(100)]
        public string SResearch { get; set; }	//	研究方向	
         [StringLength(40)]
        public string SGraduationDirection { get; set; }	//	毕业去向	
        //[Required]
         [StringLength(10)]
        public string Type { get; set; }	//	类型	
        public DateTime? EnterTime { get; set; }//入学时间
        public DateTime? GraduationTime { get; set; }//毕业时间
        //[Required]
        public int? UserInfoID { get; set; }   //人员ID	授课老师
        //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        //[Required]
        public int? AgencyID { get; set; } //所属部门
    }
}
