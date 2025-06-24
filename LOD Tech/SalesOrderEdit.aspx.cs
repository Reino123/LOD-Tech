using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


public partial class SalesOrderEdit : System.Web.UI.Page
{
    private DataTable OrderDetails
    {
        get
        {
            if (ViewState["OrderDetails"] == null)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductID", typeof(int));
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("UnitPrice", typeof(decimal));
                ViewState["OrderDetails"] = dt;
            }
            return (DataTable)ViewState["OrderDetails"];
        }
        set
        {
            ViewState["OrderDetails"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            litTitle.Text = Request.QueryString["id"] != null ? "Edit Sales Order" : "Add Sales Order";
            LoadCustomers();
            LoadProducts();
            txtOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            if (Request.QueryString["id"] != null)
            {
                LoadOrder();
            }
            else
            {
                BindOrderDetails();
            }
            gvOrderDetails.RowDataBound += gvOrderDetails_RowDataBound;
        }
    }

    private void LoadCustomers()
    {
        string query = "SELECT CustomerID, Name FROM Customers";
        DataTable dt = DbHelper.ExecuteSelect(query);
        ddlCustomer.DataSource = dt;
        ddlCustomer.DataTextField = "Name";
        ddlCustomer.DataValueField = "CustomerID";
        ddlCustomer.DataBind();
        ddlCustomer.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Customer --", ""));
    }

    private void LoadProducts()
    {
        string query = "SELECT ProductID, Name FROM Products";
        DataTable dt = DbHelper.ExecuteSelect(query);
        ViewState["Products"] = dt;
    }

    private void BindOrderDetails()
    {
        gvOrderDetails.DataSource = OrderDetails;
        gvOrderDetails.DataBind();
        decimal total = 0;
        foreach (DataRow row in OrderDetails.Rows)
        {
            total += Convert.ToInt32(row["Quantity"]) * Convert.ToDecimal(row["UnitPrice"]);
        }
        lblTotalAmount.Text = total.ToString("C");
    }

    protected void gvOrderDetails_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            var footer = gvOrderDetails.FooterRow;
            var ddlProduct = (System.Web.UI.WebControls.DropDownList)footer.FindControl("ddlProduct");
            var txtQuantity = (System.Web.UI.WebControls.TextBox)footer.FindControl("txtQuantity");
            var txtUnitPrice = (System.Web.UI.WebControls.TextBox)footer.FindControl("txtUnitPrice");

            int productId;
            int quantity;
            decimal unitPrice;

