using KNUST_Medical_Refund.Shared;
using KNUST_Medical_Refund.Shared.Models;
using Newtonsoft.Json;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Refund.Client.Pages.Reports
{
    public partial class Lists
    {
        public DateTime selectedDateFrom, selectedDateTo, selectedDate;
        private List<Request> dataOriginal;
        private List<Request> data;
        public AuthUser user;
        string threshold = "Threshold", status = "Status", filterKey;
        int thresholdValue = 3, statusValue;
        public List<ReportType> types = new List<ReportType>
        {
            new ReportType{
                TypeName = "Custom Range Reports",
                TypeValue = 1
            },
            new ReportType{
                TypeName = "Daily Reports",
                TypeValue = 2
            },
            new ReportType{
                TypeName = "Monthly Reports",
                TypeValue = 3
            },
        };
        public int selectedType = 1;
        int Type = 1;
        protected override async Task OnInitializedAsync()
        {
            selectedDateTo = DateTime.Now;
            selectedDateFrom = DateTime.Now.AddYears(-1);
            selectedDate = DateTime.Now;

            string json = await sessionStorage.GetItemAsync<string>("userCred");
            if (!String.IsNullOrEmpty(json))
            {
                user = JsonConvert.DeserializeObject<AuthUser>(json);
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
            SpinnerService.OnShow += ShowSpinner;
            SpinnerService.OnHide += HideSpinner;
            data = new List<Request>();
            await getData();
        }
        string FormatAsUSD(object value)
        {
            string newvalue = ((double)value).ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
            return "GH¢ " + newvalue.Substring(1);
        }
        protected bool IsVisible { get; set; }
        async Task getData()
        {
            selectedType = Type;
            ShowSpinner();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "api/allrequests/filter?fmonth=" + selectedDateFrom.Month + "&fyear=" + selectedDateFrom.Year + "&tmonth=" + selectedDateTo.Month + "&tyear=" + selectedDateTo.Year);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.AccessToken);
                using var httpResponse = await Http.SendAsync(request);
                dataOriginal = await httpResponse.Content.ReadFromJsonAsync<List<Request>>();
                data = dataOriginal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            HideSpinner();
        }
        public void ShowSpinner()
        {
            IsVisible = true;
            StateHasChanged();
        }
        public void HideSpinner()
        {
            IsVisible = false;
            StateHasChanged();
        }
        async void onThreshold(RadzenSplitButtonItem item)
        {
            if (item != null)
            {
                switch (int.Parse(item.Value))
                {
                    case 1:
                        thresholdValue = 1;
                        threshold = item.Text;
                        data = dataOriginal.Where(r => r.RequestedAmount < 2000).ToList();
                        if (statusValue == 1)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending").ToList();
                        }
                        else if (statusValue == 2)
                        {
                            data = data.Where(s => s.Status.ToLower() == "approved").ToList();
                        }
                        else if (statusValue == 3)
                        {
                            data = data.Where(s => s.Status.ToLower() == "declined").ToList();
                        }
                        else if (statusValue == 4)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending preapproval").ToList();
                        }
                        break;
                    case 2:
                        thresholdValue = 2;
                        threshold = item.Text;
                        data = dataOriginal.Where(r => r.RequestedAmount >= 2000).ToList();
                        if (statusValue == 1)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending").ToList();
                        }
                        else if (statusValue == 2)
                        {
                            data = data.Where(s => s.Status.ToLower() == "approved").ToList();
                        }
                        else if (statusValue == 3)
                        {
                            data = data.Where(s => s.Status.ToLower() == "declined").ToList();
                        }
                        else if (statusValue == 4)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending preapproval").ToList();
                        }
                        break;
                    case 3:
                        threshold = item.Text;
                        thresholdValue = 3;
                        data = dataOriginal;
                        if (statusValue == 1)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending").ToList();
                        }
                        else if (statusValue == 2)
                        {
                            data = data.Where(s => s.Status.ToLower() == "approved").ToList();
                        }
                        else if (statusValue == 3)
                        {
                            data = data.Where(s => s.Status.ToLower() == "declined").ToList();
                        }
                        else if (statusValue == 4)
                        {
                            data = data.Where(s => s.Status.ToLower() == "pending preapproval").ToList();
                        }
                        break;
                }
            }
            if (selectedType == 3) await getMonthly();
            StateHasChanged();
        }
        void onStatus(RadzenSplitButtonItem item)
        {
            if (item != null)
            {
                switch (int.Parse(item.Value))
                {
                    case 1:
                        statusValue = 1;
                        status = item.Text;
                        data = dataOriginal.Where(s => s.Status.ToLower() == "pending review").ToList();
                        if (thresholdValue == 1)
                        {
                            data = data.Where(r => r.RequestedAmount < 2000).ToList();
                        }
                        else if (thresholdValue == 2)
                        {
                            data = data.Where(r => r.RequestedAmount >= 2000).ToList();
                        }

                        break;
                    case 2:
                        statusValue = 2;
                        status = item.Text;
                        data = dataOriginal.Where(s => s.Status.ToLower() == "approved").ToList();
                        if (thresholdValue == 1)
                        {
                            data = data.Where(r => r.RequestedAmount < 2000).ToList();
                        }
                        else if (thresholdValue == 2)
                        {
                            data = data.Where(r => r.RequestedAmount >= 2000).ToList();
                        }
                        break;
                    case 3:
                        statusValue = 3;
                        status = item.Text;
                        data = dataOriginal.Where(s => s.Status.ToLower() == "declined").ToList();
                        if (thresholdValue == 1)
                        {
                            data = data.Where(r => r.RequestedAmount < 2000).ToList();
                        }
                        else if (thresholdValue == 2)
                        {
                            data = data.Where(r => r.RequestedAmount >= 2000).ToList();
                        }
                        break;
                    case 4:
                        statusValue = 4;
                        status = item.Text;
                        data = dataOriginal.Where(s => s.Status.ToLower() == "pending preapproval").ToList();
                        if (thresholdValue == 1)
                        {
                            data = data.Where(r => r.RequestedAmount < 2000).ToList();
                        }
                        else if (thresholdValue == 2)
                        {
                            data = data.Where(r => r.RequestedAmount >= 2000).ToList();
                        }
                        break;
                    case 5:
                        statusValue = 4;
                        status = "Status";
                        data = dataOriginal;
                        if (thresholdValue == 1)
                        {
                            data = data.Where(r => r.RequestedAmount < 2000).ToList();
                        }
                        else if (thresholdValue == 2)
                        {
                            data = data.Where(r => r.RequestedAmount >= 2000).ToList();
                        }
                        break;
                }
            }
            StateHasChanged();
        }
        void filterHospitalNumber(string value)
        {
            filterKey = value;
            data = dataOriginal.Where(c => c.claimant.HospitalNo.ToLower().Replace("-", "").Contains(filterKey.ToLower().Replace("-", ""))).ToList();
            if (statusValue == 1)
            {
                data = data.Where(s => s.Status.ToLower() == "pending review").ToList();
            }
            else if (statusValue == 2)
            {
                data = data.Where(s => s.Status.ToLower() == "approved").ToList();
            }
            else if (statusValue == 3)
            {
                data = data.Where(s => s.Status.ToLower() == "declined").ToList();
            }
            else if (statusValue == 4)
            {
                data = data.Where(s => s.Status.ToLower() == "pending preapproval").ToList();
            }

            if (thresholdValue == 1)
            {
                data = data.Where(r => r.RequestedAmount < 2000).ToList();
            }
            else if (thresholdValue == 2)
            {
                data = data.Where(r => r.RequestedAmount >= 2000).ToList();
            }
            StateHasChanged();
        }
        async Task getMonthly()
        {
            selectedType = Type;
            ShowSpinner();
            try
            {
                HttpRequestMessage request;
                if (thresholdValue == 3)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "api/allrequests/monthly-all");
                }
                else if (thresholdValue == 1)
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "api/allrequests/monthly-less");
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Post, "api/allrequests/monthly-over");
                }
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.AccessToken);
                request.Content = new StringContent(JsonConvert.SerializeObject(selectedDate), Encoding.UTF8, "application/json");
                using var httpResponse = await Http.SendAsync(request);
                dataOriginal = await httpResponse.Content.ReadFromJsonAsync<List<Request>>();
                data = dataOriginal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            HideSpinner();
        }
        async Task getDaily()
        {
            selectedType = Type;
            ShowSpinner();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "api/allrequests/daily");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", user.AccessToken);
                request.Content = new StringContent(JsonConvert.SerializeObject(selectedDate), Encoding.UTF8, "application/json");
                using var httpResponse = await Http.SendAsync(request);
                dataOriginal = await httpResponse.Content.ReadFromJsonAsync<List<Request>>();
                data = dataOriginal;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            HideSpinner();
        }
        async void onchange()
        {
            if (Type == 1)
            {
                await getData();
            }
            else if (Type == 2)
            {
                await getDaily();
            }
            else if (Type == 3)
            {
                await getMonthly();
            }
        }
    }
}
