namespace PruebaTecnicaTekus.Dtos
{
    public class ProviderWithCustomFieldsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public List<CustomFieldDto> CustomProviderFields { get; set; }
    }

    public class CustomFieldDto
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string FieldType { get; set; }
    }
}
