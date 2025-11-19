using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.HelperClasses;
using LibraryAPI.Models;
using LibraryAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryService _service;

        public LibraryController(LibraryService service)
        {
            _service = service;
        }

        [HttpPost("newuser")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            if (_service.UserExists(dto.Name, dto.UserType))
            {
                return Conflict("User already exists");
            }

            var user = _service.AddUser(dto);
            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPost("newitem")]
        public ActionResult CreateItem([FromBody] CreateItemDto dto)
        {
            if (_service.ItemExists(dto.Name, dto.ItemType))
            {
                return Conflict("Item already exists");
            }

            var item = _service.AddItem(dto);
            return CreatedAtAction(nameof(CreateItem), new { id = item.Id }, item);
        }

        [HttpPost("newborrowing")]
        public ActionResult BorrowItem([FromBody] BorrowItemDto dto)
        {
            if (!_service.BorrowingIsPossible(dto.UserId, dto.ItemId))
            {
                return BadRequest("Borrowing not possible");
            }

            var result = _service.BorrowItem(dto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetBorrowings(Guid id)
        {
            var borrowings = _service.GetActiveBorrowingsForUser(id);
            if (borrowings.Count == 0)
            {
                return NotFound("No active borrowings");
            }

            return Ok(borrowings);
        }
    }
}