using System;
using System.Collections.Generic;

namespace RZA_OMwebsite.Models;

public partial class Animallocation
{
    public int AnimalId { get; set; }

    public string ZooName { get; set; } = null!;

    public DateOnly DateArrived { get; set; }

    public DateOnly? DateLeft { get; set; }

    public virtual Animal Animal { get; set; } = null!;

    public virtual Zoo ZooNameNavigation { get; set; } = null!;
}
