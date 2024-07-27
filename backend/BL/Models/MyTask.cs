using BL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BL.Models
{
    public class MyTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        public string Title { get; set; }

        public string? Description { get; set; }

      
        public TaskPriority Priority { get; set; }

    
        public DateTime CreatedAt { get; set; }

        public MyTask(int id, string title, string? description, TaskPriority priority, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            Priority = priority;
            CreatedAt = createdAt;
        }

        public MyTask() { }



        // Constructor to initialize Title and Priority
        public MyTask(string title, TaskPriority priority)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Priority = priority;
            CreatedAt = DateTime.Now;
        }
    }
}

