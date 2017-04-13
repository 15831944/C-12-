/**编写人：李金秋
 * 时间：2014年7月28号
 * 功能：操作日志表的相关操作
 * 修改履历：1.时间：8月11日
 *           修改人：张凡凡
 *           修改内容：添加根据Id查找操作日志信息函数FindbyId(int)
 **/
using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Data.Objects;

namespace BLHelper
{
   public class BLLOperationLog
    {
        DataBaseContext dbcontext = new DataBaseContext();

        //更新IsPass状态
        public void UpdateIsPass(int ID, bool isPass)
        {
            try
            {
                OperationLog Newop = dbcontext.OperationLogContext.Find(ID);
                if (Newop == null)
                    return;
                Newop.IsPass = isPass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

      // 根据Id查找操作日志信息
        public OperationLog FindbyId(int ID)
        {
            return dbcontext.OperationLogContext.Find(ID);
        }

        //插入操作日志信息
        public void Insert(OperationLog operationLog)
        {
            operationLog.IsPass = true;
            if (operationLog.LoginName == "admin")
                return;
            try
            {
                dbcontext.OperationLogContext.Add(operationLog);
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //删除操作日志信息
        public bool Delete(OperationLog operationLog)
        {
            try
            {
                OperationLog OP = operationLog;
                dbcontext.OperationLogContext.Attach(OP);
                dbcontext.OperationLogContext.Remove(OP);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool Delete(int[] operationLogID)
        {
            try
            {
                int count = operationLogID.Count();
                for (int i = 0; i < count; i++)
                {
                    OperationLog operationLog = new OperationLog { OperationLogID = operationLogID[i] };
                    dbcontext.OperationLogContext.Attach(operationLog);
                    dbcontext.OperationLogContext.Remove(operationLog);
                }
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }
       //更新
        public void Update(OperationLog operationLog)
        {
            try
            {
                OperationLog AoperationLog = dbcontext.OperationLogContext.Find(operationLog.OperationLogID);
                AoperationLog.LoginName = operationLog.LoginName;
                AoperationLog.LoginIP = operationLog.LoginIP;
                AoperationLog.OperationType = operationLog.OperationType;
                AoperationLog.OperationContent = operationLog.OperationContent;
                AoperationLog.OperationDataID = operationLog.OperationDataID;
                AoperationLog.OperationTime = operationLog.OperationTime;
                AoperationLog.Remark = operationLog.Remark;
                dbcontext.SaveChanges();
            }
            catch(System .Data .SqlClient .SqlException e)
            {
                throw e;
            }
        }
       //查看
        public List<OperationLog> Find()
        {
            return dbcontext.OperationLogContext.ToList();
        }
        
        //分页
        public List<OperationLog> FindPaged(bool ispass)
        {
            return dbcontext.OperationLogContext.Where(u => u.IsPass == ispass).OrderBy(c => c.OperationLogID).ToList();
        }
    }
}
