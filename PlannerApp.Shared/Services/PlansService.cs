using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class PlansService
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();

        public PlansService(string baseUrl)
        {
            _baseUrl = baseUrl;

        }
        public string AccessToken
        {
            get => client.AccessToken;
            set
            {
                client.AccessToken = value;
            }
        }
        ///<Summary>
        ///Retrive all the plans fro the api with paging
        ///</summary>
        ///<param name="page">Number of page</param>
        ///<returns></returns>
        public async Task<PlansCollectionPagingResponse> GetAllPlansByPageAsync(int page = 1)
        {
            var respose = await client.GetProtectedAsync<PlansCollectionPagingResponse>($"{_baseUrl}api/plans?page={page}");
            return respose.Result;
        }
        ///<Summary>
        ///Retrive all the plans fro the api with paging
        ///</summary>
        ///<param name="page">Number of page</param>
        ///<returns></returns>
        public async Task<PlansCollectionPagingResponse> SearchPlansByPageAsync(string query, int page = 1)
        {
            var respose = await client.GetProtectedAsync<PlansCollectionPagingResponse>($"{_baseUrl}api/plans?query={query}&page={page}");
            return respose.Result;
        }
    }
}
