﻿using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NdcDemo.Services;

namespace NdcDemo.Installers
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