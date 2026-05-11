using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class VFacture
{
    public DateOnly DateFacture { get; set; }

    public int Id { get; set; }

    public decimal PrixUnitaire { get; set; }

    public int Quantite { get; set; }

    public string Nom { get; set; }

    public string Prenom { get; set; }
}
