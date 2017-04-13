/**编写人：方淑云
 * 时间：2014年8月24号
 * 功能:下载
 * 修改履历：
 **/
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Achievement.AchievementInfo
{
    public partial class Operate : System.Web.UI.Page
    {
        BLHelper.BLLAttachment at = new BLHelper.BLLAttachment();
        BLHelper.BLLAgency agen = new BLHelper.BLLAgency();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLAchievement ach = new BLHelper.BLLAchievement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitData();
            }
        }
        //初始化
        public void InitData()
        {
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                Common.Entities.Achievement caa = ach.FindAll(id);
                name.Text = caa.AchievementName;
                agency.Text = agen.FindAgenName(caa.AgencyID);
                unit.Text = caa.AppraisalUnit;
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            FineUI.LinkButton lb = (FineUI.LinkButton)sender;
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = ach.FindAttachment(id);
                Common.Entities.Achievement model = ach.Findmodel(id);
                switch (lb.ID)
                {
                    case "Delete":
                        attachId = model.AttachmentID == null ? 0 : model.AttachmentID.Value;
                        break;
                    case "DelOpinionPage":
                        attachId = model.OpinionPage == null ? 0 : model.OpinionPage.Value;
                        break;
                    case "DelMemberPage":
                        attachId = model.MemberPage == null ? 0 : model.MemberPage.Value;
                        break;
                    case "DelSealPage":
                        attachId = model.SealPage == null ? 0 : model.SealPage.Value;
                        break;

                }
                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果不存在相关文档");
                }
                else
                {
                    pm.DeleteFile(attachId, srcPath);
                    Common.Entities.Achievement caa = ach.FindAll(id);
                    caa.AttachmentID = null;
                    ach.Update(caa);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
        //下载
        protected void DownFile_Click(object sender, EventArgs e)
        {
            FineUI.LinkButton lb = (FineUI.LinkButton)sender;
            try
            {
                int id = Convert.ToInt32(Request.QueryString["id"].ToString());

                //int attachId = ach.FindAttachment(id);
                Common.Entities.Achievement model = ach.Findmodel(id);
                int attachId = 0;
                switch (lb.ID)
                {
                    case "DownFile":
                        attachId = model.AttachmentID == null ? 0 : model.AttachmentID.Value;
                        break;
                    case "DownOpinionPage":
                        attachId = model.OpinionPage == null ? 0 : model.OpinionPage.Value;
                        break;
                    case "DownMemberPage":
                        attachId = model.MemberPage == null ? 0 : model.MemberPage.Value;
                        break;
                    case "DownSealPage":
                        attachId = model.SealPage == null ? 0 : model.SealPage.Value;
                        break;
                        
                }

                string srcPath = at.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该成果不存在相关文档");
                }
                else
                {
                    pm.DownloadFile(srcPath);
                }
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}