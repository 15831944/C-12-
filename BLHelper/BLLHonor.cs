/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能：荣誉称号表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月15号
 *           修改人：王会会
 *           修改内容； 添加根据HonorID查找录入人FindEntryPerson(int); 
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
   public class BLLHonor
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Honor NewHonor = dbcontext.HonorContext.Find(ID);
                if (NewHonor == null)
                    return;
                NewHonor.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //为某用户添加荣誉称号
        public bool InsertForPeople(Honor honor)
        {
            try
            {
                if (honor != null )
                {
                   
                    dbcontext.HonorContext.Add(honor);
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
       
        //根据人员ID查找荣誉称号
        public List<Honor> SelectByID(int UserInfoID,int? SecrecyLevel)
        {
            if (UserInfoID != null && SecrecyLevel != null)
            {
                return dbcontext.HonorContext.Where(h => h.UserInfoID == UserInfoID && h.SecrecyLevel <= SecrecyLevel && h.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据授予时间查询
        public List<Honor> SelectByTime(int GiveTime, int? SecrecyLevel)
        {
            if (GiveTime != null && SecrecyLevel != null)
            {
                return dbcontext.HonorContext.Where(h => h.GiveTime.Value .Year== GiveTime && h.SecrecyLevel <= SecrecyLevel && h.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据授予部门查询(模糊查询)
        public List<Honor> SelectByDivision(string GivDivision,int? SecrecyLevel)
        {
            if (GivDivision != null && SecrecyLevel != null)
            {
                return dbcontext.HonorContext.Where(h => h.GivDivision.Contains(GivDivision) && h.SecrecyLevel <= SecrecyLevel && h.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据(分类)级别查询
        public List<Honor> SelectBySort(string Sort, int? SecrecyLevel)
        {
            if (Sort != null && SecrecyLevel != null)
            {
                return dbcontext.HonorContext.Where(h => h.Sort == Sort && h.SecrecyLevel <= SecrecyLevel && h.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //更新用户荣誉称号
        public bool Update(Honor honor)
        {
            try
            {
                if (honor != null)
                {
                    Honor newhonor = dbcontext.HonorContext.Find(honor.HonorID);
                    //newhonor.HonorID = honor.HonorID;
                    newhonor.UserInfoID = honor.UserInfoID;
                    newhonor.TitleName = honor.TitleName;
                    newhonor.Sort = honor.Sort;
                    newhonor.Remark = honor.Remark;
                    newhonor.GiveTime = honor.GiveTime;
                    newhonor.GivDivision = honor.GivDivision;
                    newhonor.SecrecyLevel = honor.SecrecyLevel;
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
        //删除用户荣誉称号
        public bool Delete(int honorID)
        {
            try
            {

                Honor honor = dbcontext.HonorContext.Where(u => u.HonorID == honorID).FirstOrDefault();
                    dbcontext.HonorContext.Attach(honor);
                    dbcontext.HonorContext.Remove(honor);
                    dbcontext.SaveChanges();
                    return true;
             
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //分页(根据等级查找用户荣誉)
        public List<Honor> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.HonorContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //根据HonorID查找录入人
        public string FindEntryPerson(int HonorID)
        {
            if (HonorID != 0)
            {
                var results = dbcontext.HonorContext.Where(u => u.HonorID == HonorID && u.IsPass == true).Select(u => new { u.EntryPerson }).ToList();
                return results.FirstOrDefault().EntryPerson;
            }
            else
            {
                return null;
            }

        }
        //根据HonorID查找荣誉称号信息
        public Common.Entities.Honor FindByHonorID(int HonorID, bool ispass)
        {
            if (HonorID != 0)
            {
                var results = dbcontext.HonorContext.Where(u => u.HonorID == HonorID && u.IsPass == ispass).FirstOrDefault();
                return results;
            }
            else
            {
                return null;
            }

        }
        //根据ID查询备注
        public string FindRemark(int id)
        {
            List<Honor> list = new List<Honor>();
            list = dbcontext.HonorContext .Where(a => a.HonorID == id).ToList();
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
        //根据人员ID查找HonorID
        public List<int> FindHonorIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.HonorContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].HonorID));
            }
            return list;
        }
    }
}
