/**编写人：王会会
 * 时间：2014年8月15号
 * 功能：更新人员荣誉称号信息的相关操作
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.Honor
{
    public partial class Update_HonorTitle : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLHonor bllHonor = new BLHelper.BLLHonor();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListSort();
                BindData();
                DatePickerGiveTime.MaxDate = DateTime.Now;
            }
        }
        public void BindData()
        {
            try
            {
                Common.Entities.Honor honor = bllHonor.FindByHonorID(Convert.ToInt32(Session["HonorID"]), true);
                UserInfoName.Text = bllUser.FindByUserID(honor.UserInfoID);
                TitleName.Text = honor.TitleName;
                DropDownListSort.SelectedValue = honor.Sort;
                //Sort.Text = honor.Sort;
                Remark.Text = honor.Remark;
                GivDivision.Text = honor.GivDivision;
                DatePickerGiveTime.SelectedDate = honor.GiveTime;
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(honor.SecrecyLevel - 1);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInfoName.Text.Trim() != "")
                {
                    if (bllUser.IsUser(UserInfoName.Text.Trim()) != null)
                    {
                        if (bllUser.IsUser(UserInfoName.Text.Trim()).IsPass == true)
                        {
                            if (TitleName.Text.Trim() == "")
                            {
                                Alert.ShowInTop("称号名称不能为空！");
                                TitleName.Text = "";
                                return;
                            }
                            if (GivDivision.Text.Trim() == "")
                            {
                                Alert.ShowInTop("授予部门不能为空！");
                                GivDivision.Text = "";
                                return;
                            }
                            Common.Entities.Honor honor = new Common.Entities.Honor();
                            honor.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            honor.TitleName = TitleName.Text.Trim();
                            honor.Sort = DropDownListSort.SelectedItem.Text;
                            honor.Remark = Remark.Text.Trim();
                            honor.GiveTime = DatePickerGiveTime.SelectedDate;
                            honor.GivDivision = GivDivision.Text.Trim();
                            honor.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            honor.EntryPerson = bllHonor.FindEntryPerson(Convert.ToInt32(Session["HonorID"]));// Session["LoginName"].ToString();
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                bllHonor.UpdateIsPass(Convert.ToInt32(Session["HonorID"]), false);
                                honor.IsPass = false;
                                bllHonor.InsertForPeople(honor);//插入荣誉称号表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = honor.EntryPerson;
                                operate.LoginIP = "";
                                operate.OperationType = "更新";
                                operate.OperationContent = "Honor";
                                operate.OperationDataID = Convert.ToInt32(Session["HonorID"]);
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = honor.HonorID.ToString();
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员荣誉称号信息已提交审核！"));
                            }
                            else
                            {
                                honor.HonorID = Convert.ToInt32(Session["HonorID"]);
                                honor.IsPass = true;
                                bllHonor.Update(honor);//更新荣誉称号表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员荣誉称号信息已修改完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("该人员正在审核中！");
                    }
                    else
                    {
                        Alert.ShowInTop("人员不存在！");
                        UserInfoName.Text = "";
                    }
                }
                else
                {
                    Alert.ShowInTop("人员名称不能为空！");
                    UserInfoName.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfoName.Reset();
                TitleName.Reset();
                DropDownListSort.Reset();
                Remark.Reset();
                DatePickerGiveTime.Reset();
                GivDivision.Reset();
                DropDownListSecrecyLevel.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化分类级别下拉框
        public void InitDropDownListSort()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("级别");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListSort.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}