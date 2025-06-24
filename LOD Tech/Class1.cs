using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public static class DbHelper
{
    private static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["ERPConnectionString"].ConnectionString;
        }
    }

    public static DataTable ExecuteSelect(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }

    public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }

    public static object ExecuteScalar(string query, params SqlParameter[] parameters)
    {
        using (SqlConnection conn = new SqlConnection(ConnectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            conn.Open();
            return cmd.ExecuteScalar();
        }
    }
}