            if (int.TryParse(ddlProduct.SelectedValue, out productId) &&
                int.TryParse(txtQuantity.Text, out quantity) &&
                decimal.TryParse(txtUnitPrice.Text, out unitPrice))
            {
                DataTable dt = OrderDetails;
                DataRow row = dt.NewRow();
                row["ProductID"] = productId;
                row["ProductName"] = ddlProduct.SelectedItem.Text;
                row["Quantity"] = quantity;
                row["UnitPrice"] = unitPrice;
                dt.Rows.Add(row);
                OrderDetails = dt;
                BindOrderDetails();
            }
        }
    }

    protected void gvOrderDetails_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        DataTable dt = OrderDetails;
        dt.Rows.RemoveAt(e.RowIndex);
        OrderDetails = dt;
        BindOrderDetails();
    }

    protected void gvOrderDetails_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Footer)
        {
            var ddlProduct = (System.Web.UI.WebControls.DropDownList)e.Row.FindControl("ddlProduct");
            if (ddlProduct != null)
            {
                DataTable products = ViewState["Products"] as DataTable;
                ddlProduct.DataSource = products;
                ddlProduct.DataTextField = "Name";
                ddlProduct.DataValueField = "ProductID";
                ddlProduct.DataBind();
            }
        }
    }

    private void LoadOrder()
    {
        int orderId = int.Parse(Request.QueryString["id"]);
        // Load order master
        string query = "SELECT * FROM SalesOrders WHERE SalesOrderID = @SalesOrderID";
        DataTable dt = DbHelper.ExecuteSelect(query, new SqlParameter("@SalesOrderID", orderId));
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            ddlCustomer.SelectedValue = row["CustomerID"].ToString();
            txtOrderDate.Text = Convert.ToDateTime(row["OrderDate"]).ToString("yyyy-MM-dd");
        }

        // Load order details
        string detailsQuery = @"
            SELECT sod.ProductID, p.Name AS ProductName, sod.Quantity, sod.UnitPrice
            FROM SalesOrderDetails sod
            INNER JOIN Products p ON sod.ProductID = p.ProductID
            WHERE sod.SalesOrderID = @SalesOrderID";
        DataTable details = DbHelper.ExecuteSelect(detailsQuery, new SqlParameter("@SalesOrderID", orderId));
        OrderDetails = details;
        BindOrderDetails();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlCustomer.SelectedValue == "" || OrderDetails.Rows.Count == 0)
        {
            // Validation
            return;
        }

        int customerId = int.Parse(ddlCustomer.SelectedValue);
        DateTime orderDate = DateTime.Parse(txtOrderDate.Text);
        decimal totalAmount = 0;
        foreach (DataRow row in OrderDetails.Rows)
        {
            totalAmount += Convert.ToInt32(row["Quantity"]) * Convert.ToDecimal(row["UnitPrice"]);
        }

        if (Request.QueryString["id"] != null)
        {
            // Update order
            int orderId = int.Parse(Request.QueryString["id"]);
            string updateOrder = "UPDATE SalesOrders SET CustomerID=@CustomerID, OrderDate=@OrderDate, TotalAmount=@TotalAmount WHERE SalesOrderID=@SalesOrderID";
            DbHelper.ExecuteNonQuery(updateOrder,
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@OrderDate", orderDate),
                new SqlParameter("@TotalAmount", totalAmount),
                new SqlParameter("@SalesOrderID", orderId)
            );

            // Delete old details
            string deleteDetails = "DELETE FROM SalesOrderDetails WHERE SalesOrderID=@SalesOrderID";
            DbHelper.ExecuteNonQuery(deleteDetails, new SqlParameter("@SalesOrderID", orderId));

            // Insert new details
            foreach (DataRow row in OrderDetails.Rows)
            {
                string insertDetail = "INSERT INTO SalesOrderDetails (SalesOrderID, ProductID, Quantity, UnitPrice) VALUES (@SalesOrderID, @ProductID, @Quantity, @UnitPrice)";
                DbHelper.ExecuteNonQuery(insertDetail,
                    new SqlParameter("@SalesOrderID", orderId),
                    new SqlParameter("@ProductID", row["ProductID"]),
                    new SqlParameter("@Quantity", row["Quantity"]),
                    new SqlParameter("@UnitPrice", row["UnitPrice"])
                );
            }
        }
        else
        {
            // Insert order
            string insertOrder = "INSERT INTO SalesOrders (CustomerID, OrderDate, TotalAmount) VALUES (@CustomerID, @OrderDate, @TotalAmount); SELECT SCOPE_IDENTITY();";
            object result = DbHelper.ExecuteScalar(insertOrder,
                new SqlParameter("@CustomerID", customerId),
                new SqlParameter("@OrderDate", orderDate),
                new SqlParameter("@TotalAmount", totalAmount)
            );
            int orderId = Convert.ToInt32(result);

            // Insert details
            foreach (DataRow row in OrderDetails.Rows)
            {
                string insertDetail = "INSERT INTO SalesOrderDetails (SalesOrderID, ProductID, Quantity, UnitPrice) VALUES (@SalesOrderID, @ProductID, @Quantity, @UnitPrice)";
                DbHelper.ExecuteNonQuery(insertDetail,
                    new SqlParameter("@SalesOrderID", orderId),
                    new SqlParameter("@ProductID", row["ProductID"]),
                    new SqlParameter("@Quantity", row["Quantity"]),
                    new SqlParameter("@UnitPrice", row["UnitPrice"])
                );
            }
        }

        Response.Redirect("SalesOrders.aspx");
    }
}

