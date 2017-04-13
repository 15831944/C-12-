/*
 * 编写人：吕博扬
 * 时间：2015年12月5日
 * 功能：项目相关文档信息更新界面
 * 修改履历：    暂无
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using BLHelper;
using BLCommon;
using Common.Entities;

namespace WDFramework.Projects.Projects
{
    public partial class Update_Document : System.Web.UI.Page
    {
        ProjectFile projectFile = new ProjectFile();
        BLLProjectFile bllProjectFile = new BLLProjectFile();
        BLLProject bllProject = new BLLProject();
        BLLAttachment bllAttachment = new BLLAttachment();
        OperationLog operationLog = new OperationLog();
        BLLOperationLog bllOperationLog = new BLLOperationLog();
        PublicMethod publicMethod = new PublicMethod();
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropListFileType();
                InitDropListSecrecyLevel();
                BindData();
            }
        }

        //绑定数据
        public void BindData()
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Convert.ToInt32(Session["ProjectFileID"]) == 0)
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("必须选择一个文件！"));
                        return;
                    }
                    projectFile = bllProjectFile.FindByProjectFileId(Convert.ToInt32(Session["ProjectFileID"]), Convert.ToInt32(Session["SecrecyLevel"]));
                    if (projectFile == null)
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("没有检测到文件！"));
                        return;
                    }
                    txtFileCode.Text = projectFile.FileCode.ToString();
                    txtFileName.Text = projectFile.FileName;
                    listFileType.SelectedValue = projectFile.FileType;
                    listSecrecyLevel.SelectedIndex = Convert.ToInt32(projectFile.SecrecyLevel - 1);
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //初始化文件类型下拉列表
        public void InitDropListFileType()
        {
            try
            {
                string[] FileTypes = new string[] { "合同", "技术协议", "工作报告", "技术报告", "总结报告", "验收报告", "测试报告", "查询报告", "鉴定报告" };
                foreach(string temp in FileTypes)
                    listFileType.Items.Add(temp, temp);
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //初始化涉密等级下拉列表
        public void InitDropListSecrecyLevel()
        {
            try
            {
                string[] SecrecyLevels = new string[] { "四级", "三级", "二级", "一级", "管理员" };
                for (int i = 0; i < Convert.ToInt32(Session["SecrecyLevel"]); i++)
                {
                    listSecrecyLevel.Items.Add(SecrecyLevels[i].ToString(), SecrecyLevels[i].ToString());
                }
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //更新文件信息
        public void Update(object sender, EventArgs e)
        {
            try
            {
                projectFile = bllProjectFile.FindByProjectFileId(Convert.ToInt32(Session["ProjectFileID"]), Convert.ToInt32(Session["SecrecyLevel"]));
                projectFile.FileCode = txtFileCode.Text;
                projectFile.FileName = txtFileName.Text.Trim();
                projectFile.FileType = listFileType.SelectedText;
                projectFile.SecrecyLevel = listSecrecyLevel.SelectedIndex + 1;
                projectFile.EntryPerson = bllUser.FindByLoginName(Session["LoginName"].ToString()).UserName;

                int AttachmentID = bllProjectFile.FindAttachmentID(Convert.ToInt32(Session["ProjectFileID"]));
                string path = bllAttachment.FindPath(AttachmentID);

                if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                {
                    bllProjectFile.ChangePass(Convert.ToInt32(Session["ProjectFileID"]), false);
                    projectFile.IsPass = false;
                    operationLog.LoginIP = " ";
                    operationLog.LoginName = Session["LoginName"].ToString();
                    operationLog.OperationType = "更新";
                    operationLog.OperationContent = "ProjectFile";
                    operationLog.OperationTime = DateTime.Now;

                    int attachid = publicMethod.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//有值
                    {
                        switch (attachid)
                        {
                            case -1:
                                Alert.ShowInTop("文件类型不符，请重新选择！");
                                return;
                            case 0:
                                Alert.ShowInTop("文件名已经存在！");
                                return;
                            case -2:
                                Alert.ShowInTop("文件不能大于150M");
                                return;
                        }
                        projectFile.AttachmentID = attachid;//附件为新插入的附件ID              
                    }
                    else//上传控件没有值
                    {
                        Alert.Show("没有上传文件！");
                        return;
                        //if (AttachmentID != 0)//原来有附件
                        //    projectFile.AttachmentID = AttachmentID;
                    }
                    bllProjectFile.InsertProjectFile(projectFile);//插入
                    operationLog.OperationDataID = Convert.ToInt32(Session["ProjectFileID"]);
                    operationLog.Remark = projectFile.ProjectFileID.ToString();
                    bllOperationLog.Insert(operationLog);//将更新插入操作表    
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息已提交，正在等待审核！"));
                }
                else
                {
                    projectFile.IsPass = true;
                    int attachid = publicMethod.UpLoadFile(fileupload).Attachid;
                    if (attachid != -3)//上传控件是否有值
                    {
                        switch (attachid)
                        {
                            case -1:
                                Alert.ShowInTop("文件类型不符，请重新选择！");
                                return;
                            case 0:
                                Alert.ShowInTop("文件名已经存在！");
                                return;
                            case -2:
                                Alert.ShowInTop("文件不能大于150M");
                                return;
                        }
                        projectFile.AttachmentID = attachid;//附件为新插入的附件ID
                        publicMethod.DeleteFile(AttachmentID, path);
                    }
                    else //上传控件没有值
                    {
                        Alert.Show("没有上传文件！");
                        return;
                        //if (AttachmentID != 0)
                        //    projectFile.AttachmentID = AttachmentID;
                    }
                    bllProjectFile.Update(projectFile);//直接更新
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息已更新完成！"));
                }

            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }

        //重置输入信息
        public void Reset(object sender, EventArgs e)
        {
            try
            {
                txtFileCode.Reset();
                txtFileName.Reset();
                listFileType.Reset();
                listSecrecyLevel.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                publicMethod.SaveError(ex, this.Request);
            }
        }
    }
}