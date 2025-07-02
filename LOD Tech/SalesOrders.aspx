<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesOrders.aspx.cs" Inherits="SalesOrders" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Orders</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <a href="Default.aspx" class="btn btn-secondary mb-3">
                &larr; Back to ERP
            </a>
            <h2>Sales Orders</h2>
            <a href="SalesOrderEdit.aspx" class="btn btn-primary mb-3">Add Sales Order</a>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="SalesOrderID" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="SalesOrderID" HeaderText="Order ID" ReadOnly="True" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:C}" />
                    <asp:CommandField ShowEditButton="True" EditText="Edit" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>