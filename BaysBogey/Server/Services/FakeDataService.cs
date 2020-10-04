using AspNetMonsters.Blazor.Geolocation;
using BaysBogey.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaysBogey.Server.Services
{
    public class FakeDataService : IDataService
    {
        public List<Course> Courses;

        public FakeDataService()
        {
            Courses = new List<Course>();
        }
        public Task<Course> GetCourse(string id)
        {
            var fakeHole = new Hole();
            fakeHole.Number = 1;
            fakeHole.TeeBoxes = new Dictionary<string, Location>();
            fakeHole.TeeBoxes.Add("Blue", new Location()
            {
                Latitude = 50,
                Longitude = 50
            });

            fakeHole.Pin = new Location()
            {
                Latitude = 51,
                Longitude = 51
            };

            var course = new Course();
            course.Name = "Fake Course";
            course.Holes = new List<Hole>() {  fakeHole };

            return Task.FromResult(course);
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await Task.FromResult(Courses);
        }

        public void AddCourse(Course course)
        {
            Courses.Add(course);
        }

        public void UpdateHole(string courseId, Hole hole)
        {
            var course = Courses.Where(x => x.Id == courseId).FirstOrDefault();
            if (course == null)
                throw new Exception("Attempted to Update Course that does not exist");

            if (course.Holes.Where(x => x.Number == hole.Number).FirstOrDefault() != null)
            {
                var holeToRemove = course.Holes.Where(x => x.Number == hole.Number).FirstOrDefault();
                course.Holes.Remove(holeToRemove);
                course.Holes.Add(hole);
            }
            else
            {
                course.Holes.Add(hole);
            }
        }

    }
}
