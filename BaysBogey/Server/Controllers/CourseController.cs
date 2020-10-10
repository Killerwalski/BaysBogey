using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaysBogey.Server.Services;
using BaysBogey.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaysBogey.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IDataService DataService;
        public CourseController(IDataService dataService)
        {
            DataService = dataService;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await DataService.GetCourses();
            return courses;
        }

        // GET api/<CourseController>/abc
        [HttpGet("{id}")]
        public async Task<Course> Get(string id)
        {
            return await DataService.GetCourse(id);
        }

        // POST api/<CourseController>
        [HttpPost]
        public void Post([FromBody] Course course)
        {
            DataService.AddCourse(course);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(string id, [FromBody] Course course)
        {
            await DataService.UpdateCourse(course);
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
