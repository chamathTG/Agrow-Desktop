using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgrowDesktop.Models;

public class OrderModel
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = "";

    public string FarmerName { get; set; } = "";

    public string ProductTitle { get; set; } = "";

    public int Qty { get; set; }

    public double Total { get; set; }

    public string Status { get; set; } = "";
}
