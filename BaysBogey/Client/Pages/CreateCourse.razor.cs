using AspNetMonsters.Blazor.Geolocation;
using BaysBogey.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazorise;

namespace BaysBogey.Client.Pages
{
    public partial class CreateCourse
    {
        [Inject] protected LocationService LocationService { get; set; }
        [Inject] protected HttpClient Http { get; set; }
        protected bool hideInitialCards { get; set;}
        protected bool hideCreateCourseForm { get; set;}
        protected Location location { get; set; }
        protected Course courseToCreate { get; set; }
        protected Course loadedCourse { get; set; }
        protected List<Course> existingCourses { get; set;}
        protected override async Task OnInitializedAsync()
        {
            hideCreateCourseForm = true;

            courseToCreate = new Course();
            location = await LocationService.GetLocationAsync();
            existingCourses = await Http.GetFromJsonAsync<List<Course>>(Http.BaseAddress + "api/Course");
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync(Http.BaseAddress + "api/Course", courseToCreate);
            existingCourses = await Http.GetFromJsonAsync<List<Course>>(Http.BaseAddress + "api/Course");
            StateHasChanged();
        }

        protected async Task CreateCourseDialog()
        {
            hideInitialCards = true;
            hideCreateCourseForm = false;
            StateHasChanged();
        }
    }
}
