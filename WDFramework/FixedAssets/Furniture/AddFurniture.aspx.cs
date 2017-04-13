using FineUI;
/**编写人：张凡凡
 * 时间：2015年5月29号
 * 功能:家具添加界面后台
 * 修改履历： 2015年8月27日 郝瑞 所属部门控件改为dropdownlist
 *                              修复未添加购置时间时可能造成的空引用异常
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.FixedAssets.Furniture
{
    public partial class AddFurniture : System.Web.UI.Page
    {
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            DdlBindData();
        }

        private void DdlBindData()
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
                    ddl_Level.Items.Add(SecrecyLevels[i], i.ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            tb_FurnitureName.Reset();
            ddl_isgov.Reset();
            ddl_Level.Reset();
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
                if (string.IsNullOrEmpty(tb_FurnitureName.Text.Trim()))
                {
                    Alert.ShowInTop("请填写家具名称!");
                    return;
                }
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
                furniture.PurchaseTime = dp_PurchaseTime.SelectedDate.Value;
                furniture.UsePerson = tb_UsePerson.Text.Trim();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                furniture.EntryPerson = username;
                furniture.SecrecyLevel = Convert.ToInt32(ddl_Level.SelectedIndex + 1);
                
                //furniture.AgencyID = agency.SelectAgencyID(ddl_agencyname.SelectedText.Trim());
                if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                {
                    log.LoginName = username;
                    log.OperationTime = DateTime.Now;
                    log.LoginIP = " ";
                    log.OperationContent = "Furnitures";
                    log.OperationType = "添加";
                    furniture.IsPass = false;

                    blfurni.Insert(furniture);//插入家具表
                    log.OperationDataID = furniture.FurnitureID;
                    op.Insert(log);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("您的数据已提交，请等待审核！"));
                }
                else
                {
                    furniture.IsPass = true;
                    blfurni.Insert(furniture);//插入家具表
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("保存成功"));
                }
    
            }
            catch (Exception ex)
            {
                Alert.ShowInTop("保存出错，请联系管理员！");
                pm.SaveError(ex, this.Request);
            }
        }

        //判断所填家具名称是否合法
        protected void tb_FurnitureName_TextChanged(object sender, EventArgs e)
        {
            BLHelper.BLLFurniture blfurni = new BLHelper.BLLFurniture();
            if (string.IsNullOrEmpty(tb_FurnitureName.Text.Trim()))
            {
                Alert.ShowInTop("请填写家具名称!");
                return;
            }
            else
            {
                if(blfurni.FindByFurnitureName(tb_FurnitureName.Text.Trim(), 5).Count != 0)
                {
                    Alert.ShowInTop("家具名称已经存在!");
                    tb_FurnitureName.Reset();
                    return;
                }
            }
        }
    }
}