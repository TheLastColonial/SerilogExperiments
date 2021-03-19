
namespace SerilogExperiments.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Item on the ToDo list
    /// </summary>
    public class ToDoItem
    {
        /// <summary>
        /// Reference
        /// </summary>
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        public bool Completed { get; set; } = false;
    }
}
