using System;
using System.Reflection;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedActionDto_has_a_corresponding_AuditedAction : ConventionTest
    {
        private readonly Assembly auditedActionAssembly = typeof (AuditedAction).Assembly;
        private readonly string auditedActionNamespace = typeof (AuditedAction).Namespace;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = IsAuditedActionDto,
                           Must = HaveCorrespondingDtoType,
                           FailDescription = "The following AuditedActions are missing. Please create them.",
                           FailItemDescription = DtoForAuditedActionDescription
                       }.FromAssembly(auditedActionAssembly);
        }

        private string DtoForAuditedActionDescription(Type arg)
        {
            return GetMatchingAuditedActionName(arg) + " for " + arg;
        }

        private bool HaveCorrespondingDtoType(Type obj)
        {
            var dtoName = GetMatchingAuditedActionName(obj);
            var dto = auditedActionAssembly.GetType(dtoName);
            return dto != null;
        }

        private string GetMatchingAuditedActionName(Type obj)
        {
            return auditedActionNamespace + "." + obj.Name.Replace("Dto", string.Empty);
        }

        private bool IsAuditedActionDto(Type type)
        {
            return type.IsAbstract == false && typeof (AuditedActionDto).IsAssignableFrom(type);
        }
    }
}