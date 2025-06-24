<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Purchases.aspx.cs" Inherits="Purchases" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Purchases</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Purchases</h2>
            <a href="PurchaseEdit.aspx" class="btn btn-primary mb-3">Add Purchase</a>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="PurchaseID" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="PurchaseID" HeaderText="Purchase ID" ReadOnly="True" />
                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" />
                    <asp:BoundField DataField="PurchaseDate" HeaderText="Purchase Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                    <asp:CommandField ShowEditButton="True" EditText="Edit" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>