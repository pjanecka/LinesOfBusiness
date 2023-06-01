using LinesOfBusiness.Database;
using LinesOfBusiness.Repositories;
using LinesOfBusiness.Utilities;

namespace LinesOfBusiness
{
    public class Program
    {
        public static async Task InitData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var dbContext = services.GetRequiredService<GwpContext>();
            var csvParser = services.GetRequiredService<IGwpCsvParser>();
            await csvParser.ParseCsvToContext("Resources/data.csv");
        }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<GwpContext>();
            builder.Services.AddMemoryCache();

            builder.Services.AddScoped<IGwpCsvParser, GwpCsvParser>();
            builder.Services.AddScoped<IGwpRepository, GwpRepository>();
            builder.Services.Decorate<IGwpRepository, CachedGwpRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            await InitData(app);

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();

            app.Run();
        }
    }
}