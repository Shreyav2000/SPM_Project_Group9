
using HealthCare.Client.Shared.MenuItems;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;

namespace HealthCare.Client.Shared
{
    public partial class DoctorLayout
    {
        private List<NavItems> m_NavMenuItems {get;set;}

        protected override async Task OnInitializedAsync()
        {
            m_NavMenuItems = DoctorNavService.DoctorsNavigation.ToList();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            if (Http.DefaultRequestHeaders == null)
            {
                string token = await sessionStorage.GetItemAsync<string>("token");
                var authHeader = new AuthenticationHeaderValue("Bearer", token);
                Http.DefaultRequestHeaders.Authorization = authHeader;
            }
        }
    }
}
