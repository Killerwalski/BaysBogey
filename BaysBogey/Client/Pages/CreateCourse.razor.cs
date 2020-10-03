using AspNetMonsters.Blazor.Geolocation;
using BaysBogey.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BaysBogey.Client.Pages
{
    public partial class CreateCourse
    {
        [Inject] protected LocationService LocationService { get; set; }
        [Inject] protected HttpClient Http { get; set; }
        protected Location location { get; set; }
        protected Course courseToCreate { get; set; }
        protected override async Task OnInitializedAsync()
        {
            courseToCreate = new Course();
            location = await LocationService.GetLocationAsync();
        }

        protected async Task HandleValidSubmit()
        {
            await Http.PostAsJsonAsync(Http.BaseAddress + "api/Course", courseToCreate);
            StateHasChanged();
        }
    }
}
