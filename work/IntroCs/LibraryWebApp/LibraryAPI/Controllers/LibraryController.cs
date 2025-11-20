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

        //[HttpPost("newuser")]
        //public ActionResult CreateUser([FromBody] CreateUserDto dto)
        //{
        //    if (_service.UserExists(dto.Name, dto.UserType))
        //    {
        //        return Conflict("User already exists");
        //    }

        //    var user = _service.AddUser(dto);
        //    return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        //}

        [HttpPost("newuser")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            var created = _service.TryAddUser(dto, out var user);

            if (!created)
            {
                return Conflict("User already exists");
            }

            return CreatedAtAction(nameof(CreateUser), new { id = user.Id }, user);
        }

        [HttpPost("newitem")]
        public ActionResult CreateItem([FromBody] CreateItemDto dto)
        {
            var created = _service.TryAddItem(dto, out var item);

            if (!created)
            {
                return Conflict("Item already exists");
            }

            return CreatedAtAction(nameof(CreateItem), new { id = item.Id }, item);
        }


        [HttpPost("newborrowing")]
        public ActionResult CreateBorrowing([FromBody] BorrowItemDto dto)
        {
            var result = _service.TryBorrowItem(dto);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("{id:guid}")]
        public ActionResult GetBorrowings(Guid id)
        {
            var result = _service.TryGetValueOfBorrowings(id, out var borrowings);

            if (!result)
            {
                return NotFound("No active borrowings");
            }

            return Ok(borrowings);
        }

        [HttpPut("changeuserprofile/{id}")]
        public ActionResult UpdateUserProfile(Guid id, [FromBody] ChangeProfileDto updatedUserProfile)
        {
            var result = _service.TryUpdateUserProfile(id,updatedUserProfile, out var updatedUser);
            if (!result)
            {
                return BadRequest("Profile update failed");
            }

            // return NoContent();
            return Ok(updatedUser);
        }

    }
}