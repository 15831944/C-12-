/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：学术报告界面后台（该报告属于学术会议）
 * 修改履历：
 *           
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.AcademicMeeting
{
    public partial class NewMeeting : System.Web.UI.Page
    {
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLScienceReport BLLReport = new BLHelper.BLLScienceReport();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog BLLOP = new BLHelper.BLLOperationLog();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        static int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int AcademicMeetingID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //btnAddReport.OnClientClick = Window_AddReport.GetShowReference("AddReport.aspx", "增加报告");
                BindAgencyName();//机构下拉框绑定
                BindReport();
            }
        }
        //学术报告中所属机构名称绑定
        public void BindAgencyName()
        {
            List<string> listAgencyName = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            if (listAgencyName != null)
            {
                for (int i = 0; i < listAgencyName.Count(); i++)
                    DropDownList_Agency.Items.Add(listAgencyName[i].ToString(), (i + 1).ToString());
            }
        }
        //搜索
        protected void btnCheckReport_Click(object sender, EventArgs e)
        {
            if (DropDownList_Agency.SelectedText == "全部")
                BindReport();
            else
                FindByAgency();
            btnDeleteReport.Enabled = false;
            Grid_ReportInfo.PageIndex = 0;
        }
        //刷新
        protected void btnRefreshReport_Click(object sender, EventArgs e)
        {
            DropDownList_Agency.SelectedValue = "0";
            btnDeleteReport.Enabled = false;
            Grid_ReportInfo.PageIndex = 0;
            Grid_ReportInfo.PageSize = 20;
            BindReport();

        }
        //删除
        protected void btnDeleteReport_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_ReportInfo.RecordCount / this.Grid_ReportInfo.PageSize));

                if (Grid_ReportInfo.PageIndex == Pages)
                    m = (Grid_ReportInfo.RecordCount - this.Grid_ReportInfo.PageSize * Grid_ReportInfo.PageIndex);
                else
                    m = this.Grid_ReportInfo.PageSize;
                List<int> selections = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect_Report.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {

                        int? attachid = BLLReport.Delete(Convert.ToInt32(Grid_ReportInfo.DataKeys[selections[i]][0].ToString()));
                        if (attachid != 0 && attachid != null)
                        {//删除附件
                            //在附件表中删除学术报告附件
                            BLLAttachment.Delete(Convert.ToInt32(attachid));
                            //删除附件文件
                            string path = BLLAttachment.FindPath(Convert.ToInt32(attachid));
                            if (path != "")
                                publicMethod.DeleteFile(Convert.ToInt32(attachid), path);
                        }
                    }
                    Grid_ReportInfo.PageIndex = 0;
                    Grid_ReportInfo.PageSize = 20;
                    btnDeleteReport.Enabled = false;
                    DropDownList_Agency.SelectedValue = "0";
                    BindReport();
                    Alert.ShowInTop("删除成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLReport.ChangePass(Convert.ToInt32(Grid_ReportInfo.DataKeys[selections[i]][0]), false);
                        //向操作日志表插入信息
                        Common.Entities.OperationLog operationLog = new Common.Entities.OperationLog();
                        operationLog.LoginIP = " ";
                        operationLog.LoginName = Session["LoginName"].ToString();
                        operationLog.OperationType = "删除";
                        operationLog.OperationContent = "ScienceReport";
                        operationLog.OperationTime = DateTime.Now;
                        operationLog.OperationDataID = Convert.ToInt32(Grid_ReportInfo.DataKeys[selections[i]][0]);
                        BLLOP.Insert(operationLog);
                    }
                    btnDeleteReport.Enabled = false;
                    DropDownList_Agency.SelectedValue = "0";
                    Grid_ReportInfo.PageIndex = 0;
                    Grid_ReportInfo.PageSize = 20;
                    BindReport();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }

        //分页
        protected void Grid_ReportInfo_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_ReportInfo.PageIndex = e.NewPageIndex;
            //BindReport();
            switch (page)
            {
                case 0:
                    BindReport();
                    break;
                case 1:
                    FindByAgency();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_ReportInfo.PageIndex = 0;
            this.Grid_ReportInfo.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindReport();
            switch (page)
            {
                case 0:
                    BindReport();
                    break;
                case 1:
                    FindByAgency();
                    break;
            }
        }
        protected void Grid_ReportInfo_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            string Person = Grid_ReportInfo.Rows[e.RowIndex].Values[2].ToString();
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                CBoxSelect_Report.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_ReportInfo.RecordCount / this.Grid_ReportInfo.PageSize));

            if (Grid_ReportInfo.PageIndex == Pages)
                m = (Grid_ReportInfo.RecordCount - this.Grid_ReportInfo.PageSize * Grid_ReportInfo.PageIndex);
            else
                m = this.Grid_ReportInfo.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect_Report.GetCheckedState(i))
                    selections.Add(i);
            }
            if (selections.Count == 0)
            {
                btnDeleteReport.Enabled = false;
                //Alert.ShowInTop("请至少选择一项!");
                return;
            }
            else
                btnDeleteReport.Enabled = true;
        }
        //报告绑定
        public void BindReport()
        {
            try
            {
                page = 0;
                int AcademicMeetingID = Convert.ToInt32(Request.QueryString["id"].ToString());
                List<Common.Entities.ScienceReport> scienceReportlist = BLLReport.FindPaged(AcademicMeetingID, Convert.ToInt32(Session["SecrecyLevel"]));
                var res = scienceReportlist.Skip(Grid_ReportInfo.PageIndex * Grid_ReportInfo.PageSize).Take(Grid_ReportInfo.PageSize).ToList();
                Grid_ReportInfo.RecordCount = scienceReportlist.Count();
                Grid_ReportInfo.DataSource = res;
                Grid_ReportInfo.DataBind();
                btnDeleteReport.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //按报告所属部门搜索报告信息 
        public void FindByAgency()
        {
            try
            {
                page = 1;
                Grid_ReportInfo.PageIndex = 0;
                int agencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                int AcademicMeetingID = Convert.ToInt32(Request.QueryString["id"].ToString());
                List<Common.Entities.ScienceReport> list = BLLReport.FindPaged(AcademicMeetingID, Convert.ToInt32(Session["SecrecyLevel"]));
                list = list.Where(p => p.AgencyID == agencyID).ToList();
                var res = list.Skip(Grid_ReportInfo.PageIndex * Grid_ReportInfo.PageSize).Take(Grid_ReportInfo.PageSize).ToList();
                Grid_ReportInfo.RecordCount = list.Count();
                Grid_ReportInfo.DataSource = res;
                Grid_ReportInfo.DataBind();
                btnDeleteReport.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //对机构ID转化为机构名称(学术报告中显示的)
        public string AgencyName(int agency)
        {
            try
            {
                if (agency != null)
                    return BLLAgency.FindAgenName(agency);
                //return agency.AgencyName.ToString();
                else
                    return " ";
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object ScienceReportID)
        {
            int ReportID = Convert.ToInt32(ScienceReportID);
            return Window_DownLoad.GetShowReference("DownLoad_Report.aspx?id=" + ReportID, "下载");
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_ReportInfo.PageIndex) * Grid_ReportInfo.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
    }
}