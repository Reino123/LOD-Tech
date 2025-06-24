<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="Customers" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <titlCustomers</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <h2>Customers</h2>
            <a href="CustomerEdit.aspx" class="btn btn-primary mb-3">Add Customer</a>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered"
                DataKeyNames="CustomerID" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="CustomerID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="Address" HeaderText="Address" />
                    <asp:CommandField ShowEditButton="True" EditText="Edit" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" />
                </Columns>
            </asp:GridView>
        </div>
        <p>
            &nbsp;</p>
    </form>
</body>
</html>