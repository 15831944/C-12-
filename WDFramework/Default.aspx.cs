/**编写人：张凡凡
 * 时间：2014年8月14号
 * 功能:主界面界面后台
 * 修改履历：
 **/
using BLCommon;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using BLHelper;
using Common.Entities;

namespace WDFramework
{
    public partial class Default : System.Web.UI.Page
    {
        Tree innerTree = new Tree();
        Accordion accordionMenu = new Accordion();
        BLHelper.BLLUser bllUser = new BLLUser();
        BLLOperationLog blop = new BLLOperationLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Session["IsLogin"]) && Session["LoginName"].ToString() != "")
            {
                if (!IsPostBack)
                {
                    Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
                    ButtonOption.OnClientClick = Window1.GetShowReference("~/WebForms/Pop_Option.aspx", "修改密码");
                    BtnUserInfo.OnClientClick = WindowADD.GetShowReference("~/Information/ManagerInformation.aspx", "管理员消息");
                    btnExcel.OnClientClick = Window_Import.GetShowReference("~/AcademicMeeting/ImportExcel.aspx", "工具");
                    //BtnHomepage.OnClientClick = Tab1.GetShowReference();
                    DatePicker1.SelectedDate = DateTime.Now;
                    InitBtnUserfo();
                    People.Text = "欢迎," + bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName + "!";
                }
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference());
                Response.Redirect("login.aspx");
            }
        }

        //个人信息按钮初始化
        private void InitBtnUserfo()
        {
            if (Convert.ToInt32(Session["SecrecyLevel"]) < 5)
            {
                BtnUserInfo.Hidden = true;
                btnTool.Hidden = true;
            }
            else
            {
                List<OperationLog> oplist = blop.FindPaged(true);
                int count = oplist.Count();
                if (count == 0)
                {
                    BtnUserInfo.Text = "无消息需处理";
                    BtnUserInfo.Enabled = false;
                }
                else
                    BtnUserInfo.Text = "您有（" + count + "条）未处理消息";
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["load"] == null || (bool)Session["load"] != true)
            {
                Response.Redirect("login.aspx");
                Alert.Show("登录超时！");
            }
            InitAccordionMenu();

        }

        /// <summary>
        /// 创建手风琴菜单
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private Accordion InitAccordionMenu() 
        {
            int roleID = Convert.ToInt32(Session["RoleID"]);
            BLLTree bt = new BLLTree();
            accordionMenu.ID = "accordionMenu";
            accordionMenu.EnableFill = true;
            accordionMenu.ShowBorder = false;
            accordionMenu.ShowHeader = false;
            regionLeft.Items.Add(accordionMenu);
            int number = bt.GetTopsCount();
            List<TreeNav> topTreeList = bt.GetTops();
            for (int j = 0; j < number; j++)
            {
                AccordionPane accordionPane = new AccordionPane();
                accordionPane.Title = topTreeList[j].NodeText;
                accordionPane.ID = topTreeList[j].TreeNavID.ToString();
                accordionPane.Layout = Layout.Fit;
                accordionPane.ShowBorder = false;
                accordionPane.BodyPadding = "2px 0 0 0";
                //对非管理员禁用管理员项
                if (accordionPane.Title != "管理员操作" || Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    accordionMenu.Items.Add(accordionPane);
                Tree innerTree = new Tree();
                innerTree.EnableArrows = true;
                innerTree.ShowBorder = false;
                innerTree.ShowHeader = false;
                innerTree.EnableIcons = false;
                innerTree.AutoScroll = true;

                // 生成树
                InitTree(accordionPane.ID, innerTree);
                accordionPane.Items.Add(innerTree);
            }

            return accordionMenu;

        }

        void innerTree_NodeCommand(object sender, TreeCommandEventArgs e)
        {
            // e.CommandArgument
            PageContext.RegisterStartupScript(mainTabStrip.GetAddTabReference(e.NodeID, e.CommandArgument, e.Node.Text, IconHelper.GetIconUrl(Icon.Application), true));
            //添加toolbar按钮
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Alert.ShowInTop("触发事件");
        }
        private void InitTree(string panelID, Tree innerTree)
        {
            //int roleID = Convert.ToInt32(Session["RoleID"]);
            TreeSource ts = new TreeSource();
            innerTree.DataSource = ts.GetRoleTreeSource(Convert.ToInt32(panelID));

            innerTree.DataBind();
            innerTree.NodeCommand += innerTree_NodeCommand;
            //禁用子节点
            if (innerTree.FindNode("N_29") != null && Convert.ToInt32(Session["SecrecyLevel"]) < 5)
                innerTree.FindNode("N_29").Enabled = false;
            if(innerTree.FindNode("N_36") != null && Session["LoginName"].ToString() == "admin")
                innerTree.FindNode("N_36").Enabled = false;
        }

        /// <summary> 
        /// 创建快捷方式 
        /// </summary> 
        /// <param name="Title">标题</param> 
        /// <param name="URL">URL地址</param> 
        private void CreateShortcut(string Title, string URL)
        {
            string strFavoriteFolder;
            // “收藏夹”中 创建 IE 快捷方式 
            strFavoriteFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Favorites);
            CreateShortcutFile(Title, URL, strFavoriteFolder);
            // “ 桌面 ”中 创建 IE 快捷方式 
            strFavoriteFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CreateShortcutFile(Title, URL, strFavoriteFolder);
            // “ 链接 ”中 创建 IE 快捷方式 
            strFavoriteFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\链接";
            CreateShortcutFile(Title, URL, strFavoriteFolder);
            //「开始」菜单中 创建 IE 快捷方式 
            strFavoriteFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            CreateShortcutFile(Title, URL, strFavoriteFolder);
        }
        /// <summary> 
        /// 创建快捷方式 
        /// </summary> 
        /// <param name="Title">标题</param> 
        /// <param name="URL">URL地址</param> 
        /// <param name="SpecialFolder">特殊文件夹</param> 
        private void CreateShortcutFile(string Title, string URL, string SpecialFolder)
        {
            // Create shortcut file, based on Title 
            System.IO.StreamWriter objWriter = System.IO.File.CreateText(SpecialFolder + "\\" + Title + ".url");
            // Write URL to file 
            objWriter.WriteLine("[InternetShortcut]");
            objWriter.WriteLine("URL=" + URL);
            // Close file 
            objWriter.Close();
        }


        protected void createShoutCut_Click1(object sender, EventArgs e)
        {
            CreateShortcut(".net web开发框架", "http://www.baidu.com");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session["load"] = false;
            Response.Redirect("login.aspx");
        }

        protected void Btnhelp_Click(object sender, EventArgs e)
        {
            //btnHelp.OnClientClick = WindowADD.GetShowReference("ManagerInformation.aspx", "管理员消息");
            //Tab1.RefreshIFrame();
            //Alert.ShowInTop("nidainji");
        }
        //刷新主区域
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void WindowADD_Close(object sender, WindowCloseEventArgs e)
        {
            InitBtnUserfo();
        }

        //密码更改窗口关闭
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {
            if (e.CloseArgument.StartsWith("密码已更改"))
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference());
                Response.Redirect("login.aspx");
            }
        }




    }
}