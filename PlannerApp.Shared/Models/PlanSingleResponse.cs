namespace PlannerApp.Shared.Models
{
    public class PlanSingleResponse : BaseAPIResponse
    {
        public Plan Record { get; set; }
    }

    public class ToDoItemsSingleResponse : BaseAPIResponse
    {
        public ToDoItem Record { get; set; }
    }

}
