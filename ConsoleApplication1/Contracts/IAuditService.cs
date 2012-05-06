using System.ServiceModel;
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