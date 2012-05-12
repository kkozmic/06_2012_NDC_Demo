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
                           Must = HaveCorrespondingDtoType
                       };
        }

        private bool HaveCorrespondingDtoType(Type aa)
        {
            var expectedDtoTypeName = dtoNamespace + "." + aa.Name + "Dto";
            var dtoType = dtoAssembly.GetType(expectedDtoTypeName);
            return dtoType != null;
        }

        private bool AuditedActions(Type obj)
        {
            return typeof (AuditedAction).IsAssignableFrom(obj) && obj.IsAbstract == false;
        }
    }
}