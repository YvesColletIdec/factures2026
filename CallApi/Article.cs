using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Article
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Description { get; set; }

    public decimal Prix { get; set; }

    public bool Actif { get; set; }

    public virtual ICollection<dynamic> LigneFactures { get; set; } = new List<dynamic>();
}
