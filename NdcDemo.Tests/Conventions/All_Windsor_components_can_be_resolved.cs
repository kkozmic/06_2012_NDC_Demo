using ApprovalTests;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Diagnostics.DebuggerViews;
using Castle.Windsor.Diagnostics.Helpers;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class All_Windsor_components_can_be_resolved : WindsorConventionTest<IPotentiallyMisconfiguredComponentsDiagnostic>
    {
        protected override WindsorConventionData<IHandler> SetUp()
        {
            return
                new WindsorConventionData<IHandler>(
                    new WindsorContainer().Install(FromAssembly.Containing<AuditedAction>()))
                    {
                        OrderBy = h => h.GetServicesDescription(),
                        FailItemDescription = MisconfiguredComponentDescription,
                        HasApprovedExceptions = true
                    };

        }

        private string MisconfiguredComponentDescription(IHandler var)
        {
            var item = new ComponentStatusDebuggerViewItem((IExposeDependencyInfo) var);
            return item.Message;
        }
    }
}
