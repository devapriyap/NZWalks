using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public interface ITokenHandler
    {
       Task<string> CreateTokenAsync(User user);
    }
}
