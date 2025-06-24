<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="Products" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Products</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Products</h2>
            <a href="ProductEdit.aspx" class="btn btn-primary mb-3">Add Product</a>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="ProductID" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:CommandField ShowEditButton="True" EditText="Edit" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>