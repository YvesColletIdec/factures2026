using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        public string Nom { get; set; }
    }
}
