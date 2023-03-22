using HealthCare.Shared.Models;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

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

        protected override void OnInitialized()
        {
            m_fetchingData = false;
            m_period = DateTime.Now;
            base.OnInitialized();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }
    }
}
