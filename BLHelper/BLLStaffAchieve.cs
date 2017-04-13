using Common.Entities;
/**编写人：张凡凡
 * 时间：2014年7月24号
 * 功能：人员成果表的相关操作
 * 修改履历：
 **/
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLStaffAchieve
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                StaffAchieve NewStaffAchieve = dbcontext.StaffAchieveContext.Find(ID);
                if (NewStaffAchieve == null)
                    return;
                NewStaffAchieve.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //添加
        public bool Insert(StaffAchieve sa)
        {
            try
            {
                if (sa != null)
                {
                    dbcontext.StaffAchieveContext.Add(sa);
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

        //删除
        public bool Delete(int id)
        {
            try
            {

                StaffAchieve staffAchieve = dbcontext.StaffAchieveContext.Where(u => u.StaffAchieveID == id).FirstOrDefault();
                dbcontext.StaffAchieveContext.Attach(staffAchieve);
                dbcontext.StaffAchieveContext.Remove(staffAchieve);
                dbcontext.SaveChanges();
                return true;

            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //查出姓名列表
        public List<int> FindStaName(int ID)
        {
            List<int> list = new List<int>();
            var res = dbcontext.StaffAchieveContext.Where(s => s.AchievementID == ID).ToList();
            for (int i = 0; i < res.Count; i++)
                list.Add(Convert.ToInt32(res[i].UserInfoID));
            return list;
        }

        //输出成员
        public string FindMember(int Id)
        {
            List<Achievement> list = new List<Achievement>();
            list = dbcontext.AchievementContext.Where(a => a.AchievementID == Id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().ProjectPeople != "")
                {
                    return list.FirstOrDefault().ProjectPeople;
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

        //根据多个人员ID找出多个成果ID
        public List<int> SelectIDlist(List<int> userid, int? level)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < userid.Count; i++)
            {
                int idd = userid[i];
                var list = dbcontext.StaffAchieveContext.Where(u => u.UserInfoID == idd && u.SecrecyLevel <= level && u.IsPass == true);
                foreach(StaffAchieve staff in list)
                {
                    res.Add(Convert.ToInt32(staff.AchievementID));
                }
            }
            return res;
        }
        //根据成果ID（AchievementID）查找完成人
        public List<int> FindByAchivementID(int AchivementID)
        {
            List<int> StaffAchievelist = new List<int>();
            var list = dbcontext.StaffAchieveContext.Where(u => u.IsPass == true && u.AchievementID == AchivementID && u.IsPass == true).ToList();
            if (list.Count() == 1)
            {
                StaffAchievelist.Add(Convert.ToInt32(list[0].AchievementID));
            }
            return StaffAchievelist;
        }
        //根据UserID找到只有一个完成人且为该UserID的AchieveMentID
        public List<int> FindByUserID(int UserID)
        {
            List<int> StaffAchievelist = new List<int>();
            var list = dbcontext.StaffAchieveContext.Where(u => u.IsPass == true && u.UserInfoID == UserID).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                List<int> lists = FindByAchivementID(Convert.ToInt32(list[i].AchievementID));
                for (int j = 0; j < lists.Count(); j++)
                {
                    StaffAchievelist.Add(lists[j]);
                }
            }
            return StaffAchievelist;
        }
        //根据UserID找到只有一个完成人且为该UserID的StaffAchieveMentID
        public List<int> FindByStaffAchiveID(int UserID)
        {
            List<int> StaffAchievelist = new List<int>();
            var list = dbcontext.StaffAchieveContext.Where(u => u.IsPass == true && u.UserInfoID == UserID).ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                List<int> lists = FindByAchivementID(Convert.ToInt32(list[i].StaffAchieveID));
                for (int j = 0; j < lists.Count(); j++)
                {
                    StaffAchievelist.Add(lists[j]);
                }
            }
            return StaffAchievelist;
        }
    }
}
