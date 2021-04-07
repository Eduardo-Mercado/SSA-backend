using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SSA.backend.Helpers.Middleware;
using SSA.Security.DTO;
using SSA.Security.Security.Autentication;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SSA.backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticatonAction _autenticatonAction;
        private readonly IJwtAuthManager _jwtAuthManager;
        public AuthenticationController(IJwtAuthManager jwtAuthManager)
        {
            _autenticatonAction = new AuthenticatonAction();
            _jwtAuthManager = jwtAuthManager;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] UserDTO model)
        {
            if (!new AutenticationService(this._autenticatonAction).IsValidAccess(model))
            {
                return Unauthorized();
            }

            var infoUser = new AutenticationService(this._autenticatonAction).GetInfoUserByName(model.UserName);
            var claims = new[]
               {
                    new Claim(ClaimTypes.Name,infoUser.UserName),
                    new Claim(ClaimTypes.Role, infoUser.Rol),
                };
            var jwtResult = _jwtAuthManager.GenerateTokens(infoUser.UserName, claims, DateTime.Now);

            return Ok(new LoginResult
            {
                UserName = infoUser.UserName,
                Role = infoUser.Rol,
                AccessToken = jwtResult.AccessToken,
                RefreshToken = jwtResult.RefreshToken.TokenString,
                IdUser = infoUser.Id
            });
        }

        [HttpPost]
        [Route("logout")]
        public ActionResult Logout()
        {
            var userName = User.Identity.Name;
            _jwtAuthManager.RemoveRefreshTokenByUserName(userName);
            //_logger.LogInformation($"User [{userName}] logged out the system.");
            return Ok();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var userName = User.Identity.Name;
                //_logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

                if (string.IsNullOrWhiteSpace(request.RefreshToken))
                {
                    return Unauthorized();
                }

                var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
                var jwtResult = _jwtAuthManager.Refresh(request.RefreshToken, accessToken, DateTime.Now);
                //  _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
                return Ok(new LoginResult
                {
                    UserName = userName,
                    Role = User.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty,
                    AccessToken = jwtResult.AccessToken,
                    RefreshToken = jwtResult.RefreshToken.TokenString
                });
            }
            catch (SecurityTokenException e)
            {
                return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
            }
        }

        [HttpPost]
        [Route("getMenu")]
        public async Task<ActionResult> GetMenuAsync()
        {
            if (User.Identity.IsAuthenticated)
            {

                var userName = User.Identity.Name;
                var info = await new AutenticationService(this._autenticatonAction).GetMenuByUserNameAsync(userName);

                if (info != null)
                {
                    return Ok(info);
                }
            }

            return Unauthorized();
        }
    }
}
