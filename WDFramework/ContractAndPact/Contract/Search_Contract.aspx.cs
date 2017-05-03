/**编写人：李金秋
 * 时间：2014年8月12号
 * 功能：资料界面
 * 修改履历： 1.修改人：吕博扬
 *           修改时间：10月10日
 *           修改内容：撤销静态page变量，采用ViewState传值
 *           2.修改人：陈起明
 *           修改时间：2015年11月28日
 *           修改内容：1.字段名“资料著作人”改成“资料保存人”；2.增加原始文件保存人字段
 *           3.修改人：苏瑀 修改时间：2017.5.3 修改内容：增加全选，取消全选功能，实现批量删除
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WDFramework.UnitInspect;

namespace WebApplication1.ContractAndPact.Contract
{
    public partial class Search_Contract : System.Web.UI.Page
    {
        BLHelper.BLLContract BLLContract = new BLHelper.BLLContract();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLAttachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLHelper.BLLLibraryRecord BLLLibraryRecord = new BLHelper.BLLLibraryRecord();
        //BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        Common.Entities.OperationLog operate = new Common.Entities.OperationLog();
        BLCommon.PublicMethod publicMethod = new BLCommon.PublicMethod();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if (!IsPostBack)
            {
                btn_AddContract.OnClientClick = Window_addContract.GetShowReference("Add_Contract.aspx", "增加资料信息");
                btnLibraryRecord.OnClientClick = Window_LibraryRecord.GetShowReference("LibraryRecord_Contract.aspx", "查询借阅信息");
                
                reprotLibrery.OnClientClick = Window_LibraryRecord.GetShowReference("~/Report/R_Contract_LibraryRecord.aspx", "借阅统计");
                reprot.OnClientClick = Window_LibraryRecord.GetShowReference("~/Report/R_Contract_addsub.aspx", "增减统计");
              
                //btn_UpdateContract.OnClientClick = Window_UpdateContract.GetShowReference("Update_Contract.aspx", "修改资料信息");
                //删除数据
                //btnDelete.OnClientClick = Grid_Contract.GetNoSelectionAlertReference("请至少选择一项！");
                BindData();
                //管理员登陆借阅按钮可见
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                    ContractRecordLibrary.Hidden = false;

            }
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            //DropDownList_Contract.SelectedValue = "0";
            tCondition.Reset();
            btnDelete.Enabled = false;
            BindData();
        }
        //借阅界面跳转
        protected string GetRecordUrlw(object ContractID, string sort)
        {
            return Window_Add_LibraryRecord.GetShowReference("NewAdd_ContractRecord.aspx?id=" + ContractID, "增加借阅记录");
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object ContractID, string sort)
        {
            return Window_DownLoad.GetShowReference("DownLoad.aspx?id=" + ContractID, "下载");
        }
        //数据绑定
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Contract> list = BLLContract.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Contract.PageIndex * Grid_Contract.PageSize).Take(Grid_Contract.PageSize).ToList();
                Grid_Contract.RecordCount = list.Count();
                this.Grid_Contract.DataSource = res;
                this.Grid_Contract.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
        //根据资料名称绑定
        public void FindByContractHeadLine()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.Contract> list = BLLContract.FindByContractHeadLine(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                var res = list.Skip(Grid_Contract.PageIndex * Grid_Contract.PageSize).Take(Grid_Contract.PageSize).ToList();
                Grid_Contract.RecordCount = list.Count();
                this.Grid_Contract.DataSource = res;
                this.Grid_Contract.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
            
        }
        //分页
        protected void Grid_Contract_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_Contract.PageIndex = e.NewPageIndex;
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByContractHeadLine();
                    break;
            }
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Contract.PageIndex = 0;
            this.Grid_Contract.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindData();
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    FindByContractHeadLine();
                    break;
            }
        }
        //删除资料
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Contract.RecordCount / this.Grid_Contract.PageSize));

                if (Grid_Contract.PageIndex == Pages)
                    m = (Grid_Contract.RecordCount - this.Grid_Contract.PageSize * Grid_Contract.PageIndex);
                else
                    m = this.Grid_Contract.PageSize;
                List<int> selections = new List<int>();
                for (int i = 0; i < m; i++)
                {
                    if (BoxSelect_Contract.GetCheckedState(i))
                        selections.Add(i);
                }
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        int ContractID = Convert.ToInt32(Grid_Contract.DataKeys[selections[i]][0].ToString());
                        //删除资料附件
                        int AttactID = BLLContract.FindAttachmentID(ContractID);
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
                        //删除资料借阅记录
                        List<int> listRecordID = BLLLibraryRecord.FindLibraryID(ContractID, "资料");
                        if (listRecordID != null)
                        {
                            for (int j = 0; j < listRecordID.Count(); j++)
                                BLLLibraryRecord.Delete(listRecordID[j]);
                        }
                        //删除资料
                        BLLContract.Delete(Convert.ToInt32(Grid_Contract.DataKeys[selections[i]][0].ToString()));
                    }
                    Alert.ShowInTop("删除成功!");
                    btnSelect_All.Text = "全选";
                }
                else
                {

                    for (int i = 0; i < selections.Count(); i++)
                    {

                        BLLContract.UpdateIsPass(Convert.ToInt32(Grid_Contract.DataKeys[selections[i]][0]), false);
                        List<int> listRecordID = BLLLibraryRecord.FindLibraryID(Convert.ToInt32(Grid_Contract.DataKeys[selections[i]][0]), "资料");
                        for (int j = 0; j < listRecordID.Count(); i++)
                            BLLLibraryRecord.UpdateIsPass(listRecordID[j], false);
                        //BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName
                        operate.LoginName = Session["LoginName"].ToString();
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Contract";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Contract.DataKeys[selections[i]][0]);
                        op.Insert(operate);
                        //BindData();
                        Alert.ShowInTop("操作已经提交，请等待管理员确认!");
                        btnSelect_All.Text = "全选";
                    }
                }
                btnDelete.Enabled = false;
                BindData();
                Grid_Contract.PageIndex = 0;
                Grid_Contract.PageSize = 20;
                BindData();
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }

        protected void Grid_Contract_RowCommand(object sender, GridCommandEventArgs e)
        {
            string strs = Session["LoginName"].ToString();
            string Person = Grid_Contract.Rows[e.RowIndex].Values[2].ToString();
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                BoxSelect_Contract.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Contract.RecordCount / this.Grid_Contract.PageSize));

            if (Grid_Contract.PageIndex == Pages)
                m = (Grid_Contract.RecordCount - this.Grid_Contract.PageSize * Grid_Contract.PageIndex);
            else
                m = this.Grid_Contract.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect_Contract.GetCheckedState(i))
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

        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Contract.PageIndex) * Grid_Contract.PageSize;
        }
        //搜索
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            //string strContractHeadLine = DropDownList_Contract.SelectedText;
            Grid_Contract.PageIndex = 0;
            try
            {
                if (tCondition.Text.Trim() != "")
                {
                    FindByContractHeadLine();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        //转化等级
        public string ChangeSecrecyLevel(int level)
        {
            string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
            return SecrecyLevels[level - 1];
        }
        //编辑平台信息
         protected void ButtonUpdate_Click(object sender,EventArgs e)
        {
            try
            {
                List<int> selections = publicMethod.GridCount(Grid_Contract, BoxSelect_Contract);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Contract.DataKeys[selections[0]][0]);
                        Session["ContractID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_Update.GetShowReference("Updata_Contract.aspx", "编辑平台信息"), Target.Top);
                      
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

         //全选按钮
         protected void btnSelect_All_Click(object sender, EventArgs e)
         {
             Grid_Contract.SelectAllRows();
             int[] select = Grid_Contract.SelectedRowIndexArray;
             int m;
             //取整数（不是四舍五入，全舍）
             int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Contract.RecordCount / this.Grid_Contract.PageSize));

             if (Grid_Contract.PageIndex == Pages)
                 m = (Grid_Contract.RecordCount - this.Grid_Contract.PageSize * Grid_Contract.PageIndex);
             else
                 m = this.Grid_Contract.PageSize;
             bool isCheck = false;
             for (int i = 0; i < m; i++)
             {
                 if (BoxSelect_Contract.GetCheckedState(i) == false)
                     isCheck = true;
             }
             if (isCheck)
             {
                 foreach (int item in select)
                 {
                     BoxSelect_Contract.SetCheckedState(item, true);
                 }
                 btnDelete.Enabled = true;
                 btnSelect_All.Text = "取消全选";
             }
             else
             {
                 foreach (int item in select)
                 {
                     BoxSelect_Contract.SetCheckedState(item, false);
                 }
                 btnDelete.Enabled = false;
                 btnSelect_All.Text = "全选";
             }
         }
    }
}