using AgrowDesktop.Models;
using AgrowDesktop.Services;

namespace AgrowDesktop.ViewModels
{
    public class CreateAccViewModel
    {
        DbService db = new DbService();

        public string SignUp(string nic, string username, string password, string email)
        {
            if (!db.CheckNic(nic))
                return "Not authorized. Please contact agrow!";

            AdminModel admin = new AdminModel()
            {
                Nic = nic,
                Username = username,
                Password = password,
                Email = email
            };

            bool ok = db.CreateAdmin(admin);

            return ok
                ? "Account created successfully!"
                : "Failed to create account";
        }
    }
}
