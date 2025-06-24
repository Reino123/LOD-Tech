<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierEdit.aspx.cs" Inherits="SupplierEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit Supplier</title>
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
                <label>Email</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Invalid email." ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <label>Phone</label>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label>Address</label>
                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" />
            <a href="Suppliers.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </form>
</body>
</html>
