﻿namespace PruebaTecnicaTekus.Models
{
    public class Provider
    {
        public int ProviderID { get; set; }
        public string Name { get; set; }
        public string LegalName { get; set; }
        public string NIT { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICollection<CustomProviderField> CustomProviderFields { get; set; } = new List<CustomProviderField>();
        public ICollection<ProviderService> ProviderServices { get; set; }
    }
}
