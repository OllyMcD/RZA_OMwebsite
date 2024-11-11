using System;
using System.Collections.Generic;

namespace RZA_OMwebsite.Models;

public partial class Zoo
{
    public string ZooName { get; set; } = null!;

    public string Town { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<Animallocation> Animallocations { get; set; } = new List<Animallocation>();
}
