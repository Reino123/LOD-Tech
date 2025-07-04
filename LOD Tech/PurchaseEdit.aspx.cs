﻿using System;
using System.Data;
using System.Data.SqlClient;


    public partial class PurchaseEdit : System.Web.UI.Page
    {
        private DataTable PurchaseDetails
        {
            get
            {
                if (ViewState["PurchaseDetails"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductID", typeof(int));
                    dt.Columns.Add("ProductName", typeof(string));
                    dt.Columns.Add("Quantity", typeof(int));
                    dt.Columns.Add("UnitPrice", typeof(decimal));
                    dt.Rows.Add(dt.NewRow());
                    //dt.Rows.Add(1, "test", 1, 1);
                    ViewState["PurchaseDetails"] = dt;
                }
                return (DataTable)ViewState["PurchaseDetails"];
            }
            set
            {
                ViewState["PurchaseDetails"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvPurchaseDetails.RowDataBound += gvPurchaseDetails_RowDataBound;
            if (!IsPostBack)
            {
                litTitle.Text = Request.QueryString["id"] != null ? "Edit Purchase" : "Add Purchase";
                LoadSuppliers();
                LoadProducts();
                txtPurchaseDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                if (Request.QueryString["id"] != null)
                {
                    LoadPurchase();
                }
                else
                {
                    BindPurchaseDetails();
                }
            }
        }

        private void LoadSuppliers()
        {
            string query = "SELECT SupplierID, Name FROM Suppliers";
            DataTable dt = DbHelper.ExecuteSelect(query);
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataTextField = "Name";
            ddlSupplier.DataValueField = "SupplierID";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Supplier --", ""));
        }

    private void LoadProducts()
    {
        string query = "SELECT ProductID, Name, Price, Quantity FROM Products";
        DataTable dt = DbHelper.ExecuteSelect(query);
        ViewState["Products"] = dt;
        System.Diagnostics.Debug.WriteLine("Products loaded: " + dt.Rows.Count);
    }

    private void BindPurchaseDetails()
        {
            gvPurchaseDetails.DataSource = PurchaseDetails;
            gvPurchaseDetails.DataBind();
            decimal total = 0;
            foreach (DataRow row in PurchaseDetails.Rows)
            {
                try
                {
                    total += Convert.ToInt32(row["Quantity"]) * Convert.ToDecimal(row["UnitPrice"]);
                } catch
                {
                }
        }   
            lblTotalAmount.Text = total.ToString("C");
        }

        protected void gvPurchaseDetails_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                var footer = gvPurchaseDetails.FooterRow;
                var ddlProduct = (System.Web.UI.WebControls.DropDownList)footer.FindControl("ddlProduct");
                var txtQuantity = (System.Web.UI.WebControls.TextBox)footer.FindControl("txtQuantity");
                var txtPrice = (System.Web.UI.WebControls.TextBox)footer.FindControl("txtUnitPrice");

                int productId;
                int quantity;
                decimal Price;

                if (int.TryParse(ddlProduct.SelectedValue, out productId) &&
                    int.TryParse(txtQuantity.Text, out quantity) &&
                    decimal.TryParse(txtPrice.Text, out Price))
                {
                    DataTable dt = PurchaseDetails;
                    DataRow row = dt.NewRow();
                    row["ProductID"] = productId;
                    row["ProductName"] = ddlProduct.SelectedItem.Text;
                    row["Quantity"] = quantity;
                    row["UnitPrice"] = Price;
                    dt.Rows.Add(row);
                    PurchaseDetails = dt;
                    BindPurchaseDetails();
                }
            }
        }

        protected void gvPurchaseDetails_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            DataTable dt = PurchaseDetails;
            dt.Rows.RemoveAt(e.RowIndex);
            PurchaseDetails = dt;
            BindPurchaseDetails();
        }

        protected void gvPurchaseDetails_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
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

        private void LoadPurchase()
        {
            int purchaseId = int.Parse(Request.QueryString["id"]);
            // Load purchase master
            string query = "SELECT * FROM Purchases WHERE PurchaseID = @PurchaseID";
            DataTable dt = DbHelper.ExecuteSelect(query, new SqlParameter("@PurchaseID", purchaseId));
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ddlSupplier.SelectedValue = row["SupplierID"].ToString();
                txtPurchaseDate.Text = Convert.ToDateTime(row["PurchaseDate"]).ToString("yyyy-MM-dd");
            }

            // Load purchase details
            string detailsQuery = @"
            SELECT pd.ProductID, p.Name AS ProductName, pd.Quantity, pd.UnitPrice
            FROM PurchaseDetails pd
            INNER JOIN Products p ON pd.ProductID = p.ProductID
            WHERE pd.PurchaseID = @PurchaseID";
            DataTable details = DbHelper.ExecuteSelect(detailsQuery, new SqlParameter("@PurchaseID", purchaseId));
            // Add 'Price' column and copy values from 'UnitPrice'
            //if (!details.Columns.Contains("Price"))
            //    details.Columns.Add("Price", typeof(decimal));
            //foreach (DataRow r in details.Rows)
            //{
            //    r["Price"] = r["UnitPrice"];
            //}
            PurchaseDetails = details;
            BindPurchaseDetails();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlSupplier.SelectedValue == "" || PurchaseDetails.Rows.Count == 0)
            {
                // Validation
                return;
            }

            int supplierId = int.Parse(ddlSupplier.SelectedValue);
            DateTime purchaseDate = DateTime.Parse(txtPurchaseDate.Text);
            decimal totalAmount = 0;
            foreach (DataRow row in PurchaseDetails.Rows)
            {
                if (row["Quantity"] != DBNull.Value && row["UnitPrice"] != DBNull.Value)
                {
                    totalAmount += Convert.ToInt32(row["Quantity"]) * Convert.ToDecimal(row["UnitPrice"]);
                }
            }

            if (Request.QueryString["id"] != null)
            {
                // Update purchase
                int purchaseId = int.Parse(Request.QueryString["id"]);
                string updatePurchase = "UPDATE Purchases SET SupplierID=@SupplierID, PurchaseDate=@PurchaseDate, TotalAmount=@TotalAmount WHERE PurchaseID=@PurchaseID";
                DbHelper.ExecuteNonQuery(updatePurchase,
                    new SqlParameter("@SupplierID", supplierId),
                    new SqlParameter("@PurchaseDate", purchaseDate),
                    new SqlParameter("@TotalAmount", totalAmount),
                    new SqlParameter("@PurchaseID", purchaseId)
                );

                // Delete old details
                string deleteDetails = "DELETE FROM PurchaseDetails WHERE PurchaseID=@PurchaseID";
                DbHelper.ExecuteNonQuery(deleteDetails, new SqlParameter("@PurchaseID", purchaseId));

                // Insert new details
                foreach (DataRow row in PurchaseDetails.Rows)
                {
                    string insertDetail = "INSERT INTO PurchaseDetails (PurchaseID, ProductID, Quantity, Price) VALUES (@PurchaseID, @ProductID, @Quantity, @Price)";
                    DbHelper.ExecuteNonQuery(insertDetail,
                        new SqlParameter("@PurchaseID", purchaseId),
                        new SqlParameter("@ProductID", row["ProductID"]),
                        new SqlParameter("@Quantity", row["Quantity"]),
                        new SqlParameter("@Price", row["UnitPrice"])
                    );
                }
            }
            else
            {
                // Insert purchase
                string insertPurchase = "INSERT INTO Purchases (SupplierID, PurchaseDate, TotalAmount) VALUES (@SupplierID, @PurchaseDate, @TotalAmount); SELECT SCOPE_IDENTITY();";
                object result = DbHelper.ExecuteScalar(insertPurchase,
                    new SqlParameter("@SupplierID", supplierId),
                    new SqlParameter("@PurchaseDate", purchaseDate),
                    new SqlParameter("@TotalAmount", totalAmount)
                );
                int purchaseId = Convert.ToInt32(result);

                // Insert details
                foreach (DataRow row in PurchaseDetails.Rows)
                {
                    string insertDetail = "INSERT INTO PurchaseDetails (PurchaseID, ProductID, Quantity, UnitPrice) VALUES (@PurchaseID, @ProductID, @Quantity, @UnitPrice)";
                    DbHelper.ExecuteNonQuery(insertDetail,
                        new SqlParameter("@PurchaseID", purchaseId),
                        new SqlParameter("@ProductID", row["ProductID"]),
                        new SqlParameter("@Quantity", row["Quantity"]),
                        new SqlParameter("@UnitPrice", row["UnitPrice"])
                    );
                }
            }

            Response.Redirect("Purchases.aspx");
        }
    }

