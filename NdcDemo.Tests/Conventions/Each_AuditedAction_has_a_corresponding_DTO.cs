using System;
using System.Reflection;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedAction_has_a_corresponding_DTO : ConventionTest
    {
        private readonly string dtoNamespace = typeof (AuditedActionDto).Namespace;
        private readonly Assembly dtoAssembly = typeof (AnotherAuditedActionDto).Assembly;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = AuditedActions,
                           Must = HaveCorrespondingDtoType,
                           FailDescription = "The following DTO audited actions are missing. Make sure you create them. If you just renamed the AuditedActions make sure you apply the same rename to the Dto",
                           FailItemDescription = t=>GetDtoNameFor(t)+" for "+t.Name
                       };
        }

        private bool HaveCorrespondingDtoType(Type aa)
        {
            var expectedDtoTypeName = GetDtoNameFor(aa);
            var dtoType = dtoAssembly.GetType(expectedDtoTypeName);
            return dtoType != null;
        }

        private string GetDtoNameFor(Type aa)
        {
            var expectedDtoTypeName = dtoNamespace + "." + aa.Name + "Dto";
            return expectedDtoTypeName;
        }

        private bool AuditedActions(Type obj)
        {
            return typeof (AuditedAction).IsAssignableFrom(obj) && obj.IsAbstract == false;
        }
    }
}