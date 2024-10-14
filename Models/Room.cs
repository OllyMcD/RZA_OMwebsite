using System;
using System.Collections.Generic;

namespace RZA_OMwebsite.Models;

public partial class Room
{
    public int RoomNumber { get; set; }

    public int? Capacity { get; set; }

    public string? RoomType { get; set; }

    public virtual ICollection<Roombooking> Roombookings { get; set; } = new List<Roombooking>();
}
