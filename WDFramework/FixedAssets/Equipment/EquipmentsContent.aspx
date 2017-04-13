<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EquipmentsContent.aspx.cs" Inherits="WDFramework.FixedAssets.Equipment.EquipmentsContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false">
       <%-- <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server"  />
   <x:Panel ID="Panel1" runat="server"  ShowBorder="True"  EnableCollapse="true"
                Layout="Column"    BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">--%>
        <Items>
            <x:Grid ID="Grid_Equipment" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
      EnableRowNumberPaging="true" EnableTextSelection="true" EnableCheckBoxSelect="false"
         DataKeyNames="EquipmentID" AllowSorting="true" SortColumnIndex="0"
          AllowPaging="true" IsDatabasePaging="true"   OnPageIndexChange="People_Info_PageIndexChange" OnRowCommand="Grid_Files_RowCommand" >
        <Toolbars>
                            <x:Toolbar ID="Toolbar_top" runat="server" >
                                <Items>
                                     <x:Label ID="labSort" runat="server" CssClass="marginr" ShowLabel="false" Text="查询条件">
                                </x:Label>
                                <x:DropDownList ID="ddl_search" ShowLabel="false" AutoPostBack="true" runat="server" EnableEdit="false" OnSelectedIndexChanged="ddl_search_SelectedIndexChanged">
                                   <x:ListItem Text="全部" Value="0" Selected="true" />
                                   <x:ListItem Text="设备名称" Value="1" />
                                   <x:ListItem Text="购买人" Value="2" />
                                   <x:ListItem Text="购买时间" Value="3" />
                                   <x:ListItem Text="价格" Value="4" />
                                   <x:ListItem Text="使用人" Value="5" />
                                   <x:ListItem Text="存放地点" Value="6" />

                                   <x:ListItem Text="资产编号" Value="7" />
                                   <x:ListItem Text="所属机构" Value="8" />
                                </x:DropDownList> 
                                    <x:DropDownList ID="ddl_ch" ShowLabel="false" Width="50px" AutoPostBack="true" runat="server" Enabled="false" EnableEdit="false">
                                        <x:ListItem Text="=" Selected="true" Value="0" />
                                        <x:ListItem Text="<" Value="1" />
                                        <x:ListItem Text=">" Value="2" />
                                        <x:ListItem Text="<=" Value="3" />
                                        <x:ListItem Text=">=" Value="4" />
                                    </x:DropDownList> 
                                     <x:TextBox ID="tCondition" Enabled="true" ShowLabel="true" MaxLength="60" MaxLengthMessage="最多可输入40个字符" Width="100px" CssClass="marginr" runat="server"  AutoPostBack="false"  >
                                        </x:TextBox>
                                    <%--<x:TwinTriggerBox runat="server" EmptyText="输入要搜索的关键词" ShowLabel="false" ID="ttbSearch" ShowTrigger1="false"
                                        Trigger2Icon="Search">
                                    </x:TwinTriggerBox>--%> 
                                     <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch"  runat="server" OnClick="Select_Click">
                                     </x:Button>     
                                     <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click">  
                                    </x:Button>                           
                                     <x:Button ID="btnAddEquipment" runat="server" EnablePostBack="false"  Icon="Add" Text="新增设备信息">
                                     </x:Button>
                                     <x:Button ID="btnDelete" Text="删除选中行" Icon="Delete" ConfirmText="确定删除？" Enabled="false"  runat="server" OnClick="btnDelete_Click">
                                     </x:Button>
                                 <x:Button ID="ButtonUpdate" Text="编辑选中行"  Icon="Pencil"   runat="server" OnClick="ButtonUpdate_Click"   >
                                  </x:Button> 
                                    <x:Button ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick ="btn_Get_Click"   EnableAjax="false" DisableControlBeforePostBack="false">
                                    </x:Button>
                                </Items>
                            </x:Toolbar>
                        </Toolbars>

       <%--AllowPaging这是分页功能--%>
                 <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
       <%--DataKeyNames这是数据库唯一标识--%>
         <Columns>
            <x:TemplateField Width="30px">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField>
            <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="fileid"  runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
            <x:BoundField DataField="EntryPerson" Width="150px" Hidden="true" HeaderText="录入人" />
            <x:TemplateField Width="30px" Hidden ="true" >
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# RowNumber(Container.DataItemIndex + 1) %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField> 
            <x:BoundField DataField="EquipmentName" SortField="EquipmentName" Width="150px" HeaderText="资产名称" />
            <x:BoundField DataField="EquipNum" SortField="EquipNum" Width="150px" HeaderText="资产编号" />
            <x:BoundField DataField="Purchase" SortField="Purchase" Width="150px" HeaderText="购买人" />
            <x:BoundField DataField="PurchaseTime" SortField="PurchaseTime" DataFormatString="{0:yyyy-MM-dd}" Width="150px" HeaderText="购置日期" />
            <x:BoundField DataField="Price" SortField="Price" Width="150px" HeaderText="价格" />
            <x:BoundField DataField="UsePerson" SortField="UsePerson" Width="150px" HeaderText="使用人" />
            <x:BoundField DataField="StorageLocation" SortField="StorageLocation" Width="150px" HeaderText="存放地点" />
            <x:BoundField DataField="MeasurementUnit" SortField="MeasurementUnit" Width="150px" HeaderText="计量单位" />
            <x:BoundField DataField="Manufacturer" SortField="Manufacturer" Width="150px" HeaderText="生产厂家" />
            <x:BoundField DataField="Model" SortField="Model" Width="150px" HeaderText="型号" />
            <x:BoundField DataField="ClassNum" SortField="ClassNum" Width="150px" HeaderText="分类号" />
                        <%-- <x:BoundField DataField="CategoryName" SortField="CategoryName" Width="150px" HeaderText="分类" />--%><%--原为分类名称--%>
            <x:BoundField DataField="AgencName" SortField="AgencName" Width="150px" HeaderText="所属机构" />
                       <%-- <x:TemplateField Width="150px" HeaderText="所属机构">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# FindAgencyName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AgencyID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>--%>
                       <%-- <x:BoundField DataField="ContractAuthors" SortField="IsGowerProcu" Width="150px" HeaderText="是否政府采购" />--%>
            <x:TemplateField Width="80px" HeaderText="是否政府采购">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# isgor(Convert.ToString(DataBinder.Eval(Container.DataItem, "IsGowerProcu"))) %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField> 
            <x:TemplateField Width="80px" HeaderText="是否共享">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# isshare(Convert.ToString(DataBinder.Eval(Container.DataItem, "IsShare"))) %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField> 
            <x:TemplateField Width="80px" HeaderText="盘盈设备">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "CategoryName")) == "盘盈设备" ? "是" : "否" %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField>  
            <x:TemplateField Width="80px" HeaderText="盘亏设备">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "CategoryName")) == "盘亏设备" ? "是" : "否" %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField>
            <x:TemplateField Width="80px" HeaderText="保密级别">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                </ItemTemplate>
            </x:TemplateField>     
            <x:BoundField DataField="Remarks" SortField="UsePerson" Width="150px" HeaderText="备注" />
          </Columns>
       </x:Grid>
       </Items> 
       </x:Panel>
          <x:Window ID="Window_add" Popup="false" EnableIFrame="true" runat="server" 
                          EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="380px" Width="450px">
        </x:Window>
         <x:Window ID="Window_Update" Popup="false" EnableIFrame="true" runat="server"
             EnableMaximize="false" EnableMinimize="false"  EnableResize="false" Height="400px" Width="450px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>
