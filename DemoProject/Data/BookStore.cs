using DemoProject.Models;
using DemoProject.Models.DTO;

namespace DemoProject.Data
{
    public class BookStore:IBookStore
    {
        public List<Book> Books = new List<Book>
        {
            new Book { Author = "author 1", Description = "desc", Id = 1, Name = "test book 1", PageNumber = 100 },
            new Book { Author = "author 2", Description = "desc", Id = 2, Name = "test book 2", PageNumber = 200 },
            new Book { Author = "author 3", Description = "desc", Id = 3, Name = "test book 3", PageNumber = 300 }
        };

        public IList<Book> GetAllBooks()
        {
            return Books;
        }
    }
}
