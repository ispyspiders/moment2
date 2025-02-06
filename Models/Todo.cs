using System;
namespace todo.Models
{
    public class Todo
    {
        // Properties
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool isCompleted {get; set;}
    }
}