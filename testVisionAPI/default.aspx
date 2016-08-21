<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="default.aspx.cs" Inherits="testVisionAPI._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        請選擇一張照片上傳:
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="上傳並辨識" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
    
    </div>
        <asp:Image ID="Image1" runat="server" />
    </form>
</body>
</html>
