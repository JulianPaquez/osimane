using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Response;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IConfiguration config, IAuthenticationService autenticacionService)
        {
            _config = config;
            _authenticationService = autenticacionService;
        }

        [HttpPost("authenticate")]
        public ActionResult<AuthenticationResponse> Autenticate([FromBody] AuthenticationRequest authenticationRequest)
        {
            try
            {
                return Ok(_authenticationService.Autenticate(authenticationRequest));
            } catch (Exception ex)
            {
                return BadRequest("Credenciales inválidas");
            }
            
        }
    }
}
