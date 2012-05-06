using System.ServiceModel;
using ConsoleApplication1.AuditedActionDtos;
using NdcDemo;

namespace ConsoleApplication1.Contracts
{
    [ServiceContract]
    public interface IAuditService
    {
        [OperationContract]
        AuditedActionDto[] GetAudit();
    }
}