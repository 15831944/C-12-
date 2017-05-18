using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Search_WorkPlanSummary : System.Web.UI.Page
    {
        //	分类 0:个人 1:部门
        BLHelper.BLLWorkPlanSummary BLLWorkPlanSummary = new BLHelper.BLLWorkPlanSummary();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BllAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
                btnSelect_All.Text = "全选";
                //删除数据
                // btnDelete.OnClientClick = Grid_WorkPlanSummary.GetNoSelectionAlertReference("请至少选择一项！");
                btnAddPlan.OnClientClick = Window_AddPlan.GetShowReference("Add_WorkPlanSummary.aspx", "新增信息");
                BindData();
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.WorkPlanSummary> list = new List<Common.Entities.WorkPlanSummary>();
                list = BLLWorkPlanSummary.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                if (list == null)
                    Alert.ShowInTop("无信息！");
                else
                {
                    var res = list.Skip(Grid_WorkPlanSummary.PageIndex * Grid_WorkPlanSummary.PageSize).Take(Grid_WorkPlanSummary.PageSize).ToList();
                    Grid_WorkPlanSummary.RecordCount = list.Count();
                    this.Grid_WorkPlanSummary.DataSource = res;
                    this.Grid_WorkPlanSummary.DataBind();
                    btnDelete.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //对下拉框的绑定
        public void BindDropDowmList()
        {
            DropDownList.Reset();
            DropDownList.Items.Clear();
            DropDownList.Items.Add("全部", "0");
            switch (DropDownListSort.SelectedText)
            {
                case "全部":
                    break;
                case "个人/部门":
                    InitDropDownListSort();
                    break;
                case "机构":
                    DropDownList.EnableEdit = false;
                    List<Common.Entities.Agency> listAgency = BLLAgency.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int i = 0; i < listAgency.Count(); i++)
                    {
                        DropDownList.Items.Add(listAgency[i].AgencyName, (i + 1).ToString());
                    }
                    break;
                case "人员":

                    List<string> listUserName = BLLUser.FindUserBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                    for (int i = 0; i < listUserName.Count(); i++)
                    {
                        DropDownList.Items.Add(listUserName[i], (i + 1).ToString());
                    }
                    break;
                case "年份":

                    //List<int> listYear = BLLWorkPlanSummary.GetYear();
                    List<int> list = new List<int>();
                    for (int i = 1960; i < 2061; i++)
                        list.Add(i);
                    for (int i = 0; i < list.Count(); i++)
                        DropDownList.Items.Add(list[i].ToString(), (i + 1).ToString());
                    break;
            }
        }

        //WorkPlanSummaryID查询个人或部门工作计划与总结
        public string Sort(int WorkPlanSummaryID)
        {
            try
            {
                //return null;
                Common.Entities.WorkPlanSummary workPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(WorkPlanSummaryID);
                if (workPlanSummary.Sort == "个人")
                {
                    //return "个人";
                    return BLLUser.FindUserName(workPlanSummary.UserInfoID);
                }
                else
                {
                    //return "部门";
                    return BLLAgency.FindAgenName(workPlanSummary.AgencyID);
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //对时间进行截取
        //public string Time(DateTime datetime)
        //{
        //    if (datetime != null)
        //        return datetime.Year + "-" + datetime.Month + "-" + datetime.Day;
        //    else
        //        return " ";
        //}
        //分页
        protected void Grid_WorkPlanSummary_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_WorkPlanSummary.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBySort();
                    break;
                case 2:
                    FindByAgency();
                    break;
                case 3:
                    FindByStaff();
                    break;
                case 4:
                    FindByYear();
                    break;
            }
        }

        protected void Grid_WorkPlanSummary_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            string Person = Grid_WorkPlanSummary.Rows[e.RowIndex].Values[2].ToString();
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                BoxSelect.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_WorkPlanSummary.RecordCount / this.Grid_WorkPlanSummary.PageSize));

            if (Grid_WorkPlanSummary.PageIndex == Pages)
                m = (Grid_WorkPlanSummary.RecordCount - this.Grid_WorkPlanSummary.PageSize * Grid_WorkPlanSummary.PageIndex);
            else
                m = this.Grid_WorkPlanSummary.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect.GetCheckedState(i))
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

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_WorkPlanSummary.PageIndex = 0;
            this.Grid_WorkPlanSummary.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindBySort();
                    break;
                case 2:
                    FindByAgency();
                    break;
                case 3:
                    FindByStaff();
                    break;
                case 4:
                    FindByYear();
                    break;
            }
        }
        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string strSort = DropDownListSort.SelectedText;
            //if (DropDownList.SelectedText == "全部")
            //{
            //    BindData();
            //    return;
            //}
            switch (strSort)
            {
                case "全部":
                    BindData();
                    //switch (DropDownList.SelectedText)
                    //{ 
                    //    case "个人/部门":
                    //        break;
                    //    case "机构":
                    //        break;
                    //    case "人员":
                    //        break;
                    //    case "年份":
                    //        break;
                    //}
                    break;
                case "个人/部门":
                    FindBySort();
                    break;
                case "机构":
                    FindByAgency();
                    break;
                case "人员":
                    FindByStaff();
                    break;
                case "年份":
                    FindByYear();
                    break;
            }
            btnDelete.Enabled = false;
            Grid_WorkPlanSummary.PageIndex = 0;//搜索后跳回第一页
        }

        //分类下拉框所选项改变
        //protected void DropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    switch (DropDownListSort.SelectedText)
        //    {
        //        case "请选择":
        //            txtSort.Reset();
        //            txtSort.EmptyText = "请选择分类";
        //            break;
        //        case "分类":
        //            txtSort.Reset();
        //            txtSort.EmptyText = "个人/部门";
        //            break;
        //        case "机构":
        //            txtSort.Reset();
        //            txtSort.EmptyText = "机构名";
        //            break;
        //        case "人员":
        //            txtSort.Reset();
        //            txtSort.EmptyText = "人员名";
        //            break;
        //        case "时间":
        //            txtSort.Reset();
        //            txtSort.EmptyText = "年份";
        //            break;
        //    }
        //}
        public void Refresh()
        {

            DropDownListSort.SelectedValue = "0";
            //txtSort.Reset();
            DropDownList.Reset();
            BindData();
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            Grid_WorkPlanSummary.PageIndex = 0;
            Grid_WorkPlanSummary.PageSize = 20;
            Refresh();
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_WorkPlanSummary.RecordCount / this.Grid_WorkPlanSummary.PageSize));
                List<int> selections = new List<int>();
                if (Grid_WorkPlanSummary.PageIndex == Pages)
                    m = (Grid_WorkPlanSummary.RecordCount - this.Grid_WorkPlanSummary.PageSize * Grid_WorkPlanSummary.PageIndex);
                else
                    m = this.Grid_WorkPlanSummary.PageSize;
                for (int i = 0; i < m; i++)
                {
                    if (BoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }

                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    //for (int i = 0; i < selections.Count(); i++)
                    //{
                    //    BLLWorkPlanSummary.Delete(Convert.ToInt32(Grid_WorkPlanSummary.DataKeys[selections[i]][0].ToString()));
                    //    //publicMethod.DeleteFile(
                    //}
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int workPlanSummaryID = Convert.ToInt32(Grid_WorkPlanSummary.DataKeys[selections[i]][0].ToString());
                        Common.Entities.WorkPlanSummary workPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(workPlanSummaryID);
                        //if (BllAttachment.SelectAttachmentName(Convert.ToInt32(workPlanSummary.Attachment)) == "")
                        //{
                        //    Alert.ShowInTop("该不存在!");
                        //    return;
                        //}
                        //else
                        //{
                        if (BllAttachment.SelectAttachmentName(Convert.ToInt32(workPlanSummary.Attachment)) != "")
                        {
                            //删除附件文件
                            string path = BllAttachment.FindPath(Convert.ToInt32(workPlanSummary.Attachment));
                            if (path != "")
                            {
                                publicMethod.DeleteFile(Convert.ToInt32(workPlanSummary.Attachment), path);
                                //删除附件表中的数据
                                BllAttachment.Delete(Convert.ToInt32(workPlanSummary.Attachment));//删除成功返回true       
                            }
                        }
                        //删除工作计划总结
                        BLLWorkPlanSummary.Delete(workPlanSummaryID);//删除成功返回true
                        btnDelete.Enabled = false;
                        Grid_WorkPlanSummary.PageIndex = 0;
                        Grid_WorkPlanSummary.PageSize = 20;
                        Refresh();
                        btnSelect_All.Text = "全选";
                        Alert.ShowInTop("删除数据成功!");
                        //}

                    }
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLWorkPlanSummary.UpdateIsPass(Convert.ToInt32(Grid_WorkPlanSummary.DataKeys[selections[i]][0]), false);
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "WorkPlanSummary";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_WorkPlanSummary.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                    }
                    Grid_WorkPlanSummary.PageIndex = 0;
                    Grid_WorkPlanSummary.PageSize = 20;
                    btnDelete.Enabled = false;
                    Refresh();
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
        //按分类搜索(个人、部门)
        public void FindBySort()
        {
            try
            {
                ViewState["page"] = 1;
                Grid_WorkPlanSummary.PageIndex = 0;
                List<Common.Entities.WorkPlanSummary> list = new List<Common.Entities.WorkPlanSummary>();
                if (DropDownList.SelectedText != "全部")
                    list = BLLWorkPlanSummary.FindBySort(DropDownList.SelectedText, Convert.ToInt32(Session["SecrecyLevel"]));
                else
                    list = BLLWorkPlanSummary.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                //if (DropDownListSort.SelectedText == "个人")
                //    list = BLLWorkPlanSummary.FindBySort(false, Convert.ToInt32(Session["SecrecyLevel"]));
                //else
                //    list = BLLWorkPlanSummary.FindBySort(true, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_WorkPlanSummary.RecordCount = list.Count();//改变分页数
                var res = list.Skip(Grid_WorkPlanSummary.PageIndex * Grid_WorkPlanSummary.PageSize).Take(Grid_WorkPlanSummary.PageSize).ToList();
                this.Grid_WorkPlanSummary.DataSource = res;
                this.Grid_WorkPlanSummary.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //按机构搜索
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 2;
                Grid_WorkPlanSummary.PageIndex = 0;
                List<Common.Entities.WorkPlanSummary> list = new List<Common.Entities.WorkPlanSummary>();
                if (DropDownList.SelectedText == "全部")
                    list = BLLWorkPlanSummary.FindBySort("部门", Convert.ToInt32(Session["SecrecyLevel"]));
                //list = BLLWorkPlanSummary.FindByAgencyID(AgencyID, Convert.ToInt32(Session["SecrecyLevel"]));
                else
                {
                    int AgencyID = BLLAgency.SelectAgencyID(DropDownList.SelectedText);//AgencyID = 0不存在该机构
                    list = BLLWorkPlanSummary.FindByAgencyID(AgencyID, Convert.ToInt32(Session["SecrecyLevel"]));
                }
                Grid_WorkPlanSummary.RecordCount = list.Count();//改变分页数
                var res = list.Skip(Grid_WorkPlanSummary.PageIndex * Grid_WorkPlanSummary.PageSize).Take(Grid_WorkPlanSummary.PageSize).ToList();
                this.Grid_WorkPlanSummary.DataSource = res;
                this.Grid_WorkPlanSummary.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }

        }
        //按人员搜索
        public void FindByStaff()
        {
            try
            {
                ViewState["page"] = 3;
                Grid_WorkPlanSummary.PageIndex = 0;
                List<Common.Entities.WorkPlanSummary> list = new List<Common.Entities.WorkPlanSummary>();
                if (DropDownList.SelectedText == "全部")
                    list = BLLWorkPlanSummary.FindBySort("个人", Convert.ToInt32(Session["SecrecyLevel"]));
                //list = BLLWorkPlanSummary.FindByUserInfoID(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                else
                {
                    int UserInfoID = BLLUser.FindID(DropDownList.SelectedText);//
                    list = BLLWorkPlanSummary.FindByUserInfoID(UserInfoID, Convert.ToInt32(Session["SecrecyLevel"]));
                }
                Grid_WorkPlanSummary.RecordCount = list.Count();//改变分页数
                var res = list.Skip(Grid_WorkPlanSummary.PageIndex * Grid_WorkPlanSummary.PageSize).Take(Grid_WorkPlanSummary.PageSize).ToList();
                this.Grid_WorkPlanSummary.DataSource = res;
                this.Grid_WorkPlanSummary.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //按年份搜索
        public void FindByYear()
        {
            try
            {
                ViewState["page"] = 4;
                Grid_WorkPlanSummary.PageIndex = 0;
                List<Common.Entities.WorkPlanSummary> list = new List<Common.Entities.WorkPlanSummary>();
                if (DropDownList.SelectedText != "全部")
                {
                    int Year = Convert.ToInt32(DropDownList.SelectedText);
                    list = BLLWorkPlanSummary.FindByYear(Year, Convert.ToInt32(Session["SecrecyLevel"]));
                }
                else
                    list = BLLWorkPlanSummary.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_WorkPlanSummary.RecordCount = list.Count();//改变分页数
                var res = list.Skip(Grid_WorkPlanSummary.PageIndex * Grid_WorkPlanSummary.PageSize).Take(Grid_WorkPlanSummary.PageSize).ToList();
                this.Grid_WorkPlanSummary.DataSource = res;
                this.Grid_WorkPlanSummary.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
          
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object WorkPlanSummaryID)
        {
            int workPlanSummaryID = Convert.ToInt32(WorkPlanSummaryID);
            if (workPlanSummaryID != -3)
            {
                Common.Entities.WorkPlanSummary workPlanSummary = BLLWorkPlanSummary.FindByWorkPlanSummaryID(workPlanSummaryID);
                return Window_DownLoad.GetShowReference("DownLoadWPS.aspx?id=" + WorkPlanSummaryID, "下载");
            }
            else
                return Window_NoLibraryMessage.GetShowReference("/ContractAndPact/NoLibraryMessage.aspx?", "该资料不可下载");

        }

        protected void DropDownListSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDowmList();
        }
        //初始化工作计划与总结分类下拉框
        public void InitDropDownListSort()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("工作计划与总结分类");
            //DropDownList.Items.Add("全部", "0");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownList.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_WorkPlanSummary.PageIndex) * Grid_WorkPlanSummary.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_WorkPlanSummary.SelectAllRows();
            int[] select = Grid_WorkPlanSummary.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_WorkPlanSummary.RecordCount / this.Grid_WorkPlanSummary.PageSize));

            if (Grid_WorkPlanSummary.PageIndex == Pages)
                m = (Grid_WorkPlanSummary.RecordCount - this.Grid_WorkPlanSummary.PageSize * Grid_WorkPlanSummary.PageIndex);
            else
                m = this.Grid_WorkPlanSummary.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    BoxSelect.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    BoxSelect.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }

        //修改按钮
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicMethod.GridCount(Grid_WorkPlanSummary, BoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_WorkPlanSummary.DataKeys[selections[0]][0]);
                        Session["WorkPlanSummary"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_WorkPlanSummary.aspx"), Target.Top);
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
                publicMethod.SaveError(ex, this.Request);
            }
        }

    }
}