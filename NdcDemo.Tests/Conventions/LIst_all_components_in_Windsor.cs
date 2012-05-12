using System;
using ApprovalTests;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Diagnostics.Helpers;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class List_all_components_in_Windsor : IConventionTest
    {
        public void Execute()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.Containing<AuditedAction>());
            var message = BuildAllComponentsMessage(container.Kernel);
            Approve(message);
        }

        private string BuildAllComponentsMessage(IKernel kernel)
        {
            var allComponents = GetAllComponents(kernel);
            return string.Join(Environment.NewLine, allComponents);
        }

        private string[] GetAllComponents(IKernel container)
        {
            var host = container.GetSubSystem(SubSystemConstants.DiagnosticsKey) as IDiagnosticsHost;
            var diagnostic = host.GetDiagnostic<IAllComponentsDiagnostic>();

            var allHandlers = diagnostic.Inspect();
            Array.Sort(allHandlers, (i1, i2) => i1.ComponentModel.Name.CompareTo(i2.ComponentModel.Name));
            var items = Array.ConvertAll(allHandlers, h => h.GetComponentName());
            return items;
        }

        private void Approve(string message)
        {
            Approvals.Verify(new ApprovalTextWriter(message), new ConventionTestNamer(GetType().Name),
                             Approvals.GetReporter());
        }

        public string Name
        {
            get { return "List all the components in Windsor"; }
        }
    }
}