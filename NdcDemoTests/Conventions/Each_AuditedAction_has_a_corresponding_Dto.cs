using System;
using ConsoleApplication1.AuditedActionDtos;
using ConsoleApplication1.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedAction_has_a_corresponding_Dto:ConventionTest
    {
        private string dtoNamespace = typeof (AuditedActionDto).Namespace;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = IsAuditedAction,
                           Must = HaveCorrespondingDtoType
                       };
            ;
        }

        private bool HaveCorrespondingDtoType(Type obj)
        {
            var dto = Type.GetType(dtoNamespace + "." + obj.Name + "Dto");
            return dto != null;
        }

        private bool IsAuditedAction(Type type)
        {
            return type.IsAbstract == false && typeof (AuditedAction).IsAssignableFrom(type);
        }
    }
}