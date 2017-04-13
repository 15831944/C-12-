<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoneyResult.aspx.cs" Inherits="WDFramework.MoneyResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body oncontextmenu='return false'><%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <%--  --%>
       <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="资金信息">
        <Items>
            
<%--  --%>


         <x:Grid ID="gd_UnitAPeople" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
         EnableRowNumber="false" EnableRowNumberPaging="true" EnableTextSelection="true" 
         DataKeyNames="UserInfoID" AllowSorting="true" SortColumnIndex="0"
         SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="gd_UnitAPeople_PageIndexChange" >
        <Toolbars>
        <x:Toolbar ID="Toolbar1" runat="server" >
            <Items>

                <x:Label ID="Label8" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="Label9" runat="server" Label="Label" Text="查询条件:"></x:Label>
                <x:Label ID="Label10" runat="server" Label="Label" Width ="10" Text=" "></x:Label>

                <x:DropDownList ID="ddl_UnitALl" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px"  OnSelectedIndexChanged ="ddl_UnitALl_SelectedIndexChanged">
                <x:ListItem Text="年份" Value="0" />
                <x:ListItem Text="承担部门" Value="1" />
                <x:ListItem Text="负责人" Value="2" />
                <x:ListItem Text="项目来源" Value="3" />
                <x:ListItem Text="项目类型" Value="4" />
                <x:ListItem Text="项目" Value="5" />
                    <x:ListItem Text="全部" Value="6" />
                </x:DropDownList>

                <x:Label ID="Label1" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="lb_Change" runat="server" Label="Label" Text="部门名:"></x:Label>
                <x:Label ID="Label7" runat="server" Label="Label" Width ="10" Text=" "></x:Label>

                <x:TextBox ID="tb_Unit" Required="true" AutoPostBack="true" runat="server" Width="100px" OnTextChanged="tb_Unit_TextChanged"></x:TextBox>
                

                <x:Label ID="Label6" runat="server" Label="Label" Width ="15" Text=" "></x:Label>
                <x:Label ID="lb_Name" runat="server" Label="Label" Text="年份:"></x:Label>
                <x:Label ID="Label14" runat="server" Label="Label" Width ="10" Text=" "></x:Label>

                <x:DropDownList ID="ddl_PeopleName" EnableEdit="true" ForceSelection="true" ShowLabel="false" AutoPostBack="true" runat="server" Width ="100px" >
                    
                </x:DropDownList>

                

                <x:Label ID="Label13" runat="server" Label="Label" Width ="15" Text=" "></x:Label>

                <x:Button ID="btn_FindUnitPeople" Text="搜索" Type="Submit" Icon="SystemSearch"  EnablePostBack="true" runat="server" OnClick="btn_FindUnitPeople_Click">
                </x:Button>

                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise"  Text="刷新" OnClick="btnRefresh_Click">  
                                    </x:Button> 

                <%--<x:Button ID="btn_Show" Text="统计" Icon="CupEdit" EnablePostBack="true" runat="server">
                </x:Button>--%>

            </Items>
        </x:Toolbar>
        </Toolbars>

       <%--AllowPaging这是分页功能--%>
                   <PageItems>
                            <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                            </x:ToolbarSeparator>
                            <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                            </x:ToolbarText>
                            <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                <x:ListItem Text="10" Value="10" />
                                <x:ListItem Text="20" Value="20" />
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
           <x:BoundField DataField="ProjectName" DataFormatString="{0}" HeaderText="项目名称" />
            <x:BoundField DataField="ProjectHeads" DataFormatString="{0}" HeaderText="项目负责人" />
             <x:BoundField DataField="SourceUnit" HeaderText="项目来源" />
             <x:BoundField DataField="ProjectLevel" HeaderText="项目类型" />
               <x:BoundField DataField="ProjectNature" HeaderText="项目性质" />
               <x:BoundField DataField="StartTime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="开始时间" />
                <x:BoundField DataField="AcceptUnit" HeaderText="承担部门名称" />
       <x:TemplateField Width="80px" HeaderText="经费结余">   
           <ItemTemplate>
                        <asp:Label ID="ManageMoney" runat="server" Text='<%# getLostMoney(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ProjectID"))) %>'></asp:Label>
                    </ItemTemplate>
                </x:TemplateField>

       </Columns>
       </x:Grid>
            <%--  --%>
           

            <%--  --%>
       </Items>
       </x:Panel>
         
        <x:Window ID="Window1" Popup="false" EnableIFrame="true" IFrameUrl="MoneyGiveIn.aspx" runat="server"
            EnableMaximize="true" EnableResize="true" Height="380px" Width="850px" Title="添加">
        </x:Window>

        <x:Window ID="Window2" Title="查询" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Parent" 
            IsModal="True" Width="950px" Height="450px">
        </x:Window>
        <x:Label ID="labResult" Visible="false" runat="server">
        </x:Label>
    </form>
</body>
</html>