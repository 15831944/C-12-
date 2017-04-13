/**编写人：方淑云
 * 时间：2014年6月20号
 * 功能:教育经历表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *            2.时间：8月16号
 *           修改人:王会会
 *           修改内容：添加根据EduExperienceID查找EntryPerson的方法Find(int);
 *                    将按ID查询教育经历信息和查询全部信息改为可分页的
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
   public class BLLEduExperience
    {
       DataBaseContext dbcontext = new DataBaseContext();

       //更新IsPass状态
       public void UpdateIsPass(int ID, bool isPass)
       {
           try
           {
               EduExperience NewEduExperience = dbcontext.EduExperienceContext.Find(ID);
               if (NewEduExperience == null)
                   return;
               NewEduExperience.IsPass = isPass;
               dbcontext.SaveChanges();
           }
           catch
           {
               throw;
           }
       }

       //为某用户添加教育经历
       public bool InsertForPeople(EduExperience edex)
       {
           try
           {
               if (edex != null )
               {
                  
                   dbcontext.EduExperienceContext.Add(edex);
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
      
   
       //根据人员ID查找教育经历
       public List<EduExperience> SelectByID(int UserInfoID,int? SecrecyLevel)
       {
           if (UserInfoID != 0 && SecrecyLevel != null)
           {
               return dbcontext.EduExperienceContext.Where(e => e.UserInfoID == UserInfoID && e.SecrecyLevel <= SecrecyLevel && e.IsPass == true).ToList();
           }
           else
           {
               return null;
           }
       }
       //更新用户教育经历
       public bool Update(EduExperience edex)
       {
           try
           {
               if (edex != null)
               {
                   EduExperience newedex = dbcontext.EduExperienceContext.Find(edex.EduExperienceID);
                   newedex.EduExperienceID = edex.EduExperienceID;
                   newedex.UserInfoID = edex.UserInfoID;
                   newedex.StartTime = edex.StartTime;
                   newedex.EndTime = edex.EndTime;
                   newedex.Major = edex.Major;
                   newedex.EHoldOffice = edex.EHoldOffice;
                   newedex.SecrecyLevel = edex.SecrecyLevel;
                   newedex.Remark = edex.Remark;
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
       //删除用户教育经历
       public bool Delete(int EduExperienceID)
       {
           try
           {

               EduExperience edex = dbcontext.EduExperienceContext.Where(u => u.EduExperienceID == EduExperienceID).FirstOrDefault();
                   dbcontext.EduExperienceContext.Attach(edex);
                   dbcontext.EduExperienceContext.Remove(edex);
                   dbcontext.SaveChanges();
                   return true;
              
           }
           catch (System.Data.SqlClient.SqlException e)
           {
               throw e;
           }
       }
       //分页（根据等级查找用户教育经历）
       public List<EduExperience> FindPaged(int? SecrecyLevel)
       {
           return dbcontext.EduExperienceContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
       }
       //添加根据EduExperienceID查找EntryPerson的方法
       public EduExperience Find(int EduExperienceID)
       {
           return dbcontext.EduExperienceContext.Find(EduExperienceID);
       }
       //根据EducationID查找学历信息
       public List<EduExperience> FindEduExperienceID(int EduExperienceID)
       {
           if (EduExperienceID != 0)
           {
               var results = dbcontext.EduExperienceContext .Where(u => u.EduExperienceID == EduExperienceID && u.IsPass == true).ToList();
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
           List<EduExperience > list = new List<EduExperience >();
           list = dbcontext.EduExperienceContext .Where(a => a.EduExperienceID  == id).ToList();
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
       //根据人员ID查找EduExperienceID
       public List<int> FindEduExperienceIDList(int UserID)
       {
           List<int> list = new List<int>();
           var newlist = dbcontext.EduExperienceContext.Where(a => a.UserInfoID == UserID && a.IsPass == true).ToList();
           for (int i = 0; i < newlist.Count(); i++)
           {
               list.Add(Convert.ToInt32(newlist[i].EduExperienceID));
           }
           return list;
       }
    }
}
