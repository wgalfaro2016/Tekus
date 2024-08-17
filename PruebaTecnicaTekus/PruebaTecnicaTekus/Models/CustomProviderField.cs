namespace PruebaTecnicaTekus.Models
{
    public class CustomProviderField
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; } 
        public Provider Provider { get; set; }
    }
}