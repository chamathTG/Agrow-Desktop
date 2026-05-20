using AgrowDesktop.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgrowDesktop.ViewModels
{
    public class ForgotPassViewModel
    {
        DbService db = new DbService();

        public string ResetPassword(string nic, string password)
        {
            if (!db.CheckAdminAccNic(nic))
                return "NIC not found!";

            bool ok = db.ResetPassword(nic, password);

            return ok
                ? "Password reset successfully"
                : "Failed to reset password";
        }
    }
}
