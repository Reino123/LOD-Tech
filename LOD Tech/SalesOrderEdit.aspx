<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesOrderEdit.aspx.cs" Inherits="SalesOrderEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit Sales Order</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <a href="SalesOrders.aspx" class="btn btn-secondary mb-3">
                &larr; Back to Sales Orders
            </a>
            <h2><asp:Literal ID="litTitle" runat="server" /></h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
            <div class="mb-3">
                <label>Customer</label>
                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer" InitialValue="" ErrorMessage="Customer is required." CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <label>Order Date</label>
                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvOrderDate" runat="server" ControlToValidate="txtOrderDate" ErrorMessage="Order date is required." CssClass="text-danger" />
            </div>
            <h4>Order Details</h4>
            <asp:GridView ID="gvOrderDetails" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-bordered"
                OnRowCommand="gvOrderDetails_RowCommand" OnRowDeleting="gvOrderDetails_RowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                            <%# Eval("ProductName") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <%# Eval("Quantity") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Width="80px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit Price">
                        <ItemTemplate>
                            <%# Eval("UnitPrice", "{0:C}") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger btn-sm" Text="Delete" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="btnAdd" runat="server" CommandName="Add" CssClass="btn btn-success btn-sm" Text="Add" />
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div class="mb-3">
                <label>Total Amount:</label>
                <asp:Label ID="lblTotalAmount" runat="server" CssClass="fw-bold" />
            </div>
            <asp:Button ID="btnSave" runat="server" Text="Save Order" CssClass="btn btn-success" OnClick="btnSave_Click" />
            <a href="SalesOrders.aspx" class="btn btn-secondary">Cancel</a>
        </div>
    </form>
</body>
</html>