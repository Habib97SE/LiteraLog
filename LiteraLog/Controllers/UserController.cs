using LiteraLog.Models;
using LiteraLog.Models.DTOs;
using LiteraLog.Models.DTOs.Users;
using LiteraLog.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace LiteraLog.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> PostRegisterUser(UserRegistrationDTO userRegistrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == userRegistrationDTO.Email))
            {
                return BadRequest("Email already in use.");
            }

            try
            {
                userRegistrationDTO.Password = Utility.Encryption.hashPassword(userRegistrationDTO.Password);

                User user = new User
                {
                    FirstName = userRegistrationDTO.FirstName,
                    LastName = userRegistrationDTO.LastName,
                    Email = userRegistrationDTO.Email,
                    Password = userRegistrationDTO.Password,
                    ProfileImage = "default-profile-pic",
                };
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                UserDTO userDTO = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,

                };


                return Ok(userDTO);
            } catch (Exception ex) 
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : "No inner exception";
                return StatusCode(500, "An error occurred while processing your request. " + ex.Message + " Inner exception: " + innerMessage);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> PostLogin (UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Utility.Validation.emailIsValid(userLoginDto.Email))
            {
                return BadRequest("The email address is not valid.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userLoginDto.Email);
            if (user == null)
            {
                return BadRequest("The email/password is wrong, Please try again.");
            }


            try
            {
                userLoginDto.Password = Utility.Encryption.hashPassword(userLoginDto.Password);

                if (user.Password != userLoginDto.Password)
                {
                    return BadRequest("The email/password is wrong, Please try again.");
                }

                var userDTO = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName.ToString(),
                    LastName = user.LastName.ToString(),
                    Email = user.Email,
                    Books = user.Books,
                    Quotes = user.Quotes,
                };

                return Ok(userDTO);
            } catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
