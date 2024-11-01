using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Application.Interfaces;
using Application.Models.Response;

namespace Infrastructure.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;
        public AuthenticationService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.Email) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.GetByUserEmail(authenticationRequest.Email);

            if (user == null) return null;

            if (user.Email == authenticationRequest.Email && user.Password == authenticationRequest.Password) 
            {
                return user;
            } // Estamos validando que sea quien dice ser 

            return null;
        }

        public AuthenticationResponse Autenticate(AuthenticationRequest authenticationRequest)
        {
             var user = ValidateUser(authenticationRequest);

    if (user == null)
    {
        // Lanzar una excepción personalizada o devolver un mensaje más claro
        throw new InvalidOperationException("Las credenciales son incorrectas.");
    }

    var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));

    var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>
    {
        new Claim("sub", user.Id.ToString()),
        new Claim("given_name", user.Name),
        new Claim("given_email", user.Email),
        new Claim("given_register_date", user.RegisterDate.ToString()),
        new Claim("role", user.UserType.ToString()),
    };

    var jwtSecurityToken = new JwtSecurityToken(
        _options.Issuer,
        _options.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1),
        credentials);

    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    
    // Asegúrate de que el token se devuelva como respuesta
        return new AuthenticationResponse(tokenToReturn);
        }
    }

    public class AutenticacionServiceOptions
    {
        public const string AutenticacionService = "AutenticacionService";

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretForKey { get; set; }
    }
}
