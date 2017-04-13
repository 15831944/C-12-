/**
 * 作者：未知
 * 修改履历：2015年8月17日 郝瑞 修复总体.内部控件不能改变数值的bug
 */

using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Agency
{
    public partial class ChangeAgency : System.Web.UI.Page
    {
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDroplist();
                TextChange();
            }
        }

        //初始化文本框数据
        public void TextChange()
        {
            BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
            Common.Entities.Agency ag = new Common.Entities.Agency();

            if (Session["AgencyName"].ToString() == "")
                return;

            ag = agen.FindByName(Session["AgencyName"].ToString());
            AgencyName2.Text = Session["AgencyName"].ToString();
            if (ag.ParentID != null)
            {
                string str = agen.FindAgenName(Convert.ToInt32(ag.ParentID));
                if (str != "")
                    ParentID2.Text = str;
                else
                    ParentID2.Text = "  ";
            }
            else
                ParentID2.Text = " ";

            if (ag.SecrecyLevel != null)
                DroSecrecyLevel.SelectedValue = (ag.SecrecyLevel - 1).ToString();
            else
            {
                DroSecrecyLevel.Text = " ";
            }

            if (ag.AgencyHeads != null)
                AgencyHeads2.Text = ag.AgencyHeads.ToString();
            else
                AgencyHeads2.Text = " ";

            if (ag.Research != null)
                Research2.Text = ag.Research.ToString();
            else
                Research2.Text = " ";

            if (ddl_glo.SelectedText == ag.IsGlobal.Trim())
                ddl_glo.Items[0].Selected = true;
            else
                ddl_glo.Items[1].Selected = true;

            if (ag.FullTimeNumbers != null)
                FullTimeNumber2.Text = ag.FullTimeNumbers.ToString();
            else
                FullTimeNumber2.Text = " ";

            if (ag.PartTimeNumbers != null)
                PartTimeNumber2.Text = ag.PartTimeNumbers.ToString();
            else
                PartTimeNumber2.Text = " ";

            if (ag.Area != null)
                Area2.Text = ag.Area.ToString();
            else
                Area2.Text = " ";

            if (ag.Location != null)
                Location2.Text = ag.Location.ToString();
            else
                Location2.Text = " ";

        }

        //清空所有文本框
        protected void Delete_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        //
        protected void ClearText()
        {
            AgencyName2.Text = "";
            ParentID2.Text = "";
            AgencyHeads2.Text = "";
            Research2.Text = "";
            FullTimeNumber2.Text = "";
            PartTimeNumber2.Text = "";
            Area2.Text = "";
            Location2.Text = "";
        }

        //保存机构信息
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (AgencyName2.Text.Trim() == "")
                {
                    Alert.ShowInTop("机构名称不能为空！");
                    AgencyName2.Text = "";
                    return;
                }
                if (AgencyHeads2.Text.Trim() == "")
                {
                    Alert.ShowInTop("机构负责人不能为空！");
                    AgencyHeads2.Text = "";
                    return;
                }
                if (FullTimeNumber2.Text.Trim() == "")
                {
                    Alert.ShowInTop("专职人数不能为空！");
                    FullTimeNumber2.Text = "";
                    return;
                }
                Common.Entities.Agency ag = agen.FindByName(Session["AgencyName"].ToString());
                Common.Entities.OperationLog op = new Common.Entities.OperationLog();
                BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();

                ag.AgencyName = AgencyName2.Text.ToString();
                ag.ParentID = agen.SelectAgencyID(ParentID2.Text.ToString());
                ag.SecrecyLevel = Convert.ToInt32(DroSecrecyLevel.SelectedIndex + 1);
                ag.AgencyID = agen.SelectAgencyID(AgencyName2.Text.Trim());
                ag.AgencyHeads = AgencyHeads2.Text.ToString();
                ag.Research = Research2.Text.ToString();
                ag.AgencyNumber = DroAgencyNumber.SelectedText;
                ag.FullTimeNumbers = Convert.ToInt32(FullTimeNumber2.Text.ToString());
                ag.PartTimeNumbers = Convert.ToInt32(PartTimeNumber2.Text.ToString());
                ag.Area = Area2.Text.ToString();
                ag.Location = Location2.Text.ToString();
                ag.IsGlobal = ddl_glo.SelectedText.Trim();
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    ag.IsPass = true;
                    agen.Update(ag);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference("保存成功") + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    ag.IsPass = false;
                    agen.Update(ag);
                    op.LoginIP = "";
                    op.LoginName = Session["LoginName"].ToString();
                    op.OperationContent = "Agency";
                    op.OperationDataID = ag.AgencyID;
                    op.OperationType = "更新";
                    op.OperationTime = DateTime.Now;
                    blop.Insert(op);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHidePostBackReference() + Alert.GetShowInTopReference("您的操作已经提交，请等待管理员确认！"));
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //初始化下拉框
        public void InitDroplist()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                DroSecrecyLevel.Items.Add(SecrecyLevels[i], i.ToString());
            
        //初始化机构分类名称下拉框
            List<BasicCode> list = bllBasicCode.FindALLName("机构分类名称");
            for (int i = 0; i < list.Count(); i++)
            {
                DroAgencyNumber.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }

        protected void AgencyName2_TextChanged(object sender, EventArgs e)
        {
            if (AgencyName2.Text.Trim() == Session["AgencyName"])
                return;
            else
            {
                AgencyName2.Text = "";
                if (agen.FindByName(AgencyName2.Text.ToString()).IsPass == true)
                    Alert.ShowInTop("该机构已经存在，请重新填写！");
                else
                    Alert.ShowInTop("该机构正在审核中，请联系管理员！");
            }
        }

        //上级机构验证
        protected void ParentID2_TextChanged(object sender, EventArgs e)
        {
            if (agen.SelectAgencyID(ParentID2.Text.ToString()) == 0)
            {
                ParentID2.Text = "";
                Alert.ShowInTop("无此机构，请重新填写！");
                this.ParentID2.Focus();
            }
            else
                return;
        }
    }
}