﻿using BaysBogey.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaysBogey.Server.Services
{
    public interface IDataService
    {
        public Task<Course> GetCourse(string id);
        Task AddCourse(Course course);
        Task UpdateCourse(Course course);
        Task<IEnumerable<Course>> GetCourses();
    }
}
