/*
 * 编写人：陈启明
 * 时间：2015年12月5日
 * 功能：项目-项目全部信息模块 项目相关文档添加功能
 * 修改履历：    1、修改人：吕博杨
 *                 修改时间：2015年12月7日
 *                 修改内容：添加保密级别字段的读写以及修复保存中出现的BUG
 */
using Common.Entities;
using FineUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WDFramework.Projects.Projects
{
    public partial class Add_Document : System.Web.UI.Page
    {
        BLHelper.BLLProjectFile project = new BLHelper.BLLProjectFile();
        Common.Entities.ProjectFile proj = new Common.Entities.ProjectFile();
        BLCommon.PublicMethod pm = new BLCommon.PublicMethod();
        BLHelper.BLLUser user = new BLHelper.BLLUser();
        Common.Entities.OperationLog log = new OperationLog();
        BLHelper.BLLOperationLog bllLog = new BLHelper.BLLOperationLog();
        BLHelper.BLLAttachment bllAttachment = new BLHelper.BLLAttachment();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Convert.ToInt32(Session["ProjectID"]) == 0)
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("必须选择一个项目！"));
                    return;
                }
                InitDropDownList();
                //lby ↓
                InitDropListSecrecyLevel();
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (tDocument.Text.Trim() == null)
                {
                    Alert.Show("文档编号不能为空！");
                    return;
                }
                if (tNDocument.Text.Trim() == null)
                {
                    Alert.Show("文档名不能为空！");
                    return;
                }
                //lby ↓
                List<ProjectFile> list = project.FindByFileCode(tDocument.Text.Trim(), Convert.ToInt32(Session["SecrecyLevel"]));
                if (list.Count != 0)
                    if (list.FirstOrDefault().IsPass == true)
                    {
                        Alert.ShowInTop("文件已存在！");
                        tDocument.Text = "";
                        return;
                    }
                    else
                    {
                        Alert.ShowInTop("该文件正在审核中，请等待！");
                        tDocument.Text = "";
                        return;
                    }
                proj.ProjectID = Convert.ToInt32(Session["ProjectID"]);
                proj.FileCode = tDocument.Text.Trim();
                proj.FileName = tNDocument.Text.Trim();
                proj.FileType = DropDownListDocument.SelectedText;
                proj.SecrecyLevel = listSecrecyLevel.SelectedIndex + 1;
                proj.EntryPerson = user.FindByLoginName(Session["LoginName"].ToString()).UserName;

                //lby ↓
                int attachid = pm.UpLoadFile(fileupload).Attachid;

                if (Convert.ToInt32(Session["SecrecyLevel"]) == 5)
                {
                    proj.IsPass = true;
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
                    if (attachid != -3)
                        proj.AttachmentID = attachid;
                    else
                    {
                        //proj.AttachmentID = 0;
                        Alert.Show("没有上传文件！");
                        return;
                    }
                    //向表中插入数据
                    if (project.InsertProjectFile(proj))
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息已添加完成！"));
                    else
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息添加失败！"));
                }
                else
                {
                    proj.IsPass = false;
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
                    if (attachid != -3)
                        proj.AttachmentID = attachid;
                    else
                    {
                        Alert.Show("没有上传文件！");
                        return;
                        //proj.AttachmentID = 0;
                    }
                    //向表中插入数据
                    if (project.InsertProjectFile(proj))
                    {
                        log.LoginName = Session["LoginName"].ToString();
                        log.OperationTime = DateTime.Now;
                        log.LoginIP = " ";
                        log.OperationContent = "ProjectFile";
                        log.OperationType = "添加";
                        log.OperationDataID = proj.ProjectFileID;
                        bllLog.Insert(log);
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息已提交审核"));
                    }
                    else
                        PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("文件信息提交失败！"));
                }
            }
            catch (Exception ex)
            {
                int attachid = Convert.ToInt32(pm.UpLoadFile(fileupload).Attachid);
                string path = bllAttachment.FindPath(attachid);
                pm.DeleteFile(attachid, path);
                pm.SaveError(ex, this.Request);
            }
        }

        //初始化下拉框
        public void InitDropDownList()
        {
            try
            {
                string[] filetype = { "合同", "技术协议", "工作报告", "技术报告", "总结报告", "验收报告", "测试报告", "查询报告", "鉴定报告" };
                for (int i = 0; i < filetype.Length; ++i)
                    DropDownListDocument.Items.Add(filetype[i], filetype[i]);
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
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
                pm.SaveError(ex, this.Request);
            }
        }

        //重置
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                tDocument.Reset();
                tNDocument.Reset();
                DropDownListDocument.Reset();
                listSecrecyLevel.Reset();
                PageContext.RegisterStartupScript("clearFile();");
            }
            catch (Exception ex)
            {
                pm.SaveError(ex, this.Request);
            }
        }
    }
}