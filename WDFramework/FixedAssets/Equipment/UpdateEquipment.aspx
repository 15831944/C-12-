<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateEquipment.aspx.cs" Inherits="WDFramework.FixedAssets.Equipment.UpdateEquipment" %>

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
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true"
            ShowHeader="false" Title="用户管理">
            <Items>


                <%--Height="350px" Width="850px"  --%>
                <x:Panel ID="Panel2" runat="server" ShowBorder="false" EnableCollapse="true" AutoScroll="true"
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="330px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="MissionName2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="资产名：">
                                        </x:Label>
                                        <x:TextBox ID="tb_EquipmenteName" Label="资产名" AutoPostBack="true" ShowLabel="true" Width="200px"
                                             CssClass="marginr" runat="server" OnTextChanged="tb_EquipmenteName_TextChanged">
                                        </x:TextBox>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否政府采购：">
                                    </x:Label>
                                    <x:DropDownList Label="是否政府采购" ShowLabel="false" AutoPostBack="false" EnableSimulateTree="true" TabIndex="2"
                                      CssClass="marginr"  runat="server" ID="ddl_isgov" Width="200px">
                                        <x:ListItem Text="是" Value="是" Selected="true" />
                                        <x:ListItem Text="否" Value="否" />
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label13" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="是否共享：">
                                    </x:Label>
                                    <x:DropDownList Label="是否共享" AutoPostBack="false" EnableSimulateTree="true" TabIndex="2"
                                        runat="server" ID="ddl_isshare" Width="200px">
                                        <x:ListItem Text="是" Value="是" Selected="true" />
                                        <x:ListItem Text="否" Value="否" />
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="存放地点：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="存放地点" ID="tb_StorageLocation" Width="200px"  MaxLength="100" TabIndex="3" AutoPostBack="true" >
                                    </x:TextBox>
                                   <%-- <x:DropDownList Label="所属部门" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="3"
                                        runat="server" ID="ddl_agencyname" Width="200px">
                                    </x:DropDownList>--%>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="购买人：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="购买人" ID="tb_Purchase" Width="200px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="4" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                                 <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label18" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="资产编号：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="资产编号" ID="tb_Equipnum" Width="200px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="5" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label5" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label3" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="使用人：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="使用人" ID="tb_UsePerson" Width="200px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="6" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="购买时间：">
                                    </x:Label>
                                        <x:DatePicker ID="dp_PurchaseTime" runat="server" ShowLabel="true" EnableEdit="false" Label="购买时间" Width="200px" TabIndex="7">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label10" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="价格：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="价格" ID="tb_price" Width="195px"  MaxLength="20" MaxLengthMessage="最多可输入20个字符" TabIndex="8" AutoPostBack="true" >
                                    </x:TextBox>
                                    <x:Label ID="Label4" Width="10px" runat="server" CssClass="marginr" ShowLabel="false" Text="元">
                                    </x:Label>
                                </Items>
                            </x:Panel>

                                
                               <x:Label ID="Label30" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="计量单位：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="计量单位" ID="tb_MeasurementUnit" Width="200px"  MaxLength="30" TabIndex="9" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label28" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label21" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="生产厂家：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="生产厂家" ID="tb_Manufacturer" Width="200px"  MaxLength="30" TabIndex="10" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label26" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label23" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="型号：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="型号" ID="tb_Model" Width="200px"  MaxLength="30" TabIndex="11" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label24" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label><%--这是空行--%>
                            <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label25" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="分类号：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="分类号" ID="tb_ClassNum" Width="200px"  MaxLength="30" MaxLengthMessage="最多可输入30个字符" TabIndex="12" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label31" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label32" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="分类：">
                                    </x:Label>
                                    <x:DropDownList Label="分类" AutoPostBack="false" EnableSimulateTree="true" TabIndex="13"
                                        runat="server" ID="ddl_Category" Width="250px">
                                        <x:ListItem Text="无" Value="无" Selected="true" />
                                        <x:ListItem Text="盘盈设备" Value="盘盈设备" />
                                        <x:ListItem Text="盘亏设备" Value="盘亏设备" />
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label33" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel21" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label34" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属部门：">
                                    </x:Label>
                                    <%--<x:TextBox runat="server" Label="所属部门" ID="tb_Agency" Required="true" Width="255px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="14" AutoPostBack="true" >
                                    </x:TextBox>--%>
                                    <x:DropDownList ID="ddl_Agency" runat="server" Label="所属部门" Width="250px" TabIndex="14" AutoPostBack="false">
                                        <x:ListItem Text="总体" Value="总体" Selected="true"/>
                                        <x:ListItem Text="机械" Value="机械" />
                                        <x:ListItem Text="新技术" Value="新技术" />
                                        <x:ListItem Text="办公" Value="办公" />
                                        <x:ListItem Text="伺服控制" Value="伺服控制" />
                                        <x:ListItem Text="光学" Value="光学" />
                                        <x:ListItem Text="新光源" Value="新光源" />
                                        <x:ListItem Text="仿真" Value="仿真" />
                                        <x:ListItem Text="通信发射" Value="通信发射" />
                                        <x:ListItem Text="通信接收" Value="通信接收" />
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                               <x:Label ID="Label20" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label29" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="备注" ID="tb_Remarks" Width="200px"  MaxLength="100" MaxLengthMessage="最多可输入100个字符" TabIndex="14" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                               <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" EnableSimulateTree="true"
                                            runat="server" ID="DropDownListSecrecyLevel" Width="195px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                              </Items>
                            </x:Panel>  
                        </Items>
                    </x:Panel>
                


                <x:Panel ID="Panel87" runat="server" Height="40px" ShowBorder="false" EnableCollapse="true"
                    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false" Width="750px">
                    <Items>
                        <x:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <x:Label ID="Label12" runat="server" Label="Label" Text=" " Width="150px"></x:Label>
                                <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" ConfirmTarget="Top" Type="Submit" OnClick="Save_Click" ValidateForms="Panel2">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top" OnClick="Reset_Click">
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
