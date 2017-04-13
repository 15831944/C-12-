/**编写人：方淑云
 * 时间：2014年6月20号FindByContractID
 * 功能：借阅记录表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加更新IsPass状态函数UpdateIsPass(int,bool)
 *           2.时间：8月15日
 *           修改人：李金秋
 *           修改内容：添加根据资料或合同编号和分类查询资料或合同名称 FindByContractIDAndSort(int,string)
 *           3.时间：8月15日
 *           修改人：李金秋
 *           修改内容：添加根据LibreryRecordID查询借阅记录 FindByLibreryRecordID(int)BorrowPeopel
 *           4.时间：8月15日
 *           修改人：李金秋
 *           修改内容：添加借阅人查询借阅信息 FindByBorrowPeopel(string)
 *           5.时间：8月15日
 *           修改人：李金秋
 *           修改内容：添加插入借阅记录 Insert()
 *           6.时间：8月16日
 *           修改人：李金秋
 *           修改内容：添加根据资料ID和登陆名查询借阅信息 FindByContractID(int)
 *           7.时间：8月21日
 *           修改人：李金秋
 *           修改内容：添加更新借阅记录 Update(LibraryRecord)
 *           8时间2015.12.2 修改人高琪 修改履历：去掉删除中的dbcontext.SaveChanges();
 *           
 *           
 *            
 * **/
