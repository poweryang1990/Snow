using System;
using System.Collections.Generic;
using System.Threading;

namespace RPCService
{
    public class UserService: IUserService
    {
        public IList<User> GetAllUsers()
        {
            return new List<User>
            {
                new User{ Name= "Person A"},
                new User{ Name= "Person B"}
            };
        }

        public string SayHello(User user)
        {
            //throw  new Exception("获取用户异常");
            //Thread.Sleep(200);
            return $"Hello {user.Name} ,age: {user.Age}";
        }
    }
}