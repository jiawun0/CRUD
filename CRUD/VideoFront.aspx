<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoFront.aspx.cs" Inherits="CRUD.VideoFront" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="影片前台"></asp:Label>
            <br />
            <table>
            <asp:Repeater ID="Repeater_Video" runat="server" >
            <ItemTemplate>
            <%# Container.ItemIndex % 2 == 0 ? "<tr>" : "" %>
            <td>
            <%--Id: <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("AlbumName") %>' /><br />--%>
            VideoName: <%--<asp:Label ID="AlbumNLabel" runat="server" Text='<%# Eval("AlbumName") %>' /><br />--%>
                <a href='<%# Eval("VideoURL") %>'> 
                <asp:Label ID="Label_hrefVURL" runat="server" Text='<%# Eval("VideoName") %>' />
                </a>
                <iframe width="560" height="315" src="<%# "https://www.youtube.com/embed/" +GetVideoID(Eval("VideoURL").ToString()) %>" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowfullscreen></iframe>
                <br />
            VideoDescription: <asp:Label ID="VideoDLabel" runat="server" Text='<%# Eval("VideoDescription") %>' /><br />
            <img src='<%# GetThumbnailUrl(Eval("VideoIframe").ToString()) %>' width="100" height="100" />
            <%--<img src='<%# Eval("AlbumPath") %>' width="100" height="100" />--%>
            <%--<asp:Image ID="Image1" runat="server" Width="100" Height="100" ImageUrl='<%# GetRelativeImagePath(Eval("AlbumPath").ToString()) %>' /><br />--%>
            </td>
            <%# Container.ItemIndex % 2 == 1 || Container.ItemIndex == (Repeater_Video.Items.Count - 1) ? "</tr>" : "" %>
            </ItemTemplate>
            </asp:Repeater>
            </table>
        </div>
        <asp:Button ID="BackBtn" runat="server" Text="回到後台" OnClick="BackBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
