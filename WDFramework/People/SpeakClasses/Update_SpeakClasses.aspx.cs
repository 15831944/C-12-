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
    public partial class Update_SpeakClasses : System.Web.UI.Page
    {
        BLHelper.BLLUser bllUser = new BLHelper.BLLUser();
        BLHelper.BLLOperationLog bllOperate = new BLHelper.BLLOperationLog();
        BLHelper.BLLSpeakClass bllSpeak = new BLHelper.BLLSpeakClass();
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownListSort();
                BindData();
                DatePickerTeachingTime.MaxDate = DateTime.Now;
            }
        }
        public void BindData()
        {
            try
            {
                List<SpeakClass> list = bllSpeak.FindSpeakClass(Convert.ToInt32(Session["SpeakClassID"]));
                SpeakClass speak = list.FirstOrDefault();
                UserInfoName.Text = bllUser.FindByUserID(Convert.ToInt32(speak.UserInfoID));
                ClassName.Text = speak.ClassName;
                Specialty.Text = speak.Specialty;
                DropDownListTeachingDegree.SelectedValue = speak.TeachingDegree;
                DatePickerTeachingTime.SelectedDate = speak.TeachingTime;
                Grade.Text = speak.Grade;
                DropDownListSecrecyLevel.SelectedIndex = Convert.ToInt32(speak.SecrecyLevel - 1);
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserInfoName.Text.Trim() != "")
                {
                    if (bllUser.IsUser(UserInfoName.Text.Trim()) != null)
                    {
                        if (bllUser.IsUser(UserInfoName.Text.Trim()).IsPass == true)
                        {
                            if (ClassName.Text.Trim() == "")
                            {
                                Alert.ShowInTop("课程名称不能为空！");
                                ClassName.Text = "";
                                return;
                            }
                            SpeakClass speak = new SpeakClass();
                            speak.UserInfoID = bllUser.FindID(UserInfoName.Text.Trim());
                            speak.ClassName = ClassName.Text.Trim();
                            speak.Specialty = Specialty.Text.Trim();
                            speak.TeachingDegree = DropDownListTeachingDegree.SelectedItem.Text;
                            speak.TeachingTime = DatePickerTeachingTime.SelectedDate;
                            speak.Grade = Grade.Text.Trim();
                            speak.SecrecyLevel = DropDownListSecrecyLevel.SelectedIndex + 1;
                            speak.EntryPerson = bllSpeak.Find(Convert.ToInt32(Session["SpeakClassID"])).EntryPerson;
                            if (Convert.ToInt32(Session["SecrecyLevel"]) != 5)
                            {
                                bllSpeak.UpdateIsPass(Convert.ToInt32(Session["SpeakClassID"]), false);
                                speak.IsPass = false;
                                bllSpeak.InsertForPeople(speak); //插入社会兼职表
                                OperationLog operate = new OperationLog();
                                operate.LoginName = bllSpeak.Find(Convert.ToInt32(Session["SpeakClassID"])).EntryPerson;
                                operate.LoginIP = "";
                                operate.OperationType = "更新";
                                operate.OperationContent = "SpeakClass";
                                operate.OperationDataID = Convert.ToInt32(Session["SpeakClassID"]);
                                operate.OperationTime = System.DateTime.Now;
                                operate.Remark = speak.SpeakClassID.ToString();
                                bllOperate.Insert(operate);//插入操作表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员主讲课程信息已提交审核！"));
                            }
                            else
                            {
                                speak.IsPass = true;
                                speak.SpeakClassID = Convert.ToInt32(Session["SpeakClassID"]);
                                bllSpeak.Update(speak); //更新社会兼职表
                                PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("人员主讲课程信息已修改完成！"));
                            }
                        }
                        else
                            Alert.ShowInTop("该人员正在审核中！");
                    }
                    else
                    {
                        Alert.ShowInTop("人员不存在！");
                        UserInfoName.Text = "";
                    }
                }
                else
                {
                    Alert.ShowInTop("人员名称不能为空！");
                    UserInfoName.Text = "";
                    return;
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                ClassName.Reset();
                Specialty.Reset();
                DropDownListTeachingDegree.Reset();
                DatePickerTeachingTime.Reset();
                Grade.Reset();
                DropDownListSecrecyLevel.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化教学对象（学历）下拉框
        public void InitDropDownListSort()
        {
            try
            {
                List<BasicCode> list = bllBasicCode.FindALLName("学历");
                for (int i = 0; i < list.Count(); i++)
                {
                    DropDownListTeachingDegree.Items.Add(list[i].CategoryContent.ToString(), list[i].CategoryContent.ToString());
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
    }
}