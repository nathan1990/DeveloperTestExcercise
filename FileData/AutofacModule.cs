using Autofac;
using FileData.Interfaces;
using FileData.Operations;

namespace FileData
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ConsoleWrapper>().As<IConsoleWrapper>();
            builder.RegisterType<FileDetailsWrapper>().As<IFileDetailsWrapper>();
            builder.RegisterType<ArgumentsParser>().As<IArgumentsParser>();

            builder.RegisterType<GetSizeOperation>().As<IOperation>();
            builder.RegisterType<GetVersionOperation>().As<IOperation>();
            builder.RegisterType<OperationTypeParser>().As<IOperationTypeParser>();

            builder.RegisterType<Application>().As<IApplication>();
        }
    }
}
