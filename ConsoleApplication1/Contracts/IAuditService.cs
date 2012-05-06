using System.ServiceModel;
using NdcDemo.AuditedActionDtos;

namespace NdcDemo.Contracts
{
    [ServiceContract]
    public interface IAuditService
    {
        [OperationContract]
        AuditedActionDto[] GetAudit();
    }
}