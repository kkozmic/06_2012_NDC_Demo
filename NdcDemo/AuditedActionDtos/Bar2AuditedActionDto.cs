namespace NdcDemo.AuditedActionDtos
{
    public class BarAuditedActionDto : AuditedActionDto
    {
        public BarAuditedActionDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}