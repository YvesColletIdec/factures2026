using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FactureEntities.Entities
{
    [ModelMetadataTypeAttribute(typeof(ArticleMetaData))]
    public partial class Article
    {
        public string AfficherPrix
        {
            get
            {
                return $"{this.Prix} CHF";
            }
        }
    }

    public partial class ArticleMetaData
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [DisplayName("Nom de l'article")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "La description est obligatoire")]
        [DisplayName("Description de l'article")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0.1, 100000, ErrorMessage = "La valeur doit se trouver entre {0} et {1}.")]
        [DisplayName("Prix de l'article")]
        public decimal Prix { get; set; }

        [DisplayName("L'article est-il actif ou non")]
        public decimal Actif { get; set; }
    }
}
