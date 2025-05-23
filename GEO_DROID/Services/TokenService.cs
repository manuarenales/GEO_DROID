using GeoDroid.Data.GEOLINE;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;


namespace GEO_DROID.Services
{
    public class TokenService
    {
        private readonly HttpClient _httpClient;
        public TokenService()
        {
            var handler = new HttpClientHandler();
            _httpClient = new HttpClient(handler);
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };

        }
        public async Task<string> GetGeolineTokenAsync()
        {
            string serverbase = await SecureStorage.GetAsync("BaseServer");
            string usernameGeo = await SecureStorage.GetAsync("usernameGeo");
            string passwordeGeo = await SecureStorage.GetAsync("passwordeGeo");

            serverbase = "https://172.16.1.69:5055";
            string ruta = "/api/TokenAuth";

            var userCredentials = new
            {
                UserName = "superuser",
                Password = "Superuser1234#"
            };

            var content = new StringContent(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync(serverbase + ruta, content);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var resp = await response.Content.ReadAsStringAsync();
                GeolineToken result = System.Text.Json.JsonSerializer.Deserialize<GeolineToken>(resp, options);
                await SecureStorage.SetAsync("GeolineAccesToken", result.Data.AccessToken);
                return result.Data.AccessToken;
            }
            else
            {
                return "";
            }
        }
    }
}