using System;
using System.Collections.Generic;

namespace QLXE_WEB.Models;

public partial class Product
{
    public int IdP { get; set; }

    public string ProcductName { get; set; } = null!;

    public float Price { get; set; }

    public string Description { get; set; } = null!;

    public int? IdU { get; set; }

    public virtual User? IdUNavigation { get; set; }
}
