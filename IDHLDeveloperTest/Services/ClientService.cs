using IDHLDeveloperTest.Models;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace IDHLDeveloperTest.Services
{
    public class ClientService : IClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<(CharacterList characterList, HttpStatusCode statusCode)> GetCharacters(int pageNumber, int batchSize)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.disneyapi.dev/characters?page={pageNumber}&pageSize={batchSize}");

            if (!response.IsSuccessStatusCode)
            {
                return (null, response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var characterList = JsonSerializer.Deserialize<CharacterList>(content);

            return (characterList, response.StatusCode);
        }
        public async Task<Character> GetCharacter(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.disneyapi.dev/characters/{id}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Character>(content);
        }
    }
}
