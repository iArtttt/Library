using Library.Common.DTOs;
using Library.Common.Entities;
using Library.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Generates URL base: api/books
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        /// <summary>
        /// Asynchronously searches for books by title or author name.
        /// GET: /api/books?toSearch=Pushkin
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> Search([FromQuery] string? toSearch = null)
        {
            var books = await _bookRepository.FindBooksAsync(toSearch);

            List<BookDto> result = books.Select(b => new BookDto
            (
                b.Id,
                b.Name,
                b.Count,
                b.Authors.Select(a => $"{a.LastName} {a.Name}").ToList())
            ).ToList();

            return Ok(result); // Returns 200 OK with JSON array
        }

        /// <summary>
        /// Asynchronously retrieves a specific book details by its unique GUID identifier.
        /// GET: /api/books/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetById(Guid id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound(new { message = $"Book with ID '{id}' was not found." });
            }

            var result = new BookDto
            (
                book.Id,
                book.Name,
                book.Count,
                book.Authors.Select(a => $"{a.LastName} {a.Name}").ToList()
            );

            return Ok(result);
        }

        /// <summary>
        /// Asynchronously adds a new book to the library catalog.
        /// POST: /api/books
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<BookDto>> Create([FromBody] Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest(new { message = "Invalid book data payload." }); // 400 Bad Request
            }

            // Ensure the book gets a fresh clean GUID before database persistence
            if (newBook.Id == Guid.Empty)
            {
                newBook.Id = Guid.NewGuid();
            }

            await _bookRepository.AddAsync(newBook);

            // Returns 201 Created status and adds a 'Location' header pointing to GetById endpoint
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        /// <summary>
        /// Asynchronously updates an existing book properties in the catalog.
        /// PUT: /api/books
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Book updatedBook)
        {
            var existingBook = await _bookRepository.GetByIdAsync(updatedBook.Id);
            if (existingBook == null)
            {
                return NotFound(new { message = $"Cannot update. Book with ID '{updatedBook.Id}' does not exist." });
            }

            await _bookRepository.UpdateAsync(updatedBook);
            return NoContent(); // 204 NoContent is a standard successful response for PUT requests
        }

        /// <summary>
        /// Asynchronously deletes a book from the library catalog by its GUID.
        /// DELETE: /api/books/3fa85f64-5717-4562-b3fc-2c963f66afa6
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound(new { message = $"Cannot delete. Book with ID '{id}' was not found." });
            }

            await _bookRepository.DeleteAsync(id);
            return Ok(new { message = $"Book '{existingBook.Name}' was successfully removed from the system." });
        }
    }
}
