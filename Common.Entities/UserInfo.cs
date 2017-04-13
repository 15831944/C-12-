/*
 * 作者：未知
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：设置所有属性可以为空
 *              修改人：高琪   修改时间：2015年11月30日     修改内容：增加照片ID字段
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class UserInfo
    {
        public int UserInfoID { get; set; }
        // [Required]
        // [StringLength(20)]
        //public string UserInfoBH { get; set; }//用户编号
         //[Required]
         [StringLength(20)]
        public string LoginName { get; set; }//登录名
         //[Required]
         [StringLength(50)]
        public string LoginPWD { get; set; }//密码
         //[Required]
         [StringLength(20)]
        public string UserName { get; set; }//用户名
         [StringLength(50)]
        public string Email { get; set; }//邮箱地址
         [StringLength(15)]
        public string TeleNum { get; set; }//电话号
         //[Required]
        public bool? Sex { get; set; }	//性别
         [StringLength(10)]
        public string Nation { get; set; }	//民族
         [StringLength(20)]
        public string Hometown { get; set; }	//籍贯
        public DateTime? Birth { get; set; }	//出生年月
        //[Required]
        public int AgencyID { get; set; }//		项目所属单位
        [StringLength(20)]
        public string JobTitle { get; set; }	//职称
        [StringLength(15)]
        public string HomeNum { get; set; }	//家庭号码
        [StringLength(15)]
        public string OfficeNum { get; set; }	//办公电话
        [StringLength(10)]
        public string DocumentsType { get; set; }	//证件类型

        public string DocumentsNum { get; set; }	//证件号码
        [StringLength(10)]
        public string PoliticalStatus { get; set; }	//政治面貌
        [StringLength(2000)]
        public string Profile { get; set; }	//个人简介
        [StringLength(10)]
        public string Education { get; set; }	//学历
        [StringLength(10)]
        public string Degree { get; set; }	//学位
        [StringLength(100)]
        public string ResearchDirection { get; set; }//研究方向
        [StringLength(100)]
        public string Specialty { get; set; }	//专长
        public bool? Marriage { get; set; }	//婚姻状况
        [StringLength(15)]
        public string Fax { get; set; }	//传真
        [StringLength(50)]
        public string HomeAddress { get; set; }	//家庭住址
        [StringLength(10)]
        public string PostalCode { get; set; }	//邮政编码
        [StringLength(15)]
        public string qqNum { get; set; }	//qq号码
        [StringLength (200)]
        public string Remark { get; set; }	//备注
         //[Required]
        public int? SecrecyLevel { get; set; }//涉密级别
        [StringLength(40)]
        public string UnitName { get; set; }	//单位名称
        [StringLength(20)]
        public string Domicile { get; set; }	//户籍地
         public DateTime? JobTitleTime { get; set; } //职称获得时间
         public DateTime? PoliticalStatusTime { get; set; }//政治面貌获得时间
        // public DateTime? PTPostTime { get; set; }//专业技术职务获得时间
         public bool? IsDocdorTeacher { get; set; }//是否为博士生导师
         public bool? IsMasteTeacher { get; set; }//是否为硕士生导师
         public DateTime? MasterTeacherTime { get; set; }//硕士生导师取得时间
         public DateTime? DoctorTeacherTime { get; set; }//博士生导师取得时间
        [StringLength (20)]
        public string  SubjectSortName { get; set; } //学科分类
        [StringLength(20)]
        public string  AdministrativeLevelName { get; set; }	//行政级别
        //[Required]
        [StringLength(20)]
        public string EntryPerson { get; set; }//录入人
        //[Required]
        public bool? IsPass { get; set; } //是否审核
        public string LastSchool { get; set; }//最后毕业学校
        public DateTime? EnterSchoolTime { get; set; }//入校时间
         [StringLength(20)]
        public string StudySource { get; set; }//学缘
        [StringLength(50)]
       public string StaffType { get; set; }//员工类型
        public int? PhotoID { get; set; }//照片ID
    }
}
