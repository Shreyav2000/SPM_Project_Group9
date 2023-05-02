using HealthCare.Shared.Objects;
using Newtonsoft.Json;
using Radzen;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthCare.Client.Pages.Director_Components
{
    /// <summary>
    /// This is the Dashboard Page of a director here they get to see list of user consultations 
    /// of actions they performed over a specified period
    /// </summary>
    public partial class Dashboard
    {
        //Define a partial class called dashboard
        List<UserConsults> UserConsultations { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Define a property to store user consultations
            UserConsultations = new List<UserConsults>();
            //if (Http.DefaultRequestHeaders == null)
            //{
            //retrieve the authentication token from the string storage
                string token = await sessionStorage.GetItemAsync<string>("token");
                var authHeader = new AuthenticationHeaderValue("Bearer", token);
                Http.DefaultRequestHeaders.Authorization = authHeader;
            //}
            //call the get data method to retrieve user consultation data
            await getData();
        }
        async Task getData()
        {
            HttpResponseMessage response = await Http.GetAsync("api/records/patientConsults");
            string responseContent = await response.Content.ReadAsStringAsync();
            UserConsultations = JsonConvert.DeserializeObject<List<UserConsults>>(responseContent);
        }
    }
}