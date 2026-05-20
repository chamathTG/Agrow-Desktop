using AgrowDesktop.Models;
using AgrowDesktop.Services;
using System;
using System.Collections.Generic;
using System.Text;

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
