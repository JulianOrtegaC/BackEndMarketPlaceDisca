using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class Request
{
    public int ServiceIdService { get; set; }

    public string UserIdUser { get; set; } = null!;

    public int ServicesContractIdservicesContract { get; set; }

    public string Status { get; set; } = null!;

    public virtual Service ServiceIdServiceNavigation { get; set; } = null!;

    public virtual ServicesContract ServicesContractIdservicesContractNavigation { get; set; } = null!;

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
