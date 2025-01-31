using Ecommerce.Core.Interfaces;

using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using Ecommerce.Core.Model;
using System.Security.Claims;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;

    public AuthService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
    {
        _userRepository = userRepository;
        _config = config;
        _mapper = mapper;
    }

    

    public async Task<bool> Register(string username, string password, string role)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(username);
        if (existingUser != null) return false;

        var newUserModel = new UserModel
        {
            Username = username,
            Role = role
        };
        newUserModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password); 

        await _userRepository.AddUserAsync(newUserModel);
        return true;
    }


    public async Task<string> Authenticate(string username, string password)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null; // Échec de l'authentification
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
