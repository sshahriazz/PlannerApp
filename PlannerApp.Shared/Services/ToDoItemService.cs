using AKSoftware.WebApi.Client;
using PlannerApp.Shared.Models;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Services
{
    public class ToDoItemService
    {
        private readonly string _baseUrl;
        ServiceClient client = new ServiceClient();

        public ToDoItemService(string url) => _baseUrl = url;

        public string AccessToken
        {
            get => client.AccessToken;
            set
            {
                client.AccessToken = value;
            }
        }
        /// <summary>
        /// Creates new items for todo items table.
        /// </summary>
        /// <param name="model">contains todo items definiton.</param>
        /// <returns>all the items</returns>
        public async Task<ToDoItemsSingleResponse> CreateItemAsync(TodoItemRequest model)
        {
            var response = await client.PostProtectedAsync<ToDoItemsSingleResponse>($"{ _baseUrl}api/todoitems",model);
            return response.Result;
        }
        /// <summary>
        /// Edits existing items for todo items table.
        /// </summary>
        /// <param name="model">contains todo items definiton.</param>
        /// <returns>all the items</returns>
        public async Task<ToDoItemsSingleResponse> EditItemAsync(TodoItemRequest model)
        {
            var response = await client.PutProtectedAsync<ToDoItemsSingleResponse>($"{ _baseUrl}api/todoitems", model);
            return response.Result;
        }
        /// <summary>
        /// Edit the description of a specific item.
        /// </summary>
        /// <param name="model">Object change detection.</param>
        /// <returns>all the items</returns>
        public async Task<ToDoItemsSingleResponse> ChangedItemStateAsync(string id)
        {
            var response = await client.PutProtectedAsync<ToDoItemsSingleResponse>($"{ _baseUrl}api/todoitems/{id}", null);
            return response.Result;
        }
        /// <summary>
        /// Edit the description of a specific item.
        /// </summary>
        /// <param name="model">Object change detection.</param>
        /// <returns>all the items</returns>
        public async Task<ToDoItemsSingleResponse> DeleteItemStateAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<ToDoItemsSingleResponse>($"{ _baseUrl}api/todoitems/{id}");
            return response.Result;
        }

    }
}
