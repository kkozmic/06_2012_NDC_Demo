using System;
using System.Text;
using ApprovalTests;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Diagnostics.DebuggerViews;
using Castle.Windsor.Installer;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class All_Windsor_components_can_be_resolved : IConventionTest
    {
        public void Execute()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.Containing<AuditedAction>());
            var message = BuildInvalidComponentsMessage(container.Kernel);
            Approve(message);
        }

        private void Approve(string message)
        {
            Approvals.Verify(new ApprovalTextWriter(message), new ConventionTestNamer(GetType().Name),
                             Approvals.GetReporter());
        }

        private string BuildInvalidComponentsMessage(IKernel kernel)
        {
            var components = GetPotentiallyMisconfiguredComponents(kernel);
            return BuildMessage(components);
        }

        private ComponentStatusDebuggerViewItem[] GetPotentiallyMisconfiguredComponents(
            IKernel container)
        {
            var host = container.GetSubSystem(SubSystemConstants.DiagnosticsKey) as IDiagnosticsHost;
            var diagnostic = host.GetDiagnostic<IPotentiallyMisconfiguredComponentsDiagnostic>();

            var invalidHandlers = diagnostic.Inspect();
            Array.Sort(invalidHandlers, (i1, i2) => i1.ComponentModel.Name.CompareTo(i2.ComponentModel.Name));
            var items = Array.ConvertAll(invalidHandlers,
                                         h => new ComponentStatusDebuggerViewItem((IExposeDependencyInfo) h));
            return items;
        }

        private string BuildMessage(ComponentStatusDebuggerViewItem[] items)
        {
            var message = new StringBuilder();
            Array.ForEach(items, i => message.AppendLine(i.Message));
            return message.ToString();
        }

        public string Name
        {
            get { return "All Windsor components can be resolved"; }
        }
    }
}