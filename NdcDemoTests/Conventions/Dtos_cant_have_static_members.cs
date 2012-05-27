using System;
using System.Reflection;
using NdcDemo.AuditedActionDtos;

namespace ConventionTests
{
    public class Dtos_cant_have_static_members:ConventionTest
    {
        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = t => t.Name.EndsWith("Dto"),
                           Must = HaveNoStaticMembers,
                           FailDescription =
                               "DTOs are for data transfer only and therefore it makes no sense for them to have static members. The following types don't follow that rule:"
                       }.FromAssembly(typeof (AuditedActionDto).Assembly)
                       .WithApprovedExceptions("Some DTOs share trivial logic between client and server");

        }

        private bool HaveNoStaticMembers(Type obj)
        {
            var staticMembers = GetStaticMembers(obj);
            return staticMembers.Length == 0;
        }

        private static MemberInfo[] GetStaticMembers(Type obj)
        {
            var staticMembers = obj.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            return staticMembers;
        }
    }
}