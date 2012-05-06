using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ConsoleApplication1.Services;

namespace ConsoleApplication1.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<AuditService>()
                    .Where(Component.IsInSameNamespaceAs<AuditService>())
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                    .Configure(c => c.AsWcfService(
                        new DefaultServiceModel()
                            .Hosted()
                            .OpenEagerly()
                            .PublishMetadata(x => x.EnableHttpGet())
                                        )));
        }
    }
}