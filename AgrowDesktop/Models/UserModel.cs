using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrowDesktop.Models;

public class UserModel
{
    public int Id { get; set; }

    public string Role { get; set; } = "";

    public string Username { get; set; } = "";

    public string Mobile { get; set; } = "";

    public bool IsBlocked { get; set; }
}
