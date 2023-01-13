using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.Domain.Games;
using TicTacToe.Domain.Login;
using TicTacToe.Interfaces;

namespace TicTacToeGame.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly IGameRepository _gameRepository;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, IGameRepository gameRepository)
    {
        this.userManager = userManager;
        _configuration = configuration;
        _gameRepository = gameRepository;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await userManager.FindByNameAsync(model.Username);
        if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        {
            await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var secretKey = _configuration["JWT:Secret"];
            if (secretKey == null)
                throw new AggregateException("Not found secret key!");

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });

        IdentityUser user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username,
        };
        var result = await userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        await _gameRepository.CreatePlayer(new Player
        {
            Id = user.Id,
            Name = user.UserName,
            Rating = 10
        });
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }
    [HttpPost]
    [Route("token")]
    public async Task<IActionResult> CreatePost(string key)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(key);
        if (token == null)
            return NotFound();
        var userId = token.Claims.First().Value;
        return Ok(userId);
    }
}