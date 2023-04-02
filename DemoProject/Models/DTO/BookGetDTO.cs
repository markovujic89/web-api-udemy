using System.ComponentModel.DataAnnotations;

namespace DemoProject.Models.DTO
{
    public class BookGetDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public int PageNumber { get; set; }
    }
}
