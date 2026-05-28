using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class LigneFacture
{
    public decimal Montant
    {
        get
        {
            return this.Quantite * this.PrixUnitaire;
        }
    }
}
