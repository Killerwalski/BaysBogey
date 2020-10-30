using AspNetMonsters.Blazor.Geolocation;
using BaysBogey.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BaysBogey.Client.Pages
{
    public partial class TrackRound
    {
        [Inject] protected LocationService LocationService { get; set; }
        [Inject] protected HttpClient Http { get; set; }
        protected List<Course> existingCourses { get; set; }
        protected Course loadedCourse { get; set; }
        protected Location location { get; set; }

        protected override async Task OnInitializedAsync()
        {
            existingCourses = await Http.GetFromJsonAsync<List<Course>>(Http.BaseAddress + "api/Course");
        }

        protected async Task ExistingCourseChosen(string id)
        {
            Debug.WriteLine("Existing course clicked");
            loadedCourse = await Http.GetFromJsonAsync<Course>(Http.BaseAddress + "api/Course/" + id);
            location = await LocationService.GetLocationAsync();
            StateHasChanged();
        }
    }
}
