/**编写人：张凡凡
 * 时间：2014年6月20号
 * 功能：经费基本信息表的相关操作
 * 修改履历：1.7月23日，将SourceType改为SourceUnit
 *          2.7月29日，加隐秘级别SecrecyLevel
 *          3.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
   public class BLLFundInformation
    {
        DataBaseContext dbcontext = new DataBaseContext();

       //根据id查找相应信息
        public FundInformation FindByid(int fundid)
        {
            return dbcontext.FundInformationContext.Find(fundid);
        }
        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                FundInformation NewFundInformation = dbcontext.FundInformationContext.Find(ID);
                if (NewFundInformation == null)
                    return;
                NewFundInformation.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

         //根据ID删除经费信息
        public bool Delete(int ID)
        {
            try
            {
                FundInformation file = dbcontext.FundInformationContext.Where(u => u.FundInformationID == ID).FirstOrDefault();
                dbcontext.FundInformationContext.Attach(file);
                dbcontext.FundInformationContext.Remove(file);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //经费登记
        public void Insert(FundInformation FundInfo)
        {
            try
            {
                dbcontext.FundInformationContext.Add(FundInfo);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //设置经费比例
        public void Update(FundInformation Fund, string Propor)
        {
            try
            {
                FundInformation newFund = dbcontext.FundInformationContext.Find(Fund.FundInformationID);
                newFund.FundInformationID = Fund.FundInformationID;
                newFund.UserInfoID = Fund.UserInfoID;
                newFund.ProjectID = Fund.ProjectID;
                newFund.OperateType = Fund.OperateType;
                newFund.Proportion = Propor;
                newFund.Time = Fund.Time;
                newFund.FundingPurposeSortName  = Fund.FundingPurposeSortName ;
                newFund.EveItemUseMoney = Fund.EveItemUseMoney;
                newFund.BudgetDirector = Fund.BudgetDirector;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //设置提取经费类别和数量(被改了)
        public void UpdateByPM(FundInformation Fund, string  useid, string money)
        {
            try
            {
                FundInformation newFund = dbcontext.FundInformationContext.Find(Fund.FundInformationID);
                newFund.FundInformationID = Fund.FundInformationID;
                newFund.UserInfoID = Fund.UserInfoID;
                newFund.ProjectID = Fund.ProjectID;
                newFund.OperateType = Fund.OperateType;
                newFund.Proportion = Fund.Proportion;
                newFund.Time = Fund.Time;
                newFund.FundingPurposeSortName  = useid;
                newFund.EveItemUseMoney = money;
                newFund.BudgetDirector = Fund.BudgetDirector;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

   

        //分项目查看提取、进账、支出、结余经费
        public List<FundInformation> FindByPO(int ProjectID, string OperateType, int SecrecyLevel)
        {
            return dbcontext.FundInformationContext.Where(u => (u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true && u.ProjectID == ProjectID)).ToList();
        }

   

        //按项目统计进账、提取、支出经费
        public double Count(int Projectid, string OperateType, int SecrecyLevel)
        {
            List<FundInformation> Fund = dbcontext.FundInformationContext.Where(u => (u.ProjectID == Projectid && u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel)).ToList();
            FundInformation[] fund = Fund.ToArray();
            if (Fund.Count != 0)
            {
                double sum = Convert.ToDouble(fund[0].EveItemUseMoney);
                for (int i = 1; i < Fund.Count; i++)
                {
                    sum += Convert.ToDouble(fund[i].EveItemUseMoney);
                }
                return sum;
            }
            else
                return 0.0;
        }

        //分承担部门按项目负责人查看进账、结余经费
        public List<FundInformation> FindByAPO(string AgencyName, string OperateType, int SecrecyLevel)
        {
            //return dbcontext.FundInformationContext.Where(u => (u.ProjectID.AgencyID.AgencyName == AgencyName && u.ProjectID.ProjectHeads == ProjectHead && u.OperateType == OperateType)).ToList();
            List<FundInformation> Fund = dbcontext.FundInformationContext.Where(u => (u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel)).ToList();
            Fund.GroupBy(u => u.ProjectID);
            return Fund;
        }

      

     
        //按承担部门统计进账、结余经费
        public double CountByOperate(int projectid, string OperateType, int SecrecyLevel)
        {
            List<FundInformation> Fund = dbcontext.FundInformationContext.Where(u => (u.ProjectID == projectid && u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true)).ToList();
            FundInformation[] fund = Fund.ToArray();
            if (Fund.Count != 0)
            {
                double sum = Convert.ToDouble(fund[0].EveItemUseMoney);
                for (int i = 1; i < Fund.Count; i++)
                {
                    sum += Convert.ToDouble(fund[i].EveItemUseMoney);
                }
                return sum;
            }
            else
                return 0.0;
        }

        //分承担部门按项目查看提取、支出经费
        public List<FundInformation> FindByAPO(int Projectid, string OperateType, int SecrecyLevel)
        {
            List<FundInformation> Fund = new List<FundInformation>();
            if (Projectid == null || Projectid == 0)
                Fund = dbcontext.FundInformationContext.Where(u => (u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true)).OrderBy(u => u.ProjectID).ToList();
            else
                Fund = dbcontext.FundInformationContext.Where(u => u.ProjectID == Projectid && u.OperateType == OperateType && u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).ToList();
            return Fund;
        }
        /// <summary>
        /// 分科研人员查看负责项目经费情况
        /// </summary>
        /// <returns></returns>
        public List<FundInformation> FindM(int SecrecyLevel)
        {
            var results = from l in dbcontext.FundInformationContext
                          where l.SecrecyLevel <= SecrecyLevel
                          group l by l.BudgetDirector into lGroup
                          select new FundInformation { };
            return results.ToList();
        }
        //分页
        public List<FundInformation> FindPaged(int? SecrecyLevel, bool Ispass, string OperateType)
        {
            return dbcontext.FundInformationContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == Ispass && u.OperateType == OperateType).OrderBy(c => c.FundInformationID).ToList();
        }
        public List<int> FindFundIDlist(int projectID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.FundInformationContext .Where(a => a.ProjectID == projectID && a.IsPass == true).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].FundInformationID));
            }
            return list;
        }
    }
}
