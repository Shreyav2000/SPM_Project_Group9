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
        private List<Drug> Drugs { get; set; }
        public List<Complaint> Complaints { get; set; }
        private string m_patient { get; set; }
        private Timer timerObj;
        private SessionObject m_previousSession { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Patients = new List<Patient>();
            Drugs = new List<Drug>();
            Complaints = new List<Complaint>();
            m_previousSession = new SessionObject();
            if (Http.DefaultRequestHeaders == null)
            {
                string token = await sessionStorage.GetItemAsync<string>("token");
                var authHeader = new AuthenticationHeaderValue("Bearer", token);
                Http.DefaultRequestHeaders.Authorization = authHeader;
            }
            await GetDrugs();
            await GetComplaints();
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
        async Task GetComplaints()
        {
            ShowSpinner();
            try
            {
                Complaints = await Http.GetFromJsonAsync<List<Complaint>>("api/staff/complaints");

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            HideSpinner();
        }
        async Task GetDrugs()
        {
            ShowSpinner();
            try
            {
                Drugs = await Http.GetFromJsonAsync<List<Drug>>("api/drug/list");

                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            HideSpinner();
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
        async Task openDialog(Patient a_object)
        {
            m_previousSession = await dialogService.OpenAsync<SessionDialog>($"{a_object.Fname} {a_object.Lname}'s Session",
                              new Dictionary<string, object>() {
                                { "record", a_object },
                                { "drugs", Drugs},
                                {"complaints",Complaints },
                                  },
                              new DialogOptions() { Width = "1000px", Height = "800px", ShowClose = true });
        }
        /// <summary>
        /// Opens Review dialog to show a more detail view of the session
        /// </summary>
        /// <param name="a_object"></param>
        async Task openSessionDialog()
        {
            await dialogService.OpenAsync<Review>($"{m_previousSession.Patient.Fname} {m_previousSession.Patient.Lname}'s Session Review",
                            new Dictionary<string, object>() {
                                { "session", m_previousSession },
                                { "complaints", Complaints },
                                { "drugs", Drugs },
                                      },
                            new DialogOptions() { Width = "1000px", Height = "800px", ShowClose = true });

        }
    }
}
