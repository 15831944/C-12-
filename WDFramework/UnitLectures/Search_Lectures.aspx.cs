/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：单位讲学界面
 * 修改履历：
 **/
using BLHelper;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class 查询讲学页面 : System.Web.UI.Page
    {
        BLLUser BLLUser = new BLLUser();
        BLLAgency BLLAgency = new BLLAgency();
        BLLOperationLog BLLOP = new BLLOperationLog();
        BLLUnitLectures BLLUL = new BLLUnitLectures();
        BLLAttachment BllAttachment = new BLLAttachment();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                btnAddLecture.OnClientClick = Window_addLecture.GetShowReference("Add_Lectures.aspx", "新增讲学信息");
                BindAgencyName();
                BindData();

            }
        }
        //所属机构名称绑定
        public void BindAgencyName()
        {
            List<string> listAgencyName = BLLAgency.FindAgencyBySecrecyLevel(Convert.ToInt32(Session["SecrecyLevel"]));
            for (int i = 0; i < listAgencyName.Count(); i++)
                DropDownList_Agency.Items.Add(listAgencyName[i].ToString(), (i + 1).ToString());
        }
        //对机构ID转化为机构名称(Grid_Lectures中显示的)
        public string AgencyName(int agency)
        {
            try
            {
                if (agency != 0)
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
        //单位讲学信息绑定
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.UnitLectures> list = BLLUL.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Lectures.PageIndex * Grid_Lectures.PageSize).Take(Grid_Lectures.PageSize).ToList();
                Grid_Lectures.RecordCount = list.Count();
                Grid_Lectures.DataSource = res;
                Grid_Lectures.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //删除选中的单位讲学信息
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = new List<int>();
                for (int i = 0; i < Grid_Lectures.RecordCount; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int UnitLecturesID = Convert.ToInt32(Grid_Lectures.DataKeys[selections[i]][0].ToString());
                        Common.Entities.UnitLectures unitLectures = BLLUL.FindByUnitLecturesID(UnitLecturesID);
                        if (unitLectures.AttachmentID != null)
                        {
                            if (BllAttachment.SelectAttachmentName(Convert.ToInt32(unitLectures.AttachmentID)) != "")
                            {
                                //删除附件文件
                                string path = BllAttachment.FindPath(Convert.ToInt32(unitLectures.AttachmentID));
                                if (path != "")
                                {
                                    publicMethod.DeleteFile(Convert.ToInt32(unitLectures.AttachmentID), path);
                                    //删除附件表中的数据
                                    BllAttachment.Delete(Convert.ToInt32(unitLectures.AttachmentID));//删除成功返回true    
                                }
                            }
                        }
                        //删除单位讲学信息
                        BLLUL.Delete(UnitLecturesID);//删除成功返回true     
                    }
                    Grid_Lectures.PageIndex = 0;
                    Grid_Lectures.PageSize = 20;
                    DropDownList_Agency.SelectedValue = "0";
                    btnDelete.Enabled = false;
                    BindData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        BLLUL.ChangePass(Convert.ToInt32(Grid_Lectures.DataKeys[selections[i]][0]), false);
                        operate.LoginName = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "UnitLectures";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Lectures.DataKeys[selections[i]][0]);
                        BLLOP.Insert(operate);
                    }
                    Grid_Lectures.PageIndex = 0;
                    Grid_Lectures.PageSize = 20;
                    DropDownList_Agency.SelectedValue = "0";
                    btnDelete.Enabled = false;
                    BindData();
                    Alert.ShowInTop("操作已经提交，请等待管理员确认!");
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //根据所属部门查询讲学信息
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (DropDownList_Agency.SelectedText == "全部")
                BindData();
            //return;
            else
                FindByAgency();
            Grid_Lectures.PageIndex = 0;
        }
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 1;
                Grid_Lectures.PageIndex = 0;
                //根据所属机构查询
                //string AgencyName = DropDownList_Agency.SelectedText;
                int agencyID = BLLAgency.SelectAgencyID(DropDownList_Agency.SelectedText);
                List<Common.Entities.UnitLectures> list = BLLUL.FindLectures(agencyID, Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Lectures.PageIndex * Grid_Lectures.PageSize).Take(Grid_Lectures.PageSize).ToList();
                Grid_Lectures.RecordCount = list.Count();
                Grid_Lectures.DataSource = res;
                Grid_Lectures.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }


        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Lectures.PageIndex = 0;
            this.Grid_Lectures.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByAgency();
                    break;
            }
        }
        protected void Grid_Lectures_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Lectures.PageIndex = e.NewPageIndex;
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByAgency();
                    break;
            }
        }


        protected void Grid_Lectures_RowCommand(object sender, GridCommandEventArgs e)
        {
            //int LecturesID = Convert.ToInt32(Grid_Lectures.DataKeys[e.RowIndex][0].ToString());
            string strs = Session["LoginName"].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            //Common.Entities.UnitLectures unitLectures = BLLUL.FindByUnitLecturesID(LecturesID);
            string Person = Grid_Lectures.Rows[e.RowIndex].Values[3].ToString();
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                CBoxSelect.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Lectures.RecordCount / this.Grid_Lectures.PageSize));

            if (Grid_Lectures.PageIndex == Pages)
                m = (Grid_Lectures.RecordCount - this.Grid_Lectures.PageSize * Grid_Lectures.PageIndex);
            else
                m = this.Grid_Lectures.PageSize;
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
        //编辑选中行
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = new List<int>();
                for (int i = 0; i < Grid_Lectures.RecordCount; i++)
                {
                    if (CBoxSelect.GetCheckedState(i))
                        selections.Add(i);
                }
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Lectures.DataKeys[selections[0]][0]);
                        Session["UnitLecturesID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("Update_Lectures.aspx", "编辑讲学信息"), Target.Top);
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
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DropDownList_Agency.SelectedValue = "0";
            btnDelete.Enabled = false;
            Grid_Lectures.PageIndex = 0;
            Grid_Lectures.PageSize = 20;
            BindAgencyName();
            BindData();
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object UnitLecturesID)
        {
            int unitLecturesID = Convert.ToInt32(UnitLecturesID);
            return Window_DownLoad.GetShowReference("DownLoad_UnitLectures.aspx?id=" + unitLecturesID, "下载");
        }
        //备注界面跳转
        protected string GetEditUrl(object UnitLecturesID)
        {
            return Window_Remark.GetShowReference("Remark_Window.aspx?id=" + UnitLecturesID, "备注");
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Lectures.PageIndex) * Grid_Lectures.PageSize;
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
    }
}