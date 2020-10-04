using BaysBogey.Shared;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaysBogey.Server.Services
{
    public class BaysBogeyDataService : IDataService
    {
        private IConfiguration Configuration;
        private IMongoCollection<Course> CourseCollection;
        public ILogger Logger;

        public BaysBogeyDataService(IConfiguration configuration, ILogger logger)
        {
            Configuration = configuration;
            Logger = logger;

            var connectionString = Configuration["DatabaseSettings:AtlasConnectionString"];
            var databaseName = Configuration["DatabaseSettings:DatabaseName"];
            var collectionName = Configuration["DatabaseSettings:CollectionName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            CourseCollection = database.GetCollection<Course>(collectionName);
        }
        public async Task AddCourse(Course course)
        {
            await CourseCollection.InsertOneAsync(course);
        }

        public async Task<Course> GetCourse(string id)
        {
            var filter = Builders<Course>.Filter.Eq(c => c.Id, id);
            var course = await CourseCollection.FindAsync(filter);
            return await course.FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Course>> GetCourses()
        {
            throw new NotImplementedException();
        }
    }
}
