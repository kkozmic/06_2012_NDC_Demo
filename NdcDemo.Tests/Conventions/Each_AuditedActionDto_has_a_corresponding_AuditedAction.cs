using System;
using System.Reflection;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedActionDto_has_a_corresponding_AuditedAction : ConventionTest
    {
        private readonly string aaNamespace = typeof (AuditedAction).Namespace;
        private readonly Assembly aaAssembly = typeof (AuditedAction).Assembly;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = AuditedActionDtos,
                           Must = HaveCorrespondingAuditedActionType,
                           FailDescription =
                               "The following audited actions are missing. Make sure you create them. If you just renamed the AuditedActionDtos make sure you apply the same rename to the AuditedAction",
                           FailItemDescription = t => GetAuditedActionNameFor(t) + " for " + t.Name
                       };
        }

        private bool HaveCorrespondingAuditedActionType(Type dto)
        {
            var expectedAATypeName = GetAuditedActionNameFor(dto);
            var aaType = aaAssembly.GetType(expectedAATypeName);
            return aaType != null;
        }

        private string GetAuditedActionNameFor(Type dto)
        {
            var expectedAAName = aaNamespace + "." + dto.Name.Replace("Dto", string.Empty);
            return expectedAAName;
        }

        private bool AuditedActionDtos(Type obj)
        {
            return typeof (AuditedActionDto).IsAssignableFrom(obj) && obj.IsAbstract == false;
        }
    }
}