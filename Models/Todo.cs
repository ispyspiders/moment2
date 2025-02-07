using System;
using System.ComponentModel.DataAnnotations;

namespace todo.Models
{
    public class Todo
    {
        // Properties
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}