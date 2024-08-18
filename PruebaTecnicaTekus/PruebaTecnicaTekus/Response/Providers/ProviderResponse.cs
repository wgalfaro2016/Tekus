namespace PruebaTecnicaTekus.Response.Providers
{
    public class ProviderResponse
    {
        public bool IsSuccess { get; set; }
        public int? ProviderId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
