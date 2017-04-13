/**
 * 作者：未知
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 */

using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.FixedAssets.Equipment
{
    public partial class UpdateEquipment : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLStaffDevote bllStaffDevote = new BLHelper.BLLStaffDevote();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitSecrecyLevel();
                BindData();
            }
        }
        public void BindData()
        {
            BLHelper.BLLEquipment blequip = new BLHelper.BLLEquipment();
            try
            {
                //ddl_agencyname.Items.Clear();
                ////初始化机构下拉框
                //BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
                //List<Common.Entities.Agency> list = agency.FindAllAgencyName();
                //for (int i = 0; i < list.Count(); i++)
                //{
                //    ddl_agencyname.Items.Add(list[i].AgencyName.ToString(), list[i].AgencyName.ToString());
                //}

                Common.Entities.Equipment equipment = blequip.FindByid(Convert.ToInt32(Session["EquipmentID"]));
                tb_EquipmenteName.Text = equipment.EquipmentName;
                tb_price.Text = equipment.Price;
                tb_Purchase.Text = equipment.Purchase;
                tb_UsePerson.Text = equipment.UsePerson;

                switch (equipment.CategoryName.Trim())
                {
                    case "无":
                        ddl_Category.SelectedIndex = 0;
                        break;
                    case "盘盈设备":
                        ddl_Category.SelectedIndex = 1;
                        break;
                    case "盘亏设备":
                        ddl_Category.SelectedIndex = 2;
                        break;
                    default:
                        ddl_Category.SelectedIndex = 0;
                        break;
                }

                tb_ClassNum.Text = equipment.ClassNum;
                tb_Manufacturer.Text = equipment.Manufacturer;
                tb_MeasurementUnit.Text = equipment.MeasurementUnit;
                tb_Model.Text = equipment.Model;
                tb_Remarks.Text = equipment.Remarks;
                tb_StorageLocation.Text = equipment.StorageLocation;
                //tb_Agency.Text = equipment.AgencName;
                ddl_Agency.SelectedValue = equipment.AgencName;
                DropDownListSecrecyLevel.SelectedIndex = equipment.SecrecyLevel.Value - 1;
               // ddl_agencyname.SelectedValue = agency.FindAgenName(equipment.AgencyID.Value);
                if (equipment.IsGowerProcu.Value)
                    ddl_isgov.SelectedIndex = 0;
                else
                    ddl_isgov.SelectedIndex = 1;
                if (equipment.IsShare.Value)
                    ddl_isshare.SelectedIndex = 0;
                else
                    ddl_isshare.SelectedIndex = 1;
                dp_PurchaseTime.SelectedDate = equipment.PurchaseTime;
                tb_Equipnum.Text = equipment.EquipNum;

            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //初始化项目涉密等级等级
        public void InitSecrecyLevel()
        {
            try
            {
                DropDownListSecrecyLevel.Items.Clear();
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            BLHelper.BLLEquipment blequip = new BLHelper.BLLEquipment();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();

            Common.Entities.Equipment Equipment = new Common.Entities.Equipment();
            Common.Entities.OperationLog log = new Common.Entities.OperationLog();
            try
            {
                //if (string.IsNullOrEmpty(tb_EquipmenteName.Text.Trim()))
                //{
                //    Alert.ShowInTop("请填写设备名称!");
                //    return;
                //}
                Equipment.EquipmentName = tb_EquipmenteName.Text.Trim();
                if (ddl_isgov.SelectedIndex == 0)
                    Equipment.IsGowerProcu = true;
                else
                    Equipment.IsGowerProcu = false;
                if (ddl_isshare.SelectedIndex == 0)
                    Equipment.IsShare = true;
                else
                    Equipment.IsShare = false;
                Equipment.Price = tb_price.Text.Trim();
                Equipment.Purchase = tb_Purchase.Text.Trim();
                Equipment.PurchaseTime = dp_PurchaseTime.SelectedDate;
                Equipment.UsePerson = tb_UsePerson.Text.Trim();
                Equipment.EquipNum = tb_Equipnum.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                Equipment.EntryPerson = username;
                Equipment.SecrecyLevel = Convert.ToInt32(DropDownListSecrecyLevel.SelectedIndex + 1);
                //Equipment.AgencyID = agency.SelectAgencyID(ddl_agencyname.SelectedText.Trim());
                //Equipment.AgencName = tb_Agency.Text.Trim();
                Equipment.AgencName = ddl_Agency.SelectedText;
                Equipment.CategoryName = "无";
                Equipment.CategoryName = ddl_Category.SelectedText;
                Equipment.ClassNum = tb_ClassNum.Text.Trim();
                Equipment.Manufacturer = tb_Manufacturer.Text.Trim();
                Equipment.MeasurementUnit = tb_MeasurementUnit.Text.Trim();
                Equipment.Model = tb_Model.Text.Trim();
                Equipment.Remarks = tb_Remarks.Text.Trim();
                Equipment.StorageLocation = tb_StorageLocation.Text.Trim();
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Equipments";
                    log.OperationType = "更新";
                    Equipment.IsPass = false;

                    blequip.Insert(Equipment);//插入设备表
                    log.OperationDataID = Convert.ToInt32(Session["EquipmentID"]);
                    log.Remark = Convert.ToString(Equipment.EquipmentID);
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    Equipment.IsPass = true;
                    Equipment.EquipmentID = Convert.ToInt32(Session["EquipmentID"]);
                    blequip.Update(Equipment);//更新设备表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }

            }
            catch (Exception ex)
            {
                Alert.ShowInTop("保存出错，请联系管理员！");
                pm.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {

                ddl_isgov.Reset();
                DropDownListSecrecyLevel.Reset();
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
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        protected void tb_EquipmenteName_TextChanged(object sender, EventArgs e)
        {
        //    BLHelper.BLLEquipment blfurni = new BLHelper.BLLEquipment();
        //    if (string.IsNullOrEmpty(tb_EquipmenteName.Text.Trim()))
        //    {
        //        Alert.ShowInTop("请填写设备名称!");
        //        return;
        //    }
        //    else
        //    {
        //        if (blfurni.FindByEquipmentName(tb_EquipmenteName.Text.Trim(), 5).Count != 0)
        //        {
        //            Alert.ShowInTop("设备名称已经存在!");
        //            tb_EquipmenteName.Reset();
        //            return;
        //        }
        //    }
        }
    }
}