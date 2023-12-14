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
            <asp:Label ID="Label_LinkThumbnail" runat="server" Text="選取縮圖: "></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload_LinkT" runat="server" />
            <br />
            <asp:Button ID="UploadBtn" runat="server" Text="上傳" OnClick="UploadBtn_Click" />
            <br />
            <br />
            <asp:GridView ID="GridView_LinkUpload" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_LinkUpload_RowCancelingEdit" OnRowDeleting="GridView_LinkUpload_RowDeleting" OnRowEditing="GridView_LinkUpload_RowEditing" OnRowUpdating="GridView_LinkUpload_RowUpdating" AllowSorting="True" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:TemplateField HeaderText="LinkName" SortExpression="LinkName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateLN" runat="server" Text='<%# Bind("LinkName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateLN" runat="server" Text='<%# Bind("LinkName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LinkDescription" SortExpression="LinkDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplateLD" runat="server" Text='<%# Bind("LinkDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplateLD" runat="server" Text='<%# Bind("LinkDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LinkURL" HeaderText="LinkURL" SortExpression="LinkURL" ReadOnly="True" />
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image_LinkThumbnail" runat="server" Width="100" Height="100" ImageUrl='<%# GetRelativeImagePath(Eval("LinkThumbnail").ToString()) %>' AlternateText="image lost" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectLB %>" DeleteCommand="DELETE FROM [Link] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Link] ([LinkName], [LinkDescription], [LinkURL], [LinkThumbnail], [LinkCreatTime], [CategoryID]) VALUES (@LinkName, @LinkDescription, @LinkURL, @LinkThumbnail, @LinkCreatTime, @CategoryID)" SelectCommand="SELECT * FROM [Link]" UpdateCommand="UPDATE [Link] SET [LinkName] = @LinkName, [LinkDescription] = @LinkDescription, [LinkURL] = @LinkURL, [LinkThumbnail] = @LinkThumbnail, [LinkCreatTime] = @LinkCreatTime, [CategoryID] = @CategoryID WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="LinkName" Type="String" />
                    <asp:Parameter Name="LinkDescription" Type="String" />
                    <asp:Parameter Name="LinkURL" Type="String" />
                    <asp:Parameter Name="LinkThumbnail" Type="String" />
                    <asp:Parameter Name="LinkCreatTime" Type="DateTime" />
                    <asp:Parameter Name="CategoryID" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="LinkName" Type="String" />
                    <asp:Parameter Name="LinkDescription" Type="String" />
                    <asp:Parameter Name="LinkURL" Type="String" />
                    <asp:Parameter Name="LinkThumbnail" Type="String" />
                    <asp:Parameter Name="LinkCreatTime" Type="DateTime" />
                    <asp:Parameter Name="CategoryID" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
        <asp:Button ID="FrontBtn" runat="server" Text="回到連結前台" OnClick="FrontBtn_Click" style="height: 32px" />
        <br />
        <asp:Button ID="LinkCBack" runat="server" Text="返回連結種類後台" OnClick="LinkCBack_Click" style="height: 32px" />
    </form>
</body>
</html>
