/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能：社会兼职表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月15号
 *           修改人:王会会
 *           修改内容：添加根据SocialPartTimeID查找EntryPerson的方法Find(int);
 *                    将按ID，级别，时间，授予部门查询社会兼职信息改为可分页的
 *           
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
  public class BLLSocialPartTime
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                SocialPartTime Social = dbcontext.SocialPartTimeContext.Find(ID);
                if (Social == null)
                    return;
                Social.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //为某用户添加社会兼职
        public bool InsertForPeople(SocialPartTime parttime)
        {
            if (parttime != null )
            {
                dbcontext.SocialPartTimeContext.Add(parttime);
                dbcontext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        //根据SocialPartTimeID查找社会兼职信息
        public SocialPartTime FindBySocialID(int SocialID, int? SecrecyLevel, bool ispass)
        {
            if (SocialID != null && SecrecyLevel != null)
            {
                return dbcontext.SocialPartTimeContext.Where(s => s.SocialPartTimeID == SocialID && s.SecrecyLevel <= SecrecyLevel && s.IsPass == ispass).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
       
        //根据人员ID查找社会兼职 
        public List<SocialPartTime> SelectByID(int UserInfoID, int? SecrecyLevel)
        {
            if (UserInfoID != null && SecrecyLevel != null)
            {
                return dbcontext.SocialPartTimeContext.Where(s => s.UserInfoID == UserInfoID && s.SecrecyLevel <= SecrecyLevel && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据时间查询
        public List<SocialPartTime> SelectByApproveTime(int ApproveTime,int? SecrecyLevel)
        {
            if (ApproveTime != null && SecrecyLevel != null)
            {
                return dbcontext.SocialPartTimeContext.Where(s => s.ApproveTime.Value .Year  == ApproveTime && s.SecrecyLevel <= SecrecyLevel && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据授予部门查询
        public List<SocialPartTime> SelectByDepartment(string AwardDepartments,int? SecrecyLevel)
        {
            if (AwardDepartments != null && SecrecyLevel != null)
            {
                return dbcontext.SocialPartTimeContext.Where(s => s.AwardDepartments.Contains (AwardDepartments) && s.SecrecyLevel <= SecrecyLevel && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据级别查询
        public List<SocialPartTime> SelectByLevel(string LevelName,int? SecrecyLevel)
        {
            if (LevelName != null && SecrecyLevel != null)
            {
                return dbcontext.SocialPartTimeContext.Where(s => s.LevelName == LevelName && s.SecrecyLevel <= SecrecyLevel && s.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //更新用户社会兼职信息
        public bool Update(SocialPartTime parttime)
        {
            try
            {
                if (parttime != null )
                {
                    SocialPartTime newparttime = dbcontext.SocialPartTimeContext.Find(parttime.SocialPartTimeID);
                    newparttime.UserInfoID = parttime.UserInfoID;
                    newparttime.LevelName = parttime.LevelName;
                    newparttime.PartTimeName = parttime.PartTimeName;
                    newparttime.AwardDepartments = parttime.AwardDepartments;
                    newparttime.Terms = parttime.Terms;
                    newparttime.ApproveTime = parttime.ApproveTime;
                    newparttime.Remark = parttime.Remark;
                    newparttime.PartUnitName = parttime.PartUnitName;
                    newparttime.SecrecyLevel = parttime.SecrecyLevel;
                    newparttime.Sort = parttime.Sort;
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
        //删除用户兼职信息
        public bool Delete(int parttimeid)
        {
            try
            {

                SocialPartTime parttime = dbcontext.SocialPartTimeContext.Where(u => u.SocialPartTimeID == parttimeid).FirstOrDefault();
                    dbcontext.SocialPartTimeContext.Attach(parttime);
                    dbcontext.SocialPartTimeContext.Remove(parttime);
                    dbcontext.SaveChanges();
                    return true;
              
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //分页
        public List<SocialPartTime> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.SocialPartTimeContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //添加根据SocialPartTimeID查找EntryPerson的方法
        public SocialPartTime Find(int SocialPartTimeID)
        {
            return dbcontext.SocialPartTimeContext.Find(SocialPartTimeID);
        }
        //根据ID查询备注
        public string FindRemark(int id)
        {
            List<SocialPartTime> list = new List<SocialPartTime>();
            list = dbcontext.SocialPartTimeContext.Where(a => a.SocialPartTimeID == id).ToList();
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
        //根据人员ID查找SocialPartTimeID 
        public List<int> FindSocialPartTimeIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.SocialPartTimeContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].SocialPartTimeID));
            }
            return list;
        }
    }
}
