
using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class AuthenticationService
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public AuthenticationService(string url)
        {
            _baseUrl = url;
        }

        public async Task<UserManegerResponse> RegisterUserAsync(RegisterRequest request)
        {
            var url = _baseUrl+"api/auth/register";
            var response = await client.PostAsync<UserManegerResponse>( url, request);

            return response.Result;
        }
        public async Task<UserManegerResponse> LogInUserAsync(LoginRequest request)
        {
            var url = _baseUrl + "api/auth/login";
            var response = await client.PostAsync<UserManegerResponse>(url, request);

            return response.Result;
        }

    }
}
