using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Vendre_pieces_auto.Controllers.classe;
using Vendre_pieces_auto.Service;

namespace Vendre_pieces_auto.Controllers
    
{
    [Authorize]
    [Route("api/user/metadata")]
    [ApiController]
    public class UpdateUserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccessTocken _accesstocken;
        public UpdateUserController(IConfiguration configuration, IAccessTocken accessTocken)
        {
            _configuration = configuration;
            _accesstocken = accessTocken;
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
            var managementApiAccessToken = await _accesstocken.GetManagementApiAccessToken();

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


       



    }
}
