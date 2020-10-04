using BaysBogey.Server.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BaysBogey.Tests
{
    public class DataTests
    {
        private ILogger Logger;
        private IDataService DataService;
        public IConfiguration Configuration;

        public DataTests(ITestOutputHelper output)
        {
            Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.TestOutput(output, Serilog.Events.LogEventLevel.Verbose)
                .CreateLogger()
                .ForContext<DataTests>();

            Configuration = new ConfigurationBuilder()
                // .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\BaysBogey.Server\appsettings.Development.json", false, true)
                .Build();

            DataService = new BaysBogeyDataService(Configuration, Logger);
        }
        [Fact]
        public async Task DataService_Gets_Course()
        {
            var course = await DataService.GetCourse("hi");
            Assert.NotNull(course);
        }
    }
}
