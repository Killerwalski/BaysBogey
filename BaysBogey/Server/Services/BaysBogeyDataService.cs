using BaysBogey.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaysBogey.Server.Services
{
    public class BaysBogeyDataService : IDataService
    {
        public void AddCourse(Course course)
        {
            
        }

        public async Task<Course> GetCourse(string id)
        {
            return null;
        }

        public Task<IEnumerable<Course>> GetCourses()
        {
            throw new NotImplementedException();
        }
    }
}
