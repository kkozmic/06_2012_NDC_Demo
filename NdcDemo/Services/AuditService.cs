using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Cartographer;
using NdcDemo.AuditedActionDtos;
using NdcDemo.Contracts;

namespace NdcDemo.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode
        = AspNetCompatibilityRequirementsMode.Required)]
    [ServiceBehavior(InstanceContextMode
        = InstanceContextMode.PerCall)]
    public class AuditService : IAuditService
    {
        private IMapper mapper;
        private readonly ITime time;

        public AuditService(IMapper mapper, ITime time)
        {
            this.mapper = mapper;
            this.time = time;
        }

        [FooBarBehavior]
        [OperationBehavior(Impersonation
            = ImpersonationOption.Required)]
        public AuditedActionDto[] GetAudit()
        {
            throw new NotImplementedException();
        }
    }
}