using Common.Entities;
using DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace BLHelper
{
    public class BLLFurniture
    {
        DataBaseContext dbcontext = new DataBaseContext();
        //根据机构名查找家具信息
        public List<Furniture> FindByAgencyName(string agencyname, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.AgencName.Contains(agencyname) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据使用人查找家具信息
        public List<Furniture> FindByUsePerson(string useperson, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.UsePerson.Contains(useperson) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据价格查找家具信息
        public List<Furniture> FindByPrice(int flag, string price, int level)
        {
            double pri = Convert.ToDouble(price);
            double pricein;
            try
            {
                List<Furniture> res = dbcontext.FurnitureContext.Where(u => u.SecrecyLevel <= level && u.IsPass == true).ToList();
                List<Furniture> result = new List<Furniture>();
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
        public List<Furniture> FindByPurchaseTime(int year, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.PurchaseTime.Value.Year == year && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据购买人查找家具信息
        public List<Furniture> FindByPurchase(string purchase, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.Purchase.Contains(purchase) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据家具名称查找家具信息
        public List<Furniture> FindByFurnitureName(string name, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.FurnitureName.Contains(name) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //按资产编号查询家具信息
        public List<Furniture> FindByEquipNum(string equipnum, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.EquipNum.Contains(equipnum) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //根据存放地点名查找家具信息
        public List<Furniture> FindByStorageLocation(string StorageLocation, int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.StorageLocation.Contains(StorageLocation) && u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //找出所有家具信息
        public List<Furniture> FindPaged(int level)
        {
            return dbcontext.FurnitureContext.Where(u => u.SecrecyLevel <= level && u.IsPass == true).ToList();
        }

        //录入家具信息
        public void Insert(Furniture furniture)
        {
            try
            {
                dbcontext.FurnitureContext.Add(furniture);
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
                Furniture furniture = new Furniture { FurnitureID = ID };
                dbcontext.FurnitureContext.Attach(furniture);
                dbcontext.FurnitureContext.Remove(furniture);
                dbcontext.SaveChanges();
                return true;
            }
            catch
            {
                throw;
            }
        }

        //更新IsPass状态UpdateIsPass(bool)
        public void UpdateIsPass(int furID, bool Ispass)
        {
            try
            {
                Furniture furniture = dbcontext.FurnitureContext.Find(furID);
                if (furniture == null)
                    return;
                furniture.IsPass = Ispass;
                dbcontext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        //根据家具id查找家具信息
        public Furniture FindByid(int id)
        {
            try
            {
                return dbcontext.FurnitureContext.Find(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //更新
        public bool Update(Furniture furniture)
        {
            try
            {
                Furniture newfurniture = dbcontext.FurnitureContext.Find(furniture.FurnitureID);
                newfurniture.StorageLocation = furniture.StorageLocation;
                newfurniture.FurnitureName = furniture.FurnitureName;
                newfurniture.IsGowerProcu = furniture.IsGowerProcu;
                newfurniture.IsShare = furniture.IsShare;
                newfurniture.Price = furniture.Price;
                newfurniture.Purchase = furniture.Purchase;
                newfurniture.PurchaseTime = furniture.PurchaseTime;
                newfurniture.UsePerson = furniture.UsePerson;
                newfurniture.EquipNum = furniture.EquipNum;
                newfurniture.SecrecyLevel = furniture.SecrecyLevel;
                newfurniture.EntryPerson = furniture.EntryPerson;
                newfurniture.IsPass = furniture.IsPass;
                newfurniture.CategoryName = furniture.CategoryName;
                newfurniture.ClassNum = furniture.ClassNum;
                newfurniture.Manufacturer = furniture.Manufacturer;
                newfurniture.MeasurementUnit = furniture.MeasurementUnit;
                newfurniture.Model = furniture.Model;
                newfurniture.Remarks = furniture.Remarks;
                newfurniture.AgencName = furniture.AgencName;
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
