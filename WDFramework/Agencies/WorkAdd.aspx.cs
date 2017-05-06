using Common.Entities;
/**编写人：张凡凡
 * 时间：2014年8月1号
 * 功能:机构添加界面后台
 * 修改履历：2015年8月17日 郝瑞 修复总体.内部控件不能改变数值的bug
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework
{
    public partial class WorkAdd : System.Web.UI.Page
    {
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearText();
                InitDroplist();
            }
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
                Common.Entities.Agency ag = new Common.Entities.Agency();
                Common.Entities.OperationLog op = new Common.Entities.OperationLog();
                BLHelper.BLLOperationLog blop = new BLHelper.BLLOperationLog();
                BLHelper.BLLUser user = new BLHelper.BLLUser();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                ag.AgencyName = AgencyName2.Text.ToString();
                ag.ParentID = agen.SelectAgencyID(ParentID2.Text.ToString());
                ag.SecrecyLevel = Convert.ToInt32(DroSecrecyLevel.SelectedIndex + 1);
                ag.AgencyHeads = AgencyHeads2.Text.ToString();
                ag.Research = Research2.Text.ToString();
                ag.AgencyNumber = DroAgencyNumber.SelectedText;
                ag.FullTimeNumbers = Convert.ToInt32(FullTimeNumber2.Text.ToString());
                ag.PartTimeNumbers = Convert.ToInt32(PartTimeNumber2.Text.ToString());
                ag.Area = Area2.Text.ToString();
                ag.IsGlobal = ddl_glo.SelectedText.Trim();
                ag.Location = Location2.Text.ToString();
                ag.EntryPerson = username;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    ag.IsPass = true;
                    agen.Insert(ag);
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference("保存成功") + Alert.GetShowInTopReference("保存成功"));
                }
                else
                {
                    ag.IsPass = false;
                    agen.Insert(ag);
                    op.LoginIP = "";
                    op.LoginName = Session["LoginName"].ToString();
                    op.OperationContent = "Agency";
                    op.OperationDataID = agen.SelectAgencyID(AgencyName2.Text.Trim().ToString());
                    op.OperationType = "添加";
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

        //父机构合法判定
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

        //机构名称有无判定
        protected void AgencyName2_TextChanged(object sender, EventArgs e)
        {
            if (agen.FindByName(AgencyName2.Text.ToString()) == null)
                return;
            else
            {
                //AgencyName2.Text = "";
                if (agen.FindByName(AgencyName2.Text.ToString()).IsPass == true)
                    Alert.ShowInTop("该机构已经存在，请重新填写！");
                else
                    Alert.ShowInTop("该机构正在审核中，请联系管理员！");
            }
        }

        //初始化下拉框
        public void InitDroplist()
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
            {
                DroSecrecyLevel.Items.Add(SecrecyLevels[i].ToString(), i.ToString());
            }
            //初始化机构分类名称下拉框
            List<BasicCode> list = bllBasicCode.FindALLName("机构分类名称");
            for (int i = 0; i < list.Count(); i++)
            {
                DroAgencyNumber.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }
        
    }
}