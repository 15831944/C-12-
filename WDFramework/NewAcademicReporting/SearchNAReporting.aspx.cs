using BLHelper;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.NewAcademicReporting
{
    public partial class SearchNAReporting : System.Web.UI.Page
    {
        BLLUser BLLUser = new BLLUser();
        BLLAgency BLLAgency = new BLLAgency();
        BLLOperationLog BLLOP = new BLLOperationLog();
        BLLNewAcademicReporting BLLNAR = new BLLNewAcademicReporting();
        BLLAttachment BllAttachment = new BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                btnAddLecture.OnClientClick = Window_addLecture.GetShowReference("AddNAReporting.aspx", "新增报告信息");
                BindData();

            }
        }

        //报告信息绑定
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.NewAcademicReporting> list = BLLNAR.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_NAReporting.PageIndex * Grid_NAReporting.PageSize).Take(Grid_NAReporting.PageSize).ToList();
                Grid_NAReporting.RecordCount = list.Count();
                Grid_NAReporting.DataSource = res;
                Grid_NAReporting.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        protected void Grid_NAReporting_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_NAReporting.PageIndex = e.NewPageIndex;
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBuRNHeadLine();
                    break;
                case 2:
                    FindByReportTime();
                    break;
                case 3:
                    FindByReportPeople();
                    break;
            }
        }
        protected void Grid_NAReporting_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //int LecturesID = Convert.ToInt32(Grid_NAReporting.DataKeys[e.RowIndex][0].ToString());
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            //Common.Entities.UnitLectures unitLectures = BLLNAR.FindByUnitLecturesID(LecturesID);
            string Person = Grid_NAReporting.Rows[e.RowIndex].Values[2].ToString();
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                CBoxSelect.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_NAReporting.RecordCount / this.Grid_NAReporting.PageSize));

            if (Grid_NAReporting.PageIndex == Pages)
                m = (Grid_NAReporting.RecordCount - this.Grid_NAReporting.PageSize * Grid_NAReporting.PageIndex);
            else
                m = this.Grid_NAReporting.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect.GetCheckedState(i))
                    selections.Add(i);
            }
            if (selections.Count == 0)
            {
                btnDelete.Enabled = false;
                //Alert.ShowInTop("请至少选择一项!");
                return;
            }
            else
                btnDelete.Enabled = true;
        }

        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            ddl_search.Reset();
            Grid_NAReporting.PageIndex = 0;
            Grid_NAReporting.PageSize = 20;
            BindData();
        }

        //编辑选中行
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = new List<int>();
                for (int i = 0; i < Grid_NAReporting.RecordCount; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_NAReporting.DataKeys[selections[0]][0]);
                        Session["NewAcademicReportingID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("UpdateNAReporting.aspx", "编辑报告信息"), Target.Top);
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
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //删除选中的单位讲学信息
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = new List<int>();
                for (int i = 0; i < Grid_NAReporting.RecordCount; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int NAReportingID = Convert.ToInt32(Grid_NAReporting.DataKeys[selections[i]][0].ToString());
                        Common.Entities.NewAcademicReporting newacademicreporting = BLLNAR.FindByNAReportingID(NAReportingID);
                        if (newacademicreporting.AttachmentID != null)
                        {
                            if (BllAttachment.SelectAttachmentName(Convert.ToInt32(newacademicreporting.AttachmentID)) != "")
                            {
                                //删除附件文件
                                string path = BllAttachment.FindPath(Convert.ToInt32(newacademicreporting.AttachmentID));
                                if (path != "")
                                {
                                    publicMethod.DeleteFile(Convert.ToInt32(newacademicreporting.AttachmentID), path);
                                    //删除附件表中的数据
                                    BllAttachment.Delete(Convert.ToInt32(newacademicreporting.AttachmentID));//删除成功返回true    
                                }
                            }
                        }
                        //删除单位讲学信息
                        BLLNAR.Delete(NAReportingID);//删除成功返回true     
                    }
                    Grid_NAReporting.PageIndex = 0;
                    Grid_NAReporting.PageSize = 20;                 
                    btnDelete.Enabled = false;
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        //BLLNAR.ChangePass(Convert.ToInt32(Grid_NAReporting.DataKeys[selections[i]][0]), false);
                        operate.LoginName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "NewAcademicReporting";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_NAReporting.DataKeys[selections[i]][0]);
                        BLLOP.Insert(operate);
                    }
                    Grid_NAReporting.PageIndex = 0;
                    Grid_NAReporting.PageSize = 20;
                    //DropDownList_Agency.SelectedValue = "0";
                    btnDelete.Enabled = false;
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("操作已经提交，请等待管理员确认!");
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        //每页记录数
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_NAReporting.PageIndex = 0;
            this.Grid_NAReporting.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBuRNHeadLine();
                    break;
                case 2:
                    FindByReportTime();
                    break;
                case 3:
                    FindByReportPeople();
                    break;
            }
        }

        //下载界面跳转
        protected string GetRecordUrlDown(object NAReportingID)
        {
            int nareportingID = Convert.ToInt32(NAReportingID);
            return Window_DownLoad.GetShowReference("DownloadNAReporting.aspx?id=" + nareportingID, "下载");
        }
        //备注界面跳转
        protected string GetEditUrl(object NAReportingID)
        {
            return Window_Remark.GetShowReference("RemarkNAReporting.aspx?id=" + NAReportingID, "备注");
        }

        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_NAReporting.PageIndex) * Grid_NAReporting.PageSize;
        }

        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //搜索
        protected void btnCheck_Click1(object sender, EventArgs e)
        {
            Grid_NAReporting.PageIndex = 0;
            try
            {
                if (txtReportName.Text.Trim() != "")
                {
                    switch(ddl_search.SelectedIndex)
                    {
                        case 0:
                            Alert.ShowInTop("请选择查询条件！");
                            return;
                        case 1:
                            FindBuRNHeadLine();
                            break;
                        case 2:
                            FindByReportTime();
                            break;
                        case 3:
                            FindByReportPeople();
                            break;
                    }
                }
                else
                {
                    Alert.ShowInTop("请输入查询条件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //根据报告名称查询
        public void FindBuRNHeadLine()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.NewAcademicReporting> list = BLLNAR.FindByRN(txtReportName.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_NAReporting.PageIndex * Grid_NAReporting.PageSize).Take(Grid_NAReporting.PageSize).ToList();
                Grid_NAReporting.RecordCount = list.Count();
                this.Grid_NAReporting.DataSource = res;
                this.Grid_NAReporting.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //根据报告时间查询
        public void FindByReportTime()
        {
            try
            {
                ViewState["page"] = 2;
                int year = Convert.ToInt32(txtReportName.Text.Trim());
                List<Common.Entities.NewAcademicReporting> list = BLLNAR.FindByReportTime(year, Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_NAReporting.PageIndex * Grid_NAReporting.PageSize).Take(Grid_NAReporting.PageSize).ToList();
                Grid_NAReporting.RecordCount = list.Count();
                this.Grid_NAReporting.DataSource = res;
                this.Grid_NAReporting.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //根据报告人查询
        public void FindByReportPeople()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.NewAcademicReporting> list = BLLNAR.FindByReportPeople(txtReportName.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_NAReporting.PageIndex * Grid_NAReporting.PageSize).Take(Grid_NAReporting.PageSize).ToList();
                Grid_NAReporting.RecordCount = list.Count();
                this.Grid_NAReporting.DataSource = res;
                this.Grid_NAReporting.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }


        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_NAReporting.SelectAllRows();
            int[] select = Grid_NAReporting.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_NAReporting.RecordCount / this.Grid_NAReporting.PageSize));

            if (Grid_NAReporting.PageIndex == Pages)
                m = (Grid_NAReporting.RecordCount - this.Grid_NAReporting.PageSize * Grid_NAReporting.PageIndex);
            else
                m = this.Grid_NAReporting.PageSize;
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
    }
}