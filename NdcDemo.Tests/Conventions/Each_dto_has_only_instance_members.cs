using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConventionTests
{
    public class Each_dto_has_only_instance_members:ConventionTest
    {
        protected override ConventionData SetUp()
        {
            return new ConventionData
                       {
                           Types = t => t.Name.EndsWith("Dto"),
                           Must = HaveOnlyInstanceMembers,
                           FailItemDescription = ListMembersForType
                       }.WithApprovedExceptions(
                           "Checking for IsActive on a DTO is more convenient than duplicating it all around the place");
        }

        private string ListMembersForType(Type dto)
        {
            var description = new StringBuilder();
            var invalidMembers = GetInvalidMembers(dto);
            description.AppendLine(dto+" with static members:");
            foreach (var invalidMember in invalidMembers.OrderBy(x => x.ToString()))
            {
                description.AppendLine("\t\t" + invalidMember);
            }
            return description.ToString();
        }

        private bool HaveOnlyInstanceMembers(Type dto)
        {
            var invalidMembers = GetInvalidMembers(dto);
            return invalidMembers.Length == 0;
        }

        private MemberInfo[] GetInvalidMembers(Type dto)
        {
            return dto.GetMembers(BindingFlags.Static |
                                  BindingFlags.Public |
                                  BindingFlags.NonPublic);

        }
    }
}