<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewAdd_PactRecord.aspx.cs" Inherits="WDFramework.ContractAndPact.Pact.NewAdd_PactRecord" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
        <x:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>

                <x:Region ID="Region2" ShowBorder="false" ShowHeader="false" Position="Center" Layout="VBox"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Left" BodyPadding="5px"
                    EnableBackgroundColor="true" runat="server">
                    <Items>
                        <x:Panel ID="Panel2" runat="server" Height="324px" Width="450px" ShowBorder="True" EnableCollapse="true"
                            Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                           ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                                    BodyPadding="50px" ShowBorder="false" ShowHeader="false">
                                    <Items>

                                        <x:Panel ID="Panel4" Title="项目概要" BoxFlex="1" runat="server"
                                            BodyPadding="5px" ShowBorder="false" ShowHeader="false">
                                            <Items>
                                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                                    Layout="Column" runat="server">
                                                    <Items>
                                                        <x:Label ID="labPactNum" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="合同编号：">
                                                        </x:Label>
                                                        <x:TextBox ID="txtPactNum" Label="合同编号" ShowLabel="true" Required="true" 
Width="200px" CssClass="marginr" runat="server" Readonly="true" MaxLength="30" TabIndex="1">
                                                        </x:TextBox>
                                                    </Items>
                                                </x:Panel>

                                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Height="20px">
                                                </x:Label>
                                                <%--这是空行--%>

                                                <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                                    Layout="Column" runat="server">
                                                    <Items>
                                                        <x:Label ID="labBorrowPeople" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="借阅人：">
                                                        </x:Label>
                                                        <x:TextBox ID="txtBorrowPeople" Label="借阅人" ShowLabel="true" 
Required="true" Width="200px" CssClass="marginr" runat="server" MaxLength="10" TabIndex="2">
                                                        </x:TextBox>
                                                    </Items>
                                                </x:Panel>

                                                <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                                                </x:Label>
                                                <%--这是空行--%>

                                                <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                                    Layout="Column" runat="server">
                                                    <Items>
                                                        <x:Label ID="labBorrowTime" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="借阅时间：">
                                                        </x:Label>
                                                        <x:DatePicker runat="server" Width="195px" EnableEdit="false" ShowLabel="true" 
Required="true" Label="借阅时间" DateFormatString="yyyy-MM-dd" TabIndex="3"
                                                            ID="DatePicker_BorrowTime">
                                                            <%--RequiredMessage="借阅时间不能为空"--%>
                                                        </x:DatePicker>
                                                    </Items>
                                                </x:Panel>
                                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                                                </x:Label>
                                                <%--这是空行--%>

                                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                                    Layout="Column" runat="server">
                                                    <Items>
                                                        <x:Label ID="labReturnTime" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="归还时间：">
                                                        </x:Label>
                                                        <x:DatePicker runat="server" EnableEdit="false" Width="195px" ShowLabel="true" 
Label="归还时间" DateFormatString="yyyy-MM-dd"
                                                            ID="DatePicker_ReturnTime" TabIndex="4">
                                                            <%--RequiredMessage="归还时间不能为空"--%>
                                                            <%--CompareControl="DatePicker_BorrowTime" 
CompareOperator="GreaterThanEqual" CompareMessage="结束日期应该大于开始日期！"--%>
                                                        </x:DatePicker>
                                                    </Items>
                                                </x:Panel>
                                                <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                                                </x:Label>
                                                <%--这是空行--%>
                                                <x:Panel ID="Panel22" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                                    Layout="Column" runat="server">
                                                    <Items>
                                                        <x:Label ID="labSecrecyLevel" Width="100px" runat="server" CssClass="marginr" 
ShowLabel="false" Text="保密级别：">
                                                        </x:Label>
                                                        <%--<x:TextBox ID="txtAgency" ShowLabel="false" Required="true" Width="200px" 
CssClass="marginr" runat="server" >
                                         </x:TextBox>--%>
                                                        <x:DropDownList ID="DropDownList_SecrecyLevel" Label="保密级别" Width="195px" 
ShowLabel="true" AutoPostBack="false" runat="server" Required="true" TabIndex="5">
                                                            <%--<x:ListItem Text="--请选择--" Value="0" />--%>
                                                            <%--<x:ListItem Text="1" Value="1" />
                                              <x:ListItem Text="2" Value="2" />
                                              <x:ListItem Text="3" Value="3" />
                                              <x:ListItem Text="4" Value="4" />
                                              <x:ListItem Text="5" Value="5" />--%>
                                                        </x:DropDownList>
                                                    </Items>
                                                </x:Panel>

                                            </Items>
                                        </x:Panel>
                                    </Items>
                                </x:Panel>

                            </Items>
                        </x:Panel>
                        <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="true" Layout="Column" runat="server">
                            <Items>
                                <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%" Height="86px">
                                    <Items>
                                        <x:Label ID="Label15" Width="120px" runat="server" ShowLabel="true" Text=" ">
                                        </x:Label>
                                        <x:Button ID="btnSave" runat="server" CssClass="marginr" Text="保存" Size="Medium" Icon="Add" ConfirmText="确定保存？" ConfirmTarget="Top" ValidateForms="Panel2" Type="Submit" OnClick="btnSave_Click">
                                        </x:Button>
                                        <x:Button ID="btnReSet" runat="server" CssClass="marginr" Text="重置" Size="Medium" Icon="Delete" ConfirmText="确定重置？" EnablePostBack="true" ConfirmTarget="Top" OnClick="btnReSet_Click">
                                        </x:Button>
                                    </Items>
                                </x:Toolbar>
                            </Items>
                        </x:Panel>

                        <%--  --%>
                    </Items>
                </x:Region>
            </Regions>
        </x:RegionPanel>
    </form>
</body>
</html>
