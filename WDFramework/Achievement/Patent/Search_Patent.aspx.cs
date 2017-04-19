/**编写人：方淑云
 * 时间：2014年8月17号
 * 功能：专利查询界面的相关操作
 * 修改履历：    1.修改人：吕博扬
 *                时间：2015年9月19日
 *                修改内容：添加“保密级别”查询条件、成员字段
 *              2.修改人：高琪
 *                修改时间;20151010
 *                内容：撤销page静态变量
 *              3.修改人：吕博杨
 *                修改时间：2015年11月28日
 *                修改内容：隐藏经费、成员字段
 *                         将附件分为专利证书、申请书进行保存
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Entities;
using FineUI;

namespace WebApplication1
{
    public partial class Search_Patent : System.Web.UI.Page
    {
       
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        BLHelper.BLLPatent patent = new BLHelper.BLLPatent();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
       // BLHelper.BLLStaffPatent blsp = new BLHelper.BLLStaffPatent();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLBasicCode ba = new BLHelper.BLLBasicCode();
        private int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            page = ViewState["page"] == null ? 0 : (int)ViewState["page"];
            if(dChoose.SelectedValue == "保密级别")
            {
                dCondition.Enabled = false;
                tCondition.Enabled = false;
                secrecyLevel.Enabled = true;
            }
            else if (dChoose.SelectedValue == "所属机构" || dChoose.SelectedValue == "发明人" || dChoose.SelectedValue == "成员")
            {
                dCondition.Enabled = false;
                tCondition.Enabled = true;
                secrecyLevel.Enabled = false;
            }
            else
            {
                dCondition.Enabled = true;
                tCondition.Enabled = false;
                secrecyLevel.Enabled = false;
            }
            if (!IsPostBack)
            {
                InitData();
                btn_AddPatent.OnClientClick = Window_addPatent.GetShowReference("Add_Patent.aspx", "新增专利信息");
                //reprot1.OnClientClick = WindowReport.GetShowReference("~/Report/R_Agency_Patent.aspx", "分部门按专利名称统计专利情况");
            }
        }
        //下载申请书
        protected string GetEditUrla(object ID)
        {
            return DownLoad.GetShowReference("Operate_Application.aspx?id=" + ID, "操作");
        }
        //下载专利证书
        protected string GetEditUrlx(object ID)
        {
            return DownLoad.GetShowReference("Operate_Patent.aspx?id=" + ID, "操作");
        }
        //备注界面跳转
        protected string GetEditUrl(object ID)
        {
            return Remark.GetShowReference("Remark.aspx?id=" + ID, "备注");
        }
        //单位界面跳转
        protected string GetEditUrlw(object ID)
        {
            return Units.GetShowReference("Unit.aspx?id=" +ID, "单位信息");
        }
        //发明人界面跳转
        protected string GetEditUrlp(object ID)
        {
            return People.GetShowReference("People.aspx?id=" + ID, "发明人");
        }
        //成员界面跳转
        protected string GetEditUrlm(object ID)
        {
            return People.GetShowReference("Member.aspx?id=" + ID, "成员信息");
        }
        //将成果ID转化为成果名称
        protected string FindName(int ah)
        {
            try
            {
                return ach.FindAchieveName(ah);
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
        //初始化
        public void InitData()
        {
            try
            {
                ViewState["page"] = 0;
                List<Common.Entities.Patent> list = patent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

       //按所属机构查询
        public void FindByAgency()
        {
            try
            {
                ViewState["page"] = 1;
                List<Common.Entities.Patent> list = patent.FindByPatentDepartment(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按专利类型查询
        public void FindByPatentForm()
        {
            try
            {
                ViewState["page"] = 2;
                List<Common.Entities.Patent> list = patent.FindByPatentForm(dCondition.SelectedText.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //按申请时间查询
        public void FindByApplicationTime()
        {
            try
            {
                ViewState["page"] = 3;
                List<Common.Entities.Patent> list = patent.FindByApplicationTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按授权时间查询
        public void FindByAccreditTime()
        {
            try
            {
                ViewState["page"] = 4;
                List<Common.Entities.Patent> list = patent.FindByAccreditTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按发明人查询
        public void FindByPeople()
        {
            try
            {
                ViewState["page"] = 5;
                List<Patent> list = patent.FindByPatentPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按成员查询
        private void FindByMember()
        {
            try
            {
                ViewState["page"] = 6;
                List<Patent> list = patent.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //按保密级别查询
        private void FindBySecrecyLevel()
        {
            try
            {
                ViewState["page"] = 7;
                List<Patent> list = patent.FindBySecrecyLevel(secrecyLevel.SelectedIndex + 1, Convert.ToInt32(Session["SecrecyLevel"]));
                Grid_Patent.RecordCount = list.Count();
                if (list != null)
                {
                    Grid_Patent.DataSource = list.Skip(Grid_Patent.PageIndex * Grid_Patent.PageSize).Take(Grid_Patent.PageSize);
                    Grid_Patent.DataBind();
                }
                else
                {
                    return;
                }
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
                Grid_Patent.PageIndex = 0;
                if (dChoose.SelectedText == "全部")
                {
                    InitData();
                }
                else if (dChoose.SelectedText == "专利类型")
                {
                    FindByPatentForm();
                }
                else if (dChoose.SelectedText == "申请年份")
                {
                    FindByApplicationTime();
                }
                else if (dChoose.SelectedText == "授权年份")
                {
                    FindByAccreditTime();
                }
                else if(dChoose.SelectedText == "保密级别")
                {
                    FindBySecrecyLevel();
                }
                else if (tCondition.Text.Trim() != "")
                    if (dChoose.SelectedText == "发明人")
                    {
                        FindByPeople();
                    }
                    else if (dChoose.SelectedText == "所属机构")
                    {
                        FindByAgency();
                    }
                    else if(dChoose.SelectedText == "成员")
                    {
                        FindByMember();
                    }
                else
                {
                    return;
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
            dChoose.SelectedValue = "全部";
            dCondition.Reset();
            tCondition.Reset();
            dCondition.Enabled = false;
            tCondition.Enabled = false;
            secrecyLevel.Enabled = false;
            InitData();
        }
    
        //更新
        protected void btn_UpdatePatent_Click(object sender, EventArgs e)
        {
            try
            {
                if (pm.GridCount(Grid_Patent, CBoxSelect).Count() != 0)
                {
                    if (pm.GridCount(Grid_Patent, CBoxSelect).Count() == 1)
                    {
                        int rowID = Convert.ToInt32(Grid_Patent.DataKeys[pm.GridCount(Grid_Patent, CBoxSelect)[0]][0]);
                        Session["PatentID"] = rowID;
                        Alert.Show("你确定要修改该行数据吗!", "确认消息", MessageBoxIcon.Information, Window_UpdatePatent.GetShowReference("Update_Patent.aspx", "修改专利信息"), Target.Top);
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
        //分页
        protected void Grid_Patent_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            Grid_Patent.PageIndex = e.NewPageIndex;
            switch (page)
            {
                case 0:
                    InitData();
                    break;
                case 1:
                    FindByAgency();
                    break;
                case 2:
                    FindByPatentForm();;
                    break;
                case 3:
                    FindByApplicationTime();
                    break;
                case 4:
                    FindByAccreditTime();
                    break;
                case 5:
                    FindByPeople();
                    break;
                case 6:
                    FindByMember();
                    break;
                case 7:
                    FindBySecrecyLevel();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Patent.PageIndex = 0;
            this.Grid_Patent.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
             switch (page)
             {
                 case 0:
                     InitData();
                     break;
                 case 1:
                     FindByAgency();
                     break;
                 case 2:
                     FindByPatentForm(); ;
                     break;
                 case 3:
                     FindByApplicationTime();
                     break;
                 case 4:
                     FindByAccreditTime();
                     break;
                 case 5:
                     FindByPeople();
                     break;
                 case 6:
                     FindByMember();
                     break;
                 case 7:
                     FindBySecrecyLevel();
                     break;
             }
        }
        //行点击事件
        protected void Grid_Patent_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            try
            {
                string Person = Grid_Patent.Rows[e.RowIndex].Values[2].ToString();
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                if (Person != username && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                    CBoxSelect.SetCheckedState(e.RowIndex, false);
                    Alert.ShowInTop(str);
                    return;
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //搜索条件
        protected void tchoose_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (dChoose.SelectedValue)
            {
                case "全部":
                    dCondition.Enabled = false;
                    tCondition.Enabled = false;
                    secrecyLevel.Enabled = false;
                    break;
                case "所属机构":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    secrecyLevel.Enabled = false;
                    break; 
                case "申请年份":
                    dCondition.Items.Clear();
                    for (int i = 1960; i <= 2060; i++)
                    {
                        dCondition.Items.Add(i.ToString(), i.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
                    secrecyLevel.Enabled = false;
                    break;
                case "授权年份":
                    dCondition.Items.Clear();
                    for (int i = 1960; i <= 2060; i++)
                    {
                        dCondition.Items.Add(i.ToString(), i.ToString());
                    }
                    dCondition.Items[0].Selected = true;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
                    secrecyLevel.Enabled = false;
                    break;
                case "发明人":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    secrecyLevel.Enabled = false;
                    break;
                case "专利类型":
                    dCondition.Items.Clear();
                     List<BasicCode> listname = ba.FindByCategoryName("专利类型");
                     for (int i = 0; i < listname.Count(); i++)
                      {
                          dCondition.Items.Add(listname[i].CategoryContent.ToString(), listname[i].CategoryContent.ToString());
                      }
                    dCondition.Items[0].Selected = true;
                    dCondition.EnableEdit = false;
                    dCondition.Enabled = true;
                    tCondition.Enabled = false;
                    secrecyLevel.Enabled = false;
                    break;
                case "成员":
                    dCondition.Enabled = false;
                    tCondition.Enabled = true;
                    secrecyLevel.Enabled = false;
                    break;
                case "保密级别":
                    dCondition.Enabled = false;
                    tCondition.Enabled = false;
                    secrecyLevel.Enabled = true;
                    break;
            }
        }

        //导出
        protected void btn_Get_Click(object sender, EventArgs e)
        {
            try
            {
                if (page == 0)
                {
                    List<Common.Entities.Patent> list = patent.FindPaged(Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 1)
                {
                    List<Common.Entities.Patent> list = patent.FindByPatentDepartment(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 3)
                {
                    List<Common.Entities.Patent> list = patent.FindByApplicationTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 4)
                {
                    List<Common.Entities.Patent> list = patent.FindByAccreditTime(Convert.ToInt32(dCondition.SelectedText.Trim()), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 5)
                {
                    List<Patent> list = patent.FindByPatentPeople(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 6)
                {
                    List<Patent> list = patent.FindByMember(tCondition.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else if (page == 7)
                {
                    List<Patent> list = patent.FindBySecrecyLevel(secrecyLevel.SelectedIndex + 1, Convert.ToInt32(Session["SecrecyLevel"]));
                    if (list != null)
                    {
                        Grid_Patent.DataSource = list;
                        Grid_Patent.DataBind();
                    }
                }
                else
                    return;
                pm.ExportExcel(3, Grid_Patent, 4);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //获得作者
        public string getwriter(int writerid)
        {
            try
            {
                string str = patent.FindWriter(writerid);
                if (str != "" || str != null)
                {
                    return str;
                }
                else
                {
                    return " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }
        //获得单位
        public string getunit(int id)
        {
            try
            {
                string str = patent.FindByPatentID(id).PatentDepartment;
                if (str != "" || str != null)
                {
                    return str;
                }
                else
                {
                    return " ";
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                return "";
            }
        }

        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Patent.PageIndex) * Grid_Patent.PageSize;
        }

        //删除所选行数据
        protected void Delete_Click(object sender, EventArgs e)
        {
            BLHelper.BLLPatent blpant = new BLHelper.BLLPatent();
            BLHelper.BLLAttachment blat = new BLHelper.BLLAttachment();
            Common.Entities.OperationLog operate = new OperationLog();
            try
            {
                string username = user.FindByLoginName(Session["LoginName"].ToString()).UserName;
                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    for (int i = 0; i < pm.GridCount(Grid_Patent, CBoxSelect).Count(); i++)
                    {
                        int ss = Convert.ToInt32(Grid_Patent.DataKeys[pm.GridCount(Grid_Patent, CBoxSelect)[i]][0].ToString());
                        int[] attachid = blpant.Delete(ss);
                        string path_Patent = blat.FindPath(attachid[0]);
                        string path_Application = blat.FindPath(attachid[1]);
                        pm.DeleteFile(attachid[0], path_Patent);
                        pm.DeleteFile(attachid[1], path_Application);
                    }
                    InitData();
                    Alert.ShowInTop("删除数据成功!");
                }
                else
                {
                    for (int i = 0; i < pm.GridCount(Grid_Patent, CBoxSelect).Count(); i++)
                    {
                        blpant.UpdateIsPass(Convert.ToInt32(Grid_Patent.DataKeys[pm.GridCount(Grid_Patent, CBoxSelect)[i]][0]), false);
                        operate.LoginName = username;
                        operate.OperationTime = DateTime.Now;
                        operate.LoginIP = " ";
                        operate.OperationContent = "Patents";
                        operate.OperationType = "删除";
                        operate.OperationDataID = Convert.ToInt32(Grid_Patent.DataKeys[pm.GridCount(Grid_Patent, CBoxSelect)[i]][0]);
                        op.Insert(operate);
                    }
                    InitData();
                    Alert.ShowInTop("您的数据已提交，请等待确认!");
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
                Alert.ShowInTop("删除错误，请联系管理员！");
            }
            
        }
    }
}