namespace LinesOfBusiness.Models
{
    public class Gwp
    {
        public int Id { get; set; }
        public string Country { get; set; } = null!;
        public string VariableId { get; set; } = null!;
        public string VariableName { get; set; } = null!;
        public string LineOfBusiness { get; set; } = null!;
        public List<GwpValue> GwpValues { get; set; }

        public Gwp()
        {
            GwpValues = new();
        }
    }
}
