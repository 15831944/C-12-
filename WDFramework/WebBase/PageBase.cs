/*
 * 705170100@qq.com
 * 本界面是个界面的基类 完成界面初始功能
 */
using BLHelper;
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WDFramework.WebBase
{
    public class PageBase : System.Web.UI.Page
    {
        public int isOpen = 0;
        /// <summary>
        /// 本页面对应实体类的名字
        /// </summary>
        protected string TableName;

        /// <summary>
        /// 页面唯一编号
        /// </summary>
        protected string PageBH;

        /// <summary>
        /// 页面上的表格
        /// </summary>
        protected FineUI.Grid Grid_Info;

        /// <summary>
        /// 顶部的工具栏
        /// </summary>
        protected FineUI.Toolbar Toolbar_top;

        /// <summary>
        /// 是否能编辑
        /// </summary>
        protected bool CannotEdit = true;

        /// <summary>
        /// toolbar的数据源
        /// </summary>
        protected List<ToolBar> ToolBarSources;
        protected object Gridsource;
        protected global::FineUI.Window Window1;
        protected global::FineUI.Window Window2;
        protected global::FineUI.Label labResult;//
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            //未登录则跳转
            if (Session["load"] == null || (bool)Session["load"] != true)
            {
                // Response.Redirect("~/login.aspx");
            }
            InitToolbar();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (Grid_Info != null)
            {
                GridColumn genderColumn = Grid_Info.FindColumn("editField");
                if (genderColumn != null)
                {
                    genderColumn.Hidden = this.CannotEdit;
                }
            }
            base.OnLoad(e);
        }

        /// <summary>
        /// 初始化工具条
        /// </summary>
        protected virtual void InitToolbar()
        {

            BLLToolBar btb = new BLLToolBar();
            int roleID = Convert.ToInt32(Session["RoleID"]);
          //  this.ToolBarSources = btb.FindAllByRole(roleID, this.PageBH);

            if (this.PageBH == null || this.PageBH == "")
            {
                return;
            }
            else
            {
                foreach (var tb in ToolBarSources)
                {
                    switch (tb.TBtype)
                    {
                        case "Button":
                            FineUI.Button btn = new FineUI.Button();
                            btn.ID = tb.TBname;
                            btn.Text = tb.TBtext;
                            btn.ToolTip = tb.Tooltip;
                            btn.EnablePostBack = true;
                            btn.IconUrl ="~/icon/"+ tb.IconUrl;
                            if (Window1 != null && tb.OpenWindow == "True")
                            {
                                if (tb.WindwText == "查询")
                                {
                                    btn.OnClientClick = Window2.GetShowReference(tb.WindowUrl, tb.WindwText);

                                }
                                else
                                    btn.OnClientClick = Window1.GetShowReference(tb.WindowUrl, tb.WindwText);
                            }
                            if (tb.TBevent != null && tb.TBevent != "")
                            {
                                btn.Click += (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), this, GetType().GetMethod(tb.TBevent, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
                            }
                            this.Toolbar_top.Items.Add(btn);
                            break;

                        case "Edit":
                            this.CannotEdit = false;
                            break;

                    }
                }
            }
        }
        /// <summary>
        /// 初始化表格
        /// </summary>
        protected virtual void InitGrid()
        {
            Grid_Info.DataSource = Gridsource;
            Grid_Info.DataBind();
            //Alert.Show("初始化表格");
        }


        protected virtual void btn_searchClick(object sender, EventArgs e)
        {
            Alert.Show("这是查询");
        }
        protected virtual void btn_RefreshClick(object sender, EventArgs e)
        {
            InitGrid();
        }
        protected virtual void btn_AddClick(object sender, EventArgs e)
        {
            Alert.Show("这是添加");
        }
        protected virtual void btn_DeleteClick(object sender, EventArgs e)
        {
            if (Grid_Info.SelectedRowIndexArray.Count() > 0)
            {
                Alert.Show("删除" + Grid_Info.DataKeys[Grid_Info.SelectedRowIndex][0]);
            }
        }

        protected virtual void UserEvent(object sender, EventArgs e)
        {
            Alert.Show("自定义事件处理");
        }






    }
}