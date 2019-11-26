using Autofac;
using System.Net.Sockets;

namespace SnakeGame
{
    public static class AutofacConfig
    {
        private static IContainer _instance;

        public static T Resolve<T>()
            where T : class
        => _instance.Resolve<T>();

        public static void Config()
        {
            var builder = new ContainerBuilder();
            
            builder.RegisterType<TcpClient>().InstancePerDependency();
            builder.RegisterType<NetworkClient>().As<IConnection>().InstancePerDependency();

            _instance = builder.Build();
        }
    }
}
