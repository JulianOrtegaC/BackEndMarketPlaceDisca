using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class ServicesContract
{
    public int IdservicesContract { get; set; }

    public DateOnly DateInit { get; set; }

    public DateOnly DateEnd { get; set; }

    public string Price { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
