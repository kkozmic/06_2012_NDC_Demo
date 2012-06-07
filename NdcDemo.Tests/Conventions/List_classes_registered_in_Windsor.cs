using Castle.Windsor;
using Castle.Windsor.Diagnostics.Helpers;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class List_classes_registered_in_Windsor:WindsorConventionTest
    {
        protected override WindsorConventionData SetUp()
        {
            return new WindsorConventionData(new WindsorContainer()
                                                 .Install(FromAssembly.Containing<AuditedAction>()))
                {
                    FailDescription = "All Windsor components",
                    FailItemDescription = h => BuildDetailedHandlerDescription(h)+" | "+h.ComponentModel.GetLifestyleDescription(),
                    HasApprovedExceptions = true
                };

        }
    }
}