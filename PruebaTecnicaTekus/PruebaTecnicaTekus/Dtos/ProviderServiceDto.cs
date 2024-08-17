namespace PruebaTecnicaTekus.Dtos
{
    public class ProviderServiceDto
    {
        public int ProviderServiceId { get; set; }
        public int ProviderId { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartDate { get; set; }
    }
}