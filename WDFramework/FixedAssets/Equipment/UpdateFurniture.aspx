<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateFurniture.aspx.cs" Inherits="WDFramework.FixedAssets.Furniture.UpdateFurniture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="310px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="10"
                BoxConfigChildMargin="0 0 0 0" ShowHeader="false">
                <Items>
                    <x:Panel ID="Panel3" Title="" BoxFlex="1" runat="server" AutoScroll="true"
                        BodyPadding="5px" ShowBorder="false" ShowHeader="false" ColumnWidth="100%">
                        <Items>

                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Achievement" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="家具名：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="家具名" ID="tb_FurnitureName" Required="true" Width="255px" EmptyText="请输入家具名" MaxLength="100" MaxLengthMessage="最多可输入100个字符" AutoPostBack="true" TabIndex="1" OnTextChanged="tb_FurnitureName_TextChanged">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否政府采购：">
                                    </x:Label>
                                    <x:DropDownList Label="是否政府采购" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="2"
                                        runat="server" ID="ddl_isgov" Width="250px">
                                        <x:ListItem Text="是" Value="是" Selected="true" />
                                        <x:ListItem Text="否" Value="否" />
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属部门：">
                                    </x:Label>
                                    <x:DropDownList Label="所属部门" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="3"
                                        runat="server" ID="ddl_agencyname" Width="250px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="购买人：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="购买人" ID="tb_Purchase" Required="true" Width="255px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="4" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label12" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="使用人：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="使用人" ID="tb_UsePerson" Required="true" Width="255px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="4" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="购买时间：">
                                    </x:Label>
                                        <x:DatePicker ID="dp_PurchaseTime" runat="server" ShowLabel="true" EnableEdit="false" Label="购买时间" Width="250px" TabIndex="5">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label10" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="价格：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="价格" ID="tb_price" Required="true" Width="245px"  MaxLength="20" MaxLengthMessage="最多可输入20个字符" TabIndex="6" AutoPostBack="true" >
                                    </x:TextBox>
                                    <x:Label ID="Label4" Width="10px" runat="server" CssClass="marginr" ShowLabel="false" Text="元">
                                    </x:Label>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label3" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密等级：">
                                    </x:Label>
                                    <x:DropDownList Label="保密等级" Required="true" EnableSimulateTree="true" runat="server" ID="ddl_Level" Width="250px" TabIndex="7">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Panel>
                </Items>

            </x:Panel>

          
            
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label15" Width="150px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" ConfirmText="确定保存？" ValidateForms="Panel2" Size="Medium" Icon="Add" OnClick="Save_Click">
                            </x:Button>
                            <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" Size="Medium" ConfirmText="确定重置？" Icon="Delete" OnClick="DeleteAll_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>
        </div>
       
    </form>
</body>
</html>

