using System;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedAction_has_a_corresponding_Dto : ConventionTest
    {
        private readonly string dtoNamespace = typeof (AuditedActionDto).Namespace;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = IsAuditedAction,
                           Must = HaveCorrespondingDtoType,
                           FailDescription = "The following AuditedAction DTOs are missing. Please create them.",
                           FailItemDescription = DtoForAuditedActionDescription
                       }.FromAssembly(typeof (AuditedAction).Assembly);
        }

        private string DtoForAuditedActionDescription(Type arg)
        {
            return GetMatchingDtoName(arg) + " for " + arg;
        }

        private bool HaveCorrespondingDtoType(Type obj)
        {
            string dtoName = GetMatchingDtoName(obj);
            Type dto = Type.GetType(dtoName);
            return dto != null;
        }

        private string GetMatchingDtoName(Type obj)
        {
            return dtoNamespace + "." + obj.Name + "Dto";
        }

        private bool IsAuditedAction(Type type)
        {
            return type.IsAbstract == false && typeof (AuditedAction).IsAssignableFrom(type);
        }
    }
}