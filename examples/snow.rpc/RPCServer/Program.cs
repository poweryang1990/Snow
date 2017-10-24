using System;
using RPCService;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using Snow.RPC;
using Snow.RPC.Server;
using Snow.RPC.Server.Registry;

namespace RPCServer
{
    class Program
    {
        private static Container _container;
        static Program()
        {
            // Create the container as usual.
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            // Register your types, for instance:
            _container.Register(() => new ServiceRegistryAddress { Host = "127.0.0.1", Port = 8500 }, Lifestyle.Scoped);
            _container.Register<IUserService, UserService>(Lifestyle.Scoped);
            _container.Register<IRegistryService, ConsulRegistryService>(Lifestyle.Scoped);
            // Optionally verify the container.
            _container.Verify();
        }

        static void Main(string[] args)
        {
            //RpcHttpServer server=new RpcHttpServer("http://127.0.0.1:2012/");
            RPCTcpServer server=new RPCTcpServer("tcp://127.0.0.1:2012/");
            using (ThreadScopedLifestyle.BeginScope(_container))
            {
                var registryService = _container.GetInstance<IRegistryService>();
                registryService.Register(new RpcService { Name = "UserService", Host = "127.0.0.1", Port = 2012 , Protocol = server.Protocol});
                server.RegisterService<IUserService>(_container.GetInstance<IUserService>());
            }
            //server.IsCrossDomainEnabled = true;
            
            server.Start();
            Console.WriteLine("Server started.");
            Console.ReadLine();
            Console.WriteLine("Server stopped.");
        }
    }
}
