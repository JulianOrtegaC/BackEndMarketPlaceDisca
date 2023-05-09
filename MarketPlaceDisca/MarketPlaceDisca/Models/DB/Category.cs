using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class Category
{
    public int Idcategory { get; set; }

    public string NameCategory { get; set; } = null!;

    public string Description { get; set; } = null!;
}
