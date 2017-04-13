using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Projects
{
    public partial class OperateBenefit : System.Web.UI.Page
    {
        BLHelper.BLLProject bllProject = new BLHelper.BLLProject();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
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
                int ProjectID = Convert.ToInt32(Request.QueryString["id"].ToString());
                List<Common.Entities.Project> list = bllProject.FindProject(ProjectID, Convert.ToInt32(Session["SecrecyLevel"]));
                // Common.Entities.Award awa = list.FirstOrDefault();
                Common.Entities.Project project = list.FirstOrDefault();
                ProjectName.Text = project.ProjectName;
                ProjectState.Text = project.ProjectState;
                ProjectHeads.Text = project.ProjectHeads;
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }

        //删除
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                int ProjectID = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = bllProject.FindBenefit(ProjectID);
                string srcPath = bllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该项目不存在相关文档");
                }
                else
                {
                    publicmethod.DeleteFile(attachId, srcPath);
                    Common.Entities.Project project = bllProject.FindProject(ProjectID, Convert.ToInt32(Session["SecrecyLevel"])).FirstOrDefault();
                    project.BenefitAttachment = null;
                    bllProject.Update(project);
                    Alert.Show("删除成功！");
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideReference() + Alert.GetShowInTopReference("删除成功！"));
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //下载
        protected void DownFile_Click(object sender, EventArgs e)
        {
            try
            {
                int ProjectID = Convert.ToInt32(Request.QueryString["id"].ToString());
                int attachId = bllProject.FindBenefit(ProjectID);
                string srcPath = bllAttachment.FindPath(attachId);
                if (attachId == 0 || srcPath == "")
                {
                    Alert.Show("该项目不存在相关文档");
                }
                else
                {
                    publicmethod.DownloadFile(srcPath);
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}