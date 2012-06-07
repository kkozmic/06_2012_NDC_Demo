using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConventionTests
{
    public class Dtos_have_no_static_members : ConventionTest
    {
        protected override ConventionData SetUp()
        {
            return new ConventionData
                {
                    Types = t => t.Name.EndsWith("Dto"),
                    Must = t => GetStaticMembers(t).Any() == false,
                    FailDescription = "Static members found on dto types",
                    FailItemDescription = t => BuildFailDescription(t)
                };
        }

        private string BuildFailDescription(Type type)
        {
            var message = new StringBuilder("On type " + type);
            message.AppendLine();
            foreach (MemberInfo member in GetStaticMembers(type))
            {
                message.AppendLine(member.ToString());
            }
            return message.ToString();
        }

        private MemberInfo[] GetStaticMembers(Type type)
        {
            return
                type.GetMembers(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                    .OrderBy(m => m.ToString()).ToArray();
        }
    }
}