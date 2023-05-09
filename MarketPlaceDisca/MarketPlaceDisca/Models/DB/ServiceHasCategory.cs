using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class ServiceHasCategory
{
    public int ServiceIdService { get; set; }

    public int CategoryIdcategory { get; set; }

    public virtual Service ServiceIdServiceNavigation { get; set; } = null!;
}
