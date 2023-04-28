using System;
using System.Collections.Generic;

namespace MarketPlaceDisca.Models.DB;

public partial class Municipio
{
    public int Idmunicipios { get; set; }

    public string Nombremunicipio { get; set; } = null!;

    public int Codigodepartamento { get; set; }
}
