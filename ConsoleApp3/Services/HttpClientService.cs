using ConsoleApp3.Domain;
using ConsoleApp3.Interface;
namespace ConsoleApp3.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://identity.acesso.io");
        }

        public async Task<BearerToken> GenerateToken(string assertion)
        {
            BearerToken? bearerToken = new();

            var content = new { grant_type = "urn:ietf:params:oauth:grant-type:jwt-bearer", assertion = assertion };

            var body = new Dictionary<string, string>
            {
                { "grant_type", content.grant_type },
                { "assertion", content.assertion }
            };

            var requestBody = new FormUrlEncodedContent(body);

            HttpRequestMessage resquesMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress}oauth2/token"),
                Content = requestBody
            };

            try
            {
                using (var responseRequest = await _httpClient.SendAsync(resquesMessage))
                {
                    if (responseRequest.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var result = await responseRequest.Content.ReadAsStringAsync();

                        if(result is not null)
                        {
                            bearerToken = System.Text.Json.JsonSerializer.Deserialize<BearerToken>(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bearerToken;
        }
    }
}
