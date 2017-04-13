<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADD.aspx.cs" Inherits="WDFramework.ADD" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
      <script  type="text/javascript">
          function clearFile() {
              var obj = document.getElementById('fileupload');
              obj.outerHTML = obj.outerHTML;
          }
    </script>
    <title></title>
  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="Panel2" runat="server" />

            <x:Panel ID="Panel2" runat="server" Height="310px" ShowBorder="false" EnableCollapse="true"
                Layout="HBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" BoxConfigPadding="10"
                BoxConfigChildMargin="5 40 0 0" ShowHeader="false">
                <Items>
                    <x:Panel ID="Panel3" Title="项目概要" BoxFlex="1" runat="server"
                        BodyPadding="5px" ShowBorder="false" ShowHeader="false" ColumnWidth="100%">
                        <Items>

                            <x:Panel ID="Panel14" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Achievement" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文件名：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="文件名" ID="FileName" Required="true" Width="255px" EmptyText="请输入文件名" MaxLength="100" MaxLengthMessage="最多可输入100个字符" AutoPostBack="true" TabIndex="1" OnTextChanged="FileName_TextChanged">
                                    </x:TextBox>
                                </Items>
                            </x:Panel>

                            <x:Label ID="Label16" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel1" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardName" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文件分类：">
                                    </x:Label>
                                    <x:DropDownList Label="文件分类" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="2"
                                        runat="server" ID="DropDownListFile" Width="250px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>

                            <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="AwardUnit" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="发放部门：">
                                    </x:Label>
                                    <x:DropDownList Label="发放部门" AutoPostBack="false" Required="true" EnableSimulateTree="true" TabIndex="3"
                                        runat="server" ID="DropDownListAgency" Width="250px">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>
                            <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label6" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="来文单位：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="来文单位" ID="LevinUnit" Required="true" Width="255px"  MaxLength="50" MaxLengthMessage="最多可输入50个字符" TabIndex="4" AutoPostBack="true" >
                                    </x:TextBox>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel7" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label8" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文件来文时间：">
                                    </x:Label>
                                        <x:DatePicker ID="LevinTime" runat="server" ShowLabel="true" EnableEdit="false" Label="文件来文时间" Required="true" Width="250px" TabIndex="5">
                                    </x:DatePicker>
                                </Items>
                            </x:Panel>
                             <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                            </x:Label>
                            <%--这是空行--%>
                            <x:Panel ID="Panel8" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                Layout="Column" runat="server">
                                <Items>
                                    <x:Label ID="Label10" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="文件接收人：">
                                    </x:Label>
                                    <x:TextBox runat="server" Label="文件接收人" ID="FileRecipient" Required="true" Width="255px"  MaxLength="20" MaxLengthMessage="最多可输入20个字符" TabIndex="6" AutoPostBack="true" >
                                    </x:TextBox>
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
                                    <x:DropDownList Label="保密等级" Required="true" EnableSimulateTree="true" runat="server" ID="DropDownListLevel" Width="250px" TabIndex="7">
                                    </x:DropDownList>
                                </Items>
                            </x:Panel>

                        </Items>
                    </x:Panel>
                </Items>

            </x:Panel>

          
             <asp:Panel ID="Panelasp" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                Layout="Column" runat="server" Height="25px" BackColor="White">
                  <asp:Label ID="Label5" runat="server" Label="Label" CssClass="marginr" Text=" " Width="15px">
            </asp:Label>  
            <asp:Label ID="Label4" runat="server" Label="Label" CssClass="marginr" Text="相关文档： " Width="95px">
            </asp:Label>                       
            <input type="file"  id="fileupload" style="width:250px" runat="server"/>        
         </asp:Panel>
            <x:Panel ID="Panel4" ShowHeader="false" CssClass="formitem" ShowBorder="false" Layout="Column" runat="server">
                <Items>
                    <x:Toolbar ID="Toolbar1" runat="server" ColumnWidth="100%">
                        <Items>
                            <x:Label ID="Label15" Width="150px" runat="server" ShowLabel="true" Text=" ">
                            </x:Label>
                            <x:Button ID="Save" runat="server" CssClass="marginr" Type="Submit" Text="保存" ConfirmText="确定保存？" ValidateForms="Panel2" Size="Medium" Icon="Add" OnClick="btnSave_Click">
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