using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Radzen;
using Microsoft.AspNetCore.Components.Web;
using HealthCare.Shared.Models;

namespace HealthCare.Client.Pages
{
    public partial class Login
    {
        int userCategory = 1;
        string email, password;
        protected bool IsVisible { get; set; }
        NotificationMessage notificationMessage = new NotificationMessage();
        User authenticatedUser = new User();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //string json = await sessionStorage.GetItemAsync<string>("userInfo");
            //if (!String.IsNullOrEmpty(json))
            //{
            //    CurrentUser currentUser = JsonConvert.DeserializeObject<CurrentUser>(json);
            //    if (currentUser.loginType == 2)
            //    {
            //        NavigationManager.NavigateTo("/dashboard");
            //    }
            //    else
            //    {
            //        NavigationManager.NavigateTo("/summary");
            //    }
            //}
        }

        async void loginMethod()
        {
            if (userCategory == 1)
            {
               await pagaAuth();
            }
            else
            {

            }
        }
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                loginMethod();
            }
        }
        async Task pagaAuth()
        {

            ShowSpinner();
            try
            {
                authenticatedUser = new User
                {
                    Username = email,
                    Password = password
                };
                var responseMain = await Http.PostAsJsonAsync("authenticate/paga-user", authenticatedUser);
                if (responseMain.IsSuccessStatusCode)
                {
                    authenticatedUser = await responseMain.Content.ReadFromJsonAsync<User>();

                    //string json = JsonConvert.SerializeObject(new CurrentUser
                    //{
                    //    accesstoken = authenticatedUser.AccessToken,
                    //    loginType = authenticatedUser.type == "transcriber" ? 3 : 1,
                    //    category = authenticatedUser.type == "transcriber" ? "pharmacy" : "admin",
                    //    fullname = authenticatedUser.Fullname,
                    //    email = authenticatedUser.email,
                    //});
                    //await sessionStorage.SetItemAsync<string>("userInfo", json);
                    //NavigationManager.NavigateTo("/summary");
                }
                else if (responseMain.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    notificationMessage.Detail = "Please make sure you have internet accesss and your login credentials are correct !!! ";
                    notificationMessage.Summary = "Authentication Error";
                    notificationMessage.Severity = NotificationSeverity.Error;
                    notificationMessage.Duration = 5000;

                    await ShowNotification(notificationMessage);
                }
            }
            catch
            {
                notificationMessage.Detail = "Please make sure you have internet accesss and your login credentials are correct !!! ";
                notificationMessage.Summary = "Authentication Error";
                notificationMessage.Severity = NotificationSeverity.Error;
                notificationMessage.Duration = 5000;

                await ShowNotification(notificationMessage);
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
        async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);

            await InvokeAsync(() => { StateHasChanged(); });
        }
        void usernameInput(string value)
        {
            email = value;
            StateHasChanged();
        }
        void passwordInput(string value)
        {
            password = value;
            StateHasChanged();
        }
    }
}
