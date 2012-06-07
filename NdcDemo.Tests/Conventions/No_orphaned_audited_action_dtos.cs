using System;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class No_orphaned_audited_action_dtos:ConventionTest
    {
        private string auditNamespace = typeof (AuditedAction).Namespace;
        protected override ConventionData SetUp()
        {
            return new ConventionData
                {
                    Types = t => t.IsConcrete<AuditedActionDto>(),
                    Must = HaveCorrespondingAudit,
                    FailDescription = "There are orphaned audit dtos"
                };
        }

        private bool HaveCorrespondingAudit(Type obj)
        {
            var aaName = GetAuditName(obj);
            return GetAuditType(aaName) != null;
        }

        private Type GetAuditType(string aaName)
        {
            return typeof (AuditedAction).Assembly.GetType(aaName);
        }

        private string GetAuditName(Type type)
        {
            return auditNamespace + "." + type.Name.Replace("Dto", string.Empty);
        }
    }
}