using BookStore.Application.DTOs.Auth;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenGenerator _tokenGenerator;


public AuthService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ITokenGenerator tokenGenerator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthModel> RegisterAsync(RegisterDto model)
    {
        if (await _userManager.FindByEmailAsync(model.Email) != null)
        {
            return new AuthModel
            {
                Message = "Email is already registered!"
            };
        }

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserName = model.Username,
            Email = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return new AuthModel
            {
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }

        if (!await _roleManager.RoleExistsAsync("User"))
        {
            await _roleManager.CreateAsync(new IdentityRole("User"));
        }

        await _userManager.AddToRoleAsync(user, "User");

        return new AuthModel
        {
            IsAuthenticated = true,
            Email = user.Email,
            Username = user.UserName,
            Roles = new List<string> { "User" }
        };
    }

    public async Task<AuthModel> GetTokenAsync(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return new AuthModel
            {
                Message = "Invalid Email or Password!"
            };
        }

        var roles = await _userManager.GetRolesAsync(user);

        var token = _tokenGenerator.GenerateToken(user, roles);

        return new AuthModel
        {
            IsAuthenticated = true,
            Token = token,
            Email = user.Email,
            Username = user.UserName,
            Roles = roles.ToList()
        };
    }

    public async Task<string> AddRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            return "User not found!";

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        if (await _userManager.IsInRoleAsync(user, roleName))
        {
            return "User already has this role!";
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);

        return result.Succeeded
            ? "Role added successfully!"
            : "Something went wrong!";
    }


}
