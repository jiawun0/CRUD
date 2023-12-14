<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkBack.aspx.cs" Inherits="CRUD.LinkBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_Link" runat="server" Text="連結後台系統"></asp:Label>
            <br />
            <asp:Label ID="Label_LinkName" runat="server" Text="連結名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_LinkName" runat="server" Placeholder="請輸入連結名稱" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_LinkDescription" runat="server" Text="連結描述: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_LinkDescription" runat="server" Placeholder="請輸入連結描述" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_LinkURL" runat="server" Text="連結網址: " ></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_LinkURL" runat="server" Placeholder="請輸入連結完整網址" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_LinkThumbnail" runat="server" Text="連結網址縮圖: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_LinkThumbnail" runat="server" Placeholder="請上傳"></asp:TextBox>
            <br />
            <asp:Button ID="CreateLinkBtn" runat="server" Text="上傳連結資料" OnClick="CreateVideoBtn_Click" />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
