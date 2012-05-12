using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace NdcDemo.Installers
{
    public class SystemServicesInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITime>().ImplementedBy<Time>(),
                Component.For<IFileSystem>().ImplementedBy<LocalFileSystem>());
        }
    }
}