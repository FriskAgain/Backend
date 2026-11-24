using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Services;
using AarhusSpaceProgram.API.db;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AarhusSpaceProgram.db.model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AarhusSpaceProgram.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
private readonly AarhusSpaceProgramDbContext _context;
private readonly ILogger<AccountController> _logger;
private readonly IConfiguration _configuration;
private readonly UserManager<ApiUser> _userManager;
private readonly SignInManager<ApiUser> _signInManager;
public AccountController(
        AarhusSpaceProgramDbContext context,
        ILogger<AccountController> logger,
        IConfiguration configuration,
        UserManager<ApiUser> userManager,
        SignInManager<ApiUser> signInManager)
{
    _context = context;
    _logger = logger;
    _configuration = configuration;
    _userManager = userManager;
    _signInManager = signInManager;
}

[HttpPost]
public async Task<ActionResult> Register(RegisterDTO input)
{
    try
    {
        if (ModelState.IsValid)
        {
            var newUser = new ApiUser();
            newUser.UserName = input.Email;
            newUser.Email = input.Email;
            newUser.FullName = input.FullName;

            var result = await _userManager.CreateAsync(
                newUser, input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, input.role);
                _logger.LogInformation(
                    "User {userName} ({email}) has been created.",
                    newUser.UserName, newUser.Email);

                return StatusCode(201,
                    $"User '{newUser.UserName}' has been created.");
            }
            else
                throw new Exception(
                    string.Format("Error: {0}", string.Join(" ",
                    result.Errors.Select(e => e.Description))));
        }
        else
        {
            throw new Exception("Invalid model state");
        }
    }
    catch (Exception)
    {
        throw;
    }
}

[HttpPost("logIn")]
public async Task<ActionResult> Login(LogInDto input)
{
    try
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(input.UserName);

        if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
            return Unauthorized("Invalid login attempt.");

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        //Adding role claims JWT
        claims.AddRange(
            (await _userManager.GetRolesAsync(user))
            .Select(role => new Claim(ClaimTypes.Role, role))
        );

        var jwtObject = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: signingCredentials
        );

        var jwtString = new JwtSecurityTokenHandler().WriteToken(jwtObject);

        return Ok(jwtString);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}
}