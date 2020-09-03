using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System.Collections.Generic;
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
        ///<param name="page">Number of page, each page has 10 plans max</param>
        ///<returns>plan collection based on page number.</returns>
        public async Task<PlansCollectionPagingResponse> GetAllPlansByPageAsync(int page = 1)
        {
            var respose = await client.GetProtectedAsync<PlansCollectionPagingResponse>($"{_baseUrl}api/plans?page={page}");
            return respose.Result;
        }

        /// <summary>
        /// Retrive a single plan by Id
        /// </summary>
        /// <param name="id">ID of the plan</param>
        /// <returns>single pan based on id</returns>
        public async Task<PlanSingleResponse> GetPlansByIdAsync(string id)
        {
            var respose = await client.GetProtectedAsync<PlanSingleResponse>($"{_baseUrl}api/plans/{id}");
            return respose.Result;
        }

        ///<Summary>
        ///Retrive all the plans fro the api with paging
        ///</summary>
        ///<param name="page">Number of page</param>
        ///<returns></returns>
        public async Task<PlansCollectionPagingResponse> SearchPlansByPageAsync(string query, int page = 1)
        {
            var respose = await client.GetProtectedAsync<PlansCollectionPagingResponse>($"{_baseUrl}api/plans/search?query={query}&page={page}");
            return respose.Result;
        }

        /// <summary>
        /// posting plan to the api
        /// </summary>
        /// <param name="model">object added.</param>
        /// <returns>return new added plan</returns>
        public async Task<PlanSingleResponse> PostPlanAsync(PlanRequest model)
        {
            var formkeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Title", model.Title),
                new StringFormKeyValue("Description", model.Description),
            };
            if (model.CoverFile != null)
            {
                formkeyValues.Add(new FileFormKeyValue("CoverFile", model.CoverFile, model.FileName));
            }
            var response = await client.SendFormProtectedAsync<PlanSingleResponse>(
                $"{_baseUrl}api/plans",
                ActionType.POST,
                formkeyValues.ToArray()
                );
            return  response.Result;
        }
        /// <summary>
        /// Edit Existing plan based on Id.
        /// </summary>
        /// <param name="model">form data</param>
        /// <returns>Return updated plan</returns>
        public async Task<PlanSingleResponse> EditPlanAsync(PlanRequest model)
        {
            // this is for cheking if image changes or not.
            var formkeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Title", model.Title),
                new StringFormKeyValue("Description", model.Description),  
            };
            if (model.CoverFile != null)
            {
                formkeyValues.Add(new FileFormKeyValue("CoverFile", model.CoverFile, model.FileName));
            }
            var response = await client.SendFormProtectedAsync<PlanSingleResponse>(
                $"{_baseUrl}api/plans",
                ActionType.PUT,
                formkeyValues.ToArray()
                );
            return response.Result;
        }
        /// <summary>
        /// Delete Plan from the account
        /// </summary>
        /// <param name="id">Id of the plan should be deleted.</param>
        /// <returns></returns>
        public async Task<PlanSingleResponse> DeletePlanAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<PlanSingleResponse>($"{_baseUrl}api/plans/{id}");
            return response.Result;
        }

    }
}
