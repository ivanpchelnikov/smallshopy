using SmalShopy.Models;

namespace SmalShopy.Services
{
    public class UserService : IUserService
    {
        private readonly User _currentUser = new User { Name = "Ivan Pchelnikov", Token = "1234-455662-22233333-3333" };

        public User GetUserName()
        {
            return _currentUser;
        }
    }
}
