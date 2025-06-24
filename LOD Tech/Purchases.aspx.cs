using System;
using System.Data;
using System.Data.SqlClient;


public partial class Purchases : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPurchases();
        }
    }

    private void LoadPurchases()
    {
        string query = @"
            SELECT p.PurchaseID, s.Name AS SupplierName, p.PurchaseDate, p.TotalAmount
            FROM Purchases p
            INNER JOIN Suppliers s ON p.SupplierID = s.SupplierID";
        DataTable dt = DbHelper.ExecuteSelect(query);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        int purchaseId = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
        Response.Redirect("PurchaseEdit.aspx?id=" + purchaseId);
    }

    protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        int purchaseId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

        // Delete details first due to FK constraint
        string deleteDetails = "DELETE FROM PurchaseDetails WHERE PurchaseID = @PurchaseID";
        DbHelper.ExecuteNonQuery(deleteDetails, new SqlParameter("@PurchaseID", purchaseId));

        // Delete purchase
        string deletePurchase = "DELETE FROM Purchases WHERE PurchaseID = @PurchaseID";
        DbHelper.ExecuteNonQuery(deletePurchase, new SqlParameter("@PurchaseID", purchaseId));

        LoadPurchases();
    }
}

