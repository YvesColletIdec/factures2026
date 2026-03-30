using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Article
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Prix { get; set; }

    public virtual ICollection<LigneFacture> LigneFactures { get; set; } = new List<LigneFacture>();
}
