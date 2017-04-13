/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：合同表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月11日
 *           修改人：李金秋
 *           修改内容：添加根据用户级别查询合同信息FindBySecrecyLevel(int)
 *           3.时间：8月16日
 *           修改人：李金秋
 *           修改内容：添加根据登陆级别查询合同编号 FindPactNumBySecrecyLevel(int)
 *           4.时间：2015.11.28修改人：高琪 修改内容：增加更新方法update（）
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using DataBase;
using System.Text;
using Common.Entities;

namespace BLHelper
{
    public class BLLPact
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                Pact NewPact = dbcontext.PactContext.Find(ID);
                if (NewPact == null)
                    return;
                NewPact.IsPass = isPass;
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
                Pact NewPact = dbcontext.PactContext.Find(ID);
                NewPact.AttachmentID = null;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //插入合同信息
        public void Insert(Pact pact)
        {
            try
            {
                dbcontext.PactContext.Add(pact);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //根据pactID查看合同信息
        public Pact FindByPactID(int pactID)
        {
            return dbcontext.PactContext.Find(pactID);
        }
        //根据PactID删除合同信息
        public bool Delete(int pactID)
        {
            try
            {
                Pact pact = dbcontext.PactContext.Find(pactID);
                if (pact != null)
                {
                    dbcontext.PactContext.Attach(pact);
                    dbcontext.PactContext.Remove(pact);
                    dbcontext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
      
        public List<Pact> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.PactContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.PactID).ToList();
        }
       //根据用户级别查询合同信息
        public List<Pact> FindBySecrecyLevel(int SecrecyLevel)
        {
            return dbcontext.PactContext.Where(p => p.SecrecyLevel <= SecrecyLevel && p.IsPass == true).ToList();
        }
        //根据登陆级别查询合同编号
        public List<string> FindPactNumBySecrecyLevel(int SecrecyLevel)
        {
            return dbcontext.PactContext.Where(p => p.SecrecyLevel <= SecrecyLevel&&p.IsPass==true).Select(p => p.PactNum).ToList();
        }
        //根据合同编号查询合同信息
        public Pact FindByPactNum(string PactNum)
        {
            return dbcontext.PactContext.Where(p => p.PactNum == PactNum && p.IsPass == true).FirstOrDefault();
        }
       
        //查找附件id
        public int FindAttachmentID(int Pactid)
        {
            var Pact = dbcontext.PactContext.Find(Pactid);
            if (Pact != null)
            {
                if (Pact.AttachmentID != null)
                    return Convert.ToInt32(Pact.AttachmentID);
                else
                    return 0;
            }
            else
                return 0;
        }
        //模糊查询
        public List<Pact> FindPactList(string PactNum, int level)
        {
            return dbcontext.PactContext.Where(a => a.PactNum.Contains(PactNum) && a.SecrecyLevel <= level && a.IsPass == true).ToList();
        }
        //根据ProjectID找到PactID
        public List<int> FindPactIDList(int ProjectID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.PactContext.Where(u => u.ProjectID == ProjectID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].PactID ));
            }
            return list;
        }

        //根据所属项目查询
        public List<Pact> FindByProject(int projectid, int level)
        {
            return dbcontext.PactContext.Where(u => u.ProjectID == projectid && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据合同名称查询
        public List<Pact> FindByPactName(string pactname, int level)
        {
            return dbcontext.PactContext.Where(u => u.PactName == pactname && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据合同完成情况查询
        public List<Pact> FindByPactCompletion(string pactcomplete, int level)
        {
            return dbcontext.PactContext.Where(u => u.PactCompletion == pactcomplete && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
        //完成合同负责人查询
        public List<Pact> FindByChargePerson(string ChargePerson, int level)
        {
            return dbcontext.PactContext.Where(u => u.ChargePerson.Contains(ChargePerson) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }
        //查看合同信息
        public Pact FindAll(int id)
        {
            return dbcontext.PactContext.Find(id);
        }
        public bool update(Pact pacts)
        {
            try
            {
                Pact newPact = dbcontext.PactContext.Find(pacts.PactID);
                newPact.FileNum = pacts.FileNum;
                newPact.PactNum = pacts.PactNum;
                newPact.PactName = pacts.PactName;
                newPact.PactType = pacts.PactType;
                newPact.StartTime = pacts.StartTime;
                newPact.EndTime = pacts.EndTime;
                newPact.ProjectID = pacts.ProjectID;
                newPact.SecrecyLevel = pacts.SecrecyLevel;
                newPact.ChargePerson = pacts.ChargePerson;
                newPact.PactMoney = pacts.PactMoney;
                newPact.RealMoney = pacts.RealMoney;
                newPact.PactCompletion = pacts.PactCompletion;
                newPact.IsExistingFile = pacts.IsExistingFile;
                newPact.IsPass = pacts.IsPass;
                dbcontext.SaveChanges();
                return true;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }


    }
}
