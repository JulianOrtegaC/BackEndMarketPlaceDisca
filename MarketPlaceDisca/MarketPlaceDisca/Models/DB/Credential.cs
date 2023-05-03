using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MarketPlaceDisca.Models.DB;

public partial class Credential
{
    public int IdCredentials { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int UserUserTypeIduserType { get; set; }

    public string UserIdUser { get; set; } = null!;

    [JsonIgnore]
    public virtual User UserIdUserNavigation { get; set; } = null!;
}
