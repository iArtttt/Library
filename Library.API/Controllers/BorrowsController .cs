using Library.Common.DTOs;
using Library.Common.Interfaces.Repositories;
using Library.Common.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowsController : ControllerBase
    {
        private readonly IBorrowService _borrowService;
        private readonly IBorrowedBookRepository _borrowRepository;

        public BorrowsController(IBorrowService borrowService, IBorrowedBookRepository borrowRepository)
        {
            _borrowService = borrowService;
            _borrowRepository = borrowRepository;
        }

        [HttpPost("issue")]
        public async Task<IActionResult> IssueBook([FromQuery] Guid readerId, [FromQuery] Guid bookId)
        {
            bool success = await _borrowService.BorrowBookAsync(readerId, bookId);
            if (!success) return BadRequest(new { message = "Transaction failed. Check book stock or reader validity." });

            return Ok(new { message = "Book successfully issued!" });
        }

        [HttpPost("return/{loanId:guid}")]
        public async Task<IActionResult> ReturnBook(Guid loanId)
        {
            bool success = await _borrowService.ReturnBookAsync(loanId);
            if (!success) return BadRequest(new { message = "Failed to process book return." });

            return Ok(new { message = "Book successfully returned and restocked!" });
        }

        [HttpGet("history/{readerId:guid}")]
        public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetHistory(Guid readerId)
        {
            var historyDto = await _borrowService.GetReaderHistoryAsync(readerId);
            return Ok(historyDto);
        }
    }
}
