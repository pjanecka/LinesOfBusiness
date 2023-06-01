The solution contains 2 projects, built in .NET 7
- LinesOfBusiness - the main project
- GwpTest - NUnit project containing basic unit tests

The solution can be run via either Visual Studio or dotnet command line. For example, running it via 'dotnet run' while in the LinesOfBusiness project should start a localhost Kestrel server using the port 9091.

The functionality can be tested via swagger (http://localhost:9091/swagger/index.html) or tools such as Postman. The API consists of a single endpoint as per the requirements - http://localhost:9091/server/api/gwp/avg.
Input body is the same as in the original example:
{
  "country": "ae",
  "lob": [
    "property",
    "transport"
  ]
}

The solution also contains some basic unit tests (mainly testing whether the repository returns the correct number of items).

The LinesOfBusiness project consists of these main folders:
- Controllers - contains the required controller, which uses IGwpRepository to retrieve data.
- Database - contains GwpContext - EF class that represents the DB (containing 2 mock tables - Gwps and GwpValues), using InMemory implementation.
- DTO - contains the DTO class for the API request (response is only a dictionary, so a separate class was not needed).
- Models - contains the model classes for Gwp and GwpValue entries.
- Repositories - contains repository stuff - interface, the repository that uses GwpContext to retrieve data and a caching repository via the decorator pattern (using MemoryCache, set up via DI in Program.cs).
- Resources - contains the csv input.
- Utilities - helper class to parse the csv input and fill GwpContext at startup.

I chose to represent the DB with 2 tables (one for gwps, one for gwp values per year per gwp), rather than 1 table with columns for each year as is in the CSV. (1 table with columns named Year2000, Year2001, etc. looked rather ugly. While the years end at 2015, indicating that the system might not be meant to be dynamic and expand with newer years, it still looked a bit odd. It seemed to me that the goal was to create 2 tables with 1:N relationship but the CSV data was written like this for the sake of avoiding overcomplicating things in this assignment scenario).

Also, I decided to keep the entire solution in one project (except for the test project) as opposed to splitting it into smaller parts (to represent domain, data, etc.), because it looked like an overkill to me in this scenario with only 1 controller, 1 endpoint, and so on.

Notes:
- The lone API call is using async/await.
- Built-in ASP.NET CORE DI solution is used to handle dependency injection and registering of services.
- Repository pattern is used to retrieve the data from the DbContext.
- Swagger is present using Swashbuckle.AspNetCore; a few simple unit tests are also included in the NUnit GwpTest project.
- Basic error handling is done via built-in ASP NET Core functionality (input model validation - some basic checks based on the provided info - country code must be present, lines of business must be present, etc.). I did not error-handle the CSV data because I believe they are meant for mocking purposes, considering they're static and loaded at each app startup. I imagine that in a real-life scenario, there could be a service that periodically checks for new csv data to import into a DB, in which case validating them would make sense, but that's another story.
- Caching is implemented via MemoryCache in CachedGwpRepository, which decorates the basic GwpRepository.