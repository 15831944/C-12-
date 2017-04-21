<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search_Patent.aspx.cs" Inherits="WebApplication1.Search_Patent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu='return false'>
    <%--取消鼠标右键的点击--%>
    <form id="form1" runat="server">
        <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
            Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" EnableCollapse="true"
            ShowHeader="false">
            <Items>

                <x:Grid ID="Grid_Patent" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableRowNumberPaging="true"
                    DataKeyNames="PatentID" AllowSorting="true" SortColumnIndex="0"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" AutoPostBack="true" OnPageIndexChange="Grid_Patent_PageIndexChange" OnRowCommand="Grid_Patent_RowCommand">
                    <Toolbars>
                        <x:Toolbar ID="Toolbar1" runat="server" Width="1300px">
                            <Items>
                                <%-- <x:Label ID="Label1" runat="server" Label="Label" Width ="15" Text=" "></x:Label>--%>
                                <x:Label ID="Label2" runat="server" Label="Label" Text="查询类别："></x:Label>
                                <%-- <x:Label ID="Label3" runat="server" Label="Label" Width ="15" Text=" "></x:Label>--%>

                                <x:DropDownList ID="dChoose" Width="100px" ShowLabel="false" AutoPostBack="true" runat="server" OnSelectedIndexChanged="tchoose_SelectedIndexChanged">
                                    <x:ListItem Text="全部" Value="全部" />
                                    <x:ListItem Text="所属机构" Value="所属机构" />
                                    <x:ListItem Text="专利类型" Value="专利类型" />
                                    <x:ListItem Text="申请年份" Value="申请年份" />
                                    <x:ListItem Text="授权年份" Value="授权年份" />
                                    <x:ListItem Text="发明人" Value="发明人" />
                                    <%-- <x:ListItem Text="成员" Value="成员" /> --%>
                                    <x:ListItem Text="保密级别" Value="保密级别" />
                                </x:DropDownList>
                                <x:Label ID="Label3" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:TextBox ID="tCondition" ShowLabel="true" MaxLength="40" Enabled="false" MaxLengthMessage="最多可输入40个字符" EmptyText="请输入搜索条件" Width="100px" CssClass="marginr" runat="server" AutoPostBack="true">
                                </x:TextBox>
                                <x:Label ID="Label4" runat="server" Label="Label" Text=" " Width="5px">
                                </x:Label>
                                <%--这是空行--%>
                                <x:DropDownList ID="dCondition" Enabled="false" Width="100px" ShowLabel="false" EnableEdit="true" AutoPostBack="true" runat="server">
                                </x:DropDownList>
                                <x:DropDownList ID="secrecyLevel" Width="100px" runat="server">
                                    <x:ListItem Text="四级" Value="1" Selected="true" />
                                    <x:ListItem Text="三级" Value="2" />
                                    <x:ListItem Text="二级" Value="3" />
                                    <x:ListItem Text="一级" Value="4" />
                                    <x:ListItem Text="管理员" Value="5" />
                                </x:DropDownList>
                                <x:Button ID="Select" Text="搜索" Type="Submit" Icon="SystemSearch" runat="server" OnClick="Select_Click">
                                </x:Button>
                                <x:Button ID="btnRefresh" runat="server" Icon="ArrowRotateClockwise" Text="刷新" OnClick="btnRefresh_Click">
                                </x:Button>
                                <x:Button ID="btn_AddPatent" Text="新增专利信息" Icon="Add" EnablePostBack="true" runat="server">
                                </x:Button>

                                <x:Button ID="btn_UpdatePatent" Text="编辑选中行" Icon="BulletEdit" EnablePostBack="true" runat="server" OnClick="btn_UpdatePatent_Click">
                                </x:Button>
                                <x:Button ID="btnSelect_All" runat="server" Text="全选" OnClick="btnSelect_All_Click"></x:Button>
                                <x:Button ID="Delete" Text="删除" Icon="Delete" ConfirmText="确定删除？" Enabled="false" ConfirmTarget="Top" runat="server" OnClick="Delete_Click">
                                </x:Button>
                                <x:Button ID="btnTool" EnablePostBack="false" Text="工具"  runat="server">
                                <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="btnExcel" runat="server" Text="Excel导入">
                                    </x:MenuButton>
                                    <x:MenuButton ID="btn_Get" Text="导出Excel文件" ConfirmText="确定导出？" Icon="Disk" EnablePostBack="true" runat="server" OnClick="btn_Get_Click" EnableAjax="false">
                                        </x:MenuButton>
                                </Menu>
                                </x:Button>
                                
                            <%--</x:Button>--%>
                                <%-- <x:Button ID="reprot" Text="报表"  Icon="Report" EnablePostBack="true" runat="server" >
                                         <Menu ID="Menu1" runat="server">
                                    <x:MenuButton ID="reprot1" runat="server" Text="分部门按专利名称统计专利情况">
                                    </x:MenuButton>                                
                                </Menu>
                                    </x:Button>--%>
                            </Items>
                        </x:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <x:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </x:ToolbarSeparator>
                        <x:ToolbarText ID="ToolbarText1" runat="server" Text="每页记录数：">
                        </x:ToolbarText>
                        <x:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                            <x:ListItem Text="10" Value="10" />
                            <x:ListItem Text="20" Value="20" Selected="true" />
                            <x:ListItem Text="30" Value="30" />
                            <x:ListItem Text="50" Value="50" />
                        </x:DropDownList>
                    </PageItems>
                    <Columns>
                        <x:TemplateField Width="30px">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:CheckBoxField ID="CBoxSelect" CommandName="CBSelect" DataField="insepctid" runat="server" AutoPostBack="true" Enabled="true" RenderAsStaticField="false" Width="30" />
                        <x:BoundField Enabled="true" DataField="EntryPerson" SortField="EntryPerson" Hidden="true" />
                        <x:TemplateField Width="30px" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%#RowNumber( Container.DataItemIndex + 1) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:BoundField DataField="PatentNumber" HeaderText="专利号" />
                        <x:BoundField DataField="PatentName" HeaderText="专利名称" />
                        <x:BoundField DataField="FirstPeople" HeaderText="第一发明人" />
                        <x:BoundField DataField="CertificateNumber" HeaderText="证书号" />
                        <x:BoundField DataField="PatentForm" HeaderText="专利类型" />
                        <x:BoundField DataField="ApplicationTime" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="AccreditTime" HeaderText="授权时间" DataFormatString="{0:yyyy-MM-dd}" />
                        <x:BoundField DataField="GivenUnit" HeaderText="授予机构" />
                        <x:BoundField DataField="State" HeaderText="状态" />
                        <x:BoundField DataField="AgencyID" HeaderText="所属机构" />
                        <%-- <x:BoundField DataField="Fund" HeaderText="资助经费" /> --%>
                        <x:BoundField DataField="PatentCondition" HeaderText="专利情况" />
                        <x:BoundField DataField="PatentAuthorization" HeaderText="专利授权号" />
                        <x:BoundField DataField="PatentCertificate" HeaderText="专利证书号" />
                        <%-- <x:BoundField DataField="ApplyNum" HeaderText="申请号" /> --%>

                        <x:TemplateField Width="200px" HeaderText="所属成果">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# FindName(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "AchievementID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField Width="80px" HeaderText="保密级别">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ChangeSecrecyLevel(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "SecrecyLevel"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:TemplateField HeaderText="单位" Hidden="true">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# getunit(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "PatentID"))) %>'></asp:Label>
                            </ItemTemplate>
                        </x:TemplateField>

                        <x:BoundField DataField="PatentPeople" HeaderText="全部发明人" Hidden="true" />
                        <x:BoundField DataField="PatentDepartment" HeaderText="单位" Hidden="true" />
                        <x:BoundField DataField="Comment" HeaderText="备注" Hidden="true" />

                        <x:TemplateField HeaderText="全部发明人" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlp(Eval("PatentID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="单位" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlw(Eval("PatentID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="备注" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrl(Eval("PatentID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="申请书" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrla(Eval("PatentID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                        <x:TemplateField HeaderText="专利证书" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlx(Eval("PatentID")) %>">操作</a>
                            </ItemTemplate>
                        </x:TemplateField>
                      <%-- <x:TemplateField HeaderText="成员" Width="60px">
                            <ItemTemplate>
                                <a href="javascript:<%# GetEditUrlm(Eval("PatentID")) %>">详情</a>
                            </ItemTemplate>
                        </x:TemplateField>  --%>
                    </Columns>
                </x:Grid>
            </Items>

        </x:Panel>
        <x:Window ID="Window_addPatent" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="565px" Width="800px" Title="添加">
        </x:Window>
        <x:Window ID="Window_UpdatePatent" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="595px" Width="800px" Title="更新">
        </x:Window>
        <x:Window ID="Remark" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="People" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="Units" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px">
        </x:Window>
        <x:Window ID="DownLoad" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="false" EnableMinimize="false" EnableResize="false" Height="250px" Width="350px" Title="相关文件操作">
        </x:Window>
        <x:Window ID="WindowReport" Popup="false" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="false" Height="450px" Width="800px">
        </x:Window>
        <x:Window ID="Window_Import" Popup="false" EnableIFrame="true" runat="server" 
            EnableMaximize="false" EnableResize="false" Height="250px" Width="350px" OnClose="Window_Import_Close" CloseAction="HidePostBack">
        </x:Window>
    </form>
</body>
</html>
