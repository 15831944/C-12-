/**编写人：方淑云
 * 时间：2014年11月14号
 * 功能:文件查询界面后台
 * 修改履历：    1.修改人：吕博扬
 *                修改时间：10月10日
 *                修改内容：撤销静态变量page
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WDFramework.WithinPost
{
    public partial class Search_WithinPost : System.Web.UI.Page
    {
        //BLHelper.BLLFiles files = new BLHelper.BLLFile
        BLHelper.BLLAgency agency = new BLHelper.BLLAgency();
        BLHelper.BLLWithinPost wh = new BLHelper.BLLWithinPost();
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
            if (!IsPostBack)
            {
                btnAddFile.OnClientClick = Window_add.GetShowReference("AddData.aspx", "添加所内文件");
                InitDroplistFile();
                InitDropListAgency(); 
                InitData();
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Files.PageIndex) * Grid_Files.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
       
        //下载
        protected string GetEditUrl(object ID)
        {
            return DownLoad.GetShowReference("Operation.aspx?id=" + ID, "操作");
        }
        //界面初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.WithinPost> list = wh.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
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
                List<Common.Entities.Agency> list = agency.FindAllAgencyName();
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
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            DropDownListAgency.Reset();
            DropDownListFile.Reset();
            InitData();
        }

      
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_Files, CBoxSelect).Count(); i++)
                    {
                        int attachid = wh.Delete(Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0].ToString()));
                        string path = blat.FindPath(attachid);
                        pm.DeleteFile(attachid, path);
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_Files, CBoxSelect).Count(); i++)
                    {
                        wh.UpdateIsPass(Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "WithinPost";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //行点击事件
        protected void Grid_Files_RowCommand(object sender, FineUI.GridCommandEventArgs e)
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

        protected void Grid_Files_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
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
        //按部门搜索
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 1;
                //int AgencyID = agency.SelectAgencyID(DropDownListAgency.SelectedText);
                List<Common.Entities.WithinPost> listfile = wh.FindByAgency(DropDownListAgency.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
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
                List<Common.Entities.WithinPost> listfile = wh.FindByFileType(DropDownListFile.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
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
                List<Common.Entities.WithinPost> listfile = wh.FindAllInfo(DropDownListFile.SelectedText, DropDownListAgency.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
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
        //编辑选中行事件
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Files, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Files, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Files.DataKeys[pm.GridCount(Grid_Files, CBoxSelect)[0]][0]);
                        Session["WithinPostID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("EditData.aspx", "编辑文件信息"), Target.Top);
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

    }
}