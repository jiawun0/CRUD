﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkFront.aspx.cs" Inherits="CRUD.LinkFront" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="連結前台"></asp:Label>
            <br />
            <table>
            <asp:Repeater ID="Repeater_link" runat="server" >
            <ItemTemplate>
            <%# Container.ItemIndex % 3 == 0 ? "<tr>" : "" %>
            <td>
            <%--Id: <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("AlbumName") %>' /><br />--%>
            <asp:Label ID="Label_LinkN" runat="server" Text='<%# Eval("LinkName") %>' /><br />
            <asp:Label ID="Label_LinkD" runat="server" Text='<%# Eval("LinkDescription") %>' /><br />
            <a href ='<%# Eval("LinkURL") %>' target="_blank" >
                <img src='<%# GetRelativeImagePath(Eval("LinkThumbnail").ToString()) %>' width="100" height="100" />
            </a>
            <%--<img src='<%# Eval("AlbumPath") %>' width="100" height="100" />--%>
            <%--<asp:Image ID="Image1" runat="server" Width="100" Height="100" ImageUrl='<%# GetRelativeImagePath(Eval("AlbumPath").ToString()) %>' /><br />--%>
            </td>
            <%# Container.ItemIndex % 3 == 2 || Container.ItemIndex == (Repeater_link.Items.Count - 1) ? "</tr>" : "" %>
            </ItemTemplate>
            </asp:Repeater>
            </table>
        </div>
         <asp:Button ID="BackBtn" runat="server" Text="回到後台" OnClick="BackBtn_Click" style="height: 32px" />
    
    </form>
</body>
</html>
