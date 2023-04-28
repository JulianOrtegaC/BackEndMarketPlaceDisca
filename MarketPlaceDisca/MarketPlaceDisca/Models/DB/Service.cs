using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class Service
{
    public int IdService { get; set; }

    public string NameService { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double InitialPrice { get; set; }

    public string? PathPhotos { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<UserHasService> UserHasServices { get; set; } = new List<UserHasService>();

    public virtual ICollection<Category> CategoryIdcategories { get; set; } = new List<Category>();
}
