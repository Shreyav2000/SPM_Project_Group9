using System.Collections.Generic;
using HealthCare.Client.Shared.MenuItems;

namespace HealthCare.Client.Shared.MenuItems
{
    public class AdminNavService
    {
        public static IEnumerable<NavItems> AdminNavigation
        {
            get
            {
                return new NavItems[]
                {
                    new NavItems
                    {
                        Name = "Login",
                        Path = "/login",
                        Icon = "&#xf007"
                    },
                    new NavItems
                    {
                        Name = "Logout",
                        Path = "/logout",
                        Icon = "&#xf08b"
                    },
                    new NavItems
                    {
                        Name = "System Metrics",
                        Path = "/systemMetrics",
                        Icon = "&#xf080"
                    }
                };
            }
        }
    }
}
