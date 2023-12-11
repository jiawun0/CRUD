<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoCategoryBack.aspx.cs" Inherits="CRUD.VideoCategoryBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_VideoCategory" runat="server" Text="影片分類系統"></asp:Label>
            <br />
            <asp:Label ID="Label_CategoryName" runat="server" Text="分類名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_CategoryName" runat="server" Placeholder="請輸入分類名稱" ></asp:TextBox>
            <br />
            <asp:Button ID="CreateVideoCategoryBtn" runat="server" Text="新增影片分類" OnClick="CreateVideoCategoryBtn_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:TemplateField HeaderText="CategoryName" SortExpression="CategoryName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateCN" runat="server" Text='<%# Bind("CategoryName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateCN" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CategoryCreatTime" HeaderText="CategoryCreatTime" SortExpression="CategoryCreatTime" ReadOnly="True" />
                    <asp:TemplateField HeaderText="VedioBack">
            <ItemTemplate>
                <asp:Button ID="BtnRedirectPhotoBack" runat="server" Text="影片後台" CommandArgument='<%# Eval("Id") %>' CommandName="RedirectToPhotoBack" OnClick="BtnRedirect_Click" />
            </ItemTemplate>
        </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="CategoryName" Type="String" />
                    <asp:Parameter Name="CategoryCreatTime" Type="DateTime" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CategoryName" Type="String" />
                    <asp:Parameter Name="CategoryCreatTime" Type="DateTime" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
