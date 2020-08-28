using PlannerApp.Client.Models;
using PlannerApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PlannerApp.Client.Pages.Auth
{
    public partial class Login
    {
        LoginRequest model = new LoginRequest();

        public bool isBusy = false;
        public string message = string.Empty;

        Models.AlertMessageType messageType = Models.AlertMessageType.Success;

        Dictionary<string, string> userInfo = new Dictionary<string, string>();

        public async Task LogInUser()
        {
            isBusy = true;
            var result = await authService.LogInUserAsync(model);

            if (result.IsSuccess)
            {
                //message = "Welcome to PlannerApp.";
                //messageType = Models.AlertMessageType.Success;

                var userInfo = new LocalUserInfo()
                {
                    AccessToken = result.Message,
                    Email = result.UserInfo["Email"],
                    FirstName = result.UserInfo["FirstName"],
                    LastName = result.UserInfo["LastName"],
                    Id = result.UserInfo[System.Security.Claims.ClaimTypes.NameIdentifier],
                };
                await storage.SetItemAsync<LocalUserInfo>("User", userInfo);
                await authStateProvider.GetAuthenticationStateAsync();
                navigationManager.NavigateTo("/");

            }
            else
            {
                message = result.Message;
                messageType = Models.AlertMessageType.Error;
            }
            isBusy = false;
        }
        public void GoToRegister()
        {
            navigationManager.NavigateTo("/auth/register");
        }
    }
}
