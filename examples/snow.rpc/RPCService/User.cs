using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPCService
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Male { get; set; }
        public List<User> Friends { get; set; }
    }
}
