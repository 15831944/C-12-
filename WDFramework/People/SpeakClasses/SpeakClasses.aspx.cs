using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.People.SpeakClasses
{
    public partial class SpeakClasses : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLSpeakClass bllSpeak = new BLHelper.BLLSpeakClass();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
                //添加
                btnAdd.OnClientClick = WindowAdd.GetShowReference("Add_SpeakClasses.aspx");             
                BindData();
                btnDelete.Enabled = false;
                EducationBind();
            }
        }
        public void BindData()
        {
            try
            {
                ViewState["page"] = 0;
                List<SpeakClass> SpeakList = bllSpeak.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                GridSpeakClass.RecordCount = SpeakList.Count();
                var result = SpeakList.Skip(GridSpeakClass.PageIndex * GridSpeakClass.PageSize).Take(GridSpeakClass.PageSize).ToList();
                this.GridSpeakClass.DataSource = result;
                this.GridSpeakClass.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //根据人员ID查找主讲课程(模糊查询)
        public void SelectByID()
        {
            try
            {
                ViewState["page"] = 1;
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<SpeakClass> alist = new List<SpeakClass>();
                for (int i = 0; i < UserIDlist.Count(); i++)
                {
                    //根据人员ID查找主讲课程信息
                    List<SpeakClass> SpeakList = bllSpeak.SelectByID(UserIDlist[i]).ToList();
                    for (int j = 0; j < SpeakList.Count(); j++)
                    {
                        alist.Add(SpeakList[j]);
                    }
                }

                GridSpeakClass.RecordCount = alist.Count();
                var result = alist.Skip(GridSpeakClass.PageIndex * GridSpeakClass.PageSize).Take(GridSpeakClass.PageSize).ToList();
                this.GridSpeakClass.DataSource = result;
                this.GridSpeakClass.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据人员ID和 教学对象查询授课情况
        public void FindByIT(string UserName, string TeachingDegree)
        {
            try
            {
                ViewState["page"] = 2;
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<SpeakClass> alist = new List<SpeakClass>();
                if(UserIDlist.Count==0)
                {
                    List<SpeakClass> SpeakList = bllSpeak.FindByTime(TriggerBox.Text.Trim(), TeachingDegree).ToList();
                    for (int j = 0; j < SpeakList.Count(); j++)
                    {
                        alist.Add(SpeakList[j]);
                    }
                }
               
                else
                {
                    for (int i = 0; i < UserIDlist.Count(); i++)
                    {
                        //根据人员ID查找主讲课程信息
                        List<SpeakClass> SpeakList = bllSpeak.FindByIT(UserIDlist[i], TeachingDegree).ToList();
                        for (int j = 0; j < SpeakList.Count(); j++)
                        {
                            alist.Add(SpeakList[j]);
                        }
                    }
                }

                GridSpeakClass.RecordCount = alist.Count();
                var result = alist.Skip(GridSpeakClass.PageIndex * GridSpeakClass.PageSize).Take(GridSpeakClass.PageSize).ToList();
                this.GridSpeakClass.DataSource = result;
                this.GridSpeakClass.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //根据UserID查找Username
        public string UserName(int UserID)
        {
            if (UserID != 0)
                return bllUser.FindUserName(UserID);
            else
                return "";
        }
        //刷新
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
            TriggerBox.Reset();
            DropDownListTeachingDegree.SelectedValue = "全部";
            btnDelete.Enabled = false;
        }
        //删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridSpeakClass, CBoxSelect);
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < publicmethod.GridCount(GridSpeakClass, CBoxSelect).Count(); i++)
                    {
                        bllSpeak.Delete(Convert.ToInt32(GridSpeakClass.DataKeys[selections[i]][0]));
                    }
                    BindData();
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("删除成功!");
                }
                else
                {
                    for (int i = 0; i < selections.Count(); i++)
                    {
                        bllSpeak.UpdateIsPass(Convert.ToInt32(GridSpeakClass.DataKeys[selections[i]][0]), false);
                        //向操作日志表中插入
                        OperationLog operate = new OperationLog();
                        operate.LoginName = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
                        operate.LoginIP = "";
                        operate.OperationType = "删除";
                        operate.OperationContent = "Student";
                        operate.OperationDataID = Convert.ToInt32(GridSpeakClass.DataKeys[selections[i]][0]);
                        operate.OperationTime = System.DateTime.Now;
                        operate.Remark = "";
                        bllOperate.Insert(operate);                      
                    }
                    btnSelect_All.Text = "全选";
                    Alert.ShowInTop("您的操作已提交，请等待审核！");
                    BindData();
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //修改
        protected void Btnchange_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> selections = publicmethod.GridCount(GridSpeakClass, CBoxSelect);
                if (selections.Count() != 0)
                {
                    if (selections.Count() == 1)
                    {
                        int rowID = Convert.ToInt32(GridSpeakClass.DataKeys[selections[0]][0]);
                        Session["SpeakClassID"] = rowID;

                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, WindowUpdate.GetShowReference("Update_SpeakClasses.aspx"), Target.Top);
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
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //查询
        protected void Find_Click(object sender, EventArgs e)
        {
            GridSpeakClass.PageIndex = 0;
            if (!string.IsNullOrEmpty(TriggerBox.Text.Trim()))
            {
                if (DropDownListTeachingDegree.SelectedItem.Text == "全部")
                {
                    SelectByID();
                }
                else
                {
                    FindByIT(TriggerBox.Text.Trim(), DropDownListTeachingDegree.SelectedItem.Text);
                }
                    //switch (DropDownListTeachingDegree.SelectedItem.Text)
                    //{
                    //    case "全部":
                    //        SelectByID();
                    //        break;
                    //    case "本科":
                    //        FindByIT(TriggerBox.Text.Trim(), "本科");
                    //        break;
                    //    case "研究生":
                    //        FindByIT(TriggerBox.Text.Trim(), "研究生");
                    //        break;
                    //    case "博士":
                    //        FindByIT(TriggerBox.Text.Trim(), "博士");
                    //        break;
                    //    case "博士后":
                    //        FindByIT(TriggerBox.Text.Trim(), "博士后");
                    //        break;
                    //}              
            }
            else
                Alert.ShowInTop("请填写查询条件！");
        }
        //GridSpeakClass行命令
        protected void GridSpeakClass_RowCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                string Person = GridSpeakClass.Rows[e.RowIndex].Values[2].ToString();
                string strs = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                }
                if (publicmethod.GridCount(GridSpeakClass, CBoxSelect).Count == 0)
                {
                    //Alert.ShowInTop("请选中需删除的数据！");
                    btnDelete.Enabled = false;
                    return;
                }
                if (publicmethod.GridCount(GridSpeakClass, CBoxSelect).Count != 0)
                {
                    btnDelete.Enabled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //分页
        protected void GridSpeakClass_PageIndexChange(object sender, GridPageEventArgs e)
        {
            GridSpeakClass.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID();
                    break;
                case 2:
                    if (DropDownListTeachingDegree.SelectedItem.Text == "全部")
                    {
                        SelectByID();
                    }
                    else
                    {
                        FindByIT(TriggerBox.Text.Trim(), DropDownListTeachingDegree.SelectedItem.Text);
                    }
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridSpeakClass.PageIndex = 0;
            this.GridSpeakClass.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            switch (page)
            {
                case 0:
                    BindData();
                    break;
                case 1:
                    SelectByID();
                    break;
                case 2:
                    if (DropDownListTeachingDegree.SelectedItem.Text == "全部")
                    {
                        SelectByID();
                    }
                    else
                    {
                        FindByIT(TriggerBox.Text.Trim(), DropDownListTeachingDegree.SelectedItem.Text);
                    }
                    break;
            }
        }
        //涉密等级名称
        public string SecrecyLevelName(int level)
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                //string[] SecrecyLevels = new string[] { "公开", "内部", "秘密", "机密", "管理员" };
                return SecrecyLevels[level - 1];
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
                return "";
            }
        }
        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                switch (page)
                {
                    case 0:
                        List<SpeakClass> SpeakList = bllSpeak.FindPaged(Convert.ToInt32(Session["SecrecyLevel"])).ToList();
                        this.GridSpeakClass.DataSource = SpeakList;
                        this.GridSpeakClass.DataBind();
                        break;
                    case 1:
                        List<int> UserIDlist = new List<int>();
                        //根据人员名称模糊查找人员ID
                        UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                        List<SpeakClass> alist = new List<SpeakClass>();
                        for (int i = 0; i < UserIDlist.Count(); i++)
                        {
                            //根据人员ID查找主讲课程信息
                            List<SpeakClass> SpeakLists = bllSpeak.SelectByID(UserIDlist[i]).ToList();
                            for (int j = 0; j < SpeakLists.Count(); j++)
                            {
                                alist.Add(SpeakLists[j]);
                            }
                        }
                        this.GridSpeakClass.DataSource = alist;
                        this.GridSpeakClass.DataBind();
                        break;
                    case 2:
                        FindByITs(TriggerBox.Text, DropDownListTeachingDegree.SelectedItem.Text);
                        break;
                }
                publicmethod.ExportExcel(3, GridSpeakClass, 0);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public void FindByITs(string UserName, string TeachingDegree)
        {
            try
            {
                List<int> UserIDlist = new List<int>();
                //根据人员名称模糊查找人员ID
                UserIDlist = bllUser.FindList(TriggerBox.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                List<SpeakClass> alist = new List<SpeakClass>();
                for (int i = 0; i < UserIDlist.Count(); i++)
                {
                    //根据人员ID查找主讲课程信息
                    List<SpeakClass> SpeakList = bllSpeak.FindByIT(UserIDlist[i], TeachingDegree).ToList();
                    for (int j = 0; j < SpeakList.Count(); j++)
                    {
                        alist.Add(SpeakList[j]);
                    }
                }
                this.GridSpeakClass.DataSource = alist;
                this.GridSpeakClass.DataBind();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (GridSpeakClass.PageIndex) * GridSpeakClass.PageSize;
        }
        public void EducationBind()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("学历");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownListTeachingDegree.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
            }
        }

        //全选按钮
        protected void btnSelect_All_Click(object sender, EventArgs e)
        {
            GridSpeakClass.SelectAllRows();
            int[] select = GridSpeakClass.SelectedRowIndexArray;
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(GridSpeakClass.RecordCount / this.GridSpeakClass.PageSize));

            if (GridSpeakClass.PageIndex == Pages)
                m = (GridSpeakClass.RecordCount - this.GridSpeakClass.PageSize * GridSpeakClass.PageIndex);
            else
                m = this.GridSpeakClass.PageSize;
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