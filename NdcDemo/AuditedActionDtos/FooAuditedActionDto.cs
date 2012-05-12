namespace NdcDemo.AuditedActionDtos
{
    public class FooAuditedActionDto : AuditedActionDto
    {
        public string Status { get; set; }

        public static bool IsActive(FooAuditedActionDto item)
        {
            return item != null && item.Status == "Active";
        }
    }
}