using DemoProject.Data;
using DemoProject.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DemoProject.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            return Ok(BookStore.BookDTOs);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        public ActionResult<BookDTO> GetBook(int id)
        {
            var book = BookStore.BookDTOs.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public ActionResult<BookDTO> AddBook([FromBody] BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                return BadRequest(bookDTO);
            }

            if (bookDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (bookDTO.PageNumber > 3000)
            {
                ModelState.AddModelError("CustomError", "Book store doesn't contain books with 3000 pages or more.");
                return BadRequest(ModelState);
            }

            var id = BookStore.BookDTOs.Max(x => x.Id);

            bookDTO.Id = id + 1;

            BookStore.BookDTOs.Add(bookDTO);

            return CreatedAtRoute("GetBook", new { id = bookDTO.Id }, bookDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var book = BookStore.BookDTOs.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            BookStore.BookDTOs.Remove(book);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateBook")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookDTO bookDTO)
        {
            if (bookDTO == null || bookDTO.Id != bookId)
            {
                return BadRequest();
            }

            var book = BookStore.BookDTOs.First(x => x.Id == bookId);

            if (book == null)
            {
                return BadRequest();
            }

            book.Id = bookDTO.Id;
            book.Name = bookDTO.Name;
            book.PageNumber = bookDTO.PageNumber;
            book.Author = bookDTO.Author;
            book.Description = bookDTO.Description;


            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialBook")]
        public IActionResult UpdateParitalBook(int id, JsonPatchDocument<BookDTO> patchDTO)
        {
            if (patchDTO == null || id == 0) 
            { 
                return BadRequest(); 
            }

            var book = BookStore.BookDTOs.First(x => x.Id == id);

            if (book == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(book);

            return NoContent();
        }

    }
}
