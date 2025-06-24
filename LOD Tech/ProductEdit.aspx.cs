using System;
using System.Data;
using System.Data.SqlClient;

public partial class ProductEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                litTitle.Text = "Edit Product";
                LoadProduct();
            }
            else
            {
                litTitle.Text = "Add Product";
            }
        }
    }

    private void LoadProduct()
    {
        int productId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT * FROM Products WHERE ProductID = @ProductID";
        DataTable dt = DbHelper.ExecuteSelect(query, new SqlParameter("@ProductID", productId));
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            txtName.Text = row["Name"].ToString();
            txtDescription.Text = row["Description"].ToString();
            txtPrice.Text = row["Price"].ToString();
            txtQuantity.Text = row["Quantity"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string description = txtDescription.Text.Trim();
        decimal price = decimal.Parse(txtPrice.Text.Trim());
        int quantity = int.Parse(txtQuantity.Text.Trim());

        if (Request.QueryString["id"] != null)
        {
            // Update
            int productId = int.Parse(Request.QueryString["id"]);
            string query = "UPDATE Products SET Name=@Name, Description=@Description, Price=@Price, Quantity=@Quantity WHERE ProductID=@ProductID";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Description", description),
                new SqlParameter("@Price", price),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@ProductID", productId)
            );
        }
        else
        {
            // Insert
            string query = "INSERT INTO Products (Name, Description, Price, Quantity) VALUES (@Name, @Description, @Price, @Quantity)";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Description", description),
                new SqlParameter("@Price", price),
                new SqlParameter("@Quantity", quantity)
            );
        }

        Response.Redirect("Products.aspx");
    }
}

