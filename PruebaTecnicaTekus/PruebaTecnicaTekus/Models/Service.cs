namespace PruebaTecnicaTekus.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal HourlyRate { get; set; }

        public ICollection<ProviderService> ProviderServices { get; set; }
    }
}
