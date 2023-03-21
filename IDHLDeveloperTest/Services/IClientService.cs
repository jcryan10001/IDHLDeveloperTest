using IDHLDeveloperTest.Models;
using System.Net;
using System.Threading.Tasks;

namespace IDHLDeveloperTest.Services
{
    public interface IClientService
    {
        Task<(CharacterList characterList, HttpStatusCode statusCode)> GetCharacters(int pageNumber, int batchSize);
        Task<Character> GetCharacter(int id);
    }
}
