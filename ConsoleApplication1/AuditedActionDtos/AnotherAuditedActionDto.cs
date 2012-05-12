namespace NdcDemo.AuditedActionDtos
{
    public class AnotherAuditedActionDto : AuditedActionDto
    {
        public AnotherAuditedActionDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}