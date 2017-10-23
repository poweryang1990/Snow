using SimpleInjector;
using System;
using RPCService;
using SimpleInjector.Lifestyles;
using Snow.RPC;
using Snow.RPC.Client;

namespace RPCClient
{
    class Program
    {
        private static Container container;
        static Program()
        {
            // Create the container as usual.
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            var rpcServer = new ServiceRegistryAddress() {Host = "127.0.0.1", Port = 8500};
            //container.Register<IUserService>(() => new RpcHttpClient("UserService", rpcServer).UseService<IUserService>(), Lifestyle.Scoped);
            container.Register<IUserService>(() => new RpcTcpClient("UserService", rpcServer).UseService<IUserService>(), Lifestyle.Scoped);

            // Optionally verify the container.
            container.Verify();
        }
        static void Main(string[] args)
        {

            while (true)
            {
                using (ThreadScopedLifestyle.BeginScope(container))
                {
                    try
                    {
                        IUserService userService = container.GetInstance<IUserService>();
                        var result = userService.SayHello(new User { Name = "Power Yang", Age = 19 });
                        var users = userService.GetAllUsers();
                        Console.WriteLine(result);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                Console.ReadKey();
            }
        }
    }
}
