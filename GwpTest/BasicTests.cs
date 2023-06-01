using LinesOfBusiness.Database;
using LinesOfBusiness.Repositories;
using LinesOfBusiness.Utilities;

namespace GwpTest
{
    public class BasicTests
    {
        private GwpRepository repository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var dbContext = new GwpContext();
            var csvParser = new GwpCsvParser(dbContext);
            csvParser.ParseCsvToContext("Resources/data.csv").GetAwaiter().GetResult();
            repository = new GwpRepository(dbContext);
        }

        [TestCase("ae", new string[] { "property", "transport" }, ExpectedResult = 2)]
        [TestCase("bl", new string[] { "property" }, ExpectedResult = 0)]
        [TestCase("us", new string[] { "non-existent" }, ExpectedResult = 0)]
        public async Task<int> GetGwps_ReturnsCorrectResults(string countryCode, string[] lobs)
        {
            var result = await repository.GetGwps(countryCode, lobs);

            return result.Count;
        }
    }
}