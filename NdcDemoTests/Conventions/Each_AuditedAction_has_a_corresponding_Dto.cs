﻿using System;
using System.Reflection;
using NdcDemo.AuditedActionDtos;
using NdcDemo.AuditedActions;

namespace ConventionTests
{
    public class Each_AuditedAction_has_a_corresponding_Dto : ConventionTest
    {
        private readonly Assembly auditedActionAssembly = typeof (AuditedActionDto).Assembly;
        private readonly string dtoNamespace = typeof (AuditedActionDto).Namespace;

        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = IsAuditedAction,
                           Must = HaveCorrespondingDtoType,
                           FailDescription = "The following AuditedAction DTOs are missing. Please create them.",
                           FailItemDescription = DtoForAuditedActionDescription
                       }.FromAssembly(auditedActionAssembly);
        }

        private string DtoForAuditedActionDescription(Type arg)
        {
            return GetMatchingDtoName(arg) + " for " + arg;
        }

        private bool HaveCorrespondingDtoType(Type obj)
        {
            var dtoName = GetMatchingDtoName(obj);
            var dto = auditedActionAssembly.GetType(dtoName);
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