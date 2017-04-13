/**编写人：张凡凡
 * 时间：2014年8月1号
 * 功能:机构查看界面后台
 * 修改履历：
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLHelper;
using Common.Entities;
using System.Text;
using FineUI;

namespace WDFramework.Agencies
{
    public partial class AgencyNavigate : System.Web.UI.Page
    {
        BLLAgency agen = new BLLAgency();
        BLLUser User = new BLLUser();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
                Delete.Enabled = false;
                Add.OnClientClick = Window3.GetShowReference("WorkAdd.aspx", "新增机构信息");

                Session["AgencyName"] = "";
            }
        }
        public void BindData()
        {
            TrePopulation.Nodes.Clear();
            TreInside.Nodes.Clear();
            List<string> AgencyPopulation = agen.FindByIsGlo("总体", Convert.ToInt32(Session["SecrecyLevel"]));
            for (int i = 0; i < AgencyPopulation.Count; i++)
            {
                FineUI.TreeNode treenode = new FineUI.TreeNode();
                treenode.Text = AgencyPopulation[i];
                treenode.EnablePostBack = true;
                TrePopulation.Nodes.Add(treenode);
            }
            List<string> AgencyInside = agen.FindByIsGlo("内部", Convert.ToInt32(Session["SecrecyLevel"]));
            for (int i = 0; i < AgencyInside.Count; i++)
            {
                FineUI.TreeNode treenode = new FineUI.TreeNode();
                treenode.Text = AgencyInside[i];
                treenode.EnablePostBack = true;
                TreInside.Nodes.Add(treenode);
            }
        }

        //绑定Grid1数据，绑定text数据
        protected void Tree1_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            Delete.Enabled = true;
            Common.Entities.Agency ag = agen.FindByName(e.Node.Text.Trim());
            if (ag.AgencyName != null)
            {
                AgencyName2.Text = ag.AgencyName.ToString();
                Session["AgencyName"] = ag.AgencyName;
            }
            else
                AgencyName2.Text = " ";

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
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            if (ag.SecrecyLevel != null)
                SecrecyLevel2.Text = SecrecyLevels[ag.SecrecyLevel.Value - 1];
            else
                SecrecyLevel2.Text = " ";

            if (ag.AgencyHeads != null)
                AgencyHeads2.Text = ag.AgencyHeads.ToString();
            else
                AgencyHeads2.Text = " ";

            if (ag.Research != null)
                Research2.Text = ag.Research.ToString();
            else
                Research2.Text = " ";

            if (ag.AgencyNumber != null)
                AgencyNumber2.Text = ag.AgencyNumber;
            else
                AgencyNumber2.Text = " ";

            if (ag.FullTimeNumbers != null)
                FullTimeNumber2.Text = ag.FullTimeNumbers.ToString();
            else
                FullTimeNumber2.Text = " ";

            if (ag.PartTimeNumbers != null)
                PartTimeNumber2.Text = ag.PartTimeNumbers.ToString();
            else
                PartTimeNumber2.Text = " ";

            if (ag.Area != null)
                Area2.Text = ag.Area.ToString() + "平方米";
            else
                Area2.Text = " ";

            if (ag.Location != null)
                Location2.Text = ag.Location.ToString();
            else
                Location2.Text = " ";

            BindPeople();
        }


        //人员数据绑定
        public void BindPeople()
        {
            List<UserInfo> Users = User.FindByAgencyName(agen.SelectAgencyID(AgencyName2.Text.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
            Grid_Info.RecordCount = Users.Count;
            Grid_Info.DataSource = Users.Skip(Grid_Info.PageIndex * Grid_Info.PageSize).Take(Grid_Info.PageSize).ToList();
            Grid_Info.DataBind();
        }

        //分页每页项的个数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Info.PageIndex = 0;
            this.Grid_Info.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            BindPeople();
        }

        //分页页数
        protected void People_Info_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Info.PageIndex = e.NewPageIndex;
            BindPeople();
        }



        //性别获取
        protected string getgender(string se)
        {
            return User.getgender(se);
        }

        //删除机构
        public void Delete_Click(object sender, EventArgs e)
        {
            int AgencyID = agen.SelectAgencyID(AgencyName2.Text.Trim());
            int level = Convert.ToInt32(Session["SecrecyLevel"]);

            if (level == 5)
            {
                agen.Delete(AgencyID);
                BindData();
                Clear();
                BindPeople();
                Delete.Enabled = false;
                Alert.ShowInTop("删除数据成功!");
                return;
            }
            Common.Entities.Agency ag = new Common.Entities.Agency();
            ag = agen.FindByName(AgencyName2.Text.Trim());
            OperationLog op = new OperationLog();
            BLHelper.BLLOperationLog blop = new BLLOperationLog();

            if (ag.EntryPerson != Session["LoginName"].ToString())
            {
                string str = "您无对此机构操作的权限！此机构信息为" + ag.EntryPerson + "录入，请与管理员联系!";
                Alert.ShowInTop(str);
                return;
            }
            else
            {
                agen.UpdatePass(AgencyID, false);
                op.LoginIP = "";
                op.LoginName = Session["LoginName"].ToString();
                op.OperationContent = "Agency";
                op.OperationDataID = AgencyID;
                op.OperationType = "删除";
                op.OperationTime = DateTime.Now;
                blop.Insert(op);
                BindData();
                Alert.ShowInTop("数据已提交，请等待管理员确认！");
            }
            Clear();

        }

        //刷新当前页
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            Clear();
            BindPeople();
            Delete.Enabled = false;
        }

        //清除文本框
        public void Clear()
        {
            AgencyName2.Text = "";
            ParentID2.Text = "";
            SecrecyLevel2.Text = "";
            AgencyHeads2.Text = "";
            Research2.Text = "";
            AgencyNumber2.Text = "";
            FullTimeNumber2.Text = "";
            PartTimeNumber2.Text = "";
            Area2.Text = "";
            Location2.Text = "";
        }

        public void Change_Click(object sender, EventArgs e)
        {
            if (AgencyName2.Text.Trim() == "")
            {
                Alert.ShowInTop("请选择所要修改的机构！");
                return;
            }
            Common.Entities.Agency ag = new Common.Entities.Agency();
            ag = agen.FindByName(AgencyName2.Text.Trim());
            if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
            {
                if (ag.EntryPerson != Session["LoginName"].ToString())
                {
                    string str = "您无对此机构操作的权限！此机构信息为" + ag.EntryPerson + "录入，请与管理员联系!";
                    Alert.ShowInTop(str);
                    return;
                }
            }
            Alert.Show("你确定要修改此数据吗!", "确认消息", MessageBoxIcon.Information, Window4.GetShowReference("ChangeAgency.aspx", "修改机构信息"), Target.Top);

        }

        protected void Window3_Close(object sender, WindowCloseEventArgs e)
        {
            if (e.CloseArgument.StartsWith("保存成功"))
            {
                BindData();
                Clear();
                BindPeople();
                Delete.Enabled = false;
            }
        }

        protected void Window4_Close(object sender, WindowCloseEventArgs e)
        {
            if (e.CloseArgument.StartsWith("保存成功"))
            {
                BindData();
                Clear();
                BindPeople();
                Delete.Enabled = false;
            }
        }
    }
}