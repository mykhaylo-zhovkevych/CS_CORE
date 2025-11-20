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
            bool created = _service.TryAddUser(dto, out var user);

            if (!created)
            {
                return Conflict("User already exists");
            }

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPost("newitem")]
        public ActionResult CreateItem([FromBody] CreateItemDto dto)
        {

            var item = _service.AddItem(dto);

            if (!item.Item1)
            {
                return Conflict("Item already exists");
            }

        
            return CreatedAtAction(nameof(CreateItem), new { id = item.Item2.Id }, item);
        }

        // temp disabled

        //[HttpPost("newborrowing")]
        //public ActionResult BorrowItem([FromBody] BorrowItemDto dto)
        //{
        //    //if (!_service.BorrowingIsPossible(dto.UserId, dto.ItemId))
        //    //{
        //    //    return BadRequest("Borrowing not possible");
        //    //}

        //    var result = _service.BorrowItem(dto);
        //    if (!result.Success)
        //    {
        //        return BadRequest(result.Message);
        //    }

        //    return Ok(result.Message);
        //}

        //[HttpGet("{id:guid}")]
        //public ActionResult GetBorrowings(Guid id)
        //{
        //    var borrowings = _service.GetActiveBorrowingsForUser(id);
        //    if (borrowings.Count == 0)
        //    {
        //        return NotFound("No active borrowings");
        //    }

        //    return Ok(borrowings);
        //}
    }
}