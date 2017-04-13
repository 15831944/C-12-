/**编写人：李金秋
 * 时间：2014年6月20号
 * 功能：合同借阅记录查询界面
 * 修改履历： 修改人：吕博扬
 *           修改时间：10月10日
 *           修改内容：撤销静态变量page
 *           修改人：高琪
 *           修改时间：2015.11.27
 *           修改内容：增加编辑选中行 方法
 *           修改人：高琪  修改时间：2015.11.28  修改内容：增加合同负责人查询 方法
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.ContractAndPact.Pact
{
    public partial class Search_Pact : System.Web.UI.Page//查询页面
    {
        BLHelper.BLLProject BLLProject = new BLHelper.BLLProject();
        BLHelper.BLLPact BLLPact = new BLHelper.BLLPact();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                btn_AddPact.OnClientClick = Window_AddPact.GetShowReference("Add_Pact.aspx", "新增合同信息");
                btnLibraryRecord.OnClientClick = Window_LibraryRecord_Pact.GetShowReference("LibraryRecord_Pact.aspx", "查看合同借阅记录");
                btnAddLibraryRecord.OnClientClick = Window_Add_LibraryRecord.GetShowReference("Add_PactRecord.aspx", "新增合同借阅信息");

                reprotLibraryRecord.OnClientClick = Window_LibraryRecord_Pact.GetShowReference("~/Report/R_Pact_LibraryRecord.aspx", "借阅统计");
                report.OnClientClick = Window_LibraryRecord_Pact.GetShowReference("~/Report/R_Pact_addsub.aspx", "增减统计");
                //删除数据
                //btnDelete.OnClientClick = Grid_Pact.GetNoSelectionAlertReference("请至少选择一项！");
                //btn_UpdatePact.OnClientClick = Window_UpdatePact.GetShowReference("Update_Pact.aspx", "修改合同信息");
                BindData();
                //管理员登陆借阅按钮可见
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    ContractRecordLibrary.Hidden = false;
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Pact> list = BLLPact.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Pact.PageIndex * Grid_Pact.PageSize).Take(Grid_Pact.PageSize).ToList();
                Grid_Pact.RecordCount = list.Count();
                this.Grid_Pact.DataSource = res;
                this.Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //合同借阅界面跳转
        protected string GetRecordUrlw(object PactID, string sort)
        {
            return Window_Add_LibraryRecord.GetShowReference("NewAdd_PactRecord.aspx?id=" + PactID, "增加合同借阅记录");
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object PactID, string sort)
        {
            return Window_DownLoad.GetShowReference("DownLoad_Pact.aspx?id=" + PactID, "下载");
        }
        //将项目ID转换为项目名称
        public string ProjectName(int ProjectID)
        {
            try
            {
                if (ProjectID != null)
                    return BLLProject.SelectProjectName(ProjectID);
                else
                    return " ";
            }
            catch (Exception ex)
            {
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                //pm.SaveError(ex, this.Request);
                publicMethod.SaveError(ex, this.Request);
                return "";
            }
        }

        //分页
        protected void Grid_Pact_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_Pact.PageIndex = e.NewPageIndex;
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByPactNum();
                    break;
                case 2:
                    FindByProject();
                    break;
                case 3:
                    FindByPactName();
                    break;
                case 4:
                    FindByPactCompletion();
                    break;
                case 5:
                    FindByChargePerson();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Pact.PageIndex = 0;
            this.Grid_Pact.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByPactNum();
                    break;
                case 2:
                    FindByProject();
                    break;
                case 3:
                    FindByPactName();
                    break;
                case 4:
                    FindByPactCompletion();
                    break;
                case 5:
                    FindByChargePerson();
                    break;
            }
        }

        protected void Grid_Pact_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            string strs = Session["LoginName"].ToString();
            string Person = Grid_Pact.Rows[e.RowIndex].Values[2].ToString();
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                BoxSelect_Pact.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Pact.RecordCount / this.Grid_Pact.PageSize));

            if (Grid_Pact.PageIndex == Pages)
                m = (Grid_Pact.RecordCount - this.Grid_Pact.PageSize * Grid_Pact.PageIndex);
            else
                m = this.Grid_Pact.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect_Pact.GetCheckedState(i))
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
      
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> selections = new List<int>();
            for (int i = 0; i < Grid_Pact.RecordCount; i++)
            {
                if (BoxSelect_Pact.GetCheckedState(i))
                    selections.Add(i);
            }
            if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
            {
                for (int i = 0; i < selections.Count(); i++)
                {
                    int PactID = Convert.ToInt32(Grid_Pact.DataKeys[selections[i]][0].ToString());
                    //删除资料附件
                    int AttactID = BLLPact.FindAttachmentID(PactID);
                    string strPath;
                    if (AttactID != 0)
                    {
                        strPath = BLLAttachment.FindPath(AttactID);
                        if (strPath != "")
                        {
                            //在附件表中删除附件数据
                            BLLAttachment.Delete(AttactID);
                            //删除附件文件
                            publicMethod.DeleteFile(AttactID, strPath);
                        }
                    }
                    //删除该合同的借阅记录
                    List<int> listRecordID = BLLLibraryRecord.FindLibraryID(PactID, "合同");
                    if (listRecordID != null)
                    {
                        for (int j = 0; j < listRecordID.Count(); j++)
                            BLLLibraryRecord.Delete(listRecordID[j]);
                    }
                    //合同表删除
                    BLLPact.Delete(Convert.ToInt32(Grid_Pact.DataKeys[selections[i]][0].ToString()));
                }
                Alert.ShowInTop("删除数据成功!");
            }
            else
            {
                for (int i = 0; i < selections.Count(); i++)
                {
                    BLLPact.UpdateIsPass(Convert.ToInt32(Grid_Pact.DataKeys[selections[i]][0]), false);
                    //将借阅记录表中与该资料有关的数据置为false
                    List<int> listRecordID = BLLLibraryRecord.FindLibraryID(Convert.ToInt32(Grid_Pact.DataKeys[selections[i]][0]), "合同");
                    for (int j = 0; j < listRecordID.Count(); i++)
                        BLLLibraryRecord.UpdateIsPass(listRecordID[j], false);
                    operate.LoginName = Session["LoginName"].ToString();
                    operate.OperationTime = DateTime.Now;
                    operate.LoginIP = " ";
                    operate.OperationContent = "Pact";
                    operate.OperationType = "删除";
                    operate.OperationDataID = Convert.ToInt32(Grid_Pact.DataKeys[selections[i]][0]);
                    op.Insert(operate);
                }
                Alert.ShowInTop("操作已经提交，请等待管理员确认!");
            }
            btnDelete.Enabled = false;
            Grid_Pact.PageIndex = 0;
            Grid_Pact.PageSize = 20;
            BindData();
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            tCondition.Reset();
            ddl_Pact.Reset();
            BindData();
            //switch (page)
            //{
            //    case 0:
            //        BindData();
            //        break;
            //    case 1:
            //        FindByPactName();
            //        break;
            //}
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Pact.PageIndex) * Grid_Pact.PageSize;
        }


        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_Pact.SelectedIndex == 0)
                {
                    Alert.ShowInTop("请选择查询条件！");
                    return;
                }
                Grid_Pact.PageIndex = 0;
                if (tCondition.Text.Trim() != "")
                {
                    switch(ddl_Pact.SelectedIndex)
                    {
                        case 1:
                            FindByPactNum();
                            break;
                        case 2:
                            FindByProject();
                            break;
                        case 3:
                            FindByPactName();
                            break;
                        case 4:
                            FindByPactCompletion();
                            break;
                        case 5:
                            FindByChargePerson();
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
                //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                //pm.SaveError(ex, this.Request);
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //编辑选中行
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (publicMethod.GridCount(Grid_Pact, BoxSelect_Pact).Count() != 0)
                {
                    if (publicMethod.GridCount(Grid_Pact, BoxSelect_Pact).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Pact.DataKeys[publicMethod.GridCount(Grid_Pact, BoxSelect_Pact)[0]][0]);
                        Session["PactID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_AddPact.GetShowReference("Update_PactRecord.aspx", "编辑合同信息"), Target.Top);
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

        //根据合同编号查询
        public void FindByPactNum()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.Pact> list = BLLPact.FindPactList(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Pact.RecordCount = list.Count();
                Grid_Pact.DataSource = list;
                Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //根据所属项目查询
        public void FindByProject()
        {
            try
            {
                ViewState["page"] = 2;
                int projectid = BLLProject.SelectProjectID(tCondition.Text.Trim());
                List<Common.Entities.Pact> list = BLLPact.FindByProject(projectid, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Pact.RecordCount = list.Count();
                Grid_Pact.DataSource = list;
                Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //根据合同名称查询
        public void FindByPactName()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Pact> list = BLLPact.FindByPactName(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Pact.RecordCount = list.Count();
                Grid_Pact.DataSource = list;
                Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //根据合同完成情况查询
        public void FindByPactCompletion()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Pact> list = BLLPact.FindByPactCompletion(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Pact.RecordCount = list.Count();
                Grid_Pact.DataSource = list;
                Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }

        public Grid Grid_Files { get; set; }
        public FineUI.CheckBoxField CBoxSelect { get; set; }
        //根据合同负责人查询
        public void FindByChargePerson()
        {
            try
            {
                ViewState["page"] = 5;
                List<Common.Entities.Pact> list = BLLPact.FindByChargePerson(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Pact.RecordCount = list.Count();
                Grid_Pact.DataSource = list;
                Grid_Pact.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
    }
}