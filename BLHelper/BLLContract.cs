/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：资料表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月13日
 *           修改人：李金秋
 *           修改内容：添加资料数量函数Count()
 *           3.时间：8月13日
 *           修改人：李金秋
 *           修改内容：添加根据资料名称查询资料信息 FindByContractHeadLine
 *           4.时间：8月13日
 *           修改人：李金秋
 *           修改内容：添加根据登陆级别查询资料名称 FindContractHeadLineBySecrecyLevel
 **/

using System;
using DataBase;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
   public class BLLContract
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Contract NewContract = dbcontext.ContractContext.Find(ID);
                if (NewContract == null)
                    return;
                NewContract.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //删除附件
        public void UpdateAttachment(int ID)
        {
            try
            {
                Contract NewContract = dbcontext.ContractContext.Find(ID);
                NewContract.AttachmentID = null;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //更新
        public void Update(Contract contract)
        {
            try
            {
                Contract newcontract = dbcontext.ContractContext.Find(contract.ContractID);
                newcontract.ContractHeadLine = contract.ContractHeadLine;
                newcontract.ContractAuthors = contract.ContractAuthors;
                newcontract.ContractOriginal = contract.ContractOriginal;
                newcontract.SecrecyLevel = contract.SecrecyLevel;
                dbcontext.SaveChanges();
                
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //插入资料信息
        public void Insert(Contract constract)
        {
            try
            {
                dbcontext.ContractContext.Add(constract);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据ContractID查看资料信息
        public Contract FindByContractID(int? contractID)
        {
            return dbcontext.ContractContext.Find(contractID);
        }
        
        //根据ContractID删除资料信息
        public bool Delete(int contractID)
        {
            try
            {
                //Contract contract = new Contract { ContractID = contractID };
                Contract contract = dbcontext.ContractContext.Find(contractID);
                dbcontext.ContractContext.Attach(contract);
                dbcontext.ContractContext.Remove(contract);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
       

        //根据ContractID查询AttachmentID(附件ID)
        public int FindAttachmentID(int contractID)
        {
            var contract = dbcontext.ContractContext.Find(contractID);
            if (contract != null)
            {
                if (contract.AttachmentID != null)
                    return Convert.ToInt32(contract.AttachmentID);
                else
                    return 0;
            }
            else
                return 0;
        }
 
        public List<Contract> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.ContractContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.ContractID).ToList();
        }
       //查询显示的资料数量
        public int Count(int? SecrecyLevel)
        {
            return dbcontext.ContractContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).Count();
        }
       //根据资料名称查询资料信息 FindByContractHeadLine
        public List<Contract> FindByContractHeadLine(string ContractHeadLine,int SecrecyLevel)
        {
            return dbcontext.ContractContext.Where(a => a.ContractHeadLine.Contains(ContractHeadLine) && a.SecrecyLevel <= SecrecyLevel && a.IsPass == true).ToList();
       }
        //根据登陆级别查询资料名称 FindContractHeadLineBySecrecyLevel
        public List<string> FindContractHeadLineBySecrecyLevel(int SecrecyLevel)
        {
            return dbcontext.ContractContext.Where(p => p.SecrecyLevel <= SecrecyLevel&&p.IsPass==true).Select(p => p.ContractHeadLine).ToList();
        }
       //根据ContractID判断该资料是否存在
        public bool IsExit(int ContractID)
        {
            Contract contract = dbcontext.ContractContext.Find(ContractID);
            if (contract != null)
                return true;
            else
                return false;
        }

    }
}
