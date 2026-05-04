using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Vendeur
{
    public int Id { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }

    public string MotDePasse { get; set; }

    public string Identifiant { get; set; }

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
