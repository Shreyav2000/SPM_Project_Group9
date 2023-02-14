using KNUST_Medical_Refund.Shared.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Refund.Client.Pages.Verification
{
    public partial class Transcribed
    {
        [Parameter]
        public string documentGUID { get; set; }
        [Parameter]
        public List<DrugPrescriptionFrequency> frequencies { get; set; }
        [Parameter]
        public List<DrugRouteOfAdministration> route { get; set; }
        [Parameter]
        public List<DrugUsageForm> units { get; set; }
        protected bool IsVisible { get; set; }
        List<PharmacyTranscribe> transcribes = new List<PharmacyTranscribe>();
        public AuthUser user;
        protected override async Task OnInitializedAsync()
        {
            string json = await sessionStorage.GetItemAsync<string>("userCred");
            if (!String.IsNullOrEmpty(json))
            {
                user = JsonConvert.DeserializeObject<AuthUser>(json);
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
            await base.OnInitializedAsync();
            
            await GetData();

        }
      public void ShowSpinner()
        {
            IsVisible = true;
            StateHasChanged();
        }
        public void HideSpinner()
        {
            IsVisible = false;
            StateHasChanged();
        }
        async Task GetData()
        {
            ShowSpinner();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"Transcribe/filter?documentGUID={documentGUID}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.AccessToken);
                using var response = await Http.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    this.transcribes = await response.Content.ReadFromJsonAsync<List<PharmacyTranscribe>>();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            HideSpinner();
        }
    }
}
