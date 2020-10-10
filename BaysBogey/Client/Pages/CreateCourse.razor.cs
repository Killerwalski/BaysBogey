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
using System.Diagnostics;
using Microsoft.AspNetCore.Components.Web;
using MongoDB.Bson.Serialization.Serializers;
using System.Security.Cryptography.X509Certificates;

namespace BaysBogey.Client.Pages
{
    public partial class CreateCourse
    {
        [Inject] protected LocationService LocationService { get; set; }
        [Inject] protected HttpClient Http { get; set; }
        [Parameter] public EventCallback CourseChosenCallback { get; set;}
        protected bool hideInitialCards { get; set;}
        protected bool hideCreateCourseForm { get; set;}
        protected bool hideLoadCourseForm { get; set; }
        protected bool hideModifyCourseForm { get; set;}
        protected Location location { get; set; }
        protected Course courseToCreate { get; set; }
        protected Course loadedCourse { get; set; }
        protected List<Course> existingCourses { get; set;}
        protected Hole currentHole { get; set; }
        protected int currentHolePar { get; set; }
        protected int selectedHoleNumber { get; set; }
        protected Location currentTeeBoxLocation { get; set; }
        protected Location currentPinLocation { get; set; }
        protected string selectedTeeBoxColor { get; set;}
        protected override async Task OnInitializedAsync()
        {
            hideCreateCourseForm = true;
            hideModifyCourseForm = true;
            hideLoadCourseForm = true;

            courseToCreate = new Course();
            location = await LocationService.GetLocationAsync();
            existingCourses = await Http.GetFromJsonAsync<List<Course>>(Http.BaseAddress + "api/Course");
        }

        protected async Task HandleValidSubmit()
        {
            courseToCreate.LastUpdated = DateTime.Now;
            await Http.PostAsJsonAsync(Http.BaseAddress + "api/Course", courseToCreate);
            existingCourses = await Http.GetFromJsonAsync<List<Course>>(Http.BaseAddress + "api/Course");
            loadedCourse = courseToCreate;
            hideCreateCourseForm = true;
            hideModifyCourseForm = false;
            StateHasChanged();
        }

        protected async Task CreateCourseDialog()
        {
            hideInitialCards = true;
            hideCreateCourseForm = false;
            StateHasChanged();
        }

        protected async Task LoadCourseDialog()
        {
            hideInitialCards = true;
            hideLoadCourseForm = false;
            StateHasChanged();
        }

        protected async Task ExistingCourseChosen(string id)
        {
            Debug.WriteLine("Existing cours eclicked");
            loadedCourse = await Http.GetFromJsonAsync<Course>(Http.BaseAddress + "api/Course/" + id);
            hideLoadCourseForm = true;
            hideModifyCourseForm = false;
            StateHasChanged();
        }

        protected async Task AddNewHole()
        {
            currentHole = new Hole();
            currentHole.Par = 3;
            currentHole.Number = loadedCourse.Holes.Count + 1;
            currentHole.TeeBoxes = new Dictionary<string, Location>();
        }

        protected async Task SaveHole()
        {
            currentHole.Par = currentHolePar;


            // Upsert hole to course
            var existingHole = loadedCourse.Holes.Where(x => x.Number == currentHole.Number).FirstOrDefault();
            if (existingHole != null)
                loadedCourse.Holes.Remove(existingHole);

            loadedCourse.Holes.Add(currentHole);
            var result = await Http.PutAsJsonAsync(Http.BaseAddress + "api/Course/" + loadedCourse.Id, loadedCourse);
            Debug.WriteLine(result.StatusCode);

        }

        protected async Task HoleSelected()
        {
            currentHole = loadedCourse.Holes.Where(x => x.Number == selectedHoleNumber).FirstOrDefault();

            StateHasChanged();
        }

        protected async Task SetTeeBoxLocation()
        {
            currentTeeBoxLocation = await LocationService.GetLocationAsync();
            if (!currentHole.TeeBoxes.ContainsKey(selectedTeeBoxColor))
                currentHole.TeeBoxes.Add(selectedTeeBoxColor, currentTeeBoxLocation);
            else
                currentHole.TeeBoxes[selectedTeeBoxColor] = currentTeeBoxLocation;
        }

        protected async Task SetPinLocation()
        {
            currentPinLocation = await LocationService.GetLocationAsync();
            currentHole.Pin = currentPinLocation;
        }
    }
}
