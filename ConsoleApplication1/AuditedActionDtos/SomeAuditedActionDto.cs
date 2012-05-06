namespace NdcDemo.AuditedActionDtos
{
    public class SomeAuditedActionDto : AuditedActionDto
    {
        public string Status { get; set; }

        public static bool IsActive(SomeAuditedActionDto item)
        {
            return item != null && item.Status == "Active";
        }
    }
}