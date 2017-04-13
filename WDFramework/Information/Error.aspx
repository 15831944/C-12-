<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="WDFramework.Information.Error" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">    
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">   
        body{TEXT-ALIGN: center;}   
        #center {
            position:absolute;
            top:45%;
            left:50%;
            margin:-150px 0 0 -100px;
            /*position:fixed;
            right:35%;
            top:30%;*/
            /*MARGIN-RIGHT: auto; 
            MARGIN-LEFT: auto;*/ 
            /*border:solid;*/
            height:300px;
            width:350px;
            /*vertical-align:middle;
            line-height:200px;*/
        }
        #left {
            /*float: left;*/
        }
        #right {
            font-size:x-large;
            color:gray;
            /*float: right;*/
            text-align :center;
        }
        #foot {
            width: 100%;
            margin: 0 auto;
            position: absolute;
            bottom: 0px;
            text-align: center;
            color: #ffffff;
            line-height: 25px;
            left: 10px;
            padding-bottom: 10px;
        }
        .btn {
  color: #d9eef7;  
    border: solid 1px #0076a3;  
    background: #0095cd;  
    background: -webkit-gradient(linear, left top, left bottom, from(#00adee), to(#0078a5));  
    background: -moz-linear-gradient(top,  #00adee,  #0078a5);  
    filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#00adee', endColorstr='#0078a5');  
}
        .green {  
    color: #e8f0de;  
    border: solid 1px #538312;  
    background: #64991e;  
    background: -webkit-gradient(linear, left top, left bottom, from(#7db72f), to(#4e7d0e));  
    background: -moz-linear-gradient(top,  #7db72f,  #4e7d0e);  
    filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#7db72f', endColorstr='#4e7d0e');  
}  
.green:hover {  
    background: #538018;  
    background: -webkit-gradient(linear, left top, left bottom, from(#6b9d28), to(#436b0c));  
    background: -moz-linear-gradient(top,  #6b9d28,  #436b0c);  
    filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#6b9d28', endColorstr='#436b0c');  
}  
.green:active {  
    color: #a9c08c;  
    background: -webkit-gradient(linear, left top, left bottom, from(#4e7d0e), to(#7db72f));  
    background: -moz-linear-gradient(top,  #4e7d0e,  #7db72f);  
    filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#4e7d0e', endColorstr='#7db72f');  
}  
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="center">
        <div id="left">
            <%--<img src="images/pic2.jpg" />--%>     
            <img alt="" src="/images/pic2.jpg" />
        </div>
        <div id="right">
            <p>很抱歉！</p>
            <p>您访问的页面出错啦！</p>
        </div>
        <div id="foot">
           <%-- <asp:Button CssClass="green" ID="btn_back" runat="server" Text="返回上一页面" OnClick="Back_Click"/>--%>
        </div>
    </div>            
    </form>
</body>
</html>