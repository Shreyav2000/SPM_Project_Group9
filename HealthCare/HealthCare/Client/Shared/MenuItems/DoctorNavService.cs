namespace HealthCare.Client.Shared.MenuItems
{
    public class DoctorNavService
    {
        /// <summary>
        /// Returns the list of Navigation Items for the Doctors Layout
        /// </summary>
        public static IEnumerable<NavItems> DoctorsNavigation
        {
            get
            {
                return new NavItems[] {
        new NavItems()
        {
            Name = "Dashboard",
            Updated = true,
            Path = "/doctor/dashboard",
            Icon = "&#xe871"
        },
        new NavItems
        {
            Name = "Session",
            Path = "/doctor/session",
            Icon = "&#xe037"
        },
        new NavItems
        {
            Name = "Patients",
            Path = "/patients",
            Icon = "&#xe7fb"
        },
        new NavItems
        {
            Name = "Drugs",
            Path = "/drugList",
            Icon = "&#xf033"
        },

    };
            }
        }
    }
}