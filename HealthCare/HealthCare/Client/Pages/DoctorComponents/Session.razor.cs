using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json.Linq;
using Radzen;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Timers;
using Timer = System.Timers.Timer;

namespace HealthCare.Client.Pages.DoctorComponents
{
    public partial class Session
    {
        protected bool IsVisible { get; set; }
        private List<Patient> Patients { get; set; }
        private string m_patient { get; set; }
        private Timer timerObj;
        protected override async Task OnInitializedAsync()
        {
            Patients = new List<Patient>();
            if (Http.DefaultRequestHeaders == null)
            {
                string token = await sessionStorage.GetItemAsync<string>("token");
                var authHeader = new AuthenticationHeaderValue("Bearer", token);
                Http.DefaultRequestHeaders.Authorization = authHeader;
            }

            await getData();
            timerObj = new Timer(1500);
            timerObj.Elapsed += OnUserFinish;
            timerObj.AutoReset = false;
        }
        private void OnValueChange(ChangeEventArgs e)
        {
            m_patient = e.Value.ToString();
            // remove previous one
            timerObj.Stop();
            // new timer
            timerObj.Start();
        }

        private async void OnUserFinish(Object source, ElapsedEventArgs e)
        {
            await getData();
        }
        /// <summary>
        /// Gets analytical data from the api
        /// </summary>
        /// <returns></returns>
        async Task getData()
        {
            Patients.Clear();
            ShowSpinner();
            try
            {
                if (!string.IsNullOrEmpty(m_patient))
                {
                    Patients = await Http.GetFromJsonAsync<List<Patient>>("api/patient/records/profile/list/" + m_patient);
                }
                else
                {
                    Patients = await Http.GetFromJsonAsync<List<Patient>>("api/patient/records/profile/list/100");
                }
              
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            HideSpinner();
        }
        /// <summary>
        /// Shows loading spinner
        /// </summary>
        public void ShowSpinner()
        {
            IsVisible = true;
            StateHasChanged();
        }
        /// <summary>
        /// Hides the loading spinner
        /// </summary>
        public void HideSpinner()
        {
            IsVisible = false;
            StateHasChanged();
        }
        /// <summary>
        /// Opens Review dialog to show a more detail view of the AttendanceObject
        /// </summary>
        /// <param name="a_object"></param>
        void openDialog(Patient a_object)
        {
            dialogService.Open<SessionDialog>($"{a_object.Fname} {a_object.Lname}'s Session",
                            new Dictionary<string, object>() { { "record", a_object
        }
                                      },
                            new DialogOptions() { Width = "1000px", Height = "800px", ShowClose = true });
        }
    }
}
