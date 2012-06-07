using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class List_misconfigured_components_in_Windsor:WindsorConventionTest<IPotentiallyMisconfiguredComponentsDiagnostic>
    {
        protected override WindsorConventionData<IHandler> SetUp()
        {
            return
                new WindsorConventionData<IHandler>(
                    new WindsorContainer().Install(FromAssembly.Containing<AuditedAction>()))
                    {
                        FailDescription = "The following components come up as potentially unresolvable",
                        FailItemDescription = MisconfiguredComponentDescription
                    }.WithApprovedExceptions("it's *potentially* for a reason");
        }
    }
}