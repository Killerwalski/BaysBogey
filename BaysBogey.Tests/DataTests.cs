using BaysBogey.Server.Services;
using Serilog;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace BaysBogey.Tests
{
    public class DataTests
    {
        private ILogger Logger;
        private IDataService DataService;
        public DataTests(ITestOutputHelper output)
        {
            DataService = new FakeDataService();
        }
        [Fact]
        public async Task FakeDataService_Gets_Course()
        {
            var course = await DataService.GetCourse("hi");
            Assert.NotNull(course);
        }
    }
}
