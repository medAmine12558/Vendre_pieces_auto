using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Vendre_pieces_auto.Controllers.classe;

namespace Vendre_pieces_auto.Controllers
    
{
    [Authorize]
    [Route("api/user/metadata")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UpdateUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
       
        [HttpPost]
        public async Task<IActionResult> UpdateUserMetadata([FromForm] UserMetadataModel model )
        {
            
            // Récupération de l'ID utilisateur à partir des claims du contexte HTTP actuel
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Vérification si l'ID utilisateur est null ou vide, auquel cas retourner non autorisé
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            // Obtention du token d'accès à l'API de gestion Auth0
            var managementApiAccessToken = await GetManagementApiAccessToken();

            // Création d'un nouveau client HTTP et ajout du token d'accès au header d'autorisation du http request
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", managementApiAccessToken);

            // Préparation de la requête de mise à jour avec les métadonnées utilisateur
            var updateRequest = new Dictionary<string, object>
             {
                {"user_metadata", new Dictionary<string, object>()}
             };

            // Ajout des métadonnées utilisateur à la requête si elles ne sont pas vides
            if (!string.IsNullOrEmpty(model.Nom))
            {
                ((Dictionary<string, object>)updateRequest["user_metadata"]).Add("first_name", model.Nom);
            }
            if (!string.IsNullOrEmpty(model.Prenom))
            {
                ((Dictionary<string, object>)updateRequest["user_metadata"]).Add("last_name", model.Prenom);
            }
            if (!string.IsNullOrEmpty(model.Adresse))
            {
                ((Dictionary<string, object>)updateRequest["user_metadata"]).Add("Adresse", model.Adresse);
            }
            if (!string.IsNullOrEmpty(model.Tele))
            {
                ((Dictionary<string, object>)updateRequest["user_metadata"]).Add("Telephone", model.Tele);
            }

            // Envoi de la requête PATCH à l'API de gestion Auth0 pour mettre à jour les métadonnées et la methode StringContent c'est elle qui construit le corp du requette avec un objet json
            var response = await client.PatchAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/users/{Uri.EscapeDataString(userId)}",
                new StringContent(JsonConvert.SerializeObject(updateRequest), Encoding.UTF8, "application/json"));

            // Vérification si la réponse est réussie
            if (response.IsSuccessStatusCode)
            {
                // Si réussie, retourner une réponse appropriée (à déterminer)
                return Ok(new { success = true }); // Remplacer 'null' par une réponse appropriée
            }
            else
            {
                // Si échec, lecture et journalisation du contenu de l'erreur
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erreur lors de la mise à jour des métadonnées de l'utilisateur : {errorContent}");

                // Redirection vers une autre action en cas d'échec
                return Ok(new { success = false });
            }
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
        {"scope", "update:users"} // Les permissions demandées, ici 'update:users' pour mettre à jour les utilisateurs
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
