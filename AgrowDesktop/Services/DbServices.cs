using AgrowDesktop.Models;
using MySql.Data.MySqlClient;

namespace AgrowDesktop.Services
{
    class DbService
    {
        string connString = "server=yamanote.proxy.rlwy.net;" + "port=46245;" + "user=root;" + "password=bIcWYVezsEKKMJjTjNNGvvyPWsSKQmWI;" + "database=railway;";

        //CheckNIC For CreateAcc
        public bool CheckNic(string nic)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string sql = "SELECT COUNT(*) FROM AddAdmin WHERE nic=@nic";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nic", nic);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        //CreateAcc
        //WeNot Use Model ThenADD: public bool CreateAdmin(string nic, string username, string password, string email)
        //WeUseAdminModel:V
        public bool CreateAdmin(AdminModel admin)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string sql = @"INSERT INTO AdminAcc (nic, username, password, email)
                   VALUES (@nic, @username, @password, @email)";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nic", admin.Nic);
            cmd.Parameters.AddWithValue("@username", admin.Username);
            cmd.Parameters.AddWithValue("@password", admin.Password);
            cmd.Parameters.AddWithValue("@email", admin.Email);

            return cmd.ExecuteNonQuery() > 0;
        }

        //CheckNIC For ForgotPass
        public bool CheckAdminAccNic(string nic)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string sql = "SELECT COUNT(*) FROM AdminAcc WHERE nic=@nic";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nic", nic);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        //UpdatePass When ClickForgotPass
        public bool ResetPassword(string nic, string newPassword)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string sql = @"UPDATE AdminAcc 
                   SET password=@password 
                   WHERE nic=@nic";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nic", nic);
            cmd.Parameters.AddWithValue("@password", newPassword);

            return cmd.ExecuteNonQuery() > 0;
        }

        //Login
        public AdminModel? LoginAdmin(string username, string password)
        {
            using var conn = new MySqlConnection(connString);
            conn.Open();

            string sql = @"SELECT * FROM AdminAcc
                   WHERE username=@username
                   AND password=@password";

            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new AdminModel
                {
                    Nic = reader["nic"].ToString()!,
                    Username = reader["username"].ToString()!,
                    Password = reader["password"].ToString()!,
                    Email = reader["email"].ToString()!
                };
            }

            return null;
        }

    }
}
