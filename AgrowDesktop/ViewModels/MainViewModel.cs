using AgrowDesktop.Models;
using AgrowDesktop.Services;

namespace AgrowDesktop.ViewModels
{
    public class LoginViewModel
    {
        DbService db = new DbService();

        public AdminModel? Login(string username, string password)
        {
            return db.LoginAdmin(username, password);
        }
    }
}
