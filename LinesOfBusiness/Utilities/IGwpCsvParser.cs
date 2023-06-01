namespace LinesOfBusiness.Utilities
{
    public interface IGwpCsvParser
    {
        Task ParseCsvToContext(string csvFilePath);
    }
}
