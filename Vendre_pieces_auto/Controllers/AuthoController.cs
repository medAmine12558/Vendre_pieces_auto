using Microsoft.AspNetCore.Mvc;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

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
        [Authorize]
        public async Task<IActionResult> Logout()// C’est la déclaration de la méthode. Elle est publique, donc accessible en dehors de la classe. async signifie qu’elle est asynchrone, c’est-à-dire qu’elle peut être exécutée en parallèle avec d’autres tâches. Task<IActionResult> est le type de retour, qui indique qu’elle renvoie une tâche qui, lorsqu’elle est terminée, donnera un IActionResult, qui est une interface pour les types de résultats d’action.
        {
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme);//Cette ligne déconnecte l’utilisateur de la session Auth0. "HttpContext" est une propriété qui fournit des informations sur la requête HTTP en cours. "SignOutAsync" est une méthode qui termine la session de l’utilisateur pour le schéma d’authentification donné, dans ce cas "Auth0Constants.AuthenticationScheme".
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);//Cette ligne déconnecte l’utilisateur de la session d’authentification par cookies. C’est similaire à la ligne précédente, mais pour le schéma d’authentification par défaut des cookies.
            return RedirectToAction("InterfaceUser", "User_Interface");//Enfin, cette ligne redirige l’utilisateur vers l’action "InterfaceUser" du contrôleur "User_Interface" après la déconnexion.
        }

    }
}
