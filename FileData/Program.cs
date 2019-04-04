using Autofac;
using FileData.Interfaces;

namespace FileData
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            var container = containerBuilder.Build();

            var application = container.Resolve<IApplication>();
            application.Run(args);
        }
    }
}
