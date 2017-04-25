/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：工作计划与总结表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月17日
 *           修改人：李金秋
 *           修改内容：添加根据WorkPlanSummaryID查询工作计划与总结信息 FindByWorkPlanSummaryID(int)
 *           3.时间：8月21日
 *           修改人：张凡凡
 *           修改内容：添加根据id查找附件id函数FindAttachmentid(int)
 **/

using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
   public class BLLWorkPlanSummary
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                WorkPlanSummary WorkPlanSummary = dbcontext.WorkPlanSummaryContext.Find(ID);
                if (WorkPlanSummary == null)
                    return;
                WorkPlanSummary.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

       public void Update(WorkPlanSummary work)
        {
            try
            {
                WorkPlanSummary WorkPlanSummary = dbcontext.WorkPlanSummaryContext.Find(work.WorkPlanSummaryID);
                if (WorkPlanSummary == null)
                    return;
                WorkPlanSummary.AgencyID = work.AgencyID;
                WorkPlanSummary.SecrecyLevel = work.SecrecyLevel;
                WorkPlanSummary.Sort=work.Sort;
                WorkPlanSummary.Time=work.Time;
                WorkPlanSummary.UserInfoID = work.UserInfoID;
                if (work.Attachment != -4)
                    WorkPlanSummary.Attachment = work.Attachment;
                dbcontext.SaveChanges();
            }
           catch(Exception ex)
            {
                throw;
            }
        }

        //插入工作计划与总结信息
        public void Insert(WorkPlanSummary workPlanSummary)
        {
            try
            {
                dbcontext.WorkPlanSummaryContext.Add(workPlanSummary);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据AWorkPlanSummaryID删除工作计划与总结信息
        public bool Delete(int aWorkPlanSummaryID)
        {
            try
            {
                //WorkPlanSummary aWorkPlanSummary = new WorkPlanSummary { WorkPlanSummaryID = aWorkPlanSummaryID };
                WorkPlanSummary aWorkPlanSummary = dbcontext.WorkPlanSummaryContext.Find(aWorkPlanSummaryID);
                if (aWorkPlanSummary != null)
                {
                    dbcontext.WorkPlanSummaryContext.Attach(aWorkPlanSummary);
                    dbcontext.WorkPlanSummaryContext.Remove(aWorkPlanSummary);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                //throw;
                return false;
            }
        }
      
        //根据AgencyID(机构ID)查询工作计划与总结
        public List<WorkPlanSummary> FindByAgencyID(int agencyID, int SecrecyLevel)
        {
            return dbcontext.WorkPlanSummaryContext.Where(p => p.AgencyID == agencyID&&p.SecrecyLevel<=SecrecyLevel && p.IsPass == true).ToList();
        }
        //根据UserInfoID(人员ID)查询工作计划与总结
        public List<WorkPlanSummary> FindByUserInfoID(int userInfoID, int SecrecyLevel)
        {
            return dbcontext.WorkPlanSummaryContext.Where(p => p.UserInfoID == userInfoID&&p.SecrecyLevel<=SecrecyLevel && p.IsPass == true).ToList();
        }
        //分页
        public List<WorkPlanSummary> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.WorkPlanSummaryContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.WorkPlanSummaryID).ToList();
        }
        //根据WorkPlanSummaryID查询工作计划与总结信息 FindByWorkPlanSummaryID(int)
        public WorkPlanSummary FindByWorkPlanSummaryID(int WorkPlanSummaryID)
        {
            return dbcontext.WorkPlanSummaryContext.Where(p => p.WorkPlanSummaryID == WorkPlanSummaryID).FirstOrDefault();
        }
        //获取所有工作计划与总结的年份
        public List<int> GetYear()
        {
            var q = (from c in dbcontext.WorkPlanSummaryContext
                     select c.Time.Value.Year).Distinct();
            return q.ToList();
        }
       //根据分类查询工作计划与总结
        public List<WorkPlanSummary> FindBySort(string sort, int SecrecyLevel)
        {
            return dbcontext.WorkPlanSummaryContext.Where(p => p.Sort == sort && p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
       //根据年份查找工作计划与总结
        public List<WorkPlanSummary> FindByYear(int year, int SecrecyLevel)
        {
            return dbcontext.WorkPlanSummaryContext.Where(p => p.Time.Value.Year == year&&p.SecrecyLevel<=SecrecyLevel&&p.IsPass==true).ToList();
        }

        public int FindAttachmentid(int projevtImportid)  
        {
            var proimport = dbcontext.WorkPlanSummaryContext.Find(projevtImportid);
            if (proimport != null)
            {
                if (proimport.Attachment.HasValue)
                    return proimport.Attachment.Value;
                else
                    return 0;
            }
            else
                return 0;
        }
        //根据UserInfoID找到WorkPlanSummaryID
        public List<int> FindWorkPlanSummaryIDList(int UserInfoID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.WorkPlanSummaryContext.Where(u => u.UserInfoID == UserInfoID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].WorkPlanSummaryID ));
            }
            return list;
        }
    }
}
