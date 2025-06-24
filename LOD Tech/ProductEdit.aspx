<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="ProductEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit Product</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2><asp:Literal ID="litTitle" runat="server" /></h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
            <div class="mb-3">
                <label>Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <label>Description</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Price is required." CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revPrice" runat="server" ControlToValidate="txtPrice" ErrorMessage="Invalid price." ValidationExpression="^\d+(\.\d{1,2})?$" CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <label>Quantity</label>
                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Quantity is required." CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revQuantity" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Invalid quantity." ValidationExpression="^\d+$" CssClass="text-danger" />
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
            <a href="Products.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </form>
</body>
</html>