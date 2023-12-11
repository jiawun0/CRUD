<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoBack.aspx.cs" Inherits="CRUD.VideoBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_Video" runat="server" Text="影片後台系統"></asp:Label>
            <br />
            <asp:Label ID="Label_VideoName" runat="server" Text="影片名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_VideoName" runat="server" Placeholder="請輸入影片名稱" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_VideoDescription" runat="server" Text="影片描述: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_VideoDescription" runat="server" Placeholder="請輸入影片描述" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_VideoURL" runat="server" Text="影片網址: " ></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_VideoURL" runat="server" Placeholder="請輸入影片完整網址" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_VideoIframe" runat="server" Text="影片嵌入: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_VideoIframe" runat="server" Placeholder="請輸入影片嵌入尾碼"></asp:TextBox>
            <br />
            <asp:Button ID="CreateVideoBtn" runat="server" Text="上傳影片資料" OnClick="CreateVideoBtn_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView_VideoBack" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="VideoName" HeaderText="VideoName" SortExpression="VideoName" />
                    <asp:BoundField DataField="VideoDescription" HeaderText="VideoDescription" SortExpression="VideoDescription" />
                    <%--<asp:BoundField DataField="VideoURL" HeaderText="VideoURL" SortExpression="VideoURL" />
                    <asp:BoundField DataField="VideoIframe" HeaderText="VideoIframe" SortExpression="VideoIframe" />
                    --%><asp:BoundField DataField="VideoCreatTime" HeaderText="VideoCreatTime" SortExpression="VideoCreatTime" />
                    <%--<asp:BoundField DataField="CategoryID" HeaderText="CategoryID" SortExpression="CategoryID" />--%>
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
           <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="VideoName" Type="String" />
                    <asp:Parameter Name="VideoDescription" Type="String" />
                    <asp:Parameter Name="VideoURL" Type="String" />
                    <asp:Parameter Name="VideoIframe" Type="String" />
                    <asp:Parameter Name="VideoCreatTime" Type="DateTime" />
                    <asp:Parameter Name="CategoryID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="VideoName" Type="String" />
                    <asp:Parameter Name="VideoDescription" Type="String" />
                    <asp:Parameter Name="VideoURL" Type="String" />
                    <asp:Parameter Name="VideoIframe" Type="String" />
                    <asp:Parameter Name="VideoCreatTime" Type="DateTime" />
                    <asp:Parameter Name="CategoryID" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
            <br />
        </div>
    </form>
</body>
</html>
