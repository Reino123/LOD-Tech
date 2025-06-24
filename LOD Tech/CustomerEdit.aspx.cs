using System;
using System.Data;
using System.Data.SqlClient;
public partial class CustomerEdit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                litTitle.Text = "Edit Customer";
                LoadCustomer();
            }
            else
            {
                litTitle.Text = "Add Customer";
            }
        }
    }

    private void LoadCustomer()
    {
        int customerId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT * FROM Customers WHERE CustomerID = @CustomerID";
        DataTable dt = DbHelper.ExecuteSelect(query, new SqlParameter("@CustomerID", customerId));
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
            int customerId = int.Parse(Request.QueryString["id"]);
            string query = "UPDATE Customers SET Name=@Name, Email=@Email, Phone=@Phone, Address=@Address WHERE CustomerID=@CustomerID";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address),
                new SqlParameter("@CustomerID", customerId)
            );
        }
        else
        {
            // Insert
            string query = "INSERT INTO Customers (Name, Email, Phone, Address) VALUES (@Name, @Email, @Phone, @Address)";
            DbHelper.ExecuteNonQuery(query,
                new SqlParameter("@Name", name),
                new SqlParameter("@Email", email),
                new SqlParameter("@Phone", phone),
                new SqlParameter("@Address", address)
            );
        }

        Response.Redirect("Customers.aspx");
    }
}