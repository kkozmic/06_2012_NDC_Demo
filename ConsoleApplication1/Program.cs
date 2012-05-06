using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cartographer;
using Cartographer.Compiler;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ConsoleApplication1;
using ConsoleApplication1.AuditedActionDtos;
using ConsoleApplication1.AuditedActions;
using ConsoleApplication1.Contracts;
using ConsoleApplication1.Services;

namespace NdcDemo
{
    public interface IController
    {
        // useful stuff
    }

    public class HomeController : IController
    {
        // even more useful stuff
    }


    internal class Program
    {
        private IMapper mapper;
        private string rootReportPath;

        private static void Main(string[] args)
        {
            new Program().Execute();
            var container = new WindsorContainer();
            container.Register(Component.For<IAuditService>().ImplementedBy<AuditService>());


            container.Resolve<IAuditService>();
        }

        private void Execute()
        {
            var mapperBuilder = new MapperBuilder();
            mapperBuilder.Settings.TypeMapper = new NDCTypeMapper();
            mapper = mapperBuilder.BuildMapper();
            AuditedActionDto[] audit = GetAudit();
        }


        public AuditedActionDto[] GetAudit()
        {
            IEnumerable<AuditedAction> actions = GetAuditedActions();
            return mapper.Convert<AuditedActionDto[]>(actions);
        }


        public Stream RenderReport(int reportId)
        {
            Report report = GetReport(reportId);
            string rptPath = LocateTemplate(report);
            return Render(report, rptPath);
        }

        private Stream Render(Report report, object rptPath)
        {
            throw new NotImplementedException();
        }

        private string LocateTemplate(Report report)
        {
            return Path.Combine(rootReportPath,
                                report.LocationCode,
                                report.LanguageCode,
                                report.Department,
                                report.FileName);
        }

        private Report GetReport(int reportId)
        {
            throw new NotImplementedException();
        }

        private static void AddFooDto(List<AuditedActionDto> dtos, AuditedAction action)
        {
            IWindsorContainer container = new WindsorContainer();



        }

        private static IEnumerable<AuditedAction> GetAuditedActions()
        {
            yield return new BarAuditedAction();
        }

        #region Nested type: BarAuditedAction

        public class BarAuditedAction : AuditedAction
        {
        }

        #endregion

        #region Nested type: Conference

        public class Conference
        {
            public dynamic Id { get; set; }
        }

        #endregion

        #region Nested type: IReport

        public interface IReport
        {
            // marker interface
        }

        #endregion

        #region Nested type: IReportDataSource

        public interface IReportDataSource<TReport>
            where TReport : IReport
        {
            TReport GetReportData();
        }

        #endregion

        #region Nested type: IReportRenderer

        public interface IReportRenderer<TReport>
        {
            Stream Render(TReport data, int templateId);
        }

        #endregion

        #region Nested type: MonthlyRevenuePerClientDataSource

        public class MonthlyRevenuePerClientDataSource :
            IReportDataSource<MonthlyRevenuePerClient>
        {
            #region IReportDataSource<MonthlyRevenuePerClient> Members

            public MonthlyRevenuePerClient GetReportData()
            {
                throw new Exception();
            }

            #endregion
        }

        #endregion
    }

    internal class MonthlyRevenuePerClient : Program.IReport
    {
    }

    [DebuggerStepThrough]
    [DebuggerNonUserCode]
    internal class NDCTypeMapper : ITypeMapper
    {
        #region ITypeMapper Members

        [DebuggerHidden]
        public MappingInfo GetMappingInfo(Type sourceInstanceType, Type requestedTargetType,
                                          bool preexistingTargetInstance)
        {
            if (requestedTargetType.IsArray)
            {
                return new MappingInfo(sourceInstanceType, requestedTargetType, preexistingTargetInstance);
            }
            throw new ArgumentException(
                string.Format(
                    "Could not match type {0} to target type '{1}'\n\rTarget type '{1}' does not exist.\n\rMake sure you created it.",
                    typeof (FooAuditedAction), typeof (FooAuditedActionDto)));
        }

        #endregion
    }


    namespace SystemAbstractions
    {
    }


    internal class Report
    {
        public string LocationCode
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string LanguageCode
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Department
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string FileName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    internal class FooBarBehaviorAttribute
        : Attribute
    {
    }
}

namespace NdcDemo.SystemAbstractions
{
    public interface ITime
    {
        DateTime GetCurrentLocalTime();
    }

    public interface IFileSystem
    {
    }

    public class Time : ITime
    {
        #region ITime Members

        public DateTime GetCurrentLocalTime()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class LocalFileSystem : IFileSystem
    {
    }
}