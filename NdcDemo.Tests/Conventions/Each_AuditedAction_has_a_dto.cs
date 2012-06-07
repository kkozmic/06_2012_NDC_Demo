using System;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedAction_has_a_dto : ConventionTest
    {
        private readonly string dtoNamespace = typeof (AuditedActionDto).Namespace;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                {
                    Types = t => t.IsConcrete<AuditedAction>(),
                    Must = HaveCorrespondingDto,
                };
        }

        private bool HaveCorrespondingDto(Type auditedAction)
        {
            string dtoTypeName = GetDtoTypeName(auditedAction);
            Type dtoType = GetDtoType(dtoTypeName);
            return dtoType != null;
        }

        private string GetDtoTypeName(Type auditedAction)
        {
            return dtoNamespace + "." + auditedAction.Name + "Dto";
        }

        private Type GetDtoType(string dtoTypeName)
        {
            return typeof (AuditedActionDto).Assembly.GetType(dtoTypeName);
        }
    }
}