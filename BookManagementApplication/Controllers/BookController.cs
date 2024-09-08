using BookManagementApplication.DbAccess;
using BookManagementApplication.DTO;
using BookManagementApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagementApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create-book", Name = "CreateBook")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO request)
        {
            try
            {
                BookModel book = new BookModel();
                book.Id = Guid.NewGuid().ToString();
                book.Name = request.Name;
                book.Author = request.Author;
                book.IsBorrowed = request.IsBorrowed;
                book.UserId = request.UserId;

                _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return Created(string.Empty, "Book Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-book/{bookId}", Name = "UpdateBook")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(string bookId,[FromBody] BookCreateDTO request)
        {
            try
            {
                var result = await _context.Books.FindAsync(bookId);
                if (result == null)
                {
                    return BadRequest("Book Not found");
                }

                BookModel book = result as BookModel;
                if (!string.IsNullOrEmpty(request.Name)) book.Name = request.Name;
                if (!string.IsNullOrEmpty(request.Name)) book.Author = request.Author;
                if (!string.IsNullOrEmpty(request.Name)) book.UserId = request.UserId;
                book.IsBorrowed = request.IsBorrowed;

                _context.Books.Update(book);
                await _context.SaveChangesAsync();

                return Ok("Book Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-book/{bookId}", Name = "DeleteBook")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(string bookId)
        {
            try
            {
                var result = await _context.Books.FindAsync(bookId);
                if (result == null)
                {
                    return BadRequest("Book Not found");
                }

                BookModel book = result as BookModel;

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return Ok("Book Deleted Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("find-book/{bookId}", Name = "FindBook")]
        public async Task<IActionResult> FindBook(string bookId)
        {
            try
            {
                var result = await _context.Books.FindAsync(bookId);
                if (result == null)
                {
                    return BadRequest("Book Not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("find-books", Name = "FindBooks")]
        public async Task<IActionResult> FindBooks()
        {
            try
            {
                var result = await _context.Books.ToListAsync();
                if (result == null)
                {
                    return BadRequest("Book Not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
