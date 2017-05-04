<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Paper.aspx.cs" Inherits="WebApplication1.Achievement.Paper.Update_Paper" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title></title>
    <link href="../res/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true" 
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true" 
            ShowHeader="false"    >
        <Items>--%>


        <x:Panel ID="Panel2" runat="server" ShowBorder="True" EnableCollapse="true" AutoScroll="true"
            Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="400px"
            BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
            <Items>



                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="30px" ShowBorder="false" ShowHeader="false" ColumnWidth="50%">
                    <Items>

                        <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Subject" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="论文题目：">
                                </x:Label>
                                <x:TextBox ID="tSubject" MaxLength="100" MaxLengthMessage="最多可輸入100个字符" ShowLabel="true" Label="论文题目" Width="200px" CssClass="marginr" runat="server" TabIndex="1" AutoPostBack="true" OnTextChanged="tSubject_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>


                        <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="PublicJournalName" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="发表刊物：">
                                </x:Label>
                                <x:TextBox ID="tPublicJournalName" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="发表刊物" Width="200px" CssClass="marginr" runat="server" TabIndex="2">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label23" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="PublicDate" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="发表时间：">
                                </x:Label>
                                <x:DatePicker ID="dPublicDate" runat="server" Width="200px" EnableEdit="false" ShowLabel="true" TabIndex="3" Label="发表时间" AutoPostBack="true">
                                </x:DatePicker>
                            </Items>
                        </x:Panel>


                        <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="VolumesNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="卷号：">
                                </x:Label>
                                <x:TextBox ID="tVolumesNum" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="卷号" Width="200px" CssClass="marginr" runat="server" TabIndex="4">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="JournalNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="期号：">
                                </x:Label>
                                <x:TextBox ID="tJournalNum" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="期号" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel23" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="SerialNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="刊号：">
                                </x:Label>
                                <x:TextBox ID="tSerialNum" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="刊号" Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="QuoteNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="引用次数：">
                                </x:Label>
                                <x:TextBox ID="tQuoteNum" RegexPattern="NUMBER" RegexMessage="只能输入数字" ShowLabel="true" Label="引用次数" Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="HQuoteNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="他引次数：">
                                </x:Label>
                                <x:TextBox ID="tHQuoteNum" RegexPattern="NUMBER" RegexMessage="只能输入数字" ShowLabel="true" Label="他引次数" Width="200px" CssClass="marginr" runat="server" TabIndex="8" AutoPostBack="true" OnTextChanged="tHQuoteNum_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="StartPageNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="起始页码：">
                                </x:Label>
                                <x:TextBox ID="tStartPageNum" RegexPattern="NUMBER" RegexMessage="只能输入数字" ShowLabel="true" Label="起始页码" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="EndPageNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="结束页码：">
                                </x:Label>
                                <%--<x:TextArea runat="server" ID="TextArea1" Width="150px" Height="60px" AutoGrowHeight="true" AutoGrowHeightMin="100" AutoGrowHeightMax="200">
                                        </x:TextArea>--%>
                                <x:TextBox ID="tEndPageNum" RegexPattern="NUMBER" RegexMessage="只能输入数字" ShowLabel="true" Label="结束页码" Width="200px" CssClass="marginr" runat="server" TabIndex="10" AutoPostBack="true" OnTextChanged="tEndPageNum_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="RetrieveSituation" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="检索情况：">
                                </x:Label>
                                <x:DropDownList ID="dRetrieveSituation" ShowLabel="true" Label="检索情况" Width="195px" AutoPostBack="true" runat="server" TabIndex="11">
                                </x:DropDownList>

                            </Items>
                        </x:Panel>
                        <x:Label ID="Label21" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel29" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="PaperRank" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="刊物级别：">
                                </x:Label>
                                <x:DropDownList Label="刊物级别" AutoPostBack="false" EnableSimulateTree="true" TabIndex="12"
                                    runat="server" ID="dPaperRank" Width="195px">
                                </x:DropDownList>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="IncludeNum" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="收录号：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" EmptyText="两个或两个以上请用逗号隔开" CssStyle="overflow-y:scroll" Label="收录号" ID="tIncludeNum" Width="200px" Height="105px" AutoGrowHeight="false" AutoGrowHeightMax="105" TabIndex="13">
                                </x:TextArea>
                            </Items>
                        </x:Panel>


                    </Items>
                </x:Panel>
                <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                    BodyPadding="30px" ShowBorder="false" ShowHeader="false" ColumnWidth="50%">

                    <Items>





                        <%--  <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PaperRank" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="刊物级别：">
                                    </x:Label>
                                    <x:TextBox ID="tPaperRank" ShowLabel="true" Label="刊物级别" MaxLength="10"  MaxLengthMessage="最多可输入10个字符"  Required="true" Width="200px" CssClass="marginr" runat="server" TabIndex="14">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>--%>

                        <x:Label ID="Label25" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="ImpactFactor" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="影响因子：">
                                </x:Label>
                                <x:TextBox ID="tImpactFactor" MaxLength="5" MaxLengthMessage="最多可输入5个字符" ShowLabel="true" Label="影响因子" Width="200px" CssClass="marginr" runat="server" TabIndex="14">
                                </x:TextBox>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>


                        <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="AwardUnit" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属机构：">
                                </x:Label>
                                <x:DropDownList Label="所属机构" AutoPostBack="false" EnableEdit="false" EnableSimulateTree="true"
                                    runat="server" ID="DropDownListAgency" Width="195px" TabIndex="15">
                                </x:DropDownList>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label27" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel28" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Achievement" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属成果名称">
                                </x:Label>
                                <x:TextBox ID="tAchievement" ShowLabel="true" Label="所属成果名称" MaxLength="20" MaxLengthMessage="最多可输入20个字符" Width="200px" CssClass="marginr" runat="server" TabIndex="16" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged">
                                </x:TextBox>
                            </Items>
                        </x:Panel>







                        <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="SecrecyLevel" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                </x:Label>
                                <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" Label="保密级别：" EnableEdit="false" Width="195px" Readonly="true" AutoPostBack="true" runat="server" TabIndex="17">
                                    <x:ListItem Text="公开" Value="公开" Selected="true" />

                                </x:DropDownList>
                                <%--<x:TextBox ID="SecrecyLevel1" ShowLabel="false" Required="true" Width="150px" CssClass="marginr" runat="server" TabIndex="13">
                                         </x:TextBox>--%>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label12" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一作者：">
                                </x:Label>
                                <x:TextBox ID="FirstWriter" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="第一作者" Width="200px" CssClass="marginr" runat="server" TabIndex="18">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel26" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label28" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一作者身份：">
                                </x:Label>
                                <x:DropDownList ID="dPaperIdentity" ShowLabel="true" Label="第一作者身份：" EnableEdit="false" Width="195px" AutoPostBack="true" runat="server" TabIndex="19">
                                   <%-- <x:ListItem Text="本校教工" Value="本校教工" Selected="true" />
                                    <x:ListItem Text="学生" Value="学生" />--%>

                                </x:DropDownList>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel24" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label20" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="通讯作者：">
                                </x:Label>
                                <x:TextBox ID="MessageWriter" MaxLength="10" MaxLengthMessage="最多可输入10个字符" ShowLabel="true" Label="通讯作者" Width="200px" CssClass="marginr" runat="server" TabIndex="20">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel25" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label24" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="通讯作者部门：">
                                </x:Label>
                                <x:TextBox ID="MWAgency" MaxLength="50" MaxLengthMessage="最多可输入50个字符" ShowLabel="true" Label="通讯作者部门" Width="200px" CssClass="marginr" runat="server" TabIndex="21">
                                </x:TextBox>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label29" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label30" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="分类：">
                                </x:Label>
                                <x:DropDownList ID="DropDownList_Sort" ShowLabel="true" Label="分类：" EnableEdit="false" Width="195px" AutoPostBack="true" runat="server" TabIndex="22">
                                    <x:ListItem Text="教研论文" Value="教研论文" Selected="true" />
                                    <x:ListItem Text="科研论文" Value="科研论文" />

                                </x:DropDownList>
                            </Items>
                        </x:Panel>
                        <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>
                        <x:Panel ID="Panel18" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Label6" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="作者：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" EmptyText="两个或两个以上请用逗号隔开" ShowLabel="true" CssStyle="overflow-y:scroll" Label="作者" ID="PaperPeople" Width="200px" Height="120px" AutoGrowHeight="false" AutoGrowHeightMax="100" TabIndex="22">
                                </x:TextArea>
                            </Items>
                        </x:Panel>

                        <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                        </x:Label>
                        <%--这是空行--%>

                        <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                            Layout="Column" runat="server">
                            <Items>
                                <x:Label ID="Remark" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                </x:Label>
                                <x:TextArea runat="server" MaxLength="500" MaxLengthMessage="最多可输入500个字符" ShowLabel="true" Label="备注" ID="tRemark" Width="200px" Height="100px" CssStyle="overflow-y:scroll" AutoGrowHeight="false" AutoGrowHeightMax="120" TabIndex="23">
                                </x:TextArea>
                            </Items>
                        </x:Panel>



                    </Items>
                </x:Panel>



            </Items>
        </x:Panel>
        <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
            Layout="Row" runat="server" Height="35px" BackColor="White">
            <asp:Label ID="Label31" runat="server" Label="Label" CssClass="marginr" Text=" " Width="100px">
            </asp:Label>
            <asp:Label ID="Label32" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="95px">
            </asp:Label>
            <input type="file" id="fileupload" style="width: 200px" runat="server" />
        </asp:Panel>
        <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
            <Items>
                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                    <Items>
                        <x:Label ID="Label15" Width="310px" runat="server" ShowLabel="true" Text=" ">
                        </x:Label>

                        <x:Button ID="AddWriter" runat="server" Type="Submit" CssClass="marginr" Size="Medium" Text="保存" Icon="Add" ConfirmText="确定保存？" ValidateForms="Panel2" OnClick="AddWriter_Click">
                        </x:Button>
                        <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Size="Medium" Text="重置" Icon="Delete" ConfirmText="确定重置？" OnClick="DeleteAll_Click">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Items>
        </x:Panel>
        <%--            </Items>
            </x:Panel>--%>
    </form>
</body>
</html>
