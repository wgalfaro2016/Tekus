namespace PruebaTecnicaTekus.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string Name { get; set; }
        public string ISOCode { get; set; }
        public ICollection<ServiceCountry> ServiceCountries { get; set; }
    }
}
