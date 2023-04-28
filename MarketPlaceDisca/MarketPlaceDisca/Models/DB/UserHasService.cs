using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class UserHasService
{
    public string UserIdUser { get; set; } = null!;

    public int ServiceIdService { get; set; }

    public string? PathPhotos { get; set; }

    public virtual Service ServiceIdServiceNavigation { get; set; } = null!;

    public virtual User UserIdUserNavigation { get; set; } = null!;
}
