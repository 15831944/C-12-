/**
 * 作者：未知
 * 修改履历：2015年8月17日 郝瑞 所属部门控件改为dropdownlist
 *                            修复未添加购置时间时可能造成的空引用异常
 */

using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.FixedAssets
{
    public partial class AddEquipment : System.Web.UI.Page
    {
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            DdlBindData();
        }

        private void DdlBindData()
        {
            //ddl_agencyname.Items.Clear();
            ddl_Level.Items.Clear();
            try
            {
                ////初始化机构下拉框
                //BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                //List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                //for (int i = 0; i < list.Count(); i++)
                //{
                //    ddl_agencyname.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                //}
                //初始化等级下拉框
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    ddl_Level.Items.Add(SecrecyLevels[i], i.ToString());
                }
                ddl_Level.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            ddl_isgov.Reset();
            ddl_Level.Reset();
            ddl_isshare.Reset();
            tb_EquipmenteName.Reset();
            tb_price.Reset();
            tb_Purchase.Reset();
            tb_Equipnum.Reset();
            tb_UsePerson.Reset();
            dp_PurchaseTime.Reset();
            tb_ClassNum.Reset();
            tb_Manufacturer.Reset();
            tb_MeasurementUnit.Reset();
            tb_Model.Reset();
            tb_Remarks.Reset();
            tb_StorageLocation.Reset();
            ddl_Category.Reset();
            //tb_Agency.Reset();
            ddl_Agency.Reset();
        }

        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            BLHelper.BLLEquipment blequipment = new BLHelper.BLLEquipment();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();

            Common.Entities.Equipment equipment = new Common.Entities.Equipment();
            Common.Entities.OperationLog log = new Common.Entities.OperationLog();
            try
            {
                if (string.IsNullOrEmpty(tb_EquipmenteName.Text.Trim()))
                {
                    Alert.ShowInTop("请填写设备名称!");
                    return;
                }
                equipment.EquipmentName = tb_EquipmenteName.Text.Trim();
                if (ddl_isgov.SelectedIndex == 0)
                    equipment.IsGowerProcu = true;
                else
                    equipment.IsGowerProcu = false;
                if (ddl_isshare.SelectedIndex == 0)
                    equipment.IsShare = true;
                else
                    equipment.IsShare = false;
                equipment.Price = tb_price.Text.Trim();
                equipment.CategoryName = "无";
                equipment.CategoryName = ddl_Category.SelectedText.Trim();
                equipment.Purchase = tb_Purchase.Text.Trim();
                equipment.PurchaseTime = dp_PurchaseTime.SelectedDate.Value;
                equipment.UsePerson = tb_UsePerson.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                equipment.EntryPerson = username;
                equipment.EquipNum = tb_Equipnum.Text.Trim();
                equipment.SecrecyLevel = 1;
                equipment.SecrecyLevel = Convert.ToInt32(ddl_Level.SelectedIndex + 1);
                //equipment.AgencyID = agency.SelectAgencyID(ddl_agencyname.SelectedText.Trim());
                //equipment.AgencName = tb_Agency.Text.Trim();
                equipment.AgencName = ddl_Agency.SelectedText;

                equipment.ClassNum = tb_ClassNum.Text.Trim();
                equipment.Manufacturer = tb_Manufacturer.Text.Trim();
                equipment.MeasurementUnit = tb_MeasurementUnit.Text.Trim();
                equipment.Model = tb_Model.Text.Trim();
                equipment.Remarks = tb_Remarks.Text.Trim();
                equipment.StorageLocation = tb_StorageLocation.Text.Trim();
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Equipments";
                    log.OperationType = "添加";
                    equipment.IsPass = false;

                    blequipment.Insert(equipment);//插入设备表
                    log.OperationDataID = equipment.EquipmentID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    equipment.IsPass = true;
                    blequipment.Insert(equipment);//插入设备表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }

            }
            catch (Exception ex)
            {
                Alert.ShowInTop("保存出错，请联系管理员！");
                pm.SaveError(ex, this.Request);
            }
        }

        //判断所填设备名称是否合法
        protected void tb_FurnitureName_TextChanged(object sender, EventArgs e)
        {
            BLHelper.BLLFurniture blfurni = new BLHelper.BLLFurniture();
            if (string.IsNullOrEmpty(tb_EquipmenteName.Text.Trim()))
            {
                Alert.ShowInTop("请填写设备名称!");
                return;
            }
            else
            {
                if (blfurni.FindByFurnitureName(tb_EquipmenteName.Text.Trim(), 5).Count != 0)
                {
                    Alert.ShowInTop("设备名称已经存在!");
                    tb_EquipmenteName.Reset();
                    return;
                }
            }
        }
    }
}