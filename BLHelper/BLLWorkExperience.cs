/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:工作经历表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *            2.时间：8月16号
 *           修改人:王会会
 *           修改内容：添加根据WorkExperienceID查找EntryPerson的方法Find(int);
 *                    将按ID查询工作经历信息和查询全部信息改为可分页的
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
    public class BLLWorkExperience
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                WorkExperience WorkExperience = dbcontext.WorkExperienceContext.Find(ID);
                if (WorkExperience == null)
                    return;
                WorkExperience.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //为某用户添加工作经历
        public bool InsertForPeople(WorkExperience wex)
        {
            try
            {
                if (wex != null)
                {
                  
                    dbcontext.WorkExperienceContext.Add(wex);
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

        //根据WorkExperienceID查找工作经历信息
        public List<WorkExperience> FindByWorkID(int WorkID, int? SecrecyLevel)
        {
            if (WorkID != null && SecrecyLevel != null)
            {
                return dbcontext.WorkExperienceContext.Where(s => s.WorkExperienceID == WorkID && s.SecrecyLevel <= SecrecyLevel && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据人员ID查找工作经历
        public List<WorkExperience> SelectByUserID(int UserInfoID,int? SecrecyLevel)
        {
            if (UserInfoID != null && SecrecyLevel != null)
            {
                return dbcontext.WorkExperienceContext.Where(w => w.UserInfoID == UserInfoID && w.SecrecyLevel <= SecrecyLevel && w.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }             
        //更新用户工作经历
        public bool Update(WorkExperience wex)
        {
            try
            {
                if (wex != null)
                {
                    WorkExperience newwex = dbcontext.WorkExperienceContext.Find(wex.WorkExperienceID);
                    //newwex.WorkExperienceID = wex.WorkExperienceID;
                    newwex.PartTimeUnit = wex.PartTimeUnit;
                    newwex.StartTime = wex.StartTime;
                    newwex.EndTime = wex.EndTime;
                    newwex.Post = wex.Post;
                    newwex.JobTitle = wex.JobTitle;
                    newwex.WorkUnit = wex.WorkUnit;
                    newwex.SecrecyLevel = wex.SecrecyLevel;
                    newwex.Remark = wex.Remark;
                    newwex.UserInfoID = wex.UserInfoID;
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
        //删除用户工作经历
        public bool Delete(int WorkExperienceID)
        {
            try
            {
                if (WorkExperienceID != null)
                {
                    WorkExperience wex = dbcontext.WorkExperienceContext.Where(u => u.WorkExperienceID == WorkExperienceID).FirstOrDefault();
                    dbcontext.WorkExperienceContext.Attach(wex);
                    dbcontext.WorkExperienceContext.Remove(wex);
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
        //分页（根据等级查找用户工作经历）
        public List<WorkExperience> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.WorkExperienceContext.Where(u => u.SecrecyLevel <= SecrecyLevel &&u.IsPass ==true).ToList();
        }
        //添加根据SocialPartTimeID查找EntryPerson的方法
        public WorkExperience Find(int WorkExperienceID)
        {
            return dbcontext.WorkExperienceContext.Find(WorkExperienceID);
        }
        //根据ID查询备注
        public string FindRemark(int id)
        {
            List<WorkExperience > list = new List<WorkExperience >();
            list = dbcontext.WorkExperienceContext .Where(a => a.WorkExperienceID  == id).ToList();
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
        //根据项目ID查找WorkExperienceID
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
    }
}
