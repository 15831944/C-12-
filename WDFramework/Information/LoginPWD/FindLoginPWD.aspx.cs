/**编写人：王会会
 * 时间：2014年8月24号
 * 功能：管理员重置人员登录密码的相关操作
 * 修改履历：2015年10月10日 马睿杰 去除page静态
 **/
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.LoginPWD
{
    public partial class FindLoginPWD : System.Web.UI.Page
    {
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLAgency bllAgency = new BLLAgency();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLCommon.Encrypt encrypt = new BLCommon.Encrypt();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                BindData();
                btnResetPWD.Enabled = false;
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<UserInfo> UserList = bllUser.FindResetPWD(Convert.ToInt32(Session["SecrecyLevel"]), Session["LoginName"].ToString());
                var result = UserList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                People_Info.RecordCount = UserList.Count();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnResetPWD.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //根据人员姓名模糊查找
        public void FindByName()
        {
            try
            {
                ViewState["page"] = 1;
                List<UserInfo> UserList = bllUser.FindPWDName(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]), Session["LoginName"].ToString()).ToList();
                var result = UserList.Skip(People_Info.PageIndex * People_Info.PageSize).Take(People_Info.PageSize).ToList();
                People_Info.RecordCount = UserList.Count();
                this.People_Info.DataSource = result;
                this.People_Info.DataBind();
                btnResetPWD.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //判断男女
        public string getgender(string bx)
        {
            try
            {
                return bllUser.getgender(bx);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //判断婚姻状况
        public string getMarried(string xb)
        {
            try
            {
                if (xb == "True")
                    return "已婚";
                else
                    return "未婚";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //判断是否博士生导师
        public string getDoctorTeacher(string xb)
        {
            try
            {
                if (xb == "True")
                    return "是";
                else
                    return "否";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //判断是否为硕士生导师
        public string getMasterTeacher(string xb)
        {
            try
            {
                if (xb == "True")
                    return "是";
                else
                    return "否";
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }     
        //备注界面跳转
        protected string GetEditUrl(object UserInfoID)
        {
            return Remark.GetShowReference("Remark_Window.aspx?id=" + UserInfoID, "备注");
        }
        //个人简介界面跳转
        protected string GetEditUrlP(object UserInfoID)
        {
            return Remark.GetShowReference("Profile_Window.aspx?id=" + UserInfoID, "个人简介");
        }
        //等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //查询
        protected void FindObjectAll_Click(object sender, EventArgs e)
        {
            People_Info.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
                FindByName();
            else
                Alert.ShowInTop("请填写查询条件");
        }
        //重置密码
        protected void btnResetPWD_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(People_Info, CBoxSelect);
                for (int i = 0; i < selections.Count(); i++)
                {
                    bllUser.ChangePWD(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), encrypt.MD5("000000"));
                }
                List<string> PersonName = new List<string>();
                for (int i = 0; i < selections.Count(); i++)
                {
                    string name = bllUser.Find(Convert.ToInt32(People_Info.DataKeys[selections[i]][0]), true).UserName;
                    PersonName.Add(name);
                }
                BindData();
                string str = null;
                for (int i = 0; i < PersonName.Count(); i++)
                {
                    str += PersonName[i] + "；";
                }
                Alert.ShowInTop("人员姓名为" + str + "的密码重置成功！");
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        protected void People_Info_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(People_Info, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限,请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (selections.Count() == 0)
                {
                    btnResetPWD.Enabled = false;
                    return;
                }
                if (selections.Count() != 0)
                {
                    btnResetPWD.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分页
        protected void People_Info_PageIndexChange(object sender, GridPageEventArgs e)
        {
            People_Info.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                 BindData();
                 break;
                case 1:
                 FindByName();
                 break;
            }          
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            People_Info.PageIndex = 0;
            this.People_Info.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByName();
                    break;
            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            TriggerBox.Text = "";
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (People_Info.PageIndex) * People_Info.PageSize;
        }
    }
}