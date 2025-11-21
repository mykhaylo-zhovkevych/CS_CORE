using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Storage;
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

        [HttpPost("users")]
        public ActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            var created = _service.TryAddUser(dto, out var user);

            if (!created)
            {
                return Conflict("User already exists");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user!.Id }, user);
        }

        [HttpGet("users/{id}")]
        public ActionResult GetUserById([FromRoute] Guid id)
        {
            var user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound($"User not found with id {id}");
            }

            return Ok(user);
        }

        //[HttpDelete("users/{id}")]
        //public ActionResult X([FromRoute] Guid id)
        //{
        //    var user = _service.GetUser(id);

        //    if (user == null)
        //    {
        //        return NotFound($"User not found with id {id}");
        //    }

        //    return Ok(user);
        //}

        //[HttpPatch("users/{id}")]
        //public ActionResult Y([FromRoute] Guid id)
        //{
        //    var user = _service.GetUser(id);

        //    if (user == null)
        //    {
        //        return NotFound($"User not found with id {id}");
        //    }

        //    return Ok(user);
        //}

        [HttpPost("items")]
        public ActionResult CreateItem([FromBody] CreateItemDto dto)
        {
            var created = _service.TryAddItem(dto, out var item);

            if (!created)
            {
                return Conflict("Item already exists");
            }

            return CreatedAtAction(nameof(CreateItem), new { id = item!.Id }, item);
        }


        [HttpPost("borrowings")]
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

        [HttpPut("userprofile/{id}")]
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