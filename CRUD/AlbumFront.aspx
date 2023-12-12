<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumFront.aspx.cs" Inherits="CRUD.AlbumFront" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="相簿前台"></asp:Label>
            <br />

            <%--<asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >--%>
            <table>
            <asp:Repeater ID="Repeater_album" runat="server" >
            <ItemTemplate>
            <%# Container.ItemIndex % 3 == 0 ? "<tr>" : "" %>
            <td>
            <%--Id: <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("AlbumName") %>' /><br />--%>
            AlbumName: <%--<asp:Label ID="AlbumNLabel" runat="server" Text='<%# Eval("AlbumName") %>' /><br />--%>
            <a href='<%# "PhotoFront.aspx?AlbumId=" + Eval("Id") %>'>
            <asp:Label ID="Label_hrefAN" runat="server" Text='<%# Eval("AlbumName") %>' /></a>
            <br />
            AlbumDescription: <asp:Label ID="AlbumDLabel" runat="server" Text='<%# Eval("AlbumDescription") %>' /><br />
            <asp:Image ID="Image_PhotoIsCover" runat="server" Width="100" Height="100"
                ImageUrl='<%# GetRelativeImagePath(Eval("PhotoPath").ToString()) %>' Visible='<%# !string.IsNullOrEmpty(GetRelativeImagePath(Eval("PhotoPath").ToString())) %>'/>
            <asp:Literal ID="Literal_IsCover" runat="server" Text='<%# Eval("IsCover") %>' Visible="false" />
            <br />
            <%--<img src='<%# GetRelativeImagePath(Eval("AlbumPath").ToString()) %>' width="100" height="100" />--%>
            <%--<img src='<%# Eval("AlbumPath") %>' width="100" height="100" />--%>
            <%--<asp:Image ID="Image1" runat="server" Width="100" Height="100" ImageUrl='<%# GetRelativeImagePath(Eval("AlbumPath").ToString()) %>' /><br />--%>
            </td>
            <%# Container.ItemIndex % 3 == 2 || Container.ItemIndex == (Repeater_album.Items.Count - 1) ? "</tr>" : "" %>
            </ItemTemplate>
            </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="BackBtn" runat="server" Text="回到後台" OnClick="BackBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
