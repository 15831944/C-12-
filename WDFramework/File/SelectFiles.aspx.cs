/**编写人：方淑云
 * 时间：2014年8月1号
 * 功能:文件查询界面后台
 * 修改履历：1.时间：8月7日
 *            修改人：方淑云
 *            修改内容：修改删除添加的权限
 *          2.修改人：吕博扬
 *            修改时间：10月10日
 *            修改内容：撤销静态变量page
 **/
using Common.Entities;
using DataBase;
using FineUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class 查询页面 : System.Web.UI.Page
    {
        BLHelper.BLLFiles files = new BLHelper.BLLFiles();
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment blat = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        OperationLog operate = new OperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {

                btnAddFile.OnClientClick = Window_add.GetShowReference("ADD.aspx", "添加文件");
                InitDroplistFile();
                InitDropListAgency();
                InitData();             
            }
        }
       
        //界面初始化
        public void InitData()
         {
             try
             {
                 ViewState["page"] = 0;
                 List<Files> list = files.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                 Grid_Files.RecordCount = list.Count();

                 if (list != null)
                 {
                     this.Grid_Files.DataSource = list.Skip(Grid_Files.PageIndex * Grid_Files.PageSize).Take(Grid_Files.PageSize);
                     this.Grid_Files.DataBind();
                 }
                 else
                 {
                     return;
                 }
                 btnDelete.Enabled = false;
             }
             catch (Exception ex)
             {
                 pm.SaveError(ex, this.Request);
             }
        }
        //初始化文件下拉框
        public void InitDroplistFile()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("文件分类名称");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListFile.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        
        }
        //初始化机构下拉框
        public void InitDropListAgency()
        {
            try
            {
                List<Agency> list = agency.FindAllAgencyName();
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListAgency.Items.Add(list[i].AgencyName.ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //分页
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Files.PageIndex = 0;
            this.Grid_Files.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                 InitData();
                 break;
                case 1:
                 FindByAgency();
                 break;
                case 2:
                 FindBySort();
                 break;
                case 3:
                 FindByAS();
                 break;
            }
        }
        protected void People_Info_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Files.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByAgency();
                    break;
                case 2:
                    FindBySort();
                    break;
                case 3:
                    FindByAS();
                    break;
            }
        }
      
        //将机构ID转化为机构名
        protected string FindAgencyName(int ag)
        {
            try
            {
                return agency.FindAgenName(ag);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            DropDownListAgency.Reset();
            DropDownListFile.Reset();
            InitData();
        }
        //删除选择行的点击事件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_Files, CBoxSelect).Count(); i++)
                    {
                        int attachid = files.Delete(Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0].ToString()));
                        string path = blat.FindPath(attachid);
                        pm.DeleteFile(attachid, path);
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                    btnSelect_All.Text = "全选";
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_Files, CBoxSelect).Count(); i++)
                    {
                        files.ChangePass(Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Files";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                    btnSelect_All.Text = "全选";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
         //按部门搜索
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 1;
                int AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                List<Files> listfile = files.FindByAgencyID(AgencyID, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Files.RecordCount = listfile.Count();//改变分页数
                if (listfile != null)
                {
                    this.Grid_Files.DataSource = listfile.Skip(Grid_Files.PageIndex * Grid_Files.PageSize).Take(Grid_Files.PageSize);
                    this.Grid_Files.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按文件分类搜索
        public void FindBySort()
        {
            try
            {
                ViewState["page"] = 2;
                List<Files> listfile = files.FindByDocumentCategoryID(DropDownListFile.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Files.RecordCount = listfile.Count();//改变分页数
                if (listfile != null)
                {
                    this.Grid_Files.DataSource = listfile.Skip(Grid_Files.PageIndex * Grid_Files.PageSize).Take(Grid_Files.PageSize);
                    this.Grid_Files.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按分类和部门搜索
        public void FindByAS()
        {
            try
            {
                ViewState["page"] = 3;
                int AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                List<Files> listfile = files.FindAllInfo(DropDownListFile.SelectedText, AgencyID, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Files.RecordCount = listfile.Count();//改变分页数
                if (listfile != null)
                {
                    this.Grid_Files.DataSource = listfile.Skip(Grid_Files.PageIndex * Grid_Files.PageSize).Take(Grid_Files.PageSize);
                    this.Grid_Files.DataBind();
                }
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //搜索
        protected void Select_Click(object sender, EventArgs e)
        {
            try
            {
                Grid_Files.PageIndex = 0;
                if (DropDownListFile.SelectedText == "全部")
                {
                    if (DropDownListAgency.SelectedText == "全部")
                    {
                        InitData();
                    }
                    else
                    {
                        FindByAgency();
                    }
                }
                else
                {
                    if (DropDownListAgency.SelectedText == "全部")
                    {
                        FindBySort();
                    }
                    else
                    {
                        FindByAS();
                    }
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

     
        //编辑选中行
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Files, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Files, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[0]][0]);
                        Session["FileID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("Update_File.aspx", "编辑文件信息"), Target.Top);
                    }
                    else
                    {
                        Alert.Show("一次仅可以对一行进行编辑！");
                    }
                }
                else
                {

                    Alert.Show("请选择一行！");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //下载
        protected string GetEditUrl(object ID)
        {
            return DownLoad.GetShowReference("Operate.aspx?id=" + ID, "操作");
        }
        //行点击事件
        protected void Grid_Files_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_Files.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (pm.GridCount(Grid_Files, CBoxSelect).Count == 0)
                {

                    btnDelete.Enabled = false;
                    return;
                }
                if (pm.GridCount(Grid_Files, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        
                 
        }
       //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Files.PageIndex) * Grid_Files.PageSize;
        }
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_Files.SelectAllRows();
            int[] select = Grid_Files.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Files.RecordCount / this.Grid_Files.PageSize));

            if (Grid_Files.PageIndex == Pages)
                m = (Grid_Files.RecordCount - this.Grid_Files.PageSize * Grid_Files.PageIndex);
            else
                m = this.Grid_Files.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
        //清空数据库中表数据
        protected void btn_delete_all_Click(object sender, EventArgs e)
        {

        }
    }
}