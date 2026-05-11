using System;
using System.Collections.Generic;

namespace FactureEntities.Entities;

public partial class Facture
{
    public decimal TotalFacture
    {
        get
        {
            decimal totalFacture = 0;
            foreach(LigneFacture lf in this.LigneFactures)
            {
                totalFacture += lf.Quantite * lf.PrixUnitaire;
            }
            return totalFacture;
        }
    }

}
