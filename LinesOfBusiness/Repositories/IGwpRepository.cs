namespace LinesOfBusiness.Repositories
{
    public interface IGwpRepository
    {
        Task<IDictionary<string, decimal>> GetGwps(string countryCode, IEnumerable<string> lobList);
    }
}
