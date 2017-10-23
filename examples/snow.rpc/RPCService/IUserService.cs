using System.Collections.Generic;

namespace RPCService
{
    public interface IUserService
    {
        string SayHello(User user);
        IList<User> GetAllUsers();
    }
}