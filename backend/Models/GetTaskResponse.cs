using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MvcTaskManager.Models
{
    public class GetTaskResponse
    {
        public GetTaskResponse( string title, string? description, BL.Enums.TaskPriority priority, DateTime createdAt)
        {
            
            Title = title;
            Description = description;
            Priority = priority;
            CreatedAt = createdAt;
        }

     

        public string Title { get; set; }

        public string? Description { get; set; }

        public BL.Enums.TaskPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }


    }
}
