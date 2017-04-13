using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLEquipment
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //根据存放地点名查找家具信息
        public List<Equipment> FindByAgencyID(string StorageLocation, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.StorageLocation.Contains(StorageLocation) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据使用人查找家具信息
        public List<Equipment> FindByUsePerson(string useperson, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.UsePerson.Contains(useperson) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据价格查找家具信息
        public List<Equipment> FindByPrice(int flag, string price, int level)
        {
            double pri = Convert.ToDouble(price);
            double pricein;
            try
            {
                List<Equipment> res = dbcontext.EquipmentContext.Where(u => u.SecrecyLevel <= level && u.IsPass == true).ToList();
                List<Equipment> result = new List<Equipment>();
                switch (flag)
                {
                    case 0:
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(res[i].Price))
                            {
                                pricein = Convert.ToDouble(res[i].Price);
                                if (pri == pricein)
                                    result.Add(res[i]);
                            }
                        }
                        return result;
                    case 1:
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(res[i].Price))
                            {
                                pricein = Convert.ToDouble(res[i].Price);
                                if (pri > pricein)
                                    result.Add(res[i]);
                            }
                        }
                        return result;
                    case 2:
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(res[i].Price))
                            {
                                pricein = Convert.ToDouble(res[i].Price);
                                if (pri < pricein)
                                    result.Add(res[i]);
                            }
                        }
                        return result;
                    case 3:
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(res[i].Price))
                            {
                                pricein = Convert.ToDouble(res[i].Price);
                                if (pri >= pricein)
                                    result.Add(res[i]);
                            }
                        }
                        return result;
                    case 4:
                        for (int i = 0; i < res.Count; i++)
                        {
                            if (!string.IsNullOrEmpty(res[i].Price))
                            {
                                pricein = Convert.ToDouble(res[i].Price);
                                if (pri <= pricein)
                                    result.Add(res[i]);
                            }
                        }
                        return result;
                    default:
                        return null;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //根据购买时间查找家具信息
        public List<Equipment> FindByPurchaseTime(int year, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.PurchaseTime.Value.Year == year && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据购买人查找家具信息
        public List<Equipment> FindByPurchase(string purchase, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.Purchase.Contains(purchase) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据家具名称查找家具信息
        public List<Equipment> FindByEquipmentName(string name, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.EquipmentName.Contains(name) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //找出所有家具信息
        public List<Equipment> FindPaged(int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按资产编号查询家具信息
        public List<Equipment> FindByEquipNum(string equipnum, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.EquipNum.Contains(equipnum) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按所属机构查询家具信息
        public List<Equipment> FindByAgencyName(string agencyname, int level)
        {
            return dbcontext.EquipmentContext.Where(u => u.AgencName.Contains(agencyname) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //录入家具信息
        public void Insert(Equipment Equipment)
        {
            try
            {
                dbcontext.EquipmentContext.Add(Equipment);
                dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }
        }
        //根据ID删除信息
        public bool Delete(int ID)
        {
            try
            {
                Equipment Equipment = new Equipment { EquipmentID = ID };
                dbcontext.EquipmentContext.Attach(Equipment);
                dbcontext.EquipmentContext.Remove(Equipment);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //更新IsPass状态UpdateIsPass(bool)
        public void UpdateIsPass(int quipID, bool Ispass)
        {
            try
            {
                Equipment Equipment = dbcontext.EquipmentContext.Find(quipID);
                if (Equipment == null)
                    return;
                Equipment.IsPass = Ispass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //根据家具id查找家具信息
        public Equipment FindByid(int id)
        {
            try
            {
                return dbcontext.EquipmentContext.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //更新
        public bool Update(Equipment Equipment)
        {
            try
            {
                Equipment newEquipment = dbcontext.EquipmentContext.Find(Equipment.EquipmentID);
                newEquipment.StorageLocation = Equipment.StorageLocation;
                newEquipment.IsShare = Equipment.IsShare;
                newEquipment.EquipmentName = Equipment.EquipmentName;
                newEquipment.IsGowerProcu = Equipment.IsGowerProcu;
                newEquipment.Price = Equipment.Price;
                newEquipment.Purchase = Equipment.Purchase;
                newEquipment.PurchaseTime = Equipment.PurchaseTime;
                newEquipment.UsePerson = Equipment.UsePerson;
                newEquipment.EquipNum = Equipment.EquipNum;
                newEquipment.SecrecyLevel = Equipment.SecrecyLevel;
                newEquipment.EntryPerson = Equipment.EntryPerson;
                newEquipment.IsPass = Equipment.IsPass;
                newEquipment.CategoryName = Equipment.CategoryName;
                newEquipment.ClassNum = Equipment.ClassNum;
                newEquipment.Manufacturer = Equipment.Manufacturer;
                newEquipment.MeasurementUnit = Equipment.MeasurementUnit;
                newEquipment.Model = Equipment.Model;
                newEquipment.Remarks = Equipment.Remarks;
                newEquipment.AgencName = Equipment.AgencName;
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
