using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Suppliers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSuppliers();
        }
    }

    private void LoadSuppliers()
    {
        string connStr = ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = "SELECT SupplierID, Name, Email, Phone, Address FROM Suppliers";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridViewSuppliers.DataSource = dt;
                    GridViewSuppliers.DataBind();
                }
            }
        }
    }
} 