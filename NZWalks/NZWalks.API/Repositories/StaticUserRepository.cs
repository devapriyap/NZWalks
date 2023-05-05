using Microsoft.AspNetCore.Authorization.Infrastructure;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {
        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Read Only", LastName ="User", EmailAddress = "readonly@user.com",
                Id = 1, Username = "readonly@user.com", Password = "readonly@user",
                Roles = new List<string> {"reader"}
            },

            new User()
            {
                FirstName = "Read Write", LastName ="User", EmailAddress = "readwrite@user.com",
                Id = 2, Username = "readwrite@user.com", Password = "readwrite@user",
                Roles = new List<string> {"reader", "write"}
            }
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);
           
            return user;
        }
    }
}
