<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_StaffInfos.aspx.cs" Inherits="WDFramework.People.Add_StaffInfos" %>

<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'  >
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true"
            ShowHeader="false" Title="用户管理">
            <Items>


                <%--Height="350px" Width="850px"  --%>
                <x:Panel ID="Panel2" runat="server" ShowBorder="True" EnableCollapse="false" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="370px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="人员信息" runat="server" ColumnWidth="50%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="name" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="用户姓名：">
                                        </x:Label>
                                        <x:TextBox ID="T_UserName" Label="用户姓名" MaxLength="20"  MaxLengthMessage="最多输入20个字符" ShowLabel="true"
                                            Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="1">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>
                               
                             <%--   <x:Panel ID="Panel29" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label34" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="校园一卡通号：">
                                        </x:Label>
                                        <x:TextBox ID="T_UserInfoBH" ShowLabel="true" Label="校园一卡通号"  MaxLength="20" MaxLengthMessage="最多输入20个字符" Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex ="2">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>--%>
                                <x:Label ID="Label41" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel34" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label35" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="登录名（校园一卡通号）：">
                                        </x:Label>
                                        <x:TextBox ID="T_LoginName" ShowLabel="true" Label="登录名（校园一卡通号）" MaxLength ="20" MaxLengthMessage ="最多输入20个字符" Required="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="3">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label42" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel35" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label36" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="用户密码：">
                                        </x:Label>
                                        <x:TextBox ID="T_LoginPWD" ShowLabel="true" Label="用户密码" MaxLength ="20"  MaxLengthMessage ="最多输入20个字符" MinLength ="6" MinLengthMessage ="最少输入六个字符" CssClass="input" TextMode="Password" Required="true" Width="200px" runat="server" TabIndex ="4">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel45" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label62" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="确定密码：">
                                        </x:Label>
                                        <x:TextBox ID="IsPWD" ShowLabel="true" Label="用户密码" MaxLength ="20" MaxLengthMessage ="最多输入20个字符" MinLength ="6" MinLengthMessage ="最少输入六个字符" CssClass="input" TextMode="Password" Required="true"  Width="200px"  runat="server" TabIndex ="5">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                               <x:Label ID="Label69" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Sex" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="性别：">
                                        </x:Label>
                                        <x:RadioButton ID="rbtnBoy" Label="性别" GroupName="sex" Text="男" Checked ="true"  runat="server" TabIndex ="6">
                                        </x:RadioButton>
                                        <x:RadioButton ID="rbtnGril" GroupName="sex" ShowEmptyLabel="true" Text="女" runat="server">
                                        </x:RadioButton>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel30" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Nation" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="民族：">
                                        </x:Label>
                                     
                                         <x:DropDownList AutoPostBack="false" Required ="true"  Label="民族" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex="7"
                                            runat="server" ID="DropDownListNation">
                                           
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="nativeplace" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="籍贯：">
                                        </x:Label>
                                        <x:TextBox ID="T_Hometown" ShowLabel="true" Label="籍贯" MaxLength ="20" MaxLengthMessage ="最多输入20个字符"   Width="200px" CssClass="marginr" runat="server" TabIndex ="8">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label44" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel36" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label45" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="户籍地：">
                                        </x:Label>
                                        <x:TextBox ID="T_Domicile" ShowLabel="true" Label="户籍地" Width="200px" MaxLength ="20" MaxLengthMessage ="最多输入20个字符" CssClass="marginr" runat="server" TabIndex ="9">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="DocumentsTypeNum" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="家庭住址：">
                                        </x:Label>
                                        <x:TextBox ID="T_HomeAddress" ShowLabel="true" Width ="200px" Label="家庭住址" MaxLength ="50" MaxLengthMessage ="最多输入50个字符"  CssClass="marginr" runat="server" TabIndex ="10">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label64" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label3" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="出生年月：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Required="true" Label="出生年月" EmptyText="请选择出生日期" Width="195px" CssClass="marginr" TabIndex ="11"
                                            ID="DatePickerBirth" EnableEdit ="false" >
                                        </x:DatePicker>
                                       
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label65" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="婚姻状况：">
                                        </x:Label>
                                        <x:RadioButton ID="ISMarriage" Label="性别"  GroupName="ISMarriage" Text="已婚"  runat="server" TabIndex ="12">
                                        </x:RadioButton>
                                        <x:RadioButton ID="NotMarriage" GroupName="ISMarriage"  Text="未婚" Checked ="true"  runat="server">
                                        </x:RadioButton>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Telenum" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="手机号码：">
                                        </x:Label>
                                        <x:TextBox ID="T_Telenum" ShowLabel="true" Label="手机号码" Regex="^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$" RegexMessage ="只能输入手机号码格式" MaxLength ="15" Width="200px" CssClass="marginr" runat="server" TabIndex ="13">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="HomeTetlnum" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="家庭号码：">
                                        </x:Label>
                                        <x:TextBox ID="T_HomeTetlum" ShowLabel="true" Label="家庭号码" Regex="\d{3}-\d{8}|\d{4}-\d{7}" RegexMessage ="只能输人家庭号码格式" Width="200px" CssClass="marginr" runat="server" MaxLength ="15" TabIndex ="14">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                   <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Officenum" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="办公电话：">
                                        </x:Label>
                                        <x:TextBox ID="T_Officenum" ShowLabel="true" Label="办公电话" Regex="\d{3}-\d{8}|\d{4}-\d{7}" RegexMessage ="只能输入办公电话格式" MaxLength ="15" Width="200px" CssClass="marginr" runat="server" TabIndex ="15">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label24" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label25" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="传真：">
                                        </x:Label>
                                       <%--  Regex ="/^[+]{0,1}(\d){1,3}[ ]?([-]?((\d)|[ ]){1,12})+$/"--%>
                                        <x:TextBox ID="T_Fax" ShowLabel="true" Label="传真" Width="200px" CssClass="marginr" runat="server"  RegexMessage ="只能输入传真格式" MaxLength ="15" MaxLengthMessage ="最多输入15个字符"  TabIndex ="16">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                  <x:Label ID="Label48" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel26" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label31" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="QQ号码：">
                                        </x:Label>
                                        <x:TextBox ID="T_QQnum" ShowLabel="true" Label="QQ号码" Regex="[1-9][0-9]{4,}" RegexMessage ="只能输入正确的QQ号码格式" Width="200px" MaxLength ="15" CssClass="marginr" runat="server" TabIndex ="17">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="电子信箱：">
                                        </x:Label>
                                        <x:TextBox ID="T_Email" ShowLabel="true" Label="电子信箱" RegexPattern="EMAIL" RegexMessage ="只能输入邮箱格式" Width="200px" MaxLength ="30" CssClass="marginr" runat="server" TabIndex ="18">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label49" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label26" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="邮政编码：">
                                        </x:Label>
                                        <x:TextBox ID="T_PostalCode" ShowLabel="true" Label="邮政编码" RegexPattern="POSTAL_CODE" RegexMessage ="只能输入正确的邮政编码" MaxLength ="10" Width="200px" CssClass="marginr" runat="server" TabIndex ="19">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                 <x:Label ID="Label66" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label30" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证件类型：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Required ="true" EnableSimulateTree="true" ShowLabel="true" Label ="证件类型" TabIndex ="20"
                                            Width="195px" CssClass="marginr"   runat="server" ID="DropDownListDocumentsType">
                                          <%--  <x:ListItem Text="居民身份证" Value="1" />
                                            <x:ListItem Text="护照" Value="2" />
                                             <x:ListItem Text="其他" Value="3" />--%>
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label27" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label28" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="证件号码：">
                                        </x:Label>
                                        <x:TextBox ID="T_DocumentsNum" ShowLabel="true" Label="证件号码"  MaxLength ="20"   Width="200px" CssClass="marginr" runat="server" TabIndex ="21">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label50" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel455" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label55" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="入校时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Required="true" Label="入校时间" EmptyText="请选择入校时间" Width="195px" CssClass="marginr" TabIndex ="22"
                                            ID="DatePickerEnterSchoolTime" EnableEdit ="false" >
                                        </x:DatePicker>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label58" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel33" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label54" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="最后毕业学校：">
                                        </x:Label>
                                        <x:TextBox ID="LastSchool" ShowLabel="true" Label="最后毕业学校"  RegexMessage ="只能输入正确的格式" MaxLength ="30"  Width="200px" CssClass="marginr" runat="server" TabIndex ="23">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label70" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel38" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label68" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学缘：">
                                        </x:Label>                                       
                                        <x:DropDownList AutoPostBack="false" Required ="true"  Label="学缘" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="24"
                                             runat="server" ID="DropDownListStudySource">                  
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label17" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学历：">
                                        </x:Label>                                       
                                        <x:DropDownList AutoPostBack="false" Required ="true"  Label="学历" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="25"
                                             runat="server" ID="DropDownListEducation">                  
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学位：">
                                        </x:Label>
                                       <%-- <x:TextBox ID="T_Degree" ShowLabel="true" Label="学位" Width="150px" MaxLength ="10" MaxLengthMessage ="最多输入10个字符" Required ="true"  CssClass="marginr" runat="server" TabIndex ="17">
                                        </x:TextBox>--%>
                                        <x:DropDownList AutoPostBack="false" Required ="true"  Label="学位" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="26"
                                             runat="server" ID="DropDownListDegree">
                                           <%--  <x:ListItem Text="学士" Value="0" Selected ="true" />
                    <x:ListItem Text="硕士" Value="1" />
                    <x:ListItem Text="博士" Value="2" />
                                            <x:ListItem Text="博士后" Value="3" />--%>
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label21" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="研究方向：">
                                        </x:Label>
                                        <x:TextBox ID="T_Reserch" ShowLabel="true" Label="研究方向" Width="200px" MaxLength ="100" MaxLengthMessage ="最多输入100个字符" Required ="true"  CssClass="marginr" runat="server" TabIndex ="27">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>


                                <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label23" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专长：">
                                        </x:Label>
                                        <x:TextBox ID="T_Specilty" ShowLabel="true" Label="专长" Width="200px" MaxLength ="100" MaxLengthMessage ="最多输入100个字符" Required ="true"  CssClass="marginr" runat="server" TabIndex ="28">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                        <x:Panel ID="Panel85" Title="人员信息" ColumnWidth="50%" ShowBorder="false" BodyPadding="30px"
                            runat="server" ShowHeader="false">
                            <Items>

                                 <x:Panel ID="Panel37" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label47" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="行政级别：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false"  Required="true" Label="行政级别" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="29"
                                             runat="server" ID="DropDownListAdmin">
                                        </x:DropDownList>
                                      
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label52" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel40" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label53" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学科分类名称：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Required="true" EnableSimulateTree="true" CssClass="marginr" TabIndex ="30"
                                             runat="server" ID="DropDownListSubjectSortP" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>


                                <x:Label ID="Label60" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label63" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="政治面貌：">
                                        </x:Label>
                                        <%-- <x:TextBox ID="T_PoliticalStatus" ShowLabel="true" Label ="政治面貌"  Regex="^[\u0391-\uFFE5A-Za-z]+$" Width="150px" CssClass="marginr" runat="server" >
                                         </x:TextBox>--%>
                                        <x:DropDownList AutoPostBack="false" Required ="true"  Label="政治面貌" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="31"
                                            ShowRedStar="true" runat="server" ID="DropDownListPoliticalStatus">
                                          
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label131" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel31" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="EndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="政治面貌获取时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="获得时间" EmptyText="请选择获取时间" Width="195px" CssClass="marginr" TabIndex ="32"
                                            ID="DatePickerPoliticalStatusTime" EnableEdit ="false" >
                                        </x:DatePicker>
                                        
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label39" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel28" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label33" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="单位名称：">
                                        </x:Label>
                                        <x:TextBox ID="T_UnitName" ShowLabel="true" Label="单位名称" Width="200px" MaxLength ="40" MaxLengthMessage ="最多输入40个字符" CssClass="marginr" runat="server" TabIndex ="33">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                 <x:Label ID="Label34" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                  <x:Panel ID="Panel29" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label40" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="员工类型：">
                                        </x:Label>
                                      
                                        <x:DropDownList AutoPostBack="false" Required ="true"  Label="员工类型" EnableSimulateTree="true" ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="31"
                                            ShowRedStar="true" runat="server" ID="DropDownListStaffType">
                                          
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                  <x:Label ID="Label121" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Agency" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属机构：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false"  Label="项目所属机构"   EnableSimulateTree="true" Required ="true"  ShowLabel="true" Width="195px" CssClass="marginr" TabIndex ="34"
                                             runat="server" ID="DropDownListAgencyP">
                                        </x:DropDownList>
                                        <%--<x:TextBox ID="T_Agency" ShowLabel="true" Required ="true"  Label ="所属机构"  Regex="^([\u4e00-\u9fa5]+|[a-zA-Z0-9]+)$" Width="150px" CssClass="marginr" runat="server" >
                                        </x:TextBox>--%>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="JobTitle" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="职称：">
                                        </x:Label>
                                        <x:TextBox ID="T_JobTitle" ShowLabel="true" Label="职称"  MaxLength ="20" MaxLengthMessage ="最多输入20个字符" Required ="true"  Width="200px" CssClass="marginr" runat="server" TabIndex ="35">
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                  <x:Label ID="Label46" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel39" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label51" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="职称获取时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="获得时间" EmptyText="请选择获取时间" Width="195px" CssClass="marginr" TabIndex ="36"
                                            ID="DatePickerJobTitleTime" EnableEdit ="false"  >
                                        </x:DatePicker>
                                        
                                    </Items>
                                </x:Panel> 
                                
                                  <x:Label ID="Label61" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel41" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                           
                                     <Items>
                                        <x:Label ID="Label59" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否为硕士导师：">
                                        </x:Label>
                                        <x:RadioButton ID="IsMasterTeacher"  GroupName="IsMasterTeacher" Text="是" runat="server" TabIndex ="37">
                                        </x:RadioButton>
                                        <x:RadioButton ID="NotMasterTeacher" GroupName="IsMasterTeacher"  Text="否" Checked ="true"  runat="server">
                                        </x:RadioButton>
                                    </Items>
                                </x:Panel>
                                 <x:Label ID="Label134" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel32" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ExpectEndTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="硕士生导师获取时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="获得时间" EmptyText="请选择获取时间" Width="195px" CssClass="marginr" TabIndex ="38"
                                            ID="DatePickerMasterTeacherTime" EnableEdit ="false" >
                                        </x:DatePicker>
                                       
                                    </Items>
                                </x:Panel>
                                          
                                <x:Label ID="Label43" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel43" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                
                                    <Items>
                                        <x:Label ID="Label38" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否为博士导师：">
                                        </x:Label>
                                        <x:RadioButton ID="IsDoctorTeacher"  GroupName="IsDoctorTeache" Text="是" runat="server" TabIndex ="39">
                                        </x:RadioButton>
                                        <x:RadioButton ID="NotDoctorTeacher" GroupName="IsDoctorTeache"  Text="否" Checked ="true"  runat="server">
                                        </x:RadioButton>
                                    </Items>
                                </x:Panel>
                              
                                 <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label5" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="博士生导师获取时间：">
                                        </x:Label>
                                        <x:DatePicker runat="server" Label="获得时间" EmptyText="请选择获取时间" Width="195px" CssClass="marginr" TabIndex ="40"
                                            ID="DatePickerDoctorTeacherTime" EnableEdit ="false" >
                                        </x:DatePicker>
                                       
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label37" runat="server" Label="Label" Text=" " Height="30px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label32" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex ="41"
                                             runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                        </x:DropDownList>
                                        
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label29" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel44" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label67" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="个人简介：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="2000" MaxLengthMessage ="最多输入2000个字符" ShowLabel="true" Label ="个人简介" CssStyle ="overflow-y:scroll" ID="TextAreaProfile" Width="195px" Height="490px" TabIndex="42">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label56" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                 <x:Panel ID="Panel46" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <%--<x:Label ID="Label71" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:right"
                                            ShowLabel="false" Text="人员">
                                        </x:Label>
                                        <x:Label ID="Label72" Width="80px" runat="server" CssClass="marginr" CssStyle="text-align:left"
                                            ShowLabel="false" Text="照片：">
                                        </x:Label>--%>
                                        <x:Label ID="Labe172" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="人员照片：">
                                        </x:Label>
                                        <x:FileUpload ID="photoupload" width="195" runat="server" Label="人员照片" ShowLabel="true" TabIndex="27">
                                        </x:FileUpload>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                        <x:Panel ID="Panel47" Title="空Panel" runat="server" BodyPadding="5px" ShowBorder="false"
                            ShowHeader="false" ColumnWidth="8%">
                        </x:Panel>

                        <x:Panel ID="Panel48" BoxFlex="1" runat="server" ColumnWidth="46%" BodyPadding="0px" ShowBorder="false"
                            ShowHeader="false">

                            <Items>
                                <x:Label ID="Label73" runat="server" Label="Label" Text=" " Height="10px">
                                </x:Label>
                                 <%--这是空行--%>

                                <x:Panel ID="Panel42" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label57" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage ="最多输入200个字符" Label ="备注" ShowLabel="true" CssStyle ="overflow-y:scroll" ID="T_Remark" Width="195px" Height="160px" TabIndex="43">
                                        </x:TextArea>
                                    </Items>
                                </x:Panel>
                            </Items>
                        </x:Panel>

                    </Items>
                </x:Panel>
                <x:Panel ID="Panel87" runat="server" Height="40px" ShowBorder="True" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <%--<x:Button ID="Addstaff"  Text="添加项目相关人员" runat="server" Size="Large"  >
                          </x:Button>--%>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="310px"></x:Label>
                                <x:Button ID="Save" Text="保存" runat="server" Icon ="Add"  Size="Medium"  ConfirmText="确定保存？" Type ="Submit"  ConfirmTarget="Top" OnClick="Save_Click" ValidateForms="Panel2">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" Icon ="Delete"  runat="server" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top"  OnClick ="Reset_Click" >
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
    </form>
</body>
</html>
