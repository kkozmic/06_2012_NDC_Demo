using Cartographer;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace NdcDemo.Installers
{
    public class MapperInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IMapper>().UsingFactoryMethod(new MapperBuilder().BuildMapper));
        }
    }
}