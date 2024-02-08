using Microsoft.Data.SqlClient;

namespace GlobalImpact.Utils
{
    public class RegisterVerifications
    {

        public static bool EmailExists(string email)
        {
            SqlConnection conn = new SqlConnection();
            conn.Open();
            string sql = "SELECT * FROM dbo.AspNetUsers WHERE Email = '" + email + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
        public static bool UserNameExists(string username)
        {
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-UNF0AQ95;Initial Catalog=test;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            conn.Open();
            string sql = "SELECT * FROM dbo.AspNetUsers WHERE UserName = '" + username + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
    }
}
