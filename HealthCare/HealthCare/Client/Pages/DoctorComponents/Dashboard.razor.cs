using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using Microsoft.Diagnostics.Tracing.Parsers.Clr;
using Radzen;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HealthCare.Client.Pages.DoctorComponents
{
    /// <summary>
    /// This is the Dashboard Page of a doctor here they get to see analysis or summary 
    /// of actions they performed over a specified period
    /// </summary>
    public partial class Dashboard
    {
        private bool m_fetchingData;
        private double m_closeRatio = 0;
        private int m_currentPage = 0;
        private int m_totalPages = 0;
        private string? m_error;
        private DateTime? m_period;
        protected bool IsVisible { get; set; }
        private List<AttendanceObject> Attendances { get; set; }
        private List<TopPrescribedDrug> TopPrescribedDrugs { get; set; }
        private List<Cases> Cases { get; set; }
        private List<Cases> TotalCases { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Attendances = new List<AttendanceObject>();
            Cases = new List<Cases>();
            TopPrescribedDrugs = new();
            TotalCases = new();
            m_fetchingData = false;
            m_period = DateTime.Now;

            if (Http.DefaultRequestHeaders == null)
            {
                string token = await sessionStorage.GetItemAsync<string>("token");
                var authHeader = new AuthenticationHeaderValue("Bearer", token);
                Http.DefaultRequestHeaders.Authorization = authHeader;
            }
            await getData();
        }
        /// <summary>
        /// Gets analytical data from the api
        /// </summary>
        /// <returns></returns>
        async Task getData()
        {
            ShowSpinner();
            try
            {

                var firstDayOfMonth = new DateTime(m_period.Value.Date.Year, m_period.Value.Date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
                var analysis = await Http.GetFromJsonAsync<DoctorAnalysis>("api/analysis/performance/" + firstDayOfMonth.ToString("yyyy-MM-dd") + "/" + lastDayOfMonth.ToString("yyyy-MM-dd"));
                Attendances = analysis.AttendanceObjects;
                Cases = analysis.TopCases;
                TopPrescribedDrugs = analysis.TopPrescribedDrugs;
                TotalCases = analysis.TotalCases;
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
        /// Gets analytical data from the api
        /// </summary>
        /// <returns></returns>
        async Task<SessionObject> getSessionData(int a_consultId)
        {
            ShowSpinner();
            try
            {
                SessionObject session = await Http.GetFromJsonAsync<SessionObject>("api/staff/records/session/" + a_consultId.ToString());
                HideSpinner();
                return session;

            }
            catch (Exception ex)
            {
                HideSpinner();
                return null;
            }

        }
        async Task<List<Complaint>> GetComplaints()
        {
            ShowSpinner();
            try
            {
                var complaints = await Http.GetFromJsonAsync<List<Complaint>>("api/staff/complaints");
                HideSpinner();
                return complaints;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HideSpinner();
                return null;
            }

        }
        async Task<List<Drug>> GetDrugs()
        {
            ShowSpinner();
            try
            {
                var drugs = await Http.GetFromJsonAsync<List<Drug>>("api/drug/list");

                HideSpinner();
                return drugs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                HideSpinner();
                return null;
            }

        }
        /// <summary>
        /// Opens Review dialog to show a more detail view of the AttendanceObject
        /// </summary>
        /// <param name="a_object"></param>
        async Task openDialog(AttendanceObject a_object)
        {
            SessionObject session = await getSessionData(a_object.ConsultId!.Value);
            List<Drug> drugs = await GetDrugs();
            List<Complaint> complaints = await GetComplaints();
            await dialogService.OpenAsync<Review>($"{a_object.PatientName}'s Session Review",
                            new Dictionary<string, object>() {
                                { "session", session },
                                { "complaints", complaints },
                                { "drugs", drugs },
                                      },
                            new DialogOptions() { Width = "1000px", Height = "800px", ShowClose = true });

            
                await getData();
        }
    }
}