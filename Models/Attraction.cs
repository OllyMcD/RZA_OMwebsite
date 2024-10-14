using System;
using System.Collections.Generic;

namespace RZA_OMwebsite.Models;

public partial class Attraction
{
    public int AttractionId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
