using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Prenom { get; set; } = null!;

    public string Adresse { get; set; } = null!;

    public string Npa { get; set; } = null!;

    public string Localite { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
