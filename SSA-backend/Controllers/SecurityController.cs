using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSA.Security.DTO;
using SSA.Security.Security.Autentication;

namespace SSA_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly AuthenticatonAction _authenticatonAction;
        public SecurityController()
        {
            _authenticatonAction = new AuthenticatonAction();
        }

        public IActionResult GetAllUser()
        {
            var listUsers = new AutenticationService(this._authenticatonAction).GetAllUsers();
            if (listUsers == null)
            {
                return BadRequest("unexpected error ");
            }

            return Ok(listUsers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetUserById(int id)
        {
            var result = new AutenticationService(this._authenticatonAction).GetById(id);
            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDTO data)
        {
            var response = new AutenticationService(this._authenticatonAction).CreateUser(data);

            if (response == null)
            {
                return BadRequest(new { message = "Error creating user" });
            }

            return Ok(response);
        }

    }

}
