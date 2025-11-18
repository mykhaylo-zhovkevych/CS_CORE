using ConsoleApp5._4Remastered;
using ConsoleApp5._4Remastered.Data;
using ConsoleApp5._4Remastered.Enum;
using ConsoleApp5._4Remastered.HelperClasses;
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
        private static LibraryService _service;

        public LibraryController(LibraryService service)
        {
            _service = service;
        }

        // Data Transfare Object (DTO)
        [HttpPost("newuser")]
        public ActionResult<User> CreatNewUser([FromBody] User newUser)
        {
            if (newUser is null)
            {
                return BadRequest("User data is null");
            }
            var alreadyExists = LibraryService.CheckIfUserExists(newUser);
            if (alreadyExists)
            {
                return Conflict("User already exists");
            }

            return CreatedAtAction(nameof(CreatNewUser), new { id = newUser.Id }, newUser);
        }

        [HttpPost("newitem")]
        public ActionResult<Item> CreatNewItem([FromBody] Item newItem)
        {
            if (newItem is null)
            {
                return BadRequest("User data is null");
            }

            var alreadyExists = LibraryService.CheckIfItemExistsAndHasShelf(newItem);
            if (alreadyExists)
            {
                return Conflict("Item already exists");
            }

            return CreatedAtAction(nameof(CreatNewItem), new { id = newItem.Id }, newItem);
        }

        [HttpPost("newborrowing")]
        public ActionResult CreateNewBorrowing([FromBody] LibraryService.CreateBorrowingRequest request)
        {
            var borrowing = LibraryService.CheckIfBorrowingPossible(request);

            return CreatedAtAction(nameof(CreateNewBorrowing), new { userId = request.UserId, itemId = request.ItemId }, borrowing);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<List<Borrowing>> GetBorrowingForUser([FromRoute] Guid id)
        {
            var borrowings = LibraryService.GetActiveBorrowingsForUser(id);
            if (borrowings.Count == 0)
            {
                return NotFound("No active borrowings found for the user");
            }
            return Accepted(borrowings);

        }

    }
}
