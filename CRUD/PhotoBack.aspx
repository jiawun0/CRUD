<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhotoBack.aspx.cs" Inherits="CRUD.PhotoBack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label_PhotoUpload" runat="server" Text="相片上傳系統"></asp:Label>
            <br />
            <asp:Label ID="Label_PhotoName" runat="server" Text="相片名稱: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_PhotoName" runat="server" Placeholder="請輸入相片名稱" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_PhotoDescription" runat="server" Text="相片描述: "></asp:Label>
            <br />
            <asp:TextBox ID="TextBox_PhotoDescription" runat="server" Placeholder="請輸入相片描述" ></asp:TextBox>
            <br />
            <asp:Label ID="Label_PhotoSelect" runat="server" Text="選取相片: "></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:Button ID="UploadBtn" runat="server" Text="上傳" OnClick="UploadBtn_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" ></asp:Label>
            <br />
            <asp:GridView ID="GridView_PhotoUpload" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnRowCancelingEdit="GridView_PhotoUpload_RowCancelingEdit" OnRowDeleting="GridView_PhotoUpload_RowDeleting" OnRowEditing="GridView_PhotoUpload_RowEditing" OnRowUpdating="GridView_PhotoUpload_RowUpdating" >
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:TemplateField HeaderText="PhotoName" SortExpression="PhotoName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplatePN" runat="server" Text='<%# Bind("PhotoName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplatePN" runat="server" Text='<%# Bind("PhotoName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PhotoDescription" SortExpression="PhotoDescription">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox_TemplatePD" runat="server" Text='<%# Bind("PhotoDescription") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label_TemplatePD" runat="server" Text='<%# Bind("PhotoDescription") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PhotoPath" HeaderText="PhotoPath" SortExpression="PhotoPath" ReadOnly="True" />
                    <asp:TemplateField>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" Width="100" Height="100" ImageUrl='<%# GetRelativeImagePath(Eval("PhotoPath").ToString()) %>' AlternateText="image lost" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IsCover" SortExpression="IsCover">
                        <EditItemTemplate>
                            <asp:CheckBox ID="CheckBox_TemplateIC" runat="server" Checked='<%# Bind("IsCover") %>' />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox_TemplateIC" runat="server" Checked='<%# Bind("IsCover") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="PhotoName" Type="String" />
                    <asp:Parameter Name="PhotoDescription" Type="String" />
                    <asp:Parameter Name="PhotoPath" Type="String" />
                    <asp:Parameter Name="PhotoCreatTime" Type="DateTime" />
                    <asp:Parameter Name="IsCover" Type="Boolean" />
                    <asp:Parameter Name="AlbumId" Type="Int32" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="PhotoName" Type="String" />
                    <asp:Parameter Name="PhotoDescription" Type="String" />
                    <asp:Parameter Name="PhotoPath" Type="String" />
                    <asp:Parameter Name="PhotoCreatTime" Type="DateTime" />
                    <asp:Parameter Name="IsCover" Type="Boolean" />
                    <asp:Parameter Name="AlbumId" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
        </div>
        <asp:Button ID="FrontBtn" runat="server" Text="回到相片前台" OnClick="FrontBtn_Click" style="height: 32px" />
        <br />
        <asp:Button ID="AlbumBack" runat="server" Text="返回相簿後台" OnClick="BackBtn_Click" style="height: 32px" />
    </form>
</body>
</html>
