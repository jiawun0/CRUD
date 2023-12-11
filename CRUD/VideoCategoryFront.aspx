<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoCategoryFront.aspx.cs" Inherits="CRUD.VideoCategoryFront" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="影片種類前台"></asp:Label>
            <br />
            <asp:GridView ID="GridView_VideoCategoryFront" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="VideoFront.aspx?CategoryID={0}" DataTextField="CategoryName" HeaderText="CategoryName" />
                    <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" SortExpression="CategoryName" Visible="False" />
                    <asp:BoundField DataField="CategoryCreatTime" HeaderText="CategoryCreatTime" SortExpression="CategoryCreatTime" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="BackBtn" runat="server" Text="回到後台" OnClick="BackBtn_Click" style="height: 32px" />
            </div>
    </form>
</body>
</html>
