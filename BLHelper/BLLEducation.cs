/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:学历表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *            2.时间：8月16号
 *           修改人:王会会
 *           修改内容：添加根据EducationID查找EntryPerson的方法Find(int);
 *                    将按条件查询学历表信息改为可分页的
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
   public class BLLEducation
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Education NewEducation = dbcontext.EducationContext.Find(ID);
                if (NewEducation == null)
                    return;
                NewEducation.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

       //插入一行
        public bool InsertForPeople(Education ed)
        {
            try
            {
                if (ed != null)
                {
                    dbcontext.EducationContext.Add(ed);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
     
        //根据人员ID查找学历
        public List<Education> SelectByID(int UserInfoID,int? SecrecyLevel)
        {
            if (UserInfoID != 0)
            {
                return dbcontext.EducationContext.Where(e => e.UserInfoID == UserInfoID && e.SecrecyLevel <= SecrecyLevel && e.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //更新用户学历信息
        public bool Update(Education ed)
        {
            try
            {
                if (ed != null )
                {
                    Education newed = dbcontext.EducationContext.Find(ed.EducationID);
                    newed.UserInfoID = ed.UserInfoID;
                    newed.SchoolName = ed.SchoolName;
                    newed.Degree = ed.Degree;
                    newed.EduTime = ed.EduTime;
                    newed.Major = ed.Major;
                    newed.College = ed.College;
                    newed.Series = ed.Series;
                    newed.SecrecyLevel = ed.SecrecyLevel;
                    newed.DegreeNumber = ed.DegreeNumber;
                    newed.GraduateNumber = ed.GraduateNumber;
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //删除用户学历
        public bool Delete(int EducationID)
        {
            try
            {

                Education ed = dbcontext.EducationContext.Where(u => u.EducationID == EducationID).FirstOrDefault ();
                    dbcontext.EducationContext.Attach(ed);
                    dbcontext.EducationContext.Remove(ed);
                    dbcontext.SaveChanges();
                    return true;
               
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }        
        //分页（根据等级查找用户学历）
        public List<Education> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.EducationContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //添加根据SocialPartTimeID查找EntryPerson的方法
        public Education Find(int EducationID)
        {
            return dbcontext.EducationContext.Find(EducationID);
        }
        //根据EducationID查找学历信息
        public List<Education> FindByEducationID(int EducationID)
        {
            if (EducationID != 0)
            {
                var results = dbcontext.EducationContext.Where(u => u.EducationID == EducationID && u.IsPass == true).ToList();
                return results;
            }
            else
            {
                return null;
            }

        }
        //根据项目ID查找ImprotID
        public List<int> FindWorkExperienceIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.WorkExperienceContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].WorkExperienceID));
            }
            return list;
        }
        //根据项目ID查找EducationID
        public List<int> FindEducationIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.EducationContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].EducationID));
            }
            return list;
        }
    }
}
