using DemoProject.Models.DTO;

namespace DemoProject.Data
{
    public static class BookStore
    {
        public static List<BookDTO> BookDTOs = new List<BookDTO>
        {
            new BookDTO { Author = "author 1", Description = "desc", Id = 1, Name = "test book 1", PageNumber = 100 },
            new BookDTO { Author = "author 2", Description = "desc", Id = 2, Name = "test book 2", PageNumber = 200 },
            new BookDTO { Author = "author 3", Description = "desc", Id = 3, Name = "test book 3", PageNumber = 300 }
        }; 
    }
}
