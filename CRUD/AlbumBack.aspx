<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumBack.aspx.cs" Inherits="CRUD.AlbumBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_AlbumUpload" runat="server" Text="相簿上傳系統"></asp:Label>
            <br />
            <asp:Label ID="Label_AlbumName" runat="server" Text="相簿名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_AlbumName" runat="server" Placeholder="請輸入相簿名稱" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_AlbumDescription" runat="server" Text="相簿描述: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_AlbumDescription" runat="server" Placeholder="請輸入相簿描述" ></asp:TextBox>
            <br />
            <asp:Button ID="CreateFileBtn" runat="server" Text="新增相簿" OnClick="CreateFileBtn_Click" />
            <br />
            <asp:Label ID="ResultLabel" runat="server" ></asp:Label>
            <br />
            <asp:GridView ID="GridView_AlbumUpload" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView_AlbumUpload_RowCancelingEdit">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <%--<asp:BoundField DataField="AlbumCreatTime" HeaderText="AlbumCreatTime" SortExpression="AlbumCreatTime" />--%>
                    <asp:TemplateField HeaderText="AlbumName" SortExpression="AlbumName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateAN" runat="server" Text='<%# Bind("AlbumName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateAN" runat="server" Text='<%# Bind("AlbumName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlbumDescription" SortExpression="AlbumDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateAD" runat="server" Text='<%# Bind("AlbumDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateAD" runat="server" Text='<%# Bind("AlbumDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AlbumPath" HeaderText="AlbumPath" SortExpression="AlbumPath" />
                    <asp:TemplateField HeaderText="Actions">
            <ItemTemplate>
                <asp:Button ID="BtnRedirectPhotoBack" runat="server" Text="相片後台" CommandArgument='<%# Eval("Id") %>' CommandName="RedirectToPhotoBack" OnClick="BtnRedirect_Click" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="AlbumName" Type="String" />
                    <asp:Parameter Name="AlbumDescription" Type="String" />
                    <asp:Parameter Name="AlbumPath" Type="String" />
                    <asp:Parameter Name="AlbumCreatTime" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="AlbumName" Type="String" />
                    <asp:Parameter Name="AlbumDescription" Type="String" />
                    <asp:Parameter Name="AlbumPath" Type="String" />
                    <asp:Parameter Name="AlbumCreatTime" Type="DateTime" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
        </div>
        <br />
        <asp:Button ID="FrontBtn" runat="server" Text="回到前台" OnClick="FrontBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
