using System;
using System.Collections.Generic;

namespace RZA_OMwebsite.Models;

public partial class Animal
{
    public int AnimalId { get; set; }

    public string IndividualName { get; set; } = null!;

    public string Species { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Sex { get; set; } = null!;

    public virtual ICollection<Animallocation> Animallocations { get; set; } = new List<Animallocation>();

    public virtual ICollection<Matchanimal> MatchanimalAnimalFemales { get; set; } = new List<Matchanimal>();

    public virtual ICollection<Matchanimal> MatchanimalAnimalMales { get; set; } = new List<Matchanimal>();
}