using System;
using DataBase;
using Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLLibraryRecord
    {
        ////记录资料与合同档案的借阅归还和借阅查询 (增减统计、借阅统计)

        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                LibraryRecord NewLibraryRecord = dbcontext.LibraryRecordContext.Find(ID);
                if (NewLibraryRecord == null)
                    return;
                NewLibraryRecord.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        public void Insert(LibraryRecord libraryRecord)
        {
            try
            {
                dbcontext.LibraryRecordContext.Add(libraryRecord);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //删除
        public bool Delete(int ID)
        {
            try
            {

                LibraryRecord LibraryRecord = new LibraryRecord { LibraryRecordID = ID };
                dbcontext.LibraryRecordContext.Attach(LibraryRecord);
                dbcontext.LibraryRecordContext.Remove(LibraryRecord);
                //dbcontext.SaveChanges();
                return true;

            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }
        }
        //更新借阅记录 
        public bool Update(LibraryRecord libraryRecord)
        {
            try
            {
                LibraryRecord NewLibraryRecord = dbcontext.LibraryRecordContext.Find(libraryRecord.LibraryRecordID);
                NewLibraryRecord.BorrowTime = libraryRecord.BorrowTime;
                NewLibraryRecord.ContractID = libraryRecord.ContractID;
                NewLibraryRecord.EntryPerson = libraryRecord.EntryPerson;
                NewLibraryRecord.IsPass = libraryRecord.IsPass;
                NewLibraryRecord.ReturnTime = libraryRecord.ReturnTime;
                NewLibraryRecord.SecrecyLevel = libraryRecord.SecrecyLevel;
                NewLibraryRecord.Sort = libraryRecord.Sort;
                NewLibraryRecord.UserInfoID = libraryRecord.UserInfoID;
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<LibraryRecord> FindPaged(int? SecrecyLevel)
        {
            return dbcontext.LibraryRecordContext.Where(u => u.SecrecyLevel <= SecrecyLevel && u.IsPass == true).OrderBy(c => c.LibraryRecordID).ToList();
        }
        //根据资料或合同编号和分类查询资料或合同名称
        public string FindByContractIDAndSort(int? ContractID, string Sort)
        {
            try
            {
                if (Sort == "资料")
                    return dbcontext.ContractContext.Where(p => p.ContractID == ContractID && p.IsPass == true).FirstOrDefault().ContractHeadLine;
                else if (Sort == "合同")
                    return dbcontext.PactContext.Where(p => p.PactID == ContractID && p.IsPass == true).FirstOrDefault().PactNum;
                else
                    return " ";
            }
            catch
            {
                return " ";
            }
        }
        //根据LibreryRecordID查询借阅记录 
        public LibraryRecord FindByLibreryRecordID(int LibraryRecordID)
        {
            return dbcontext.LibraryRecordContext.Find(LibraryRecordID);
        }
        //根据分类借阅人查询借阅信息 
        public List<LibraryRecord> FindByBorrowPeopel(int UserInfoID,string Sort)
        {
            return dbcontext.LibraryRecordContext.Where(p => p.UserInfoID == UserInfoID &&p.Sort==Sort&& p.IsPass == true).ToList();
        }
        //管理员所有借阅信息
        public List<LibraryRecord> FindAll(string Sort)
        {
            return dbcontext.LibraryRecordContext.Where(p => p.Sort == Sort).ToList();
        }
        
        //按用户名查询借阅记录
        public List<LibraryRecord> FindByContractID(int ContractID, string UserName,string sort)
        {
            int UserID = dbcontext.UserInfoContext.Where(p => p.UserName == UserName).FirstOrDefault().UserInfoID;
            return dbcontext.LibraryRecordContext.Where(p => p.ContractID == ContractID && p.Sort == sort && p.UserInfoID == UserID && p.IsPass == true).ToList();
        }
        //(管理员)根据资料名称查询借阅记录情况
        public List<LibraryRecord> FindByContractHeadLine(string ContractHeadLine)
        {
            //资料名称可有重复
            //查询资料ID
            List<int> listContractID = dbcontext.ContractContext.Where(p => p.ContractHeadLine == ContractHeadLine && p.IsPass == true).Select(p => p.ContractID).ToList();
            List<LibraryRecord> LibraryList = new List<LibraryRecord>();
            //存放查阅记录
            List<LibraryRecord> list = new List<LibraryRecord>();
            for (int i = 0; i < listContractID.Count(); i++)
            {
                int ContractID = listContractID[i];
                list = dbcontext.LibraryRecordContext.Where(p => p.ContractID == ContractID && p.Sort == "资料" && p.IsPass == true).ToList();
                for (int j = 0; j < list.Count(); j++)
                    LibraryList.Add(list[j]);
            }
            return LibraryList;
        }
        //(管理员)根据合同编号查询借阅记录情况
        public List<LibraryRecord> FindByPactNum(string PactNum)
        {
            //资料名称可有重复
            //查询资料ID
            List<int> listPactID = dbcontext.PactContext.Where(p => p.PactNum == PactNum && p.IsPass == true).Select(p => p.PactID).ToList();
            List<LibraryRecord> LibraryList = new List<LibraryRecord>();
            //存放查阅记录
            List<LibraryRecord> list = new List<LibraryRecord>();
            for (int i = 0; i < listPactID.Count(); i++)
            {
                int PactID = listPactID[i];
                list = dbcontext.LibraryRecordContext.Where(p => p.ContractID == PactID && p.Sort == "合同" && p.IsPass == true).ToList();
                for (int j = 0; j < list.Count(); j++)
                    LibraryList.Add(list[j]);
            }
            return LibraryList;
        }
        //个人根据资料名称查询借阅记录
        public List<LibraryRecord> FindRecordByContractHeadLine(string ContractHeadLine,int UserID)
        {
            //资料名称可有重复
            //查询资料ID
            List<int> listContractID = dbcontext.ContractContext.Where(p => p.ContractHeadLine == ContractHeadLine && p.IsPass == true).Select(p => p.ContractID).ToList();
            List<LibraryRecord> LibraryList = new List<LibraryRecord>();
            //存放查阅记录
            List<LibraryRecord> list = new List<LibraryRecord>();
            for (int i = 0; i < listContractID.Count(); i++)
            {
                int ContractID = listContractID[i];
                list = dbcontext.LibraryRecordContext.Where(p => p.ContractID == ContractID && p.Sort == "资料" && p.UserInfoID == UserID && p.IsPass == true).ToList();
                for (int j = 0; j < list.Count(); j++)
                    LibraryList.Add(list[j]);
            }
            return LibraryList;
        }
        //个人根据合同编号查询借阅记录
        public List<LibraryRecord> FindRecordByPactNum(string PactNum, int UserID)
        {
            //资料名称可有重复
            try
            {
                //查询资料ID
                List<int> listPactID = dbcontext.PactContext.Where(p => p.PactNum == PactNum && p.IsPass == true).Select(p => p.PactID).ToList();
                List<LibraryRecord> LibraryList = new List<LibraryRecord>();
                //存放查阅记录
                List<LibraryRecord> list = new List<LibraryRecord>();
                for (int i = 0; i < listPactID.Count(); i++)
                {
                    int PactID = listPactID[i];
                    list = dbcontext.LibraryRecordContext.Where(p => p.ContractID == PactID && p.Sort == "合同" && p.UserInfoID == UserID && p.IsPass == true).ToList();
                    for (int j = 0; j < list.Count(); j++)
                        LibraryList.Add(list[j]);
                }
                return LibraryList;
            }
            catch
            {
                return null;
            }
        }
        //根据ContractID和Sort查询借阅记录ID
        public List<int> FindLibraryID(int ContractID, string Sort)
        {
            return dbcontext.LibraryRecordContext.Where(p => p.ContractID == ContractID && p.Sort == Sort && p.IsPass == true).Select(p => p.LibraryRecordID).ToList();
        }
        //根据UserInfoID找到UserInfoID
        public List<int> FindUserInfoIDList(int UserInfoID)
        {
            List<int> list = new List<int>();
            var newlist = dbcontext.LibraryRecordContext.Where(u => u.UserInfoID == UserInfoID).ToList();
            for (int i = 0; i < newlist.Count(); i++)
            {
                list.Add(Convert.ToInt32(newlist[i].UserInfoID));
            }
            return list;
        }
    }
}
