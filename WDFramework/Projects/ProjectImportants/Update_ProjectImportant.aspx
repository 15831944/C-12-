<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_ProjectImportant.aspx.cs" Inherits="WDFramework.Projects.Update_ProjectImportant" %>




<%@ Register Assembly="FineUI" Namespace="FineUI" TagPrefix="x" %>
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
                    Layout="Column" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5" Height="320px"
                    BoxConfigChildMargin="0 5 0 0" ShowHeader="false">
                    <Items>

                        <x:Panel ID="Panel3" Title="项目概要" runat="server" ColumnWidth="100%" ShowBorder="false" BodyPadding="30px"
                            ShowHeader="false">
                            <Items>
                                <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="MissionName2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="节点名称：">
                                        </x:Label>
                                        <x:TextArea runat="server" Label="节点名称" MaxLength="500" MaxLengthMessage="最多输入500个字符" ShowLabel="true" ID="MissionName" Width="195px" Height="80px" TabIndex="1" CssStyle ="overflow-y:scroll">
                                        </x:TextArea>

                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>

                                        <x:Label ID="Label4" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="开始时间：">
                                        </x:Label>

                                        <x:DatePicker runat="server" Label="开始时间" EmptyText="请选择时间" EnableEdit="false" Width="200px" CssClass="marginr" TabIndex="2"
                                            ID="DatePickerStartTime">
                                        </x:DatePicker>


                                    </Items>
                                </x:Panel>

                                   <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height ="20px" >
                             </x:Label><%--这是空行--%>
                             <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>

                                        <x:Label ID="Label5" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="计划完成时间：">
                                        </x:Label>      
                                    
                                            <x:DatePicker runat="server" Label="计划完成时间" EmptyText="请选择时间" EnableEdit ="false"   Width="200px" CssClass="marginr" TabIndex ="3"                    
                                                ID="DatePickerEndTime">               
                                            </x:DatePicker>
                              

                                        </Items>                
                            </x:Panel>

                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="ProjectID" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属项目：">
                                        </x:Label>
                                        <x:DropDownList AutoPostBack="false" Label="所属项目" EnableSimulateTree="true" ShowLabel="true" Width="200px" CssClass="marginr" TabIndex="4"
                                            runat="server" ID="DropDownListProjectID">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                  <x:Label ID="Label65" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                    
                                        <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="负责人：">
                                        </x:Label>
                                        <x:TextBox ID="txtPersonCharge" MaxLength="20" ShowLabel="true" Label="负责人"  Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                                                  <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                    
                                        <x:Label ID="Label10" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="实际完成：">
                                        </x:Label>
                                        <x:TextBox ID="txtActualComleption" MaxLength="20" ShowLabel="true" Label="实际完成"  Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                        
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                            <x:Label ID="Label67" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                      
                                        <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="机构：">
                                        </x:Label>
                                        <x:DropDownList ID="Agency" MaxLength="100" ShowLabel="true" Label="机构"  Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                           <x:Label ID="Label68" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                      
                                        <x:Label ID="Label7" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="具体完成人：">
                                        </x:Label>
                                        <x:TextBox ID="CompleteSpecificPerson" MaxLength="20" ShowLabel="true" Label="具体完成人"  Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                        
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>
                                    <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                    <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                      
                                        <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="项目完成情况：">
                                        </x:Label>
                                        <x:TextBox ID="txtProjectCompletion" MaxLength="20" ShowLabel="true" Label="项目完成情况"  Width="200px" CssClass="marginr" runat="server" TabIndex="7">
                                        
                                        </x:TextBox>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label126" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>

                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                        </x:Label>
                                        <x:DropDownList Label="保密等级" AutoPostBack="false" EnableSimulateTree="true" TabIndex="8"
                                            runat="server" ID="DropDownListSecrecyLevel" Width="200px">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Remark2" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                        </x:Label>
                                        <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多输入200个字符" ShowLabel="true" ID="Remark" Width="195px" Height="80px" TabIndex="9" CssStyle ="overflow-y:scroll">
                                        </x:TextArea>

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
                                <x:Button ID="Save" Text="保存" runat="server" Icon="Add" Size="Medium" ConfirmText="确定保存？" ConfirmTarget="Top" OnClick="Save_Click" ValidateForms="Panel2" Type="Submit">
                                </x:Button>
                                <x:Button ID="Reset" Text="重置" runat="server" Icon="Delete" Size="Medium" ConfirmText="确定重置？" ConfirmTarget="Top" OnClick="Reset_Click">
                                </x:Button>
                            </Items>
                        </x:Toolbar>
                    </Items>
                </x:Panel>
            </Items>
        </x:Panel>
        <x:Window ID="WindowStaff" Popup="false" EnableIFrame="true" IFrameUrl="#" runat="server"
            EnableMaximize="true" EnableResize="true" Height="450px" Width="750px" Title="添加人员">
        </x:Window>
    </form>
</body>
</html>
