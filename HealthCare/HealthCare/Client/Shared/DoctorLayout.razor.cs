
using HealthCare.Client.Shared.MenuItems;

namespace HealthCare.Client.Shared
{
    public partial class DoctorLayout
    {
        private List<NavItems> m_NavMenuItems {get;set;}

        public DoctorLayout()
        {
            
        }
        protected override void OnInitialized()
        {
            m_NavMenuItems = DoctorNavService.DoctorsNavigation.ToList();
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }
    }
}
