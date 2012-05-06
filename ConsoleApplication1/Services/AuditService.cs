using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Cartographer;
using ConsoleApplication1.Contracts;
using NdcDemo;

namespace ConsoleApplication1.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode
        = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode
        = InstanceContextMode.PerCall)]
    public class AuditService : IAuditService
    {
        //private IMapper mapper;

        //public AuditService(IMapper mapper)
        //{
        //    this.mapper = mapper;
        //}

        [FooBarBehavior]
        [OperationBehavior(Impersonation
            = ImpersonationOption.Required)]
        public AuditedActionDto[] GetAudit()
        {
            throw new NotImplementedException();
        }
    }
}