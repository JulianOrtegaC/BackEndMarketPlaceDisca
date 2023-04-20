using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class CredentialsLog
{
    public int IdCredentialsLog { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string IpUpdater { get; set; } = null!;

    public string Date { get; set; } = null!;

    public string UserIdUser { get; set; } = null!;

    public int UserUserTypeIduserType { get; set; }

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
