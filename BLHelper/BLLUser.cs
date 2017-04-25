/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能：社会兼职表的相关操作
 * 修改履历：1.时间:8月4日
 *            修改人：方淑云
 *            修改内容：添加根据用户名查找用户ID的方法
                       添加判断是否存在该用户的方法
 *           2.时间:8月16日
 *            修改人：李金秋
 *            修改内容：添加根据用户名查询用户信息 FindByUSerName(string)
 *            3.时间：9月18号
 *            修改人：方淑云
 *            修改内容：添加根据登录名查找用户名SelectUserName(string)
 *            4.时间：2015年3月3日
 *            修改人：张凡凡
 *            修改内容：修改关于超级管理员的查询显示问题
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLHelper
{
    public class BLLUser
    {

        DataBaseContext dbcontext = new DataBaseContext();
        //插入用户
        public bool Insert(UserInfo aUser)
        {
            try
            {

                dbcontext.UserInfoContext.Add(aUser);
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //更新用户信息
        public bool Update(UserInfo aUser)
        {
            try
            {
                UserInfo NewUser = dbcontext.UserInfoContext.Find(aUser.UserInfoID);
                NewUser.UserName = aUser.UserName;
                NewUser.Sex = aUser.Sex;
                NewUser.Nation = aUser.Nation;
                NewUser.Hometown = aUser.Hometown;
                NewUser.Birth = aUser.Birth;
                NewUser.JobTitle = aUser.JobTitle;//职称
                NewUser.AgencyID = aUser.AgencyID;
                NewUser.HomeNum = aUser.HomeNum;
                NewUser.OfficeNum = aUser.OfficeNum;
                NewUser.DocumentsNum = aUser.DocumentsNum;
                NewUser.DocumentsType = aUser.DocumentsType;
                NewUser.PoliticalStatus = aUser.PoliticalStatus;
                NewUser.Profile = aUser.Profile;
                NewUser.Education = aUser.Education;
                NewUser.Degree = aUser.Degree;
                NewUser.Specialty = aUser.Specialty;
                NewUser.Marriage = aUser.Marriage;
                NewUser.Fax = aUser.Fax;
                NewUser.HomeAddress = aUser.HomeAddress;
                NewUser.PostalCode = aUser.PostalCode;
                NewUser.qqNum = aUser.qqNum;
                NewUser.Remark = aUser.Remark;
                NewUser.UnitName = aUser.UnitName;
               NewUser.StaffType = aUser.StaffType ;
                //NewUser.UserInfoBH = aUser.UserInfoBH;
                NewUser.LoginName = aUser.LoginName;
                NewUser.UserName = aUser.UserName;
                NewUser.Email = aUser.Email;
                NewUser.TeleNum = aUser.TeleNum;
                NewUser.AdministrativeLevelName = aUser.AdministrativeLevelName;
               // NewUser.PTPostName = aUser.PTPostName;
                NewUser.Domicile = aUser.Domicile;
                NewUser.SubjectSortName = aUser.SubjectSortName;
                NewUser.JobTitleTime = aUser.JobTitleTime;
                NewUser.PoliticalStatusTime = aUser.PoliticalStatusTime;
                //NewUser.PTPostTime = aUser.PTPostTime;
                NewUser.IsDocdorTeacher = aUser.IsDocdorTeacher;
                NewUser.IsMasteTeacher = aUser.IsMasteTeacher;
                NewUser.MasterTeacherTime = aUser.MasterTeacherTime;
                NewUser.DoctorTeacherTime = aUser.DoctorTeacherTime;
                NewUser.SecrecyLevel = aUser.SecrecyLevel;
                NewUser.ResearchDirection = aUser.ResearchDirection;
                NewUser.LastSchool = aUser.LastSchool;
                NewUser.StaffType = aUser.StaffType;
               
                NewUser.EnterSchoolTime = aUser.EnterSchoolTime;//入校时间
                NewUser.StudySource = aUser.StudySource;//学缘
                NewUser.PhotoID = aUser.PhotoID;//照片ID
                dbcontext.SaveChanges();
                
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }
      
        //根据用户名查找用户ID
        public int FindID(string Name)
        {
            if (Name != "")
            {
                var results = dbcontext.UserInfoContext.Where(u => u.UserName == Name).ToList();
                if (results.Count == 0)
                    return 0;
                else
                {
                    if (results.FirstOrDefault().IsPass == true)
                        return results.FirstOrDefault().UserInfoID;
                    else
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        //根据用户名模糊查询用户ID
        public List<int> FindList(string name, int? level)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.UserInfoContext.Where(a => a.UserName.Contains(name) && a.SecrecyLevel <= level && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].UserInfoID));
            }

            //去掉超级管理员
            int idadmin = FindID("超级管理员");
            list.Remove(idadmin);

            return list;
        }
        //判断是否存在该用户
        public UserInfo IsUser(string UserName)
        {
            return dbcontext.UserInfoContext.Where(u => u.UserName == UserName).FirstOrDefault();
        }
        //判断是否存在该用户编号
        //public UserInfo IsUserInfoBH(string UserInfoBH)
        //{
        //    return dbcontext.UserInfoContext.Where(u => u.UserInfoBH == UserInfoBH).FirstOrDefault();
        //}
        //判断是否存在该用户登录名
        public UserInfo IsLoginName(string LoginName)
        {
            return dbcontext.UserInfoContext.Where(u => u.LoginName == LoginName).FirstOrDefault();
        }
       
        //根据用户名修改用户密码
        public bool ChangePWD(int userID, string LoginPWD)
        {
            try
            {
                UserInfo User = dbcontext.UserInfoContext.Find(userID);
                if (User == null)
                    return false;
                User.LoginPWD = LoginPWD;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //查找用户等级
        public int FindLevel(string LoginName)
        {
            if (LoginName != null)
            {

                var results = dbcontext.UserInfoContext.Where(u => u.LoginName == LoginName).Select(u => new { u.SecrecyLevel }).ToList();
                return results.FirstOrDefault().SecrecyLevel.Value;
            }
            else
            {
                return 0;
            }
        }
        //查找某一用户信息
        public UserInfo Find(int UserInfoID, bool ispass)
        {
            return dbcontext.UserInfoContext.Where(u => u.UserInfoID == UserInfoID && u.IsPass == ispass).FirstOrDefault();
        }
        //查找姓名为name的用户的信息
        public List<UserInfo> FindByName(string name, int? SecrecyLevel)
        {
            if (name != null && SecrecyLevel != null)
            {
                return dbcontext.UserInfoContext.Where(u => u.UserName.Contains(name) && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }

        //查找姓名为name的用户的信息(审核状态)
        public List<UserInfo> FindByUserName(string name, int? SecrecyLevel)
        {
            if (name != null && SecrecyLevel != null)
            {
                return dbcontext.UserInfoContext.Where(u => u.UserName == name && u.SecrecyLevel <= SecrecyLevel).ToList();
            }
            else
            {
                return null;
            }
        }
        //按登录名查找
        public List<UserInfo> FindByLoginName(string Loginname, int? SecrecyLevel)
        {
            if (Loginname != null)
            {
                List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.LoginName.Contains(Loginname) && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
                
                //去掉超级管理员
                UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == "超级管理员").FirstOrDefault();
                res.Remove(user);

                return res;
            }
            else
            {
                return null;
            }
        }
        //分页
        public List<UserInfo> FindPaged(int? SecrecyLevel)
        {
            List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.UserInfoID).ToList();
            
            //去掉超级管理员
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == "超级管理员").FirstOrDefault();
            res.Remove(user);

            return res;
        }

        //登录
        public UserInfo Login(string LoginName, string LoginPWD)
        {
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.LoginName == LoginName && u.IsPass == true).FirstOrDefault();
            if (user != null && user.LoginPWD == LoginPWD)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        //根据用户编号查UserInfoID
        //public int FindUserInfoID(string UserInfoBH)
        //{

        //    var results = dbcontext.UserInfoContext.Where(u => u.UserInfoBH == UserInfoBH).ToList();
        //    return results.FirstOrDefault().UserInfoID;
        //}
        //删除用户
        public bool Delete(int UserInfoID)
        {
            try
            {
                UserInfo uu = dbcontext.UserInfoContext.Where(u => u.UserInfoID == UserInfoID).FirstOrDefault();
                dbcontext.UserInfoContext.Attach(uu);
                dbcontext.UserInfoContext.Remove(uu);
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //判断该用户登录名是否存在
        public bool IsNullLoginName(string LoginName)
        {
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.LoginName == LoginName && u.IsPass == true).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     
        //根据用户登录名查找用户IDUserInfoID
        public int Finduserid(string loginname)
        {
            if (loginname != null)
            {
                var results = dbcontext.UserInfoContext.Where(u => u.LoginName == loginname && u.IsPass == true).Select(u => new { u.UserInfoID }).ToList();
                return results.FirstOrDefault().UserInfoID;
            }
            else
            {
                return 0;
            }

        }
        //根据UserInfoID查找用户登录名
        public string FindEntryPerson(int? UserID)
        {
            if (UserID != null)
            {
                var results = dbcontext.UserInfoContext.Where(u => u.UserInfoID == UserID && u.IsPass == true).Select(u => new { u.EntryPerson }).ToList();
                return results.FirstOrDefault().EntryPerson;
            }
            else
            {
                return null;
            }

        }
        //根据ID查找用户名字
        public string FindUserName(int? userid)
        {
            List<UserInfo> ag = new List<UserInfo>();
            ag = dbcontext.UserInfoContext.Where(u => u.UserInfoID == userid && u.IsPass == true).ToList();
            if (ag.Count != 0)
                return ag.FirstOrDefault().UserName;
            else
                return "";
        }
        //将表中的审核状态变为False
        public bool ChangePass(int userID, bool ispass)
        {
            try
            {
                UserInfo User = dbcontext.UserInfoContext.Find(userID);
                if (User == null)
                    return false;
                User.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //按机构名称查询人员信息
        public List<UserInfo> FindByAgencyName(int AgencyID, int? SecrecyLevel)
        {
            return (from a in dbcontext.UserInfoContext
                    where a.AgencyID == AgencyID && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true
                    select new
                    {
                        UserName = a.UserName,
                        Sex = a.Sex,
                        Education = a.Education,
                        Degree = a.Degree,
                        Email = a.Email,
                        TeleNum = a.TeleNum
                    }).ToList().Select(a => new UserInfo
                                 {
                                     UserName = a.UserName,
                                     Sex = a.Sex,
                                     Education = a.Education,
                                     Degree = a.Degree,
                                   
                                     Email = a.Email,
                                     TeleNum = a.TeleNum
                                 }).ToList();
        }
        //判断男女
        public string getgender(string xb)
        {
            if (xb == "True")
                return "男";
            else
                return "女";
        }

        //根据等级查找用户姓名
        public List<string> FindUserBySecrecyLevel(int SecrecyLevel)
        {

            var q = from c in dbcontext.UserInfoContext
                    where c.SecrecyLevel <= SecrecyLevel && c.IsPass == true
                    select c.UserName;
            return q.ToList();
        }
       
        //根据UserID获取用户名
        public string FindByUserID(int? UserID)
        {
            List<UserInfo> df = new List<UserInfo>();
            df = dbcontext.UserInfoContext.Where(d => d.UserInfoID == UserID).ToList();
            if (df.Count() != 0)
            {
                return df.FirstOrDefault().UserName;
            }
            else
            {
                return "";
            }
        }
        //根据用户名查询用户信息 
        public UserInfo FindByUserName(string UserName)
        {
            //return dbcontext.UserInfoContext.Where(p => p.UserName == UserName).FirstOrDefault();
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == UserName && u.IsPass == true).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        //根据ID查询备注
        public string FindRemark(int id)
        {
            List<UserInfo> list = new List<UserInfo>();
            list = dbcontext.UserInfoContext.Where(a => a.UserInfoID == id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().Remark != "")
                {
                    return list.FirstOrDefault().Remark;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        //根据ID查询备注
        public string FindProfile(int id)
        {
            List<UserInfo> list = new List<UserInfo>();
            list = dbcontext.UserInfoContext.Where(a => a.UserInfoID == id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().Profile != "")
                {
                    return list.FirstOrDefault().Profile;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        //根据ID查找照片
        public int FindPhotoID(int UserInfoID)
        {

            var Academic = dbcontext.UserInfoContext.Find(UserInfoID);
            if (Academic == null)
                return 0;
            else
            {
                if (Academic.PhotoID == null)
                    return 0;
                else
                    return Convert.ToInt32(Academic.PhotoID);
            }
        }
        //根据人员ID(会议ID)查看人员信息
        public UserInfo FindByUserInfoID(int UserInfoID, bool ispass)
        {
            return dbcontext.UserInfoContext.Where(p => p.UserInfoID == UserInfoID && p.IsPass == ispass).FirstOrDefault();
        }
        //根据ID获取文件路径
        public string FindPath(int AttachmentID)
        {
            List<Attachment> at = new List<Attachment>();
            at = dbcontext.AttachmentContext.Where(a => a.AttachmentID == AttachmentID).ToList();
            if (at.Count() != 0)
            {
                return at.FirstOrDefault().FilePath;
            }
            else
            {
                return "";
            }
        }
        //根据用户登录名查询用户信息
        public UserInfo FindByLoginName(string LoginName)
        {
            return dbcontext.UserInfoContext.Where(p => p.LoginName == LoginName &&  p.IsPass ==true).FirstOrDefault();
        }
        //判断是否存在信息(用户名 登录名 用户编号)
        public bool IsExit(string userName, string LoginName)
        {
            UserInfo user = dbcontext.UserInfoContext.Where(p => p.UserName == userName || p.LoginName == LoginName).FirstOrDefault();
            if (user != null)
                return true;
            else
                return false;
        }
        //重置密码查询除（5级）管理员自己的
        public List<UserInfo > FindResetPWD(int SecrecyLevel,string LoginName)
        {
            return dbcontext.UserInfoContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.LoginName != LoginName && u.IsPass == true)
                .OrderBy(u => u.UserInfoID).ToList();
        }
        //重置密码查找姓名为name的用户的信息(模糊查询)除（5级）管理员自己的
        public List<UserInfo> FindPWDName(string name, int? SecrecyLevel, string LoginName)
        {
            if (name != null && SecrecyLevel != null)
            {
                return dbcontext.UserInfoContext.Where(u => u.UserName.Contains(name) && u.LoginName != LoginName && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }

        //根据入学时间查询用户信息
        public List<UserInfo> FindByEnterSchoolTime(int time, int secrecylevel)
        {
            List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.EnterSchoolTime.Value.Year == time && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
            return res;
        }

        //根据政治面貌查询用户信息
        public List<UserInfo> FindByPolitical(string political, int secrecylevel)
        {
            List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.PoliticalStatus == political && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
            
            //去掉超级管理员
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == "超级管理员").FirstOrDefault();
            if (res.Contains(user))
                res.Remove(user);

            return res;
        }

        //根据 行政级别查询人员信息
        public List<UserInfo> FindByAdministrativeLevel(string level, int secrecylevel)
        {
            List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.AdministrativeLevelName == level && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();

            //去掉超级管理员
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == "超级管理员").FirstOrDefault();
            if (res.Contains(user))
                res.Remove(user);

            return res;
        }

        //根据部门查找人员信息
        public List<UserInfo> FindByAgency(string agency, int secrecylevel)
        {
            BLLAgency blag = new BLLAgency();
            int agencyid = blag.FindByName(agency).AgencyID;
            List<UserInfo> res = dbcontext.UserInfoContext.Where(u => u.AgencyID == agencyid && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();

            //去掉超级管理员
            UserInfo user = dbcontext.UserInfoContext.Where(u => u.UserName == "超级管理员").FirstOrDefault();
            if (res.Contains(user))
                res.Remove(user);

            return res;
        }

        //根据学历查找人员信息
        public List<UserInfo> FindByEducation(string education, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.Education == education && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }

        //根据学位查找人员信息
        public List<UserInfo> FindByDegree(string degree, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.Education == degree && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }
        //根据员工类型查找
        public List<UserInfo> FindByStaffType(string StaffType, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.StaffType == StaffType && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }

        //根据研究方向查找人员信息
        public List<UserInfo> FindByResearchDirection(string researchDirection, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.Education == researchDirection && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }

        //根据最后毕业学校查找人员信息
        public List<UserInfo> FindByLastSchool(string lastSchool, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.Education == lastSchool && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }

        //根据职称查找人员信息
        public List<UserInfo> FindByJobTitle(string jobTitle, int secrecylevel)
        {
            return dbcontext.UserInfoContext.Where(u => u.Education == jobTitle && u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }

    }
}
