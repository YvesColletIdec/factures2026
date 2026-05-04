using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string Prenom { get; set; }

    public string Adresse { get; set; }

    public string Npa { get; set; }

    public string Localite { get; set; }

    public string Nom { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
