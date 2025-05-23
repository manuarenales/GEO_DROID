
using GeoDroid.Data.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GEO_DROID.Services
{
    public class GeolineOdataService
    {

        private HttpClient _httpClient;
        private TokenService _tokenService;

        public GeolineOdataService()
        {
            _tokenService = new TokenService();
            var handler = new HttpClientHandler();
            _httpClient = new HttpClient(handler);
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
        }

        public async Task<RecaudacionEstablecimientoDTO> GetEstablecimientoByCodigoAsync(string codigo)
        {
            string accessToken = await _tokenService.GetGeolineTokenAsync();
            try
            {
                string ruta = $"https://172.16.1.69:5055/odata/ODataRecaudacionEstablecimiento?$filter=codigoEstablecimiento eq '{codigo}'";
                // Agregar token al encabezado de autorización
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                HttpResponseMessage response = await _httpClient.GetAsync(ruta);
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    var contenido = await response.Content.ReadAsStringAsync();

                    if (contenido.Length > 2)
                    {
                        RecaudacionEstablecimientoDTO establecimiento = JsonSerializer.Deserialize<List<RecaudacionEstablecimientoDTO>>(contenido, options)[0];
                        return establecimiento;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
