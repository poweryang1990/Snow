using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
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
        private static readonly object locker = new object();
        static Program()
        {
            // Create the container as usual.
            container = new Container();
            container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            var rpcServer = new ServiceRegistryAddress() {Host = "127.0.0.1", Port = 8500};
            //var userServiceClient = new RpcHttpClient("UserService", rpcServer, RandomLoadBalancer.GetInstance());
            var userServiceClient = new RpcTcpClient("UserService", rpcServer, RandomLoadBalancer.GetInstance());
            container.Register<IUserService>(() => userServiceClient.UseService<IUserService>(), Lifestyle.Scoped);

            // Optionally verify the container.
            container.Verify();
        }
        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("请输入测试的并发数 如100 200 500");
                var readStr= Console.ReadLine();
                int max;
                if (!int.TryParse(readStr,out  max))
                {
                    Console.WriteLine("请输入正确的数字");
                    continue;
                }
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.WriteLine($"开始执行 并发 {max}");
                var errorTotal = 0;
                var ts = new List<Thread>();
                for (int i = 0; i < max; i++)
                {
                    var tmp = i;
                    ts.Add(new Thread(() => {
                        using (ThreadScopedLifestyle.BeginScope(container))
                        {

                            try
                            {

                                IUserService userService = container.GetInstance<IUserService>();
                                var result = userService.SayHello(new User { Name = $"Power Yang {tmp + 1}", Age = 19 });
                                Console.WriteLine(result);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                errorTotal++;
                            }
                        }
                    }));
                }
                ts.ForEach(a => a.Start());
                ts.ForEach(a => a.Join());
                stopwatch.Stop();
                Console.WriteLine($"结束执行 耗时 {stopwatch.ElapsedMilliseconds} 毫秒，错误率 {Math.Round((decimal)errorTotal*100/max,2)} %");
            }
        }
    }
}
