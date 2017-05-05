<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Update_Monograph.aspx.cs" Inherits="WebApplication1.Update_Monograph" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">
        function clearFileF() {
            var obj = document.getElementById('fileuploadF');
            obj.outerHTML = obj.outerHTML;
        }

        function clearFileB() {
            var obj = document.getElementById('fileuploadB');
            obj.outerHTML = obj.outerHTML;
        }
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="390px" Width="734px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="5"
                BoxConfigChildMargin="0 5 0 0" ShowHeader="false">

                <Items>


                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" ShowBorder="false" BoxMargin="20 0 0 30" ShowHeader="false">
                        <Items>
                            <%-- %>x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="Sort" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="类别：">
                                        </x:Label>                        
                                        <x:TextBox ID="tSort" MaxLength="10"  MaxLengthMessage="最多可输入10个字符"  ShowLabel="true" Label="类别" Width="200px" CssClass="marginr" runat="server" TabIndex="1 ">
                                         </x:TextBox>
                                        </Items>                
                            </%>   

                              <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height ="20px" >
                            </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="MonographName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="著作名称：">
                                    </x:Label>
                                    <x:TextBox ID="tMonographName" MaxLength="100" MaxLengthMessage="最多可输入100个字符" ShowLabel="true" Label="著作名称" Width="200px" CssClass="marginr" runat="server" TabIndex="2" OnTextChanged="tMonographName_TextChanged" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>


                            <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label7" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="著作类型：">
                                    </x:Label>
                                    <x:DropDownList ID="ddlMonographType" EnableEdit="false" runat="server" Width="195px" TabIndex="3" AutoPostBack="true"></x:DropDownList>

                                </Items>
                            </x:Panel>

                            <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel15" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label13" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一作者：">
                                    </x:Label>
                                    <x:TextBox ID="TFirstWriter" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="第一作者" Width="200px" CssClass="marginr" runat="server" TabIndex="4" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label6" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                               <%--这是空行--%>
                                <x:Panel ID="Panel9" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="AwardUnit" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属机构：">
                                        </x:Label>
                                        <x:DropDownList Label="所属机构" AutoPostBack="false" EnableEdit="false" Required="true" EnableSimulateTree="true"
                                            runat="server" ID="DropDownListAgency" Width="185px" TabIndex="15">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>

                                <x:Label ID="Label27" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Publisher" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="出版单位：">
                                    </x:Label>
                                    <x:TextBox ID="tPublisher" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="出版单位" Width="200px" CssClass="marginr" runat="server" TabIndex="5">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label18" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel10" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="IssueRegin" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="出版地：">
                                    </x:Label>
                                    <x:TextBox ID="tIssueRegin" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="出版地" Width="200px" CssClass="marginr" runat="server" TabIndex="6">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>


                            <x:Panel ID="Panel5" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="PUblicationTime" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="出版时间：">
                                    </x:Label>
                                    <x:DatePicker ID="dPUblicationTime" runat="server" Width="200px" EnableEdit="false" ShowLabel="true" Label="出版时间" TabIndex="7" AutoPostBack="true">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="BookNuber" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="图书编号：">
                                    </x:Label>
                                    <x:TextBox ID="tBookNuber" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="图书编号" Width="200px" CssClass="marginr" runat="server" TabIndex="8">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label11" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Revision" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="版次：">
                                    </x:Label>
                                    <x:TextBox ID="tRevision" MaxLength="20" MaxLengthMessage="最多可输入20个字符" ShowLabel="true" Label="版次" Width="200px" CssClass="marginr" runat="server" TabIndex="9">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>




                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel6" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" ShowBorder="false" BoxMargin="20 0 0 30" ShowHeader="false">

                        <Items>



                            <%--x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                              Layout="Column" runat="server">
                                        <Items>
                                        <x:Label ID="AchievementID" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="所属成果名称：">
                                        </x:Label>                        
                                        <x:TextBox ID="tAchievement"  ShowLabel="true" MaxLength="100" MaxLengthMessage="最多可输入100个字符" Label="所属成果名称"   Width="200px" CssClass="marginr" runat="server" TabIndex="10" AutoPostBack="true" OnTextChanged="tAchievement_TextChanged">
                                        </x:TextBox>
                                        </Items>                
                            </%>
                             <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height ="20px" >
                           </x:Label><%--这是空行--%>

                            <x:Panel ID="Panel16" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label14" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="ISBN号：">
                                    </x:Label>
                                    <x:TextBox ID="tISBN" ShowLabel="true" MaxLength="30" MaxLengthMessage="最多可输入30个字符" Label="ISBN号" Width="200px" CssClass="marginr" runat="server" TabIndex="11" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel17" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label21" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="CIP号：">
                                    </x:Label>
                                    <x:TextBox ID="tCIP" ShowLabel="true" MaxLength="30" MaxLengthMessage="最多可输入30个字符" Label="CIP号" Width="200px" CssClass="marginr" runat="server" TabIndex="12" AutoPostBack="true">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label22" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="SecrecyLevel" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="保密级别：">
                                    </x:Label>
                                    <x:DropDownList ID="dSecrecyLevel" ShowLabel="true" Label="保密级别" Width="195px" EnableEdit="false" AutoPostBack="true" runat="server" TabIndex="13">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>



                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel20" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label19" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="著作人：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" MaxLengthMessage="最多可输入200个字符" ShowLabel="true" Label="著作人" CssStyle="overflow-y:scroll" EmptyText="两个或两个以上请用逗号隔开" ID="MoPeople" Width="195px" Height="80px" AutoGrowHeight="false" AutoGrowHeightMax="80" TabIndex="14">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label10" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Remark" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="备注：">
                                    </x:Label>
                                    <x:TextArea runat="server" MaxLength="200" ShowLabel="true" Label="备注" ID="tRemark" Width="195px" Height="80px" AutoGrowHeight="false" CssStyle="overflow-y:scroll" AutoGrowHeightMax="80" TabIndex="15">
                                    </x:TextArea>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                            <%--这是空行--%>
                                <x:Panel ID="Panel26" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                    Layout="Column" runat="server">
                                    <Items>
                                        <x:Label ID="Label26" Width="110px" runat="server" CssClass="marginr" ShowLabel="false" Text="第一作者身份：">
                                        </x:Label>
                                        <x:DropDownList ID="dPaperIdentity" ShowLabel="true" Label="第一作者身份：" Required="true" EnableEdit="false" Width="195px" AutoPostBack="false" runat="server" TabIndex="19" EnableSimulateTree="true">
                                        </x:DropDownList>
                                    </Items>
                                </x:Panel>
                                <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                </x:Label>
                        </Items>
                    </x:Panel>

                </Items>
            </x:Panel>
            <asp:Panel ID="Panelasp" ShowHeader="false" ShowBorder="false"
                Layout="Column" runat="server" Height="45px" BackColor="White">
                <asp:Label ID="Labeld" runat="server" Label="Label" CssClass="marginr" Text=" " Width="35px">
                </asp:Label>
                <asp:Label ID="Label20" runat="server" Label="Label" CssClass="marginr" Text="封面： " Width="95px">
                </asp:Label>
                <input type="file" id="fileuploadF" style="width: 200px" runat="server" />
                <asp:Label ID="Label5" runat="server" Label="Label" CssClass="marginr" Text=" " Width="80px">
                </asp:Label>
                <asp:Label ID="Label4" runat="server" Label="Label" CssClass="marginr" Text="版权页： " Width="100px">
                </asp:Label>
                <input type="file" id="fileuploadB" style="width: 200px" runat="server" />
            </asp:Panel>
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label15" Width="310px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" Size="Medium" Icon="Add" ValidateForms="Panel2" OnClick="Save_Click" ConfirmText="确定保存？">
                            </x:Button>
                            <x:Button ID="DeleteAll" runat="server" CssClass="marginr" Text="重置" Icon="Delete" Size="Medium" ConfirmText="确定重置？" OnClick="DeleteAll_Click">
                            </x:Button>
                        </Items>
                    </x:Toolbar>
                </Items>
            </x:Panel>




        </div>
    </form>
</body>
</html>
