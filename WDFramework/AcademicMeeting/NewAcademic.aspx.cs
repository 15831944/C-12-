/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：学术会议界面后台
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年11月29日
 *                 修改内容：新增编辑功能、照片上传功能
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;


namespace WDFramework.AcademicMeeting
{
    public partial class NewAcademic : System.Web.UI.Page
    {
        BLHelper.BLLAcademicMeeting BLLAcademic = new BLHelper.BLLAcademicMeeting();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLAttendMeeting BLLAttend = new BLHelper.BLLAttendMeeting();
        BLHelper.BLLAgency BLLAgency = new BLHelper.BLLAgency();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLScienceReport BLLReport = new BLHelper.BLLScienceReport();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        private int page;//用于不同情况下Grid绑定的分页
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            btnSelect_All.Text = "全选";
            if (!IsPostBack)
            {
                btnAddMeeting.OnClientClick = Window_AddMeeting.GetShowReference("AddMeeting.aspx", "增加会议");//弹出增加学术会议界面
                btnAddReport.OnClientClick = Window_AddReport.GetShowReference("AddReport.aspx", "增加报告");//弹出增加学术报告界面
                BindYear();//年份下拉框绑定
                BindUser();//科研人员下拉框绑定
                BindData();//数据初始化 对Grid进行数据绑定

            }
        }
        //年份下拉框绑定
        public void BindYear()
        {
            //年份为1960-2060 写死的
            List<int> list = new List<int>();
            for (int i = 1960; i < 2061; i++)
                list.Add(i);
            for (int i = 0; i < list.Count(); i++)
                DropDownList_Year.Items.Add(list[i].ToString(), (i + 1).ToString());//对年份下拉框的绑定

        }
        //科研人员下拉框绑定
        public void BindUser()
        {
            try
            {
                //根据用户的登陆级别 查询保密级别小于等于该用户级别的用户名称 对用户名称下拉框进行绑定
                List<string> listUserName = BLLUser.FindUserBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
                if (listUserName != null)
                {
                    for (int i = 0; i < listUserName.Count(); i++)
                        DropDownList_User.Items.Add(listUserName[i].ToString(), (i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                //抛出异常后写入错误日志中
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }
        //数据绑定
        public void BindData()
        {
            try
            {
                DropDownList_User.Reset();
                DropDownList_Year.Reset();
                //DropDownList_Year.SelectedValue = "0";//年份绑定初始化
                //DropDownList_User.SelectedValue = "0";//人员名称绑定初始化
                ViewState["page"] = 0;
                //查找小于等于该用户保密级别的所有会议信息
                List<Common.Entities.AcademicMeeting> list = BLLAcademic.FindAll(Convert.ToInt32(Session["SecrecyLevel"]));
                //分页处理
                var res = list.Skip(Grid_MeetingName.PageIndex * Grid_MeetingName.PageSize).Take(Grid_MeetingName.PageSize).ToList();
                Grid_MeetingName.RecordCount = list.Count();
                this.Grid_MeetingName.DataSource = res;
                this.Grid_MeetingName.DataBind();
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
                ViewState["page"] = 1;
                Grid_MeetingName.PageIndex = 0;
                List<Common.Entities.AcademicMeeting> list = BLLAcademic.FindByYear(Convert.ToInt32(DropDownList_Year.SelectedText), (Convert.ToInt32(Session["SecrecyLevel"])));
                var res = list.Skip(Grid_MeetingName.PageIndex * Grid_MeetingName.PageSize).Take(Grid_MeetingName.PageSize).ToList();
                Grid_MeetingName.RecordCount = list.Count();
                this.Grid_MeetingName.DataSource = res;
                this.Grid_MeetingName.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //按科研人员搜索
        public void FindByUserName()
        {
            try
            {
                ViewState["page"] = 2;
                Grid_MeetingName.PageIndex = 0;
                //查找用户名对应的人员ID
                int UserID = BLLUser.FindID(DropDownList_User.SelectedText);
                //查找人员参加的会议ID
                List<int?> listMeetingID = BLLAttend.FindMeetingIDByUserID(UserID, (Convert.ToInt32(Session["SecrecyLevel"])));
                //查找人员参加的会议信息
                List<Common.Entities.AcademicMeeting> list = BLLAcademic.FindAcademicMeetingByMeetingID(listMeetingID, (Convert.ToInt32(Session["SecrecyLevel"])));
                List<int?> listID = new List<int?>();
                for (int i = 0; i < list.Count(); i++)
                    listID.Add(list[i].AcademicMeetingID);
                list = BLLAcademic.FindAcademicMeetingByMeetingID(listID, (Convert.ToInt32(Session["SecrecyLevel"])));
                var res = list.Skip(Grid_MeetingName.PageIndex * Grid_MeetingName.PageSize).Take(Grid_MeetingName.PageSize).ToList();
                Grid_MeetingName.RecordCount = list.Count();
                this.Grid_MeetingName.DataSource = res;
                this.Grid_MeetingName.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //根据年份和科研人员搜索
        public void FindByUserAndYear()
        {
            try
            {
                ViewState["page"] = 3;
                Grid_MeetingName.PageIndex = 0;
                //查找用户名对应的人员ID
                int UserID = BLLUser.FindID(DropDownList_User.SelectedText);
                //查找人员参加的会议ID
                List<int?> listMeetingID = BLLAttend.FindMeetingIDByUserID(UserID, (Convert.ToInt32(Session["SecrecyLevel"])));

                //查找人员参加的会议信息
                List<Common.Entities.AcademicMeeting> list = BLLAcademic.FindAcademicMeetingByMeetingID(listMeetingID, (Convert.ToInt32(Session["SecrecyLevel"])));
                List<int?> listID = new List<int?>();
                for (int i = 0; i < list.Count(); i++)
                    listID.Add(list[i].AcademicMeetingID);
                list = BLLAcademic.FindAcademicMeetingByMeetingID(listID, (Convert.ToInt32(Session["SecrecyLevel"])));
                list = BLLAcademic.FindByUserAndYear(list, Convert.ToInt32(DropDownList_Year.SelectedText));
                var res = list.Skip(Grid_MeetingName.PageIndex * Grid_MeetingName.PageSize).Take(Grid_MeetingName.PageSize).ToList();
                Grid_MeetingName.RecordCount = list.Count();
                this.Grid_MeetingName.DataSource = res;
                this.Grid_MeetingName.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        protected void Grid_MeetingName_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            //checkbox选择 如果不是录入人或超级管理员操作，则没有操作权限;若录入人不是超级管理员 在进行操作后会向管理员发送相应信息,管理员同意后，才会真正进行
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            string Person = Grid_MeetingName.Rows[e.RowIndex].Values[2].ToString();
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)//不是录入人或超级管理员操作
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                CBoxSelect_MeetingName.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_MeetingName.RecordCount / this.Grid_MeetingName.PageSize));

            if (Grid_MeetingName.PageIndex == Pages)
                m = (Grid_MeetingName.RecordCount - this.Grid_MeetingName.PageSize * Grid_MeetingName.PageIndex);
            else
                m = this.Grid_MeetingName.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect_MeetingName.GetCheckedState(i))
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
        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (DropDownList_Year.SelectedText == "全部" && DropDownList_User.SelectedText == "全部")
                BindData();
            //仅对人员条件进行搜索
            else if (DropDownList_Year.SelectedText == "全部")
                FindByUserName();
            //仅对年份进行搜索
            else if (DropDownList_User.SelectedText == "全部")
                FindByYear();
            else
                FindByUserAndYear();
            btnDelete.Enabled = false;
            Grid_MeetingName.PageIndex = 0;
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //DropDownList_User.SelectedValue = "0";
            DropDownList_User.Reset();
            DropDownList_Year.Reset();
            //DropDownList_Year.SelectedValue = "0";
            btnDelete.Enabled = false;
            BindData();
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //如果为超级管理员操作则直接删除，否则将IsPass置为false 向管理员发送删除信息
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_MeetingName.RecordCount / this.Grid_MeetingName.PageSize));

                if (Grid_MeetingName.PageIndex == Pages)
                    m = (Grid_MeetingName.RecordCount - this.Grid_MeetingName.PageSize * Grid_MeetingName.PageIndex);
                else
                    m = this.Grid_MeetingName.PageSize;
                List<int> selections = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (CBoxSelect_MeetingName.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int AcademicMeetingID = Convert.ToInt32(Grid_MeetingName.DataKeys[selections[i]][0].ToString());

                        //删除学术会议附件文件->删除附件表中的信息->删除会议参加人员信息->删除学术报告->删除学术会议信息
                        int AttactID = BLLAcademic.FindAttachmentID(AcademicMeetingID);
                        string strPath;
                        if (AttactID != 0)
                        {
                            strPath = BLLAttachment.FindPath(AttactID);
                            if (strPath != "")
                            {
                                //删除附件文件
                                publicMethod.DeleteFile(AttactID, strPath);
                                //在附件表中删除附件数据
                                BLLAttachment.Delete(AttactID);
                            }
                        }
                        //删除会议参见人员表中的数据
                        BLLAttend.DeleteStaffByMeetingID(AcademicMeetingID);
                        //删除学术会议中的学术报告信息
                        BLLAcademic.DeleteReportByMeetingID(AcademicMeetingID);
                        //删除学术会议
                        BLLAcademic.Delete(Convert.ToInt32(Grid_MeetingName.DataKeys[selections[i]][0].ToString()));
                    }
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除成功!");
                }
                else
                {
                    //录入人(非管理员)操作
                    for (int i = 0; i < selections.Count(); i++)
                    {

                        BLLAcademic.UpdateIsPass(Convert.ToInt32(Grid_MeetingName.DataKeys[selections[i]][0]), false);
                        //BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "AcademicMeeting";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_MeetingName.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                        //BindData();
                        btnSelect_All.Text = "全选";
                        Alert.ShowInTop("操作已经提交，请等待管理员确认!");

                    }
                }
                Grid_MeetingName.PageIndex = 0;
                Grid_MeetingName.PageSize = 20;
                btnDelete.Enabled = false;
                BindData();
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return;
            }
        }
        //分页
        protected void Grid_MeetingName_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_MeetingName.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByUserName();
                    break;
                case 3:
                    FindByUserAndYear();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_MeetingName.PageIndex = 0;
            this.Grid_MeetingName.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByYear();
                    break;
                case 2:
                    FindByUserName();
                    break;
                case 3:
                    FindByUserAndYear();
                    break;
            }
        }
        //下载界面跳转
        protected string GetMeetingUrlDown(object AcademicMeetingID)
        {
            try
            {
                return Window_DownLoad.GetShowReference("DownLoad_Meeting.aspx?id=" + AcademicMeetingID, "操作");
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //会议参加人员界面跳转
        protected string GetPeopletUrl(object AcademicMeetingID)
        {
            return Window_AttendPeople.GetShowReference("AttendMeetingPeople.aspx?id=" + AcademicMeetingID, "会议参加人员");
        }
        //学术报告界面跳转
        protected string GetReportUrl(object AcademicMeetingID)
        {
            return Window_Report.GetShowReference("NewMeeting.aspx?id=" + AcademicMeetingID, "学术报告");
        }
        //会议内容界面跳转
        protected string GetMeetingContentUrl(object AcademicMeetingID)
        {
            return Window_AttendPeople.GetShowReference("MeetingContent.aspx?id=" + AcademicMeetingID, "会议内容");
        }
        //lby 会议照片界面跳转
        protected string GetPhotoUrl(object AcademicMeetingID)
        {
            try
            {
                return Window_DownLoad.GetShowReference("DownLoad_Photo.aspx?id=" + AcademicMeetingID, "操作");
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_MeetingName.PageIndex) * Grid_MeetingName.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        //更新学术会议
        protected void btnUpdateMeeting_Click(object sender, EventArgs e)
        {
            try
            {
                if (publicMethod.GridCount(Grid_MeetingName, CBoxSelect_MeetingName).Count() != 0)
                {
                    if (publicMethod.GridCount(Grid_MeetingName, CBoxSelect_MeetingName).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_MeetingName.DataKeys[publicMethod.GridCount(Grid_MeetingName, CBoxSelect_MeetingName)[0]][0]);
                        Session["AcademicMeetingID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", FineUI.MessageBoxIcon.Information, Window_UppdateAcademic.GetShowReference("UpdateAcademic.aspx", "更新学术会议信息"), Target.Top);
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
        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            Grid_MeetingName.SelectAllRows();
            int[] select = Grid_MeetingName.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_MeetingName.RecordCount / this.Grid_MeetingName.PageSize));

            if (Grid_MeetingName.PageIndex == Pages)
                m = (Grid_MeetingName.RecordCount - this.Grid_MeetingName.PageSize * Grid_MeetingName.PageIndex);
            else
                m = this.Grid_MeetingName.PageSize;
            bool isCheck = false;
            for (int i = 0; i < m; i++)
            {
                if (CBoxSelect_MeetingName.GetCheckedState(i) == false)
                    isCheck = true;
            }
            if (isCheck)
            {
                foreach (int item in select)
                {
                    CBoxSelect_MeetingName.SetCheckedState(item, true);
                }
                btnDelete.Enabled = true;
                btnSelect_All.Text = "取消全选";
            }
            else
            {
                foreach (int item in select)
                {
                    CBoxSelect_MeetingName.SetCheckedState(item, false);
                }
                btnDelete.Enabled = false;
                btnSelect_All.Text = "全选";
            }
        }
    }
}