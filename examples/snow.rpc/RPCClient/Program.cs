using SimpleInjector;
using System;
using RPCService;
using SimpleInjector.Lifestyles;
using Snow.RPC;
using Snow.RPC.Client;
using Snow.RPC.Client.LoadBalancer;

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
            //var userServiceClient = new RpcHttpClient("UserService", rpcServer, RandomLoadBalancer.GetInstance();
            var userServiceClient = new RpcTcpClient("UserService", rpcServer, RandomLoadBalancer.GetInstance());
            container.Register<IUserService>(() => userServiceClient.UseService<IUserService>(), Lifestyle.Scoped);

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
