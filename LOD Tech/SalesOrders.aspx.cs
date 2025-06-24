using System;
using System.Data;
using System.Data.SqlClient;


public partial class SalesOrders : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSalesOrders();
        }
    }

    private void LoadSalesOrders()
    {
        string query = @"
        SELECT so.SalesOrderID, c.Name AS CustomerName, so.OrderDate, so.TotalAmount
        FROM SalesOrders so
        INNER JOIN Customers c ON so.CustomerID = c.CustomerID";
        DataTable dt = DbHelper.ExecuteSelect(query);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        int salesOrderId = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        Response.Redirect("SalesOrderEdit.aspx?id=" + salesOrderId);
    }

    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int salesOrderId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        // Delete details first due to FK constraint
        string deleteDetails = "DELETE FROM SalesOrderDetails WHERE SalesOrderID = @SalesOrderID";
        DbHelper.ExecuteNonQuery(deleteDetails, new SqlParameter("@SalesOrderID", salesOrderId));

        // Delete order
        string deleteOrder = "DELETE FROM SalesOrders WHERE SalesOrderID = @SalesOrderID";
        DbHelper.ExecuteNonQuery(deleteOrder, new SqlParameter("@SalesOrderID", salesOrderId));

        LoadSalesOrders();
    }
}
