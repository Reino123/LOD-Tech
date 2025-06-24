using System;
using System.Data;


    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        private void LoadProducts()
        {
            string query = "SELECT * FROM Products";
            DataTable dt = DbHelper.ExecuteSelect(query);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            int productId = Convert.ToInt32(GridView1.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("ProductEdit.aspx?id=" + productId);
        }

        protected void GridView1_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            int productId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string query = "DELETE FROM Products WHERE ProductID = @ProductID";
            DbHelper.ExecuteNonQuery(query, new System.Data.SqlClient.SqlParameter("@ProductID", productId));
            LoadProducts();
        }
    }

