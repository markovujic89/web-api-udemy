using DemoProject.Models;

namespace DemoProject.Data
{
    public interface IBookStore
    {
        IList<Book> GetAllBooks();
    }
}
