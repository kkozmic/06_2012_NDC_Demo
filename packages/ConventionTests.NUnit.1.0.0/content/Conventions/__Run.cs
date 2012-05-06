using System;
using System.Linq;
using NUnit.Framework;

namespace ConventionTests
{
    [TestFixture]
    public class ConventionTestsRunner
    {
        [TestFixtureSetUp]
        public static void GlobalSetUp()
        {
        }

        public TestCaseData[] Conventions
        {
            get
            {
                var types = GetConventionTypes();
                var conventionTests = Array.ConvertAll(types, BuildTestData);
                return conventionTests;
            }
        }

        private TestCaseData BuildTestData(Type t)
        {
            var convention = CreateConvention(t);
            return new TestCaseData(convention).SetName(convention.Name);
        }

        private static IConventionTest CreateConvention(Type t)
        {
            return (IConventionTest) Activator.CreateInstance(t);
        }

        private static Type[] GetConventionTypes()
        {
            var types = typeof(ConventionTestsRunner).Assembly.GetExportedTypes().Where(t => t.IsClass && t.IsAbstract == false && t.Is<IConventionTest>()).ToArray();
            return types;
        }

        [Test]
        [TestCaseSource("Conventions")]
        public void Run(IConventionTest test)
        {
            test.Execute();
        }
    }
}