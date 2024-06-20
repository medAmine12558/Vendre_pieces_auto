using Auth0.ManagementApi;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO.Pipelines;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Vendre_pieces_auto.Controllers.classe;
using Vendre_pieces_auto.Data;
using Vendre_pieces_auto.Models.Tabels;
using Vendre_pieces_auto.Service;

namespace Vendre_pieces_auto.Controllers
{
    public class CommandeController : Controller
    {
        private readonly Context _context;
        
        private readonly IConfiguration _configuration;

        public CommandeController(Context context, IConfiguration configuration)
        {
            this._context = context;
           
            this._configuration = configuration;
        }
        public IActionResult ajouter_comm()
        {
            List<int> pieces_id=new List<int>();
            List<int> quatite=new List<int>();
            Commande comm = null;
            List<Piece> panier = HttpContext.Session.Get<List<Piece>>("panier");
            foreach(var p in panier)
            {
                pieces_id.Add(p.Id_piece);
                quatite.Add(p.Quantite_stock);
            }
            foreach (Piece piece in panier) {
                comm = new Commande
                {
                    Id_Vendeur = piece.Id_Vendeur,
                    Id_Acheteur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                };
                _context.Commande.Add(comm);
                _context.SaveChanges();
               
                var f = new Facture
                {
                    Id_Vendeur = piece.Id_Vendeur,
                    Id_Acheteur = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    Id_Comm = comm.Id
                };
                _context.Facture.Add(f);
            }
            var commander = new Commander
            {
                Commande_id = comm.Id,
                Piece_id = pieces_id,

                quantite = quatite
            };
            _context.Commander.Add(commander);
            _context.SaveChanges();
            HttpContext.Session.Set<int>("Commande_id", comm.Id);
            Console.WriteLine("le id du comm est : "+HttpContext.Session.Get<int>("Commande_id"));
            return Json(new { success = true });
        }
        public async  Task<IActionResult> Afficher_facture()
        {
            //float prix = 0;
            //List<string> nom_piece = new List<string>();
            var idUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var managementApiClient = new ManagementApiClient(await GetManagementApiAccessToken(), new Uri($"https://{_configuration["Auth0:Domain"]}/api/v2/"));
            var user = await managementApiClient.Users.GetAsync(idUser);
            var userFName = user.UserMetadata.first_name;
            var UserLName = user.UserMetadata.last_name;
            float prixtot = 0;
            foreach (var p in HttpContext.Session.Get<List<Piece_Session>>("panier"))
            {
                prixtot += p.prixtotal;
            }

            //Commander pieces_id = _context.Commander.FirstOrDefault(x => x.Id == HttpContext.Session.Get<int>("Commande_id"));
            //List<int> quantite = pieces_id.quantite;
            /*foreach( var piece in pieces_id.Piece_id ) {
                Piece p = _context.Piece.FirstOrDefault(x => x.Id_piece == piece);
                nom_piece.Add(p.Nom_piece);
                prix = prix + p.prix;
            }*/

            return View("~/Views/FactureInterface/Facture.cshtml", new {nom= userFName , prenom= UserLName ,piece=HttpContext.Session.Get<List<Piece_Session>>("panier"), prixtot = prixtot });
        }
        public async Task<string> GetManagementApiAccessToken()
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
        }
    }
}
