using System;
using System.Data;

    public partial class Customers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCustomers();
            }
        }

        private void LoadCustomers()
        {
            string query = "SELECT * FROM Customers";
            DataTable dt = DbHelper.ExecuteSelect(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            int customerId = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("CustomerEdit.aspx?id=" + customerId);
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int customerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string query = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
            DbHelper.ExecuteNonQuery(query, new System.Data.SqlClient.SqlParameter("@CustomerID", customerId));
            LoadCustomers();
        }
    }
