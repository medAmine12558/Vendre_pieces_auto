using Newtonsoft.Json;

namespace Vendre_pieces_auto.Service
{
    public class AccessToken : IAccessTocken
    {
        private readonly IConfiguration _configuration;
        public  AccessToken(IConfiguration configuration)
        {
            this._configuration = configuration;
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
