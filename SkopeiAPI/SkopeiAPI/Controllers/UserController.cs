using Microsoft.AspNetCore.Mvc;
using SkopeiAPI.Models;
using SkopeiAPI.Models.Dto;
using SkopeiAPI.UnitOfWorks;
using System.Threading.Tasks;

namespace SkopeiAPI.Controllers
{
    // The controller class registers the endpoints of the API and calls the corresponding services.
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // The UnitOfWork is injected with Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
            // A user is extracted from the json body of post request and translated to an object.
            // Can best be done with automappers functionality but timebox was to short to do this.
        {
            User newUser = new User
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email
            };

            bool success = await _unitOfWork.UserRepo.Add(newUser);

            if (success)
            {
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetUserById", new { newUser.Id }, newUser);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _unitOfWork.UserRepo.All());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepo.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
            // Could be improved by the use of Dto object and automapper, timebox to short to do this.
        {
            User user = await _unitOfWork.UserRepo.GetById(id);

            if (user == null)
                return NotFound();

            _unitOfWork.UserRepo.Update(user, updateUserDto);
            await _unitOfWork.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            User userToDelete = await _unitOfWork.UserRepo.GetById(id);

            if (userToDelete == null)
                return BadRequest();

            await _unitOfWork.UserRepo.Delete(id);
            await _unitOfWork.SaveAsync();

            return Ok(userToDelete);
        }

    }
}
