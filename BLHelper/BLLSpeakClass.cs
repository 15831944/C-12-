/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:主讲课程表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *            2.时间：8月16号
 *           修改人:王会会
 *           修改内容：添加根据SpeakClassID查找EntryPerson的方法Find(int);
 *                    将根据ID和教学对象查询主讲课程信息和查看全部信息改为可分页的
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
   public class BLLSpeakClass
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                SpeakClass  speak = dbcontext.SpeakClassContext .Find(ID);
                if (speak == null)
                    return;
                speak.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //为某用户添加主讲课程
        public bool InsertForPeople(SpeakClass sc)
        {
            try
            {
                if (sc != null )
                {
                    dbcontext.SpeakClassContext.Add(sc);
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
        //根据SpeakClassID查找主讲课程信息
        public List<SpeakClass> FindSpeakClass(int SpeakClassID)
        {
            if (SpeakClassID != 0)
            {
                return dbcontext.SpeakClassContext.Where(t => t.SpeakClassID == SpeakClassID).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据人员ID查找主讲课程
        public List<SpeakClass> SelectByID(int UserInfoID)
        {
            if (UserInfoID != null)
            {
                return dbcontext.SpeakClassContext.Where(t => t.UserInfoID == UserInfoID && t.IsPass == true).ToList();
            }
            else
            {
                return null;
            }
        }
        //根据人员ID和教学对象查询授课情况
        public List<SpeakClass> FindByIT( int UserInfoID ,string TeachingDegree)
        {
            if (TeachingDegree != null)
            {
                return dbcontext.SpeakClassContext.Where(s => s.TeachingDegree == TeachingDegree &&s.UserInfoID ==UserInfoID  && s.IsPass==true).ToList();
            }
            else
            {
                return null;
            }
        }
        //更新用户主讲课程
        public bool Update(SpeakClass sc)
        {
            try
            {
                if (sc != null )
                {
                    SpeakClass newsc = dbcontext.SpeakClassContext.Find(sc.SpeakClassID);
                    newsc.UserInfoID = sc.UserInfoID;
                    newsc.ClassName = sc.ClassName;
                    newsc.Specialty = sc.Specialty;
                    //newsc.SpeakClassID = sc.SpeakClassID;
                    newsc.TeachingDegree = sc.TeachingDegree;
                    newsc.TeachingTime = sc.TeachingTime;
                    newsc.Grade = sc.Grade;
                    newsc.SecrecyLevel = sc.SecrecyLevel;
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
        //删除用户主讲课程
        public bool Delete(int cno)
        {
            try
            {

                SpeakClass sc = dbcontext.SpeakClassContext.Where(u => u.SpeakClassID == cno).FirstOrDefault();
                    dbcontext.SpeakClassContext.Attach(sc);
                    dbcontext.SpeakClassContext.Remove(sc);
                    dbcontext.SaveChanges();
                    return true;
             
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //分页
        public List<SpeakClass> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.SpeakClassContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
        }
        //添加根据SocialPartTimeID查找EntryPerson的方法
        public SpeakClass Find(int SpeakClassID)
        {
            return dbcontext.SpeakClassContext.Find(SpeakClassID);
        }
        //根据项目ID查找SpeakClassID
        public List<int> FindSpeakClassIDList(int UserID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.SpeakClassContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].SpeakClassID));
            }
            return list;
        }
    }
}
