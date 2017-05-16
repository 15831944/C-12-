/**编写人：王会会
 * 时间：2014年6月20号
 * 功能：项目基本信息表的相关操作
 * 修改履历：1.时间：8月14日
 *           修改人：李金秋
 *           修改内容：添加根据登陆用户级别查询项目FindBySecrecyLevel(int)
 *          2.时间：8月14日
 *           修改人：张凡凡
 *           修改内容：添加根据承担部门查找项目id函数FindIDlistByAU(string,int)
 *                      根据项目所属机构查寻项目id FindIDlistByAgency(int,int)
 *          3.时间：8月16号
 *          修改人：王会会
 *           修改内容：项目类型是项目级别,修改相应的方法分年份按项目类型查询FindTP(int, string, int),
 *                    分项目类型按年份查询FindPT(string , string , int )
 *          4.时间：8月18日
 *           修改人：张凡凡
 *           修改内容：添加根据来款单位找出项目id函数FindByGMUnits(string,int)
 *                      添加根据负责人查找项目ID函数FindIDListByPeople(string,int)
 *                      根据项目类型来查找项目id FindIDListByProlevel(string,int)
 *                      根据项目来源查找项目Id FindIDListBySource(string,int)
 *                      根据时间来查找项目 FindListByTime(int,int)
 *                      根据idlist查找项目list FindByIdList(List<int>)
 *           5.20150824 郝瑞 添加项目成员字段，以及针对其的相关功能
 *           6.20150923 郝瑞 添加函数FindPM
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Entity;
namespace BLHelper
{
    public class BLLProject
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //根据项目id查找项目
        public Project FindByid(int proid)
        {
            var restult = dbcontext.ProjectContext.Find(proid);
            return restult;
        }


        //按照项目负责人查询
        public List<Project> FindByProjectHeads(string projectHeads, int secrelevel)
        {
            var res = dbcontext.ProjectContext.Where(u => u.ProjectHeads == projectHeads && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
            return res;
        }

        //按照项目性质查询
        public List<Project> FindByProjectNature(string projectNature, int secrelevel)
        {
            var res = dbcontext.ProjectContext.Where(u => u.ProjectNature == projectNature && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
            return res;
        }

        //按照项目成员查询
        public List<Project> FindByProjectMember(string projectMember, int secrelevel)
        {
            var res = dbcontext.ProjectContext.Where(u => (u.ProjectMember.Contains(projectMember) ||u.ProjectManager.Contains(projectMember)) && u.SecrecyLevel <= secrelevel && u.IsPass == true).ToList();
            return res;
        }

        /// <summary>
        /// 录入横向(纵向，校内)信息，向项目基本表（Project）中插入数据【合同在合同表中录入】
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="apact"></param>
        public bool InsertProject(Project ap)
        {
            try
            {
                dbcontext.ProjectContext.Add(ap);
                dbcontext.SaveChanges();
                return true;
            }
            //catch (System.Data.SqlClient.SqlException e)
            //{
            //    throw e;
            //}
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }      
        //删除项目信息
        public void Delete(int projectid)
        {
            try
            {
                Project pro = dbcontext.ProjectContext.Where(u => u.ProjectID == projectid).FirstOrDefault();
                dbcontext.ProjectContext.Attach(pro);
                dbcontext.ProjectContext.Remove(pro);
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            { throw e; }

        }
        //将表中的审核状态变为False
        public bool ChangePass(int ProjectID, bool ispass)
        {
            try
            {
                Project project = dbcontext.ProjectContext.Find(ProjectID);
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
        /// <summary>
        /// 修改校内项目信息，纵向项目信息或横向项目信息，向项目基本信息表中修改信息
        /// </summary>
        /// <param name="ap"></param>
        /// <param name="avp"></param>
        /// <param name="aProjectID"></param>
        public void Update(Project ap)
        {
            try
            {                
                Project aproject = dbcontext.ProjectContext.Find(ap.ProjectID);
                aproject.ProjectName = ap.ProjectName;
                aproject.AgencyID = ap.AgencyID;
                aproject.AcceptUnit = ap.AcceptUnit;
                aproject.SourceUnit = ap.SourceUnit;
                aproject.ProjectSortName  = ap.ProjectSortName ;
                aproject.ProjectState = ap.ProjectState;
                aproject.ApprovedMoney = ap.ApprovedMoney;
                aproject.GetMoney = ap.GetMoney;
                aproject.CooperationForms = ap.CooperationForms;
                aproject.ProjectLevel = ap.ProjectLevel;
                aproject.ProjectHeads = ap.ProjectHeads;
                aproject.StartTime = ap.StartTime;
                aproject.EndTime = ap.EndTime;
                aproject.BenefitAttachment = ap.BenefitAttachment;
                aproject.BudgetAttachment = ap.BudgetAttachment;
                aproject.ExpecteResults = ap.ExpecteResults;
                aproject.GivenMoneyUnits = ap.GivenMoneyUnits;
                aproject.ProjectNature = ap.ProjectNature;
                aproject.Remark = ap.Remark;
                aproject.SecrecyLevel = ap.SecrecyLevel;
                aproject.ExpectEndTime = ap.ExpectEndTime;
                aproject.PactNum = ap.PactNum;
                aproject.TaskNum = ap.TaskNum;
                aproject.SecrecyLevel = ap.SecrecyLevel;
                aproject.IsPass = ap.IsPass;
                aproject.EntryPerson = ap.EntryPerson;
                aproject.ManageMoney = ap.ManageMoney;//管理费比例
                aproject.ProjectManager = ap.ProjectManager;//项目负责人（三个）
                aproject.ProjectInNum = ap.ProjectInNum;//项目内部编号(科技处)
                aproject.ProjectMember = ap.ProjectMember; //项目成员
                dbcontext.SaveChanges();
            }
            catch (System.Data.SqlClient.SqlException e)
            { } 
        }
        //查出全部信息
        public List<Project> FindAll()
        {
            return dbcontext.ProjectContext.Where(u => u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
        }
        //根据项目ID找效益附件ID
        public int FindBenefit(int ProjectID)
        {
            if (ProjectID !=0)
            {
                var results = dbcontext.ProjectContext.Where(a => a.ProjectID == ProjectID).Select(a => new { a.BenefitAttachment }).ToList();
                if (results.Count != 0)
                {
                    if (results.FirstOrDefault().BenefitAttachment.HasValue)
                    {
                        return results.FirstOrDefault().BenefitAttachment.Value;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }     
 
        }       
        //根据项目ID找经济预算附件ID
        public int FindBudget(int ProjectID)
        {
            if (ProjectID !=0)
            {
                var results = dbcontext.ProjectContext.Where(a => a.ProjectID == ProjectID).Select(a => new { a.BudgetAttachment  }).ToList();
                if (results.Count != 0)
                {
                    if (results.FirstOrDefault().BudgetAttachment.HasValue)
                        return results.FirstOrDefault().BudgetAttachment.Value;
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }

        }
       
        // 根据项目ID查找全部信息        
        public List<Project> FindProject(int aProjectID, int? secrecylevel)
        {
            bool Ispass = true;
            if (aProjectID != null && secrecylevel != null)
            {
                return dbcontext.ProjectContext.Where(z => z.ProjectID == aProjectID &&
                    z.SecrecyLevel <= secrecylevel && z.IsPass == Ispass).ToList();
            }
            else
                return null;
        }
        //根据项目名模糊查询项目ID
        public List<int> FindProjectList(string name, int? level)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.ProjectContext.Where(a => a.ProjectName.Contains(name) && a.SecrecyLevel <= level && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].ProjectID));
            }
            return list;
        }
      //根据项目ID查项目名称
        public string SelectProjectName(int ProjectID)
        {
            if (ProjectID != null)
            {
                var results = dbcontext.ProjectContext.Where(a => a.ProjectID == ProjectID).ToList();
                if (results.Count() != 0)
                {
                    return results.FirstOrDefault().ProjectName;
                }
                else
                {
                    return "";
                }
            }
            else
                return "";

        }
        //用项目名称查找项目ID
        public int SelectProjectID(string ProjectName)
        {
            if (!string.IsNullOrEmpty(ProjectName))
            {
                var results = dbcontext.ProjectContext.Where(a => a.ProjectName == ProjectName).Select(a => new { a.ProjectID }).ToList();
                if (results.Count() != 0)
                {
                    if (results.FirstOrDefault().ProjectID != 0)
                    {
                        return results.FirstOrDefault().ProjectID;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        //查找所有项目名
        public List<Project> FindALLName()
        {
            List<Project> list = new List<Project>();
            var newlist = dbcontext.ProjectContext.Where(a => a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(newlist[i]);
            }
            return list;
        }
       
        //项目名称是否已存在
        public Project  IsNullProject(string ProjectName)
        {
            return dbcontext.ProjectContext.Where (u=>u.ProjectName ==ProjectName ).FirstOrDefault ();           
        }       
       
        ////根据ProjecyID查找用户登录名
        //public string FindEntryPerson(int ProjectID)
        //{
        //    if (ProjectID != null)
        //    {
        //        var results = dbcontext.ProjectContext.Where(u => u.ProjectID == ProjectID && u.IsPass == true).Select(u => new { u.EntryPerson }).ToList();
        //        return results.FirstOrDefault().EntryPerson;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
        /// <summary>
        /// 分年份按项目来源查看项目信息 （） 
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>             
        public List<Project> FindYP(int Time, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = new List<Project>();
            if(projectnature==null)
            {
                res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year == Time && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year == Time
                && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            res.OrderBy(u => u.SourceUnit);
            return res;          
        }
      
        /// <summary>
        /// 分年份按项目类型查看 横向项目信息
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public List<Project> FindTP(int Time, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year == Time
                && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            res.OrderBy(u => u.ProjectLevel);
            return res;
        }
        /// <summary>
        /// 分年份按承接单位查看 项目信息
        /// </summary>
        /// <param name="Time"></param>
        /// <returns></returns>
        public List<Project> FindACU(int Time, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = new List<Project>();
            if(projectnature==null)
            {
                res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year == Time
                && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year == Time
                && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            res.OrderBy(u => u.AcceptUnit);
            return res;
        }
        /// <summary>
        /// 分项目来源按年份查看项目信息（模糊查询）
        /// </summary>
        /// <param name="sourceunit"></param>
        /// <returns></returns>
        public List<Project> FindSU(string sourceunit, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = new List<Project>();
            if(projectnature==null)
            {
                res = dbcontext.ProjectContext.Where(u => u.SourceUnit.Contains(sourceunit)
                && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.SourceUnit.Contains(sourceunit)
                && u.SecrecyLevel <= secrecylevel && u.ProjectNature == projectnature && u.IsPass == Ispass).ToList();
            }
            res.OrderBy(u => u.StartTime.Value.Year);
            return res;
        }
       
        /// <summary>
        ///  分项目类型按年份查看 横向项目信息
        /// </summary>
        /// <param name="projecttype"></param>
        /// <returns></returns>
        public List<Project> FindPT(string ProjectLevel, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = new List<Project>();
            if(projectnature==null)
            {
                res = dbcontext.ProjectContext.Where(u => u.ProjectLevel == ProjectLevel
                && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.ProjectLevel == ProjectLevel
                && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            res.OrderBy(u => u.StartTime.Value.Year);
            return res;       
        }
        /// <summary>
        /// 分承接部门按年份 查询项目信息（模糊查询）
        /// </summary>
        /// <param name="acceptunit"></param>
        /// <returns></returns>
        public List<Project> FindTime(string acceptunit, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
            List<Project> res = new List<Project>();
            if (projectnature == null)
            {
                res = dbcontext.ProjectContext.Where(u => u.AcceptUnit.Contains(acceptunit)
                && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.AcceptUnit.Contains(acceptunit)
                && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            res.OrderBy(u => u.StartTime.Value.Year);
            return res;      
        }
        /// <summary>
        /// 分承接部门按负责人查询项目信息（模糊查询）
        /// </summary>
        /// <param name="acceptunit"></param>
        /// <returns></returns>
        public List<Project> FindPH(string acceptunit, string projectnature, int secrecylevel)
        {
            bool Ispass = true;
             List<Project> res = new List<Project>();
            if (projectnature == null)
            {
               res = dbcontext.ProjectContext.Where(u => u.AcceptUnit.Contains(acceptunit)
               && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
            else
            {
               res = dbcontext.ProjectContext.Where(u => u.AcceptUnit.Contains(acceptunit)
               && u.ProjectNature == projectnature && u.SecrecyLevel <= secrecylevel && u.IsPass == Ispass).ToList();
            }
           
            res.OrderBy(u => u.ProjectHeads);
            return res;
        }
        //按保密等级查询
        public List<Project> FindTA(int SecrecyLevel,string projectnature,int secrecylevel)
        {
            List<Project> res = new List<Project>();
            if (projectnature == null)
            {
                res = dbcontext.ProjectContext.Where(u => u.SecrecyLevel == SecrecyLevel &&
               u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
            }
            else
            {
                res = dbcontext.ProjectContext.Where(u => u.SecrecyLevel == SecrecyLevel && u.ProjectNature == projectnature &&
               u.SecrecyLevel <= secrecylevel && u.IsPass == true).ToList();
            }
            res.OrderBy(u => u.ProjectHeads);
            return res;
        }
        //科技研发 查询已完成或在研项目
        public List<Project> FindCS(string projectstate, int secrecylevel)
        {
            return dbcontext.ProjectContext.Where(u => u.ProjectState == projectstate && u.SecrecyLevel <= secrecylevel).ToList();

        }
        //分科研人员查看其负责项目经费情况(查出项目表中项目经费ApproveMoney,到账经费GetMoney,经费预算AttachmentID 和项目效益相同)
        public List<Project> FindFund(string userinfo, int secrecylevel)
        {
            var results = from l in dbcontext.ProjectContext
                          where l.ProjectHeads == userinfo && l.SecrecyLevel <= secrecylevel
                          select new Project
                          {
                              ApprovedMoney = l.ApprovedMoney,
                              GetMoney = l.GetMoney,
                          };
            return results.ToList();
        }
        //分页
        public List<Project> FindPaged(int SecrecyLevel)
        {
            return dbcontext.ProjectContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true)
                .OrderBy(u => u.ProjectID).ToList();
        }      
        //根据登陆用户级别查询项目
       public List<Common.Entities.Project> FindBySecrecyLevel(int SecrecyLevel)
       {
           return dbcontext.ProjectContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
       }

        //根据承担部门查找项目id
       public List<int> FindIDlistByAU(string AcceptUnit, int SecrecyLevel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.AcceptUnit.Contains(AcceptUnit) && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }

        //根据项目所属机构查询项目id
       public List<int> FindIDlistByAgency(int AgencyId, int Secrecylevel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.AgencyID == AgencyId && u.SecrecyLevel <= Secrecylevel && u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }

        //根据来款单位找出项目id
       public List<int> FindIDlistByGMUnits(string GivenMoneyUnit, int secrecylecel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.GivenMoneyUnits.Contains(GivenMoneyUnit) && u.SecrecyLevel <= secrecylecel && u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }

        //根据负责人查找项目ID
       public List<int> FindIDListByPeople(string people, int secrecylecel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.ProjectHeads.Contains(people) && u.SecrecyLevel <= secrecylecel && u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }

        //根据项目id查找负责人
       public string FindPeoById(int Projectid)
       {
           string res = dbcontext.ProjectContext.Find(Projectid).ProjectHeads;
           return res;
       }

        //根据项目来源查找项目Id
       public List<int> FindIDListBySource(string source, int secrecylevel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.SourceUnit.Contains(source) && u.SecrecyLevel <= secrecylevel && u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }

        //根据项目类型来查找项目id
       public List<int> FindIDListByProlevel(string prolevel, int secrecylevel)
       {
           List<int> res = new List<int>();
           var list = dbcontext.ProjectContext.Where(u => u.ProjectLevel.Contains(prolevel) && u.SecrecyLevel <= secrecylevel && u.IsPass == true).OrderBy(u => u.ProjectID).ToList();
           for (int i = 0; i < list.Count; i++)
               res.Add(list[i].ProjectID);
           return res;
       }
       //根据ID查询备注
       public string FindRemark(int id)
       {
           List<Project> list = new List<Project>();
           list = dbcontext.ProjectContext .Where(a => a.ProjectID  == id).ToList();
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

        //根据id查询成员
       public string FindMember(int id)
       {
           List<Project> list = new List<Project>();
           list = dbcontext.ProjectContext.Where(a => a.ProjectID == id).ToList();
           if (list != null)
           {
               if (list.FirstOrDefault().Remark != "")
               {
                   return list.FirstOrDefault().ProjectMember;
               }
               else
               {
                   return list.FirstOrDefault().ProjectMember;
               }
           }
           else
           {
               return "";
           }
       }

        //根据结束来查找项目
       public List<Project> FindListByTime(int Time, int secrelevel)
       {
           var res = dbcontext.ProjectContext.Where(u => u.StartTime.Value.Year <= Time && u.SecrecyLevel <= secrelevel && u.IsPass == true).OrderBy(u => u.StartTime.Value).ToList();
           return res;
       }

        //根据idlist查找项目list
       public List<Project> FindByIdList(List<int> ProID)
       {
           List<Project> res = new List<Project>();
           for (int i = 0; i < ProID.Count; i++)
           {
               int Projectid = ProID[i];
               res.Add(dbcontext.ProjectContext.Where(u => u.IsPass == true && u.ProjectID == Projectid).FirstOrDefault());
           }
           return res;
       }

        //分承担部门按项目负责人
       public List<Project> FindByAgPeo(string Agen, string peo, int secre)
       {
           var res = dbcontext.ProjectContext.Where(u => u.AcceptUnit == Agen && u.ProjectHeads == peo && u.SecrecyLevel <= secre && u.IsPass == true).OrderBy(u => u.ProjectHeads).ToList();
           return res;
       }

       //按项目经理查询
       public List<Project> FindPM(string pm, int secrecylevel)
       {
           return dbcontext.ProjectContext.Where(u => u.ProjectManager.Contains(pm) && u.SecrecyLevel <= secrecylevel).ToList();

       }

    }
}

