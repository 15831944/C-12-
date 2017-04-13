/**编写人：张凡凡
 * 时间：2015年5月29号
 * 功能:家具编辑界面后台
 * 修改履历：    修改人：吕博扬
 *              修改时间：2015年9月23日
 *              修改内容：取消所有输入项的数据校验
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.FixedAssets.Furniture
{
    public partial class UpdateFurniture1 : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLFurniture bllfurni = new BLHelper.BLLFurniture();
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
                    DropDownListSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
                }

                Common.Entities.Furniture furniture = bllfurni.FindByid(Convert.ToInt32(Session["FurnitureID"]));
                switch (furniture.CategoryName.Trim())
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

                tb_ClassNum.Text = furniture.ClassNum;
                tb_Manufacturer.Text = furniture.Manufacturer;
                tb_MeasurementUnit.Text = furniture.MeasurementUnit;
                tb_Model.Text = furniture.Model;
                tb_Remarks.Text = furniture.Remarks;
                tb_StorageLocation.Text = furniture.StorageLocation;
                //tb_Agency.Text = furniture.AgencName;
                ddl_Agency.SelectedValue = furniture.AgencName;

                tb_FurnitureName.Text = furniture.FurnitureName;
                tb_price.Text = furniture.Price;
                tb_Purchase.Text = furniture.Purchase;
                tb_UsePerson.Text = furniture.UsePerson;
                tb_Equipnum.Text = furniture.EquipNum;
                DropDownListSecrecyLevel.SelectedIndex = furniture.SecrecyLevel.Value - 1;
                //ddl_agencyname.SelectedValue = agency.FindAgenName(furniture.AgencyID.Value);
                if (furniture.IsGowerProcu.Value)
                    ddl_isgov.SelectedIndex = 0;
                else
                    ddl_isgov.SelectedIndex = 1;
                dp_PurchaseTime.SelectedDate = furniture.PurchaseTime;

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
            BLHelper.BLLFurniture blfurni = new BLHelper.BLLFurniture();
            BLHelper.BLLUser user = new BLHelper.BLLUser();
            BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
            BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();

            Common.Entities.Furniture furniture = new Common.Entities.Furniture();
            Common.Entities.OperationLog log = new Common.Entities.OperationLog();
            try
            {
                //if (string.IsNullOrEmpty(tb_FurnitureName.Text.Trim()))
                //{
                //    Alert.ShowInTop("请填写家具名称!");
                //    return;
                //}
                furniture.FurnitureName = tb_FurnitureName.Text.Trim();
                if (ddl_isgov.SelectedIndex == 0)
                    furniture.IsGowerProcu = true;
                else
                    furniture.IsGowerProcu = false;

                if (ddl_isshare.SelectedIndex == 0)
                    furniture.IsShare = true;
                else
                    furniture.IsShare = false;
                //furniture.AgencName = tb_Agency.Text.Trim();
                furniture.AgencName = ddl_Agency.SelectedText;
                furniture.CategoryName = "无";
                furniture.CategoryName = ddl_Category.SelectedText.Trim();
                furniture.ClassNum = tb_ClassNum.Text.Trim();
                furniture.EquipNum = tb_Equipnum.Text.Trim();
                furniture.Manufacturer = tb_Manufacturer.Text.Trim();
                furniture.MeasurementUnit = tb_MeasurementUnit.Text.Trim();
                furniture.Model = tb_Model.Text.Trim();
                furniture.Remarks = tb_Remarks.Text.Trim();
                furniture.StorageLocation = tb_StorageLocation.Text.Trim();
                furniture.Price = tb_price.Text.Trim();
                furniture.Purchase = tb_Purchase.Text.Trim();
                furniture.PurchaseTime = dp_PurchaseTime.SelectedDate;
                furniture.UsePerson = tb_UsePerson.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                furniture.EntryPerson = username;
                furniture.SecrecyLevel = Convert.ToInt32(DropDownListSecrecyLevel.SelectedIndex + 1);
                //furniture.AgencyID = agency.SelectAgencyID(ddl_agencyname.SelectedText.Trim());
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Furnitures";
                    log.OperationType = "更新";
                    furniture.IsPass = false;

                    blfurni.Insert(furniture);//插入家具表
                    log.OperationDataID = Convert.ToInt32(Session["FurnitureID"]);
                    log.Remark = Convert.ToString(furniture.FurnitureID);
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    furniture.IsPass = true;
                    furniture.FurnitureID = Convert.ToInt32(Session["FurnitureID"]);
                    blfurni.Update(furniture);//更新家具表
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
                tb_FurnitureName.Reset();
                ddl_isgov.Reset();
                DropDownListSecrecyLevel.Reset();
                ddl_isshare.Reset();
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
            //BLHelper.BLLFurniture blfurni = new BLHelper.BLLFurniture();
            //if (string.IsNullOrEmpty(tb_FurnitureName.Text.Trim()))
            //{
            //    Alert.ShowInTop("请填写家具名称!");
            //    return;
            //}
            //else
            //{
            //    if (blfurni.FindByFurnitureName(tb_FurnitureName.Text.Trim(), 5).Count != 0)
            //    {
            //        Alert.ShowInTop("家具名称已经存在!");
            //        tb_FurnitureName.Reset();
            //        return;
            //    }
            //}
        }
    }
}