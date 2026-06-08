using System.ComponentModel.DataAnnotations;

namespace FactureWeb.Models
{
    public class Pwd
    {
        public string OldPassword { get; set; }
        [RegularExpression(
        @"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$",
        ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères, une majuscule, un chiffre et un caractère spécial."
    )]
        public string NewPassword { get; set; }
    }
}
