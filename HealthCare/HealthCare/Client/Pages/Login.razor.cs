using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Radzen;
using Microsoft.AspNetCore.Components.Web;
using HealthCare.Shared.Models;
using System.Net.Http.Headers;

namespace HealthCare.Client.Pages
{
    public partial class Login
    {
        int userCategory = 1;
        string email, password;
        protected bool IsVisible { get; set; }
        NotificationMessage notificationMessage = new NotificationMessage();
        User authenticatedUser = new User();
       
        /// <summary>
        /// Determines the type of authentication 
        /// For example, employee's authentication methods are handled differently from a patient trying
        /// to login
        /// </summary>
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
        /// <summary>
        /// Handles the click or press event of the enter key
        /// </summary>
        /// <param name="e"></param>
        public void Enter(KeyboardEventArgs e)
        {
            if (e.Code == "Enter" || e.Code == "NumpadEnter")
            {
                loginMethod();
            }
        }
        /// <summary>
        /// Authenticates the user and determines the type of page layout to show
        /// </summary>
        /// <returns></returns>
        async Task pagaAuth()
        {

            ShowSpinner();
            try
            {
                authenticatedUser = new User
                {
                    UserId = 0,
                    Role = new UserRole(),
                    RoleId = 0,
                    Username = email,
                    Password = password
                };
                var responseMain = await Http.PostAsJsonAsync("api/Tokens/authenticate", authenticatedUser);
                if (responseMain.IsSuccessStatusCode)
                {
                    string token = await responseMain.Content.ReadAsStringAsync();
                    await sessionStorage.SetItemAsync<string>("token", token);
                    var authHeader = new AuthenticationHeaderValue("Bearer", token);
                    Http.DefaultRequestHeaders.Authorization = authHeader;
                    int? role = tokenService.GetRoleFromToken($"Bearer {token}");
                    if (role != null)
                    {
                        switch (role)
                        {
                            case 1://Admin
                                //Todo implement Navigation to Admin Dashboard
                                NavigationManager.NavigateTo("/admin/dashboard");
                                break;
                            case 2://Doctor
                                NavigationManager.NavigateTo("/doctor/dashboard");
                                break;
                            case 3://Nurse
                                //Todo implement Navigation to Nurse Dashboard
                                break;
                            case 6:
                                NavigationManager.NavigateTo("/director/dashboard");
                                break;
                        }
                    }

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
        /// <summary>
        /// Shows the loading spinner
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
        /// Shows a notification message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        async Task ShowNotification(NotificationMessage message)
        {
            notificationService.Notify(message);

            await InvokeAsync(() => { StateHasChanged(); });
        }
        /// <summary>
        /// Sets the username value
        /// </summary>
        /// <param name="value"></param>
        void usernameInput(string value)
        {
            email = value;
            StateHasChanged();
        }
        /// <summary>
        /// Sets the password value
        /// </summary>
        /// <param name="value"></param>
        void passwordInput(string value)
        {
            password = value;
            StateHasChanged();
        }
    }
}
