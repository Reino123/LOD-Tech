using System;
using System.Data;
using System.Data.SqlClient;

//namespace LOD_Tech
//{
public partial class SupplierEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                litTitle.Text = "Edit Supplier";
                LoadSupplier();
            }
            else
            {
                litTitle.Text = "Add Supplier";
            }
        }
    }

    private void LoadSupplier()
    {
        int supplierId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT * FROM Suppliers WHERE SupplierID = @SupplierID";
        DataTable dt = DbHelper.ExecuteSelect(query, new SqlParameter("@SupplierID", supplierId));
        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            txtName.Text = row["Name"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtPhone.Text = row["Phone"].ToString();
            txtAddress.Text = row["Address"].ToString();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string name = txtName.Text.Trim();
        string email = txtEmail.Text.Trim();
        string phone = txtPhone.Text.Trim();
        string address = txtAddress.Text.Trim();

        if (Request.QueryString["id"] != null)
        {
            // Update
            int supplierId = int.Parse(Request.QueryString["id"]);
            string query = "UPDATE Suppliers SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address WHERE SupplierID=@SupplierID";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address),
                new SqlParameter("@SupplierID", supplierId)
            );
        }
        else
        {
            // Insert
            string query = "INSERT INTO Suppliers (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address)
            );
        }

        Response.Redirect("Suppliers.aspx");
    }
}
//}