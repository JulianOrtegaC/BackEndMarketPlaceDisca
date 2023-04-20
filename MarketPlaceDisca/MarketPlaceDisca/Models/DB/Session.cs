using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class Session
{
    public int IdSession { get; set; }

    public DateOnly SessionStarted { get; set; }

    public DateOnly SessionEnded { get; set; }

    public int Status { get; set; }

    public string Token { get; set; } = null!;

    public int UserUserTypeIduserType { get; set; }

    public string UserIdUser { get; set; } = null!;

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
