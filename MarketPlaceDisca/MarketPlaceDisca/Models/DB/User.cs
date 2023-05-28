using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class User
{
    public string IdUser { get; set; } = null!;

    public string NameUser { get; set; } = null!;

    public string LastNameUser { get; set; } = null!;

    public string? Address { get; set; }

    public string? Telephone { get; set; }

    public string Email { get; set; } = null!;

    public string TypeDocument { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? Photo { get; set; }

    public string? CoverPhoto { get; set; }

    public DateOnly BirthDate { get; set; }

    public virtual ICollection<Credential> Credentials { get; set; } = new List<Credential>();

    public virtual ICollection<CredentialsLog> CredentialsLogs { get; set; } = new List<CredentialsLog>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<UserHasService> UserHasServices { get; set; } = new List<UserHasService>();
}
