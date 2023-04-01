using DemoProject.Data;
using DemoProject.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>>  GetBooks()
        {
            return Ok(BookStore.BookDTOs);
        }

        [HttpGet("id")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            var book = BookStore.BookDTOs.FirstOrDefault(x => x.Id == id);

            if(book == null)
            {
                return NotFound();
            }
            
            return Ok(book);
        }
    }
}
