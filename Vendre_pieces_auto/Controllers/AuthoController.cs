using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Vendre_pieces_auto.Controllers
{
    public class AuthoController : Controller
    {
        public async Task Login(string returnUrl = "/") //la fonction qui gere l'auth externe (avec google ou facbook) et l'auth avec email et password
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder() //Cette ligne crée une nouvelle instance de LoginAuthenticationPropertiesBuilder, un constructeur pour construire des propriétés d'authentification pour l'authentification externe. Ces propriétés permettent de configurer le comportement de l'authentification.
                .WithRedirectUri(returnUrl) //définit l'URL de redirection après l'authentification. Elle prend comme argument l'URL returnUrl fournie en paramètre de la méthode Login.
                .Build(); // Cette méthode Build finalise la construction des propriétés d'authentification et retourne l'objet authenticationProperties configuré.

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);//Cette ligne lance le processus d'authentification en utilisant le schéma d'authentification spécifié (dans ce cas, Auth0Constants.AuthenticationScheme). L'appel à ChallengeAsync demande à ASP.NET Core de lancer un flux d'authentification externe. Les propriétés d'authentification configurées sont passées en tant que paramètre.
        }
    }
}
