/**编写人：王会会
 * 时间：2014年8月24号
 * 功能：管理员添加基本代码表数据的相关操作
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

namespace WDFramework.People.ManageBasicCode
{
    public partial class Add_BasicCode : System.Web.UI.Page
    {
        BLHelper.BLLBasicCode bllBasicCode = new BLHelper.BLLBasicCode();
        BasicCode basiccode = new BasicCode();
        BLCommon.PublicMethod publicmethod = new BLCommon.PublicMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropListDropDownListCategoryName();
            }
        }
        //保存
        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (CategoryContent.Text.Trim() == "")
                {
                    Alert.ShowInTop("分类内容不能为空！");
                    CategoryContent.Text = "";
                    return;
                }
                if (bllBasicCode.IsTrue(DropDownListCategoryName.SelectedIndex + 1, DropDownListCategoryName.SelectedItem.Text, CategoryContent.Text))
                {
                    basiccode.CategoryID = DropDownListCategoryName.SelectedIndex + 1;
                    basiccode.CategoryName = DropDownListCategoryName.SelectedItem.Text;
                    basiccode.CategoryContent = CategoryContent.Text.Trim ();
                    bllBasicCode.Insert(basiccode);
                    PageContext.RegisterStartupScript(ActiveWindow.GetConfirmHideRefreshReference() + Alert.GetShowInTopReference("添加成功！"));
                }
                else
                    Alert.ShowInTop("已存在！");
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request); ;
            }
        }
        //重置
        protected void Reset_Click(object sender, EventArgs e)
        {
            try
            {
                DropDownListCategoryName.Reset();
                CategoryContent.Reset();
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }
        }
        //初始化分类名称下拉框
        public void InitDropListDropDownListCategoryName()
        {
            try
            {
                string[] SecrecyLevels = new string[] {"学历","学位","证件类型","政治面貌","学生类型","民族","级别", "项目性质","项目状态"  
                                ,"收录情况","专利类型","通知公告分类名称","机构分类名称","文件分类名称","经费用途分类名称","会议分类名称"
                                  ,"学科分类名称","人员行政级别名称","工作计划计划总结分类","项目来源","合作形式","预期成果","学缘","刊物级别","著作类型","获奖类型","获奖等级","获奖类别",
                                  "平台级别","批复部门","平台类别","报告类别","鉴定级别","鉴定形式","鉴定水平","专利情况","项目等级（一类）","项目等级（二类）","项目等级（三类）"};
                for (int i = 0; i < SecrecyLevels.Count(); i++)
                {
                    DropDownListCategoryName.Items.Add(SecrecyLevels[i], SecrecyLevels[i]);
                }
            }
            catch (Exception ex)
            {
                publicmethod.SaveError(ex, this.Request);
            }    
        }     
    }
}