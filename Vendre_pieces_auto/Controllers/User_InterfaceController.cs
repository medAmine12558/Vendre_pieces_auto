using Auth0.ManagementApi;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;
using Vendre_pieces_auto.Service;

namespace Vendre_pieces_auto.Controllers
{
    public class User_InterfaceController : Controller
    {
        private readonly Context _context; // Ajoutez une variable privée pour stocker le contexte
        private readonly IAccessTocken _accessTocken; //fffffff
        private readonly IConfiguration _configuration;
        public User_InterfaceController(Context context, IAccessTocken accessToken, IConfiguration configuration)
        {
            _context = context; // Injectez le contexte dans le constructeur
            _accessTocken = accessToken;
            _configuration = configuration;
        }
        public IActionResult Checkpoint()
        {
            string userid=HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cont=_context.Controleur.FirstOrDefault(x => x.Id.Equals(userid));
            if (cont != null)
            {
                var piece = _context.Piece.Where(x => x.is_valide==false);
                    
                return View("~/Views/Controller_Iterface/InterfaceController.cshtml", piece);
            }
            else
            {
                return RedirectToAction("InterfaceUser", "User_Interface");
            }
        }

        public IActionResult InterfaceUser()
        {
            //var pieces=_context.Piece.ToList();
            string userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cont = _context.Controleur.FirstOrDefault(x => x.Id.Equals(userid));
            if (cont != null)
            {
                var piece = _context.Piece.Where(x => x.is_valide == false);

                return View("~/Views/Controller_Iterface/InterfaceController.cshtml", piece);
            }
            var pieces = _context.Piece.Include(p => p.Photos).Where(x=>x.is_valide==true).ToList();
            String pictureUrl = null;
            if (User.Identity.IsAuthenticated)
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;//recuperer le uid d'utilsateur connecter
                pictureUrl=HttpContext.User.FindFirst("picture")?.Value;


                if (_context.Controleur.Any(c => c.Id == userId)) {//verifier est ce que le id de l'utilisateur connecter est ce qu'il est enregistrer dans la base de donnees des controlleur , c'est une methode pour que mon systeme soit capable de savoir automatiquement si le user connecter est un controlleur ou non
                    Console.WriteLine("le id est trouve");
                }
                else
                {
                    Console.WriteLine("le id n'est pas trouve");
                }
            }
            return View(new { pieces = pieces , pictureUrl = pictureUrl });
        }
        public IActionResult Ajouter_Piece()//cette methode verifie lorsque le user veux ajouter une piece est ce qu'il est authentifie ou non
        {
            if(User.Identity.IsAuthenticated)//si user authentifie je vais le rederiger vers la page d'ajout de piece
            return View();
            else// si non je vais le rederiger vers la page d'autentifier 
            {
                return RedirectToAction("Login_Piece", "Autho");//ici on a appeler le controleur "AuthoController" avec la methode "Login_Piece" qui va lui meme appeler le processus d'auth
            }
        }
        [HttpGet]
        public IActionResult CategorieSelected(string categorie)
        {
            var pictureUrl = HttpContext.User.FindFirst("picture")?.Value;
            var cat=_context.Piece.Include(x => x.Photos).Where(p => p.Type_name.Equals(categorie));//recuperer toutes les produits ayant le meme categorie passe dans le url
            ViewBag.SelectedCategorie = categorie;
            return View("~/Views/User_Interface/InterfaceUser.cshtml", new { pieces = cat, pictureUrl = pictureUrl });
        }
        public async Task<IActionResult> PageProfile()
        {
            var email=HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var idUser= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var picture = HttpContext.User.FindFirst("picture")?.Value;
            var managementApiClient = new ManagementApiClient(await _accessTocken.GetManagementApiAccessToken(), new Uri($"https://{_configuration["Auth0:Domain"]}/api/v2/"));
            var user = await managementApiClient.Users.GetAsync(idUser);
            var userFName = user.UserMetadata.first_name;
            var UserLName = user.UserMetadata.last_name;
            Console.WriteLine(UserLName);
            var UserAdresse = user.UserMetadata.Adresse;
            var UserTele=user.UserMetadata.Telephone;
            return View("~/Views/User_Interface/InterfaceProfile.cshtml",new {email=email,picture=picture, nom = userFName , prenom = UserLName , adresse= UserAdresse ,tele= UserTele });

        }
        /*private async Task<string> GetManagementApiAccessToken()
        {
            // Création d'un nouveau client HTTP pour envoyer la requête
            var client = new HttpClient();

            // Préparation du corps de la requête avec les informations nécessaires pour obtenir le token
            var requestBody = new Dictionary<string, string>
    {
        {"grant_type", "client_credentials"}, // Le type d'autorisation, ici 'client_credentials' pour une application
        {"client_id", _configuration["Auth0:ClientId"]}, // L'ID client fourni par Auth0
        {"client_secret", _configuration["Auth0:ClientSecret"]}, // Le secret client fourni par Auth0
        {"audience", $"https://{_configuration["Auth0:Domain"]}/api/v2/"}, // L'audience, qui est l'URL de l'API de gestion Auth0
        {"scope", "read:users"} // Les permissions demandées, ici 'update:users' pour mettre à jour les utilisateurs
    };

            // Encodage des informations de la requête en tant que contenu de formulaire URL encodé
            var requestContent = new FormUrlEncodedContent(requestBody);

            // Envoi de la requête POST au point de terminaison '/oauth/token' d'Auth0 pour obtenir le token
            var response = await client.PostAsync($"https://{_configuration["Auth0:Domain"]}/oauth/token", requestContent);

            // Vérification si la réponse est réussie (code de statut HTTP 200-299)
            if (response.IsSuccessStatusCode)
            {
                // Lecture du contenu de la réponse en tant que chaîne
                var responseContent = await response.Content.ReadAsStringAsync();
                // Désérialisation du contenu JSON de la réponse en dictionnaire
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                // Récupération du token d'accès à partir du dictionnaire et retour du token
                return tokenResponse["access_token"];
            }

            // Si la réponse n'est pas réussie, lecture et journalisation du contenu de l'erreur
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erreur lors de la récupération du token d'accès : {errorContent}");
            // Lancement d'une exception avec le message d'erreur
            throw new ApplicationException("Unable to retrieve access token for management API.");
        }*/
    }
    
}
