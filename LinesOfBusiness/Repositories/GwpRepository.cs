using LinesOfBusiness.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace LinesOfBusiness.Repositories
{
    public class GwpRepository : IGwpRepository
    {
        private readonly GwpContext context;

        public GwpRepository(GwpContext context)
        {
            this.context = context;
        }

        public async Task<IDictionary<string, decimal>> GetGwps(string countryCode, IEnumerable<string> lobList)
        {
            var data = await context.Gwps
                .Where(gwp => string.Equals(gwp.Country, countryCode, StringComparison.OrdinalIgnoreCase) && lobList.Contains(gwp.LineOfBusiness))
                .Include(gwp => gwp.GwpValues.Where(gwpValue => gwpValue.Year >= 2008 && gwpValue.Year <= 2015))
                .ToListAsync();

            var output = data.GroupBy(gwp => gwp.LineOfBusiness)
                .ToImmutableDictionary(g => g.Key, g => g.SelectMany(gwp => gwp.GwpValues).Average(gwpVal => gwpVal.Value));

            return output;
        }
    }
}
