using System.Linq;
using Castle.Core.Internal;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class List_all_components_in_Windsor : WindsorConventionTest
    {
        protected override WindsorConventionData SetUp()
        {
            return new WindsorConventionData(new WindsorContainer().Install(FromAssembly.Containing<AuditedAction>()))
                       {
                           FailItemDescription = h => BuildDetailedHandlerDescription(h),
                           HasApprovedExceptions = true,
                           FailDescription = "All components registered in the container"
                       };

        }

        private string BuildDetailedHandlerDescription(IHandler handler)
        {
            var componentName = handler.ComponentModel.ComponentName;
            if (componentName.SetByUser)
            {
                return string.Format("\"{0}\" {1}", componentName.Name, GetServicesDescription(handler));
            }
            return GetServicesDescription(handler);
        }

        private string GetServicesDescription(IHandler handler)
        {
            var component = handler.ComponentModel;
            var services = component.Services.ToArray();
            if (services.Length == 1 && services[0] == component.Implementation)
            {
                return component.Implementation.ToCSharpString();
            }

            string value;
            if (component.Implementation == typeof(LateBoundComponent))
            {
                value = "late bound ";
            }
            else if (component.Implementation == null)
            {
                value = "no impl / ";
            }
            else
            {
                value = component.Implementation.ToCSharpString() + " / ";
            }
            return value + string.Join(", ", services.Select(s => s.ToCSharpString()));
        }
    }
}
