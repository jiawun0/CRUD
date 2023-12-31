﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoBack.aspx.cs" Inherits="CRUD.VideoBack" %>

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
            <%--<asp:Label ID="Label_VideoIframe" runat="server" Text="影片Id v= "></asp:Label>
            <br />--%>
            <%--<asp:TextBox ID="TextBox_VideoIframe" runat="server" Placeholder="請輸入影片網址尾碼"></asp:TextBox>
            <br />--%>
            <asp:Button ID="CreateVideoBtn" runat="server" Text="上傳影片資料" OnClick="CreateVideoBtn_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView_VideoBack" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_VideoBack_RowCancelingEdit" OnRowDeleting="GridView_VideoBack_RowDeleting" OnRowEditing="GridView_VideoBack_RowEditing" OnRowUpdating="GridView_VideoBack_RowUpdating" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:TemplateField HeaderText="VideoName" SortExpression="VideoName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateVN" runat="server" Text='<%# Bind("VideoName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateVN" runat="server" Text='<%# Bind("VideoName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="VideoURL" HeaderText="VideoURL" SortExpression="VideoURL" />
                    <asp:BoundField DataField="VideoIframe" HeaderText="VideoIframe" SortExpression="VideoIframe" />
                    --%>
                    <asp:TemplateField HeaderText="VideoDescription" SortExpression="VideoDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateVD" runat="server" Text='<%# Bind("VideoDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateVD" runat="server" Text='<%# Bind("VideoDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="VideoCreatTime" HeaderText="VideoCreatTime" SortExpression="VideoCreatTime" ReadOnly="True" />
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
        <asp:Button ID="FrontBtn" runat="server" Text="回到前台" OnClick="FrontBtn_Click" style="height: 32px" />
        <br />
        <asp:Button ID="AlbumBack" runat="server" Text="返回影片分類後台" OnClick="BackBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
