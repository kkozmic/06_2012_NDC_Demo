using System;
using System.Text;
using Castle.MicroKernel;
using Castle.MicroKernel.Handlers;
using Castle.Windsor;
using Castle.Windsor.Diagnostics;
using Castle.Windsor.Diagnostics.DebuggerViews;
using Castle.Windsor.Installer;
using NUnit.Framework;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class All_components_registered_in_windsor_are_configured_properly : IConventionTest
    {
        public void Execute()
        {
            var container = new WindsorContainer();
            container.Install(FromAssembly.Containing<AuditedAction>());
            var misconfiguredComponents = GetPotentiallyMisconfiguredComponents(container);
            Assert.AreEqual(0, misconfiguredComponents.Length, BuildMessage(misconfiguredComponents));
        }

        public string Name
        {
            get { return GetType().Name; }
        }

        private ComponentStatusDebuggerViewItem[] GetPotentiallyMisconfiguredComponents(
            WindsorContainer container)
        {
            var host = container.Kernel.GetSubSystem(SubSystemConstants.DiagnosticsKey) as IDiagnosticsHost;
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
    }
}