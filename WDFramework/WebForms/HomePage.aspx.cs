/**编写人：李金秋
 * 时间：2014年8月13号
 * 功能：通知公告表的相关操作
 * 修改履历：
 **/
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.WebForms
{
    public partial class HomePage : System.Web.UI.Page
    {
        //通知公告分类
        //通知1 学校公告2 外来公告3
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLHelper.BLLAnnouncement BLLAnnouncemet = new BLHelper.BLLAnnouncement();
        BLHelper.BLLOperationLog op = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment BLLattachment = new BLHelper.BLLAttachment();
        BLHelper.BLLUser BLLUser = new BLHelper.BLLUser();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();

        Common.Entities.OperationLog operate = new OperationLog();
        static int page;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListAnnouceSort();
                btn_AddAnnouncement.OnClientClick = Window_addAnnouncement.GetShowReference("~/Announcement/Add_Announcement.aspx", "增加通知公告");
                //btn_UpdateAnnouncement.OnClientClick = Window_UpdateAnnouncement.GetShowReference("~/Announcement/Update_Announcement.aspx", "修改通知公告");
                BindGrid_Announce();
                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    btn_AddAnnouncement.Hidden = true;
                    btnDelete.Hidden = true;
                    BoxSelect_Announce.Enabled = false;
                    BoxSelect_Announce.Hidden = true;
                }
            }
        }
        //通知公告绑定 
        public void BindGrid_Announce()
        {
            try
            {
                page = 0;
                List<Common.Entities.Announcement> list = BLLAnnouncemet.FindAll();
                var res = list.Skip(Grid_Announce.PageIndex * Grid_Announce.PageSize).Take(Grid_Announce.PageSize).ToList();
                Grid_Announce.RecordCount = list.Count();
                this.Grid_Announce.DataSource = res;
                this.Grid_Announce.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据通知查找
        public void FindByiInform()
        {
            try
            {
                page = 1;
                Grid_Announce.PageIndex = 0;
                List<Common.Entities.Announcement> list = new List<Common.Entities.Announcement>();
                list = BLLAnnouncemet.FindPaged("通知");
                var res = list.Skip(Grid_Announce.PageIndex * Grid_Announce.PageSize).Take(Grid_Announce.PageSize).ToList();
                Grid_Announce.RecordCount = list.Count();
                this.Grid_Announce.DataSource = res;
                this.Grid_Announce.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //根据学校公告查找
        public void FindBySchoolAnnounce()
        {
            try
            {
                page = 2;
                List<Common.Entities.Announcement> list = new List<Common.Entities.Announcement>();
                list = BLLAnnouncemet.FindPaged("学校公告");
                var res = list.Skip(Grid_Announce.PageIndex * Grid_Announce.PageSize).Take(Grid_Announce.PageSize).ToList();
                Grid_Announce.RecordCount = list.Count();
                this.Grid_Announce.DataSource = res;
                this.Grid_Announce.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }

        }
        //根据外来公告查找
        public void FindByOutAnnounce()
        {
            try
            {
                page = 3;
                List<Common.Entities.Announcement> list = new List<Common.Entities.Announcement>();
                list = BLLAnnouncemet.FindPaged("外来公告");
                var res = list.Skip(Grid_Announce.PageIndex * Grid_Announce.PageSize).Take(Grid_Announce.PageSize).ToList();
                Grid_Announce.RecordCount = list.Count();
                this.Grid_Announce.DataSource = res;
                this.Grid_Announce.DataBind();
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //分页
        protected void Grid_Inform_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            this.Grid_Announce.PageIndex = e.NewPageIndex;
            //BindGrid_Announce();
            switch (page)
            {
                case 0:
                    BindGrid_Announce();
                    break;
                case 1:
                    FindByiInform();
                    break;
                case 2:
                    FindBySchoolAnnounce();
                    break;
                case 3:
                    FindByOutAnnounce();
                    break;
            }
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid_Announce.PageIndex = 0;
            this.Grid_Announce.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);
            //BindGrid_Announce();
            switch (page)
            {
                case 0:
                    BindGrid_Announce();
                    break;
                case 1:
                    FindByiInform();//搜索通知
                    break;
                case 2:
                    FindBySchoolAnnounce();//搜索学校公告
                    break;
                case 3:
                    FindByOutAnnounce();//搜索外来公告
                    break;
            }
        }
        //刷新事件
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            btnDelete.Enabled = false;
            DropDownList_AnnouceSort.SelectedValue = "0";
            BindGrid_Announce();
        }
        //删除(管理员可删除)
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int m;
                //取整数（不是四舍五入，全舍）
                int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Announce.RecordCount / this.Grid_Announce.PageSize));
                List<int> selections = new List<int>();
                if (Grid_Announce.PageIndex == Pages)
                    m = (Grid_Announce.RecordCount - this.Grid_Announce.PageSize * Grid_Announce.PageIndex);
                else
                    m = this.Grid_Announce.PageSize;
                for (int i = 0; i < m; i++)
                {
                    if (BoxSelect_Announce.GetCheckedState(i))
                        selections.Add(i);
                }
                for (int i = 0; i < selections.Count(); i++)
                {
                    Common.Entities.Announcement NewAnnounce = BLLAnnouncemet.Find(Convert.ToInt32(Grid_Announce.DataKeys[selections[i]][0].ToString()));
                    //删除通知公告
                    int attachID = BLLAnnouncemet.Delete(NewAnnounce.AnnouncementID);
                    //删除附件表信息
                    BLLattachment.Delete(attachID);
                    string path = BLLattachment.FindPath(attachID);
                    if (attachID != 0 && path != "")
                        pm.DeleteFile(attachID, path);
                }
                btnDelete.Enabled = false;
                DropDownList_AnnouceSort.SelectedValue = "0";
                Grid_Announce.PageIndex = 0;
                Grid_Announce.PageSize = 20;
                BindGrid_Announce();
                Alert.ShowInTop("删除数据成功!");
            }
            catch (Exception ex)
            {
                BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
                pm.SaveError(ex, this.Request);
            }
        }
        protected void Grid_Files_RowCommand(object sender, GridCommandEventArgs e)
        {

            int AnnounceID = Convert.ToInt32(Grid_Announce.DataKeys[e.RowIndex][0].ToString());
            string Person = BLLAnnouncemet.Find(AnnounceID).EntryPerson;
            string strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            strs = BLLUser.FindByLoginName(Session["LoginName"].ToString()).UserName;
            if (Person != strs && Convert.ToInt32(Session["SecrecyLevel"]) != 5)
            {
                string str = "您无对此行操作的权限！此行信息为" + Person + "录入，请与管理员联系!";
                BoxSelect_Announce.SetCheckedState(e.RowIndex, false);
                Alert.ShowInTop(str);
            }
            int m;
            //取整数（不是四舍五入，全舍）
            int Pages = (int)Math.Floor(Convert.ToDouble(Grid_Announce.RecordCount / this.Grid_Announce.PageSize));

            if (Grid_Announce.PageIndex == Pages)
                m = (Grid_Announce.RecordCount - this.Grid_Announce.PageSize * Grid_Announce.PageIndex);
            else
                m = this.Grid_Announce.PageSize;
            List<int> selections = new List<int>();
            for (int i = 0; i < m; i++)
            {
                if (BoxSelect_Announce.GetCheckedState(i))
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
            switch (DropDownList_AnnouceSort.SelectedText)
            {
                case "全部":
                    BindGrid_Announce();
                    break;
                case "通知":
                    FindByiInform();
                    break;
                case "学校公告":
                    FindBySchoolAnnounce();
                    break;
                case "外来公告":
                    FindByOutAnnounce();
                    break;
            }
            btnDelete.Enabled = false;
        }
        //下载界面跳转
        protected string GetRecordUrlDown(object AnnouncementID)
        {
            return Window_DownLoad.GetShowReference("/Announcement/DownLoad_Announce.aspx?id=" + AnnouncementID, "下载");
        }
        //初始化通知公告分类名称下拉框
        public void InitDropDownListAnnouceSort()
        {
            List<BasicCode> list = bllBasicCode.FindALLName("通知公告分类名称");
            for (int i = 0; i < list.Count(); i++)
            {
                DropDownList_AnnouceSort.Items.Add(list[i].CategoryContent.ToString(), (i + 1).ToString());
            }
        }
        //grid序号
        public int RowNumber(int dataItemIndex)
        {
            return dataItemIndex + (Grid_Announce.PageIndex) * Grid_Announce.PageSize;
        }
    }
}