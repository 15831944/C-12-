/**编写人：王会会
 * 时间：2014年6月20号
 * 功能：人员投入表的相关操作
 * 修改履历：
 **/
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entities;

using System.Data.Entity;
namespace BLHelper
{
    public class BLLStaffDevote
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                StaffDevote NewStaffDevote = dbcontext.StaffDevoteContext.Find(ID);
                if (NewStaffDevote == null)
                    return;
                NewStaffDevote.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 录入科研人员投入时间,，向人员投入表中插入数据
        /// </summary>
        /// <param name="staff"></param>
        public bool Insert(StaffDevote staff)
        {
            dbcontext.Configuration.ValidateOnSaveEnabled = false;//关闭验证
            dbcontext.StaffDevoteContext.Add(staff);
            dbcontext.SaveChanges();
            dbcontext.Configuration.ValidateOnSaveEnabled = true;//保存完之后再开启
            return true;
        }
        //删除科研人员投入的信息
        public void Delete(int staffdevoteid)
        {
            StaffDevote sd = dbcontext.StaffDevoteContext.Where(u => u.StaffDevoteID == staffdevoteid).FirstOrDefault();
            dbcontext.StaffDevoteContext.Attach(sd);
            dbcontext.StaffDevoteContext.Remove(sd);
            dbcontext.SaveChanges();
        }
        //修改科研人员投入的信息
        public void Update(StaffDevote sd)
        {
            try
            {
                StaffDevote nsd = dbcontext.StaffDevoteContext.Find(sd.StaffDevoteID);
                nsd.UserInfoID = sd.UserInfoID;
                nsd.ProjectID = sd.ProjectID;
                nsd.DevoteTime = sd.DevoteTime;
                nsd.ExitTime = sd.ExitTime;
                nsd.IsPass = sd.IsPass;
                nsd.ProjectCompletion = sd.ProjectCompletion;
                nsd.SecrecyLevel = sd.SecrecyLevel;
                nsd.EntryPerson = sd.EntryPerson;
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //判断UserID是否存在相同的Project
        public StaffDevote  IsNullUserInfoID(int userid, int projectid)
        {
            return dbcontext.StaffDevoteContext.Where(u => u.UserInfoID == userid && u.ProjectID == projectid).FirstOrDefault();
        }
        //判断序号Sort是否存在在项目
        public StaffDevote  IsNullSort(int ProjectID, int Sort)
        {
            return dbcontext.StaffDevoteContext.Where(u => u.ProjectID == ProjectID && u.Sort == Sort).FirstOrDefault();
        }
        //根据UserID和ProjectID查找StaffDevoteID
        public int FindStaffDevoteID(int UserID,int ProjectID)
        {
            if (UserID != 0 && ProjectID != 0)
            {
                var results = dbcontext.StaffDevoteContext.Where(u => u.UserInfoID == UserID && u.ProjectID == ProjectID)
                    .Select(u => new { u.StaffDevoteID }).ToList();
                return results.FirstOrDefault().StaffDevoteID;
            }
            else
                return 0;
        }
        //查看人员投入表的全部信息
        public List<StaffDevote> FindAll(int? SecrecyLevel)
        {
            return dbcontext.StaffDevoteContext.Where(u =>u.SecrecyLevel <= SecrecyLevel && u.IsPass ==true).ToList();
        
        }
        //将表中的审核状态变为False
        public bool ChangePass(int ProjectID, bool ispass)
        {
            try
            {
                StaffDevote  project = dbcontext.StaffDevoteContext .Find(ProjectID);
                project.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        /// <summary>
        ///  分项目查看科研人员投入时间
        /// </summary>
        /// <param name="aProjectID"></param>
        /// <returns></returns>
        public List<StaffDevote> FindDevotetimes(int projectID, int secrecylevel)
        {
            bool Ispass = true;
            return dbcontext.StaffDevoteContext.Where(z => z.ProjectID == projectID &&
                z.SecrecyLevel == secrecylevel && z.IsPass == Ispass).OrderBy(c => c.StaffDevoteID).ToList();
        }
        //根据人员投入ID查找人员投入信息
        public List<StaffDevote> FindStaffDevoteID(int StaffDevoteID)
        {
            return dbcontext.StaffDevoteContext.Where(u => u.StaffDevoteID == StaffDevoteID && u.IsPass == true).ToList();
        }
       
        //根据ProjecyID查找用户登录名
        public string FindEntryPerson(int StaffDevoteID)
        {
            if (StaffDevoteID != null)
            {
                var results = dbcontext.StaffDevoteContext.Where(u => u.StaffDevoteID == StaffDevoteID && u.IsPass == true).Select(u => new { u.EntryPerson }).ToList();
                return results.FirstOrDefault().EntryPerson;
            }
            else
            {
                return null;
            }

        }
        //分页
        public List<StaffDevote> FindPaged(int aProjectID,int? SecrecyLevel)
        {
            return dbcontext.StaffDevoteContext.Where(u => u.ProjectID == aProjectID && u.SecrecyLevel <= SecrecyLevel && u.IsPass ==true).ToList();
        }
        //根据项目ID查找StaffDevoteID
        public List<int> FindStaffDevoteIDList(int projectID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.StaffDevoteContext .Where(a => a.ProjectID ==projectID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].StaffDevoteID));
            }
            return list;
        }
        public StaffDevote Find(int StaffDevoteID)
        {
            return dbcontext.StaffDevoteContext.Find(StaffDevoteID);
        }

        //根据人员姓名查询
        public List<StaffDevote> FindByName(int userid)
        {
            return dbcontext.StaffDevoteContext.Where(u => u.UserInfoID == userid && u.IsPass == true).ToList();
        }
    }
}
