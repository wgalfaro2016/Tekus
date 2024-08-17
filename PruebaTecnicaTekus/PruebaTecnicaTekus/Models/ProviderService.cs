namespace PruebaTecnicaTekus.Models
{
    public class ProviderService
    {
        public int ProviderServiceID { get; set; }
        public int ProviderID { get; set; }
        public int ServiceID { get; set; }
        public DateTime StartDate { get; set; }

        public Provider Provider { get; set; }
        public Service Service { get; set; }
        public ICollection<ServiceCountry> ServiceCountries { get; set; }
    }
}
