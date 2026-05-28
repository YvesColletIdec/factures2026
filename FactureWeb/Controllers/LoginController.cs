using FactureEntities.Entities;
using Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

public class LoginController : Controller
{
    private readonly SqlServerContext _context;
    public LoginController(SqlServerContext context)
    {
        _context = context;
    }
    public IActionResult Login()
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        else
        {
#if DEBUG
            return Connexion("admin", "1234");
#else
    return View("Login");
#endif
        }
    }

    [HttpPost]
    public IActionResult Connexion(string utilisateur, string motdepasse)
    {
        if (HttpContext.User.Identity.IsAuthenticated)
            return Redirect("~/Home/Index");

        if (string.IsNullOrEmpty(utilisateur) || string.IsNullOrEmpty(motdepasse))
        {
            TempData["ko"] = "Veuillez saisir un utilisateur et un mot de passe";
            return Redirect("~/Login/Login");
        }

        Vendeur u = _context.Vendeurs.FirstOrDefault(ux => ux.Identifiant == utilisateur);
        if (u != null)
        {
            //if (motdepasse != u.MotDePasse)
            //bool ok = Security.Verify(mdp, mdpChiffre);
            if (!Security.Verify(motdepasse, u.MotDePasse))
            {
                u = null;
            }
        }

        if (u == null)
        {
            TempData["ko"] = "Echec de lors de la connexion";
            return Redirect("~/Login/Login");
        }
        string claimRole = u.Role;

        var userClaims = new[] {
                        new Claim("Login", utilisateur),
                        new Claim("Role", claimRole) ,
                        new Claim(ClaimTypes.Name, utilisateur),//pour authorize
                        new Claim(ClaimTypes.Role, claimRole) ,//pour authorize
                        new Claim("Id", Convert.ToString(u.Id))
                    };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, "custom");
        //--> il faut laisser custom pour que HttpContext.User.IsAuthenticated soit à True

        ClaimsPrincipal userPrincipal = new ClaimsPrincipal(new[] { claimsIdentity });
        HttpContext.User = userPrincipal;
        HttpContext.SignInAsync(userPrincipal);

        HttpContext.Session.SetString("id", Convert.ToString(u.Id));
        HttpContext.Session.SetString("userName", utilisateur);
        HttpContext.Session.SetString("role", u.Role);
        TempData["ok"] = $"Bienvenue {utilisateur}";
        return Redirect("/Home/Index");
    }

    [Authorize]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();

        return Redirect("~/Login/Login");
    }
}
