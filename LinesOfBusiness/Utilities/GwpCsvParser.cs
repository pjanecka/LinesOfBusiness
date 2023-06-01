using LinesOfBusiness.Database;
using LinesOfBusiness.Models;
using System.Globalization;

namespace LinesOfBusiness.Utilities
{
    public class GwpCsvParser : IGwpCsvParser
    {
        private readonly GwpContext context;

        public GwpCsvParser(GwpContext context)
        {
            this.context = context;
        }

        public async Task ParseCsvToContext(string csvFilePath)
        {
            var csvLines = await File.ReadAllLinesAsync(csvFilePath);
            var dataLines = csvLines.Skip(1);

            foreach (var line in dataLines)
            {
                var values = line.Split(',');

                var gwp = new Gwp
                {
                    Country = values[0],
                    VariableId = values[1],
                    VariableName = values[2],
                    LineOfBusiness = values[3]
                };

                context.Gwps.Add(gwp);

                for (int i = 4; i < values.Length; i++)
                {
                    var year = 2000 + i - 4;
                    if (!decimal.TryParse(values[i], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal value))
                    {
                        value = 0m;
                    }

                    var gwpValue = new GwpValue
                    {
                        Year = year,
                        Value = value,
                        GwpId = gwp.Id
                    };

                    context.GwpValues.Add(gwpValue);
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
