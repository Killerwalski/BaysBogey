using AspNetMonsters.Blazor.Geolocation;
using BaysBogey.Server.Services;
using BaysBogey.Shared;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                .AddJsonFile("appsettings.Development.json", false, true)
                //.AddJsonFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\BaysBogey\Server\appsettings.Development.json", false, true)
                .Build();

            DataService = new BaysBogeyDataService(Configuration, Logger);
        }

        //TODO
        // Update Course
        [Fact]
        public async Task DataService_Updates_Course()
        {
            var course = await DataService.GetCourse("4c7065ed-73fe-465b-9900-1e14292ee2f0");

            var hole = course.Holes.Where(h => h.Number == 1).FirstOrDefault();
            hole.TeeBoxes.Add("Yellow", new Location() { Latitude = 49, Longitude = 49, Accuracy = 1 });
            
            await DataService.UpdateCourse(course);
        }
        // - Delete holes in course
        // - Add Tee Boxes
        // - Update Pin Location


        // Delete Course



        [Fact]
        public async Task DataService_Gets_Course()
        {
            var course = await DataService.GetCourse("4c7065ed-73fe-465b-9900-1e14292ee2f0");
            Assert.NotNull(course);
            Logger.Information("Found Course: " + course.Name);
        }

        [Fact]
        public async Task DataService_Inserts_Course()
        {
            var course = new Course()
            {
                Name = "Hog Neck Executive",
                Holes = new List<Hole>()
            };
            course.Id = Guid.NewGuid().ToString();
            var hole = new Hole();
            hole.Number = 1;
            hole.TeeBoxes = new Dictionary<string, Location>();
            hole.TeeBoxes.Add("Blue", new Location() { Latitude = 50, Longitude = 50, Accuracy = 1 });
            hole.Pin = new Location() { Latitude = 51, Longitude = 51, Accuracy = 1 };
            course.Holes.Add(hole);

            await DataService.AddCourse(course);
        }
    }
}