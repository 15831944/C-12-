<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeopleResume.aspx.cs" Inherits="WDFramework.People.PeopleResume" %>




<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <x:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server" />
            <x:Panel ID="Panel1" runat="server" BodyPadding="5px" EnableBackgroundColor="true"
                ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start" AutoScroll="true"
                ShowHeader="false" Title="" Height="1250px">
                <Items>
                    <x:Panel ID="Panel15" runat="server" Height="300px"
                        BodyPadding="5px" Layout="Column" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel4" runat="server" Height="300px" ColumnWidth="10%"
                                BodyPadding="5px" Layout="Column" ShowBorder="false" ShowHeader="false">
                                <Items>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel20" Title="" runat="server" ColumnWidth="50%"
                                BodyPadding="5px" ShowBorder="true" ShowHeader="false" Height="300px">
                                <Items>
                                    <x:Label ID="Label19" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Panel ID="Panel2" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label13" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="姓">
                                            </x:Label>
                                            <x:Label ID="Label20" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="名：">
                                            </x:Label>
                                            <x:TextBox ID="UserName" ShowLabel="true" Readonly="true" Required="true" ShowRedStar="true" Width="340px" TabIndex="1" CssClass="marginr" runat="server" MaxLength="10" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>

                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label1" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel13" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label21" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="性">
                                            </x:Label>
                                            <x:Label ID="Label22" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="别：">
                                            </x:Label>
                                            <x:TextBox ID="Sex" MaxLength="50" ShowLabel="false" Readonly="true" Required="true" Width="340px" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label15" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Panel ID="Panel6" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label23" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="民">
                                            </x:Label>
                                            <x:Label ID="Label24" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="族：">
                                            </x:Label>
                                            <x:TextBox ID="Nation" MaxLength="30" ShowLabel="false" Readonly="true" Required="true" Width="340px" ShowRedStar="true" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label14" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Panel ID="Panel11" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label6" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="籍">
                                            </x:Label>
                                            <x:Label ID="Label25" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="贯：">
                                            </x:Label>
                                            <x:TextBox ID="Hometown" MaxLength="30" ShowLabel="false" Readonly="true" Required="true" Width="340px" ShowRedStar="true" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label12" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Panel ID="Panel12" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label10" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="出">
                                            </x:Label>
                                            <x:Label ID="Label26" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="生年月：">
                                            </x:Label>

                                            <x:TextBox ID="Birth" MaxLength="30" ShowLabel="false" Readonly="true" Required="true" Width="340px" ShowRedStar="true" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label2" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>

                                    <x:Panel ID="Panel3" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label11" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="所">
                                            </x:Label>
                                            <x:Label ID="Label27" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="属科研机构：">
                                            </x:Label>

                                            <x:TextBox ID="AgencyID" MaxLength="30" ShowLabel="false" Readonly="true" Required="true" Width="340px" ShowRedStar="true" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>
                                        </Items>
                                    </x:Panel>
                                    <x:Label ID="Label3" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Panel ID="Panel27" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="Column" runat="server">
                                        <Items>
                                            <x:Label ID="Label28" Width="240px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="保">
                                            </x:Label>
                                            <x:Label ID="Label29" Width="80px" runat="server" CssClass="marginr" ShowLabel="false" Text="密级别：">
                                            </x:Label>

                                            <x:TextBox ID="SecrecyLevel" MaxLength="30" ShowLabel="false" Readonly="true" Required="true" Width="340px" ShowRedStar="true" CssClass="marginr" runat="server" MaxLengthMessage="字符过长" AutoPostBack="true">
                                            </x:TextBox>

                                        </Items>
                                    </x:Panel>

                                </Items>
                            </x:Panel>
                            <x:Panel ID="panel1455" BodyPadding="5px" runat="server" ColumnWidth="30%" Height="300px" CssStyle="text-align:center"
                                ShowBorder="true" ShowHeader="false">
                                <Items>

                                    <x:Label ID="Label30" runat="server" Label="Label" Text=" " Height="20px">
                                    </x:Label>
                                    <%--这是空行--%>
                                    <x:Image ID="imgPhoto" runat="server" ImageUrl="~/images/blank.png" Label="Label" ImageHeight="200px" ImageWidth="160px"></x:Image>
                                    <x:Panel ID="Panel19" ShowHeader="false" CssClass="formitem" ShowBorder="false"
                                        Layout="HBox" runat="server">
                                        <Items>
                                            <x:Label ID="Label32" Width="130px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="">
                                            </x:Label>
                                            <x:FileUpload runat="server" ID="filePhoto" EmptyText="请选择上传的图片" Width="250px">
                                            </x:FileUpload>
                                            <x:Label ID="Label31" Width="10px" CssStyle="text-align:right" runat="server" CssClass="marginr" ShowLabel="false" Text="">
                                            </x:Label>
                                            <x:Button ID="Save" Text="上传" Icon="SystemSearch" OnClick="Save_Click" runat="server">
                                            </x:Button>
                                        </Items>
                                    </x:Panel>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel7" runat="server" Height="300px" ColumnWidth="10%"
                                BodyPadding="5px" Layout="Column" ShowBorder="false" ShowHeader="false">
                                <Items>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Panel>
                    <x:Panel ID="Panel21" runat="server" Height="1000px"
                        BodyPadding="5px" Layout="Column" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <x:Panel ID="Panel22" runat="server" Height="1000px" ColumnWidth="10%"
                                BodyPadding="5px" Layout="Column" ShowBorder="false" ShowHeader="false">
                                <Items>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel16" Title="" runat="server" Height="1000px" ColumnWidth="80%"
                                Layout="Column" ShowBorder="true" ShowHeader="false">
                                <Items>
                                    <x:Panel ID="Panel18" runat="server" Height="1000px" ColumnWidth="25%"
                                        Layout="Column" ShowBorder="false" ShowHeader="false">
                                        <Items>
                                        </Items>
                                    </x:Panel>
                                    <x:Panel ID="Panel17" runat="server" Height="1000px" ColumnWidth="65%"
                                        Layout="VBox" ShowBorder="false" ShowHeader="false">
                                        <Items>
                                            <x:Panel ID="Panel8" ShowHeader="false" ShowBorder="false" 
                                                runat="server">
                                                <Items>
                                                    <x:Label ID="Label5"  runat="server"  ShowLabel="false" Text="研究方向：">
                                                    </x:Label>
                                                    <x:TextArea runat="server" ID="TextAreaResearchDirection" Readonly="true" Width="700px" Height="70px">
                                                    </x:TextArea>
                                                </Items>
                                            </x:Panel>
                                            <x:Label ID="Label8" runat="server" Label="Label" Text=" " Height="20px">
                                            </x:Label>
                                            <%--这是空行--%>
                                            <x:Panel ID="Panel14" ShowHeader="false"  ShowBorder="false" 
                                                 runat="server">
                                                <Items>
                                                    <x:Label ID="Label16" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="专长：">
                                                    </x:Label>
                                                    <x:TextArea runat="server" ID="TextAreaSpecialty" Readonly="true" Width="700px" Height="70px">
                                                    </x:TextArea>
                                                </Items>
                                            </x:Panel>

                                            <x:Label ID="Label17" runat="server" Label="Label" Text=" " Height="20px">
                                            </x:Label>
                                            <%--这是空行--%>
                                            <x:Panel ID="Panel9" ShowHeader="false" ShowBorder="false"
                                                 runat="server">
                                                <Items>
                                                    <x:Label ID="lContent" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="学历：">
                                                    </x:Label>
                                                    <x:TextArea runat="server" ID="TextAreaEducation" Readonly="true" Width="700px" Height="200px">
                                                    </x:TextArea>
                                                </Items>
                                            </x:Panel>
                                            <x:Label ID="Label7" runat="server" Label="Label" Text=" " Height="20px">
                                            </x:Label>
                                            <%--这是空行--%>
                                            <x:Panel ID="Panel5" ShowHeader="false" ShowBorder="false"
                                                 runat="server">
                                                <Items>
                                                    <x:Label ID="Label4" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="工作经历：">
                                                    </x:Label>
                                                    <x:TextArea runat="server" ID="TextAreaWork" Readonly="true" Width="700px" Height="200px">
                                                    </x:TextArea>
                                                </Items>
                                            </x:Panel>
                                            <x:Label ID="Label9" runat="server" Label="Label" Text=" " Height="20px">
                                            </x:Label>
                                            <%--这是空行--%>
                                            <x:Panel ID="Panel10" ShowHeader="false"  ShowBorder="false" 
                                                runat="server">
                                                <Items>
                                                    <x:Label ID="Label18" Width="100px" runat="server" CssClass="marginr" ShowLabel="false" Text="教育经历：">
                                                    </x:Label>
                                                    <x:TextArea runat="server" ID="TextAreaEducations" Readonly="true" Width="700px" Height="200px">
                                                    </x:TextArea>
                                                </Items>
                                            </x:Panel>
                                        </Items>
                                    </x:Panel>
                                </Items>
                            </x:Panel>
                            <x:Panel ID="Panel23" runat="server" Height="1000px" ColumnWidth="10%"
                                Layout="Column" ShowBorder="false" ShowHeader="false">
                                <Items>
                                </Items>
                            </x:Panel>
                        </Items>
                    </x:Panel>
                </Items>
            </x:Panel>
        </div>
    </form>
</body>
</html>



