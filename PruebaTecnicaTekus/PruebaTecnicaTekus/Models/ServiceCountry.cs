using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaTekus.Models
{
    public class ServiceCountry
    {
        [Key]
        public int ServiceCountryID { get; set; }

        [Required]
        public int ProviderServiceID { get; set; }

        [Required]
        public int CountryID { get; set; }

        [ForeignKey(nameof(ProviderServiceID))]
        public ProviderService ProviderService { get; set; }

        [ForeignKey(nameof(CountryID))]
        public Country Country { get; set; }
    }
}
