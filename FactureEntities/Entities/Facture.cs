using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Facture
{
    public int Id { get; set; }

    public DateOnly DateFacture { get; set; }

    public string Numero { get; set; } = null!;

    public int ClientId { get; set; }

    public int VendeurId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<LigneFacture> LigneFactures { get; set; } = new List<LigneFacture>();

    public virtual Vendeur Vendeur { get; set; } = null!;
}
