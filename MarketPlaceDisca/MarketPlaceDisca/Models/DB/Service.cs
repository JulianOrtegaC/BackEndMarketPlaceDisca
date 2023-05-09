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

    public string Address { get; set; } = null!;

    public string? DatesDispo { get; set; }

    public string Categoria { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<ServiceHasCategory> ServiceHasCategories { get; set; } = new List<ServiceHasCategory>();

    public virtual ICollection<User> UserIdUsers { get; set; } = new List<User>();
}
