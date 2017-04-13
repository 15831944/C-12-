/**编写人：王会会
 * 时间：2014年6月20号
 * 功能：项目重大节点表的相关操作
 * 修改履历：
 **/
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Entities;
namespace BLHelper
{
    public class BLLProjectImportantNode
    {
        DataBaseContext dbcontext = new DataBaseContext();
        /// <summary>
        /// 录入重要时间节点机任务，向项目重大节点表中插入数据（包括暂停项目，中止项目，验收项目）
        /// </summary>
        /// <param name="important"></param>
        public void insert(ProjectImportantNode important)
        {
            try
            {
                dbcontext.ProjectImportantNodeContext.Add(important);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            { throw e; }
        }
        //删除重大节点表的一条信息
        public bool Delete(int projectimportantnodeid)
        {
            if (projectimportantnodeid != 0)
            {
                ProjectImportantNode pi = dbcontext.ProjectImportantNodeContext.Where(u => u.ProjectImportantNodeID == projectimportantnodeid).FirstOrDefault();
                dbcontext.ProjectImportantNodeContext.Attach(pi);
                dbcontext.ProjectImportantNodeContext.Remove(pi);
                dbcontext.SaveChanges();
                return true;
            }
            else
            { return false; }

        }
        //将表中的审核状态变为False
        public bool ChangePass(int ProjectID, bool ispass)
        {
            try
            {
                ProjectImportantNode  project = dbcontext.ProjectImportantNodeContext .Find(ProjectID);
                if (project == null)
                    return false;
                project.IsPass = ispass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //修改重大节点表的信息
        public void Update(ProjectImportantNode pin)
        {
            try
            {
                ProjectImportantNode npin = dbcontext.ProjectImportantNodeContext.Find(pin.ProjectImportantNodeID);
                npin.MissionName = pin.MissionName;
                //npin.Time = pin.Time;
                npin.ProjectID = pin.ProjectID;
                npin.Remark = pin.Remark;
                npin.SecrecyLevel = pin.SecrecyLevel;
                npin.CompleteSpecificPerson = pin.CompleteSpecificPerson;
                npin.EndTime = pin.EndTime;
                npin.PersonCharge = pin.PersonCharge;
                npin.ActualComleption = pin.ActualComleption;
                npin.ProjectCompletion = pin.ProjectCompletion;
                npin.ResearchCharge = pin.ResearchCharge;
                npin.StartTime = pin.StartTime;
                
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //查询项目重大节点的全部信息
        public List<ProjectImportantNode> FindAll(int secrecylevel)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
        }
        public int FindAllCount(int? secrecylevel)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.SecrecyLevel <= secrecylevel && u.IsPass == true).Count();
        }
        //根据节点名称和时间和录入人查找ProjectImportantID
        public int FindImportantID(string Misson, DateTime time, string loginname)
        {
            if (!string.IsNullOrEmpty(Misson) && !string.IsNullOrEmpty(loginname) && !string.IsNullOrEmpty(time.ToString()))
            {
                var results = dbcontext.ProjectImportantNodeContext.Where(u => u.MissionName == Misson && u.StartTime == time && u.EntryPerson == loginname)
                    .Select(u => new { u.ProjectImportantNodeID }).ToList();
                return results.FirstOrDefault().ProjectImportantNodeID;
            }
            else
                return 0;
        }
        //根据项目ID查看项目重大节点表的信息
        public List<ProjectImportantNode> Findidpin(int projectid, int secrecylevel)
        {
            bool Ispass = true;
            return dbcontext.ProjectImportantNodeContext.Where(l => l.ProjectID == projectid 
                && l.SecrecyLevel <= secrecylevel && l.IsPass == Ispass).ToList();
        }
       //根据项目重大节点ID查找项目重大节点信息
        public ProjectImportantNode FindProjectImportant(int projectImportantID, bool ispass)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.ProjectImportantNodeID == projectImportantID && u.IsPass == ispass).FirstOrDefault();
        }
        //根据ProjecyID查找用户登录名
        public string FindEntryPerson(int ProjectImportantNodeID)
        {
            if (ProjectImportantNodeID != 0)
            {
                var results = dbcontext.ProjectImportantNodeContext.Where(u => u.ProjectImportantNodeID == ProjectImportantNodeID && u.IsPass == true).Select(u => new { u.EntryPerson }).ToList();
                return results.FirstOrDefault().EntryPerson;
            }
            else
            {
                return null;
            }

        }
        /// <summary>
        /// 分项目按时间节点查询项目信息（完成情况，拖期情况等）
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public List<ProjectImportantNode> FindTime(int projectID,int secrecylevel)
        {
            bool Ispass = true;
            List<ProjectImportantNode> res = dbcontext.ProjectImportantNodeContext.Where(u => u.ProjectID == projectID
                && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            res.OrderBy(u => u.StartTime);
            return res;
        }
       
        //根据ID查询备注
        public string FindRemark(int id)
        {
            List<ProjectImportantNode> list = new List<ProjectImportantNode>();
            list = dbcontext.ProjectImportantNodeContext .Where(a => a.ProjectImportantNodeID  == id).ToList();
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
        //根据ID查询节点名称
        public string FindMissionName(int id)
        {
            List<ProjectImportantNode> list = new List<ProjectImportantNode>();
            list = dbcontext.ProjectImportantNodeContext.Where(a => a.ProjectImportantNodeID == id).ToList();
            if (list != null)
            {
                if (list.FirstOrDefault().Remark != "")
                {
                    return list.FirstOrDefault().MissionName;
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
        //根据项目ID查找ImprotID
        public List<int> FindImprotIDList(int projectID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.ProjectImportantNodeContext .Where(a => a.ProjectID == projectID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].ProjectImportantNodeID ));
            }
            return list;
        }

        //根据负责人查询项目重要节点信息
        public List<ProjectImportantNode> FindByPersonCharge(string person, int level)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.PersonCharge == person && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
        //根据完成情况查询项目重要节点信息
        public List<ProjectImportantNode> FindByActualComleption(string actul, int level)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.ActualComleption == actul && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
        //根据项目完成情况查询项目重要节点信息
        public List<ProjectImportantNode> FindByProjectCompletion(string project, int level)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.ProjectCompletion == project && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据机构查询项目重要节点信息
        public List<ProjectImportantNode> FindByResearchCharge(string research, int level)
        {
            return dbcontext.ProjectImportantNodeContext.Where(u => u.ResearchCharge == research && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
    }
}
