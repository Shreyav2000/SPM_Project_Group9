using HealthCare.Shared.Objects;
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
        private List<Cases> TotalCases { get;set; }

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
            catch (Exception ex){ 
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
        void openDialog(AttendanceObject a_object)
        {
            dialogService.Open<Review>($"{a_object.PatientName}'s Record Review",
                            new Dictionary<string, object>() { { "record", a_object
        }
                                      },
                            new DialogOptions() { Width = "1000px", Height = "800px", ShowClose = true });
        }
    }
}