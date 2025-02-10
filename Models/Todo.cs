using System;
using System.ComponentModel.DataAnnotations;

namespace todo.Models
{
    public class Todo
    {
        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Titel är obligatoriskt.")]
        public string? Title { get; set; }

        [StringLength(200, ErrorMessage = "Beskrivning får inte vara mer än 200 tecken.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}