<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseEdit.aspx.cs" Inherits="PurchaseEdit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit Purchase</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <a href="Purchases.aspx" class="btn btn-secondary mb-3">
                ← Back to Purchases
            </a>
            <h2><asp:Literal ID="litTitle" runat="server" /></h2>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="text-danger" />
            <div class="mb-3">
                <label>Supplier</label>
                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvSupplier" runat="server" ControlToValidate="ddlSupplier" InitialValue="" ErrorMessage="Supplier is required." CssClass="text-danger" />
            </div>
            <div class="mb-3">
                <label>Purchase Date</label>
                <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="form-control" />
                <asp:RequiredFieldValidator ID="rfvPurchaseDate" runat="server" ControlToValidate="txtPurchaseDate" ErrorMessage="Purchase date is required." CssClass="text-danger" />
            </div>
            <h4>Purchase Details</h4>
            <asp:GridView ID="gvPurchaseDetails" ShowHeaderWhenEmpty="true" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-bordered"
                OnRowCommand="gvPurchaseDetails_RowCommand" OnRowDeleting="gvPurchaseDetails_RowDeleting">
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
            <asp:Button ID="btnSave" runat="server" Text="Save Purchase" CssClass="btn btn-success" OnClick="btnSave_Click" />
            <a href="Purchases.aspx" class="btn btn-secondary">Cancel</a>
        </div>
        <asp:Image ID="Image1" runat="server" />
    </form>
</body>
</html>