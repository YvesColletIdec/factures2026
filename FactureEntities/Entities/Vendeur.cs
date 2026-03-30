using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Vendeur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public string Identifiant { get; set; } = null!;

    public virtual ICollection<Facture> Factures { get; set; } = new List<Facture>();
}
