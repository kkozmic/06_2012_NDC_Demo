using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
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