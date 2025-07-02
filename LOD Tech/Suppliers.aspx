<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="Suppliers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Suppliers</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <a href="Default.aspx" class="btn btn-secondary mb-3">
                &larr; Back to ERP
            </a>
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2>Suppliers</h2>
                <a href="SupplierEdit.aspx" class="btn btn-primary">Add Supplier</a>
            </div>
            <asp:GridView ID="GridViewSuppliers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="SupplierID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href='<%# "SupplierEdit.aspx?id=" + Eval("SupplierID") %>' class="btn btn-sm btn-warning">Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>