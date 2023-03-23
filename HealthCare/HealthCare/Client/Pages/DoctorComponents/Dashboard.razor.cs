using HealthCare.Shared.Models;
using HealthCare.Shared.Objects;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace HealthCare.Client.Pages.DoctorComponents
{
    public partial class Dashboard
    {
        private bool m_fetchingData;
        private double m_closeRatio = 0;
        private int m_currentPage = 0;
        private int m_totalPages = 0;
        private string? m_error;
        private DateTime? m_period;
        private List<AttendanceObject> Attendances { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Attendances = new List<AttendanceObject>();
            m_fetchingData = false;
            m_period = DateTime.Now;
            //string token = await sessionStorage.GetItemAsync<string>("token");
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkMWIwMWU4MC0xNjg5LTRmNDMtOGYzYy01NDE4YWNhNjhjYWQiLCJpYXQiOiIyMy8wMy8yMDIzIDE6NDU6NDAgcG0iLCJ1c2VyaWQiOiI4Iiwicm9sZSI6IjEiLCJ1c2VybmFtZSI6InRlc3REb2MiLCJleHAiOjE2ODA3ODg3NDAsImlzcyI6IkFVVEggU0VSVkVSIiwiYXVkIjoiSGVhbHRoIE1hbmFnZW1lbnQifQ.N-4sLhmw4382KrFd9QKytdeSEhmTn0--vteb1w2Iots";
            var authHeader = new AuthenticationHeaderValue("Bearer", token);
            Http.DefaultRequestHeaders.Authorization = authHeader;
            await getData();
        }
       
        async Task getData()
        {
            try
            {
                
                var firstDayOfMonth = new DateTime(m_period.Value.Date.Year, m_period.Value.Date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
                Attendances = await Http.GetFromJsonAsync<List<AttendanceObject>>("api/patient/records/doctor/" + firstDayOfMonth.ToString("yyyy-MM-dd") + "/" + lastDayOfMonth.ToString("yyyy-MM-dd"));
                StateHasChanged();
            }
            catch (Exception ex){ 
            Console.WriteLine(ex.Message);
            }
        }
    }
}