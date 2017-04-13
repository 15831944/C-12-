<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_FutherStudy.aspx.cs" Inherits="WebApplication1.新增进修学习_接受_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="460px" ShowBorder="True" EnableCollapse="true" AutoHeight ="false" 
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                <Items>
                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server" AutoScroll ="true" 
                        BodyPadding="30px" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label3" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="姓名">
                                    </x:Label>
                                    <x:Label ID="Name" Width="80px" runat="server" CssClass="marginr" ShowRedStar="true" ShowLabel="false" Text="：">
                                    </x:Label>
                                    <x:TextBox ID="tName" MaxLength="10" MaxLengthMessage="最多输入10个字" ShowLabel="true" Label="姓名" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label19" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="性别">
                                    </x:Label>
                                    <x:Label ID="Sex" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                    </x:Label>
                                    <x:RadioButton ID="rbtnBoy" Label="性别" Width="100px" Checked="true" GroupName="sex" Text="男" runat="server">
                                    </x:RadioButton>
                                    <x:RadioButton ID="rbtnGril" GroupName="sex" ShowEmptyLabel="true" Text="女" runat="server">
                                    </x:RadioButton>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label20" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="籍贯">
                                    </x:Label>
                                    <x:Label ID="Hometown" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                    </x:Label>
                                    <x:TextBox ID="tHometown" MaxLength="10" MaxLengthMessage="最多输入10个字" ShowLabel="false" Width="150px" CssClass="marginr" runat="server" TabIndex="2" AutoPostBack="true" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>


                            <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label21" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="联系">
                                    </x:Label>
                                    <x:Label ID="PhoneNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="电话：">
                                    </x:Label>
                                    <x:TextBox ID="tPhoneNum" MaxLength="11" MaxLengthMessage="最多输入11位" RegexPattern="NUMBER" RegexMessage="只能输入数字" ShowLabel="true" Label="联系电话" Width="150px" CssClass="marginr" runat="server" TabIndex="3">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label22" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="电子">
                                    </x:Label>
                                    <x:Label ID="Email" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="邮箱：">
                                    </x:Label>
                                    <x:TextBox ID="tEmail" MaxLength="20" MaxLengthMessage="最多可输入20个字符" RegexPattern="EMAIL" RegexMessage="请输入正确的邮箱格式" ShowLabel="false" Width="150px" CssClass="marginr" runat="server" TabIndex="4" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label23" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="证件">
                                    </x:Label>
                                    <x:Label ID="DocuType" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="类型：">
                                    </x:Label>
                                    <x:DropDownList AutoPostBack="false" EnableSimulateTree="true" ShowLabel="false" TabIndex ="5"
                                            Width="145px" CssClass="marginr"   runat="server" ID="tDocuType">
                                          <x:ListItem Text="居民身份证" Value="1" />
                                            <x:ListItem Text="护照" Value="2" />
                                             <x:ListItem Text="其他" Value="3" />
                                        </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label24" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="证件">
                                    </x:Label>
                                    <x:Label ID="IDNum" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="号码：">
                                    </x:Label>
                                    <x:TextBox ID="tIDNum" MaxLength="20" MaxLengthMessage="最多输入20个字符"  ShowLabel="true" Label="证件号码" Width="150px" CssClass="marginr" runat="server" TabIndex="6" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label25" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="进修">
                                    </x:Label>
                                    <x:Label ID="LearnPlace" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="地点：">
                                    </x:Label>
                                    <x:TextBox ID="tLearnPlace" MaxLength="50" MaxLengthMessage="最多可输入50个字符" ShowLabel="true" Label="进修地点" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="7" AutoPostBack="true" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label26" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="进修">
                                    </x:Label>
                                    <x:Label ID="LearnSchool" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="学校：">
                                    </x:Label>
                                    <x:TextBox ID="tLearnSchool" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="进修学校" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="8" AutoPostBack="true" Regex="^[\u4E00-\u9FA5A-Za-z]+$">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label27" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="所在">
                                    </x:Label>
                                    <x:Label ID="Agency" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="部门：">
                                    </x:Label>
                                    <x:DropDownList Label="所在部门" AutoPostBack="false" Required="true" EnableSimulateTree="true"
                                        runat="server" ID="DropDownListAgency" Width="150px" TabIndex="9">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="30px" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label28" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="出生">
                                    </x:Label>
                                    <x:Label ID="Birthday" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="年月：">
                                    </x:Label>
                                    <x:DatePicker ID="dBirthday" runat="server" Width="150px" Label="出生年月" TabIndex="10">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label29" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="进修">
                                    </x:Label>
                                    <x:Label ID="LearnBeginTime" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="开始时间：">
                                    </x:Label>
                                    <x:DatePicker ID="dLearnBeginTime" runat="server" Width="150px" Label="进修开始时间" EnableEdit="false" Required="true" TabIndex="11">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label30" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="进修">
                                    </x:Label>
                                    <x:Label ID="LearnEndTime" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="结束时间：">
                                    </x:Label>
                                    <x:DatePicker ID="dLearnEndTime" runat="server" Width="150px" Label="进修结束时间"  EnableEdit="false" TabIndex="12">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label31" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="保密">
                                    </x:Label>
                                    <x:Label ID="SecrecyLevel" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="级别：">
                                    </x:Label>
                                    <x:DropDownList ID="DroSecrecyLevel" ShowLabel="true" Label="保密级别" Width="150px" AutoPostBack="true" runat="server" Required="true" TabIndex="13">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label32" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="个人">
                                    </x:Label>
                                    <x:Label ID="Profile1" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="简介：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ID="tPintroduce" Width="150px" Height="80px" AutoGrowHeight="false" TabIndex="14" CssStyle ="overflow-y:scroll"
