<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LinkCategoryBack.aspx.cs" Inherits="CRUD.LinkCategoryBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_LinkCategory" runat="server" Text="連結分類系統後台"></asp:Label>
            <br />
            <asp:Label ID="Label_LinkCategoryName" runat="server" Text="分類名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_LinkCategoryName" runat="server" Placeholder="請輸入分類名稱" ></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="CreateLinkCategoryBtn" runat="server" Text="新增連結分類" OnClick="CreateLinkCategoryBtn_Click" />
            <br />
            
            <asp:GridView ID="GridView_LinkCategoryBack" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_LinkCategoryBack_RowCancelingEdit" OnRowDeleting="GridView_LinkCategoryBack_RowDeleting" OnRowEditing="GridView_LinkCategoryBack_RowEditing" OnRowUpdating="GridView_LinkCategoryBack_RowUpdating" >
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
                    <asp:TemplateField HeaderText="LinkBack">
            <ItemTemplate>
                <asp:Button ID="BtnRedirectLinkBack" runat="server" Text="連結後台" CommandArgument='<%# Eval("Id") %>' CommandName="RedirectToLinkBack" />
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
        <asp:Button ID="FrontBtn" runat="server" Text="回到前台" OnClick="FrontBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
