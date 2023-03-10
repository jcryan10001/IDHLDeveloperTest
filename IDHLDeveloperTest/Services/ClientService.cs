using IDHLDeveloperTest.Models;
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

        public async Task<CharacterList> GetCharacters(int pageNumber)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.disneyapi.dev/characters?page={pageNumber}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CharacterList>(content);
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