>
                                    </x:TextArea>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label33" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="进修">
                                    </x:Label>
                                    <x:Label ID="LearnContent" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="内容：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="30" Required="true" MaxLengthMessage="最多输入30个字" ID="tLearnContent" ShowLabel="true" Label="进修内容" Width="150px" Height="60px" AutoGrowHeight="false" TabIndex="15" AutoPostBack="true" CssStyle ="overflow-y:scroll"
>
                                    </x:TextArea>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label34" Width="60px" runat="server" CssClass="marginr" CssStyle="text-align:right" ShowRedStar="true" ShowLabel="false" Text="备注">
                                    </x:Label>
                                    <x:Label ID="Remark" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符 " ID="tRemark" Width="150px" Height="60px" AutoGrowHeight="false" TabIndex="16" CssStyle ="overflow-y:scroll"
>
                                    </x:TextArea>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                        </Items>
                    </x:Panel>
                </Items>
            </x:Panel>

            <x:Panel ID="Panel87" runat="server" Height="35px" ShowBorder="True" EnableCollapse="true"
                BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                <Items>
                    <x:Toolbar ID="Toolbar2" runat="server">
                        <Items>
                            <x:Label ID="Label15" Width="250px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" Icon="Add" Size="Medium" CssClass="marginr" Type="Submit" Text="保存" ValidateForms="Panel2" ValidateTarget="Top" ConfirmText="确定保存？" OnClick="Save_Click">
                            </x:Button>
                            <x:Button ID="Reset" runat="server" Icon="Delete" Size="Medium" CssClass="marginr" Text="重置" ConfirmText="确定重置？" OnClick="Reset_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
            <x:Window ID="Window3" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
                EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加">
            </x:Window>
            <x:Window ID="Window4" Title="查询" Popup="false" EnableIFrame="true" runat="server"
                EnableMaximize="true" EnableResize="true" Target="Parent"
                IsModal="True" Width="750px" Height="450px">
            </x:Window>
        </div>
    </form>
</body>
</html>
