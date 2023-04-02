using AutoMapper;
using DemoProject.Data;
using DemoProject.Logger;
using DemoProject.Models;
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
        private readonly ILogging _logger;

        private readonly IBookStore _bookStore;

        private List<Book> books;

        private IMapper _mapper;

        public BookController(ILogging logger, IBookStore bookStore, IMapper mapper)
        {
            _logger = logger;
            _bookStore = bookStore;
            books = _bookStore.GetAllBooks().ToList();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookGetDTO>> GetBooks()
        {
            _logger.Log("Get all books", "debug");

            var booksGetDTO = _mapper.Map<List<BookGetDTO>>(books);
            return Ok(booksGetDTO);
        }

        [HttpGet("{id:int}", Name = "GetBook")]
        public ActionResult<BookGetDTO> GetBook(int id)
        {
            var book = books.First(x=>x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var bookGetDTO = _mapper.Map<BookGetDTO>(book);
            return Ok(bookGetDTO);
        }

        [HttpPost]
        public ActionResult<BookCreateDTO> AddBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            if (bookCreateDTO == null)
            {
                return BadRequest(bookCreateDTO);
            }

            if (bookCreateDTO.PageNumber > 3000)
            {
                ModelState.AddModelError("CustomError", "Book store doesn't contain books with 3000 pages or more.");
                return BadRequest(ModelState);
            }

            var id = books.Max(x => x.Id);

            var book = _mapper.Map<Book>(bookCreateDTO);
            book.Id = id + 1;

            books.Add(book);

            var bookGetDTO = _mapper.Map<BookGetDTO>(book);
            return CreatedAtRoute("GetBook", new { id = book.Id }, bookGetDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteBook")]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var book = books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            books.Remove(book);

            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateBook")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            if (bookUpdateDTO == null || bookUpdateDTO.Id != bookId)
            {
                return BadRequest();
            }

            var book = books.First(x => x.Id == bookId);

            if (book == null)
            {
                return BadRequest();
            }

            book.Id = bookUpdateDTO.Id;
            book.Name = bookUpdateDTO.Name;
            book.PageNumber = bookUpdateDTO.PageNumber;
            book.Author = bookUpdateDTO.Author;
            book.Description = bookUpdateDTO.Description;


            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialBook")]
        public IActionResult UpdateParitalBook(int id, JsonPatchDocument<BookUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0) 
            { 
                return BadRequest(); 
            }

            var book = books.First(x => x.Id == id);

            if (book == null)
            {
                return BadRequest();
            }

            var bookUpdateDto = _mapper.Map<BookUpdateDTO>(book);

            patchDTO.ApplyTo(bookUpdateDto);

            return NoContent();
        }

    }
}
