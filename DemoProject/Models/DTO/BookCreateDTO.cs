using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models.DTO
{
    public class BookCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }
    }
}
