using BookStore.Application.DTOs.Auth;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace BookStore.Application.Tests.Services;

public class AuthServiceTests
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;


private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var userStore = new Mock<IUserStore<ApplicationUser>>();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            userStore.Object,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!,
            null!);

        var roleStore = new Mock<IRoleStore<IdentityRole>>();

        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
            roleStore.Object,
            null!,
            null!,
            null!,
            null!);

        _tokenGeneratorMock = new Mock<ITokenGenerator>();

        _authService = new AuthService(
            _userManagerMock.Object,
            _roleManagerMock.Object,
            _tokenGeneratorMock.Object);
    }

    [Fact]
    public async Task RegisterAsync_Should_Return_Error_When_Email_Already_Exists()
    {
        // Arrange
        var dto = new RegisterDto
        {
            FirstName = "Yossef",
            LastName = "Essam",
            Username = "yossef",
            Email = "test@test.com",
            Password = "12345678"
        };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(dto.Email))
            .ReturnsAsync(new ApplicationUser());

        // Act
        var result = await _authService.RegisterAsync(dto);

        // Assert
        result.IsAuthenticated.Should().BeFalse();

        result.Message.Should().Be("Email is already registered!");
    }

    [Fact]
    public async Task RegisterAsync_Should_Create_User_When_Data_Is_Valid()
    {
        // Arrange
        var dto = new RegisterDto
        {
            FirstName = "Yossef",
            LastName = "Essam",
            Username = "yossef",
            Email = "test@test.com",
            Password = "12345678"
        };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(dto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        _userManagerMock
            .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
            .ReturnsAsync(IdentityResult.Success);

        _roleManagerMock
            .Setup(x => x.RoleExistsAsync("User"))
            .ReturnsAsync(true);

        _userManagerMock
            .Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.RegisterAsync(dto);

        // Assert
        result.IsAuthenticated.Should().BeTrue();

        result.Email.Should().Be(dto.Email);

        _userManagerMock.Verify(
            x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password),
            Times.Once);
    }

    [Fact]
    public async Task GetTokenAsync_Should_Return_Error_When_User_Not_Found()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@test.com",
            Password = "12345678"
        };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(dto.Email))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _authService.GetTokenAsync(dto);

        // Assert
        result.IsAuthenticated.Should().BeFalse();

        result.Message.Should().Be("Invalid Email or Password!");
    }


[Fact]
    public async Task GetTokenAsync_Should_Return_Token_When_Credentials_Are_Valid()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@test.com",
            Password = "12345678"
        };

        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = "yossef"
        };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(x => x.CheckPasswordAsync(user, dto.Password))
            .ReturnsAsync(true);

        _userManagerMock
            .Setup(x => x.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "User" });

        _tokenGeneratorMock
            .Setup(x => x.GenerateToken(user, It.IsAny<IList<string>>()))
            .Returns("Fake-JWT-Token");

        // Act
        var result = await _authService.GetTokenAsync(dto);

        // Assert
        result.IsAuthenticated.Should().BeTrue();
        result.Token.Should().Be("Fake-JWT-Token");
        result.Email.Should().Be(dto.Email);
        result.Username.Should().Be("yossef");
        result.Roles.Should().Contain("User");

        _tokenGeneratorMock.Verify(
            x => x.GenerateToken(user, It.IsAny<IList<string>>()),
            Times.Once);
    }

    [Fact]
    public async Task GetTokenAsync_Should_Return_Error_When_Password_Is_Wrong()
    {
        // Arrange
        var dto = new LoginDto
        {
            Email = "test@test.com",
            Password = "WrongPassword"
        };

        var user = new ApplicationUser
        {
            Email = dto.Email,
            UserName = "yossef"
        };

        _userManagerMock
            .Setup(x => x.FindByEmailAsync(dto.Email))
            .ReturnsAsync(user);

        _userManagerMock
            .Setup(x => x.CheckPasswordAsync(user, dto.Password))
            .ReturnsAsync(false);

        // Act
        var result = await _authService.GetTokenAsync(dto);

        // Assert
        result.IsAuthenticated.Should().BeFalse();
        result.Message.Should().Be("Invalid Email or Password!");
    }

    [Fact]
    public async Task AddRoleAsync_Should_Return_User_Not_Found()
    {
        // Arrange
        _userManagerMock
            .Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _authService.AddRoleAsync("1", "Admin");

        // Assert
        result.Should().Be("User not found!");
    }

    [Fact]
    public async Task AddRoleAsync_Should_Return_User_Already_Has_Role()
    {
        // Arrange
        var user = new ApplicationUser();

        _userManagerMock
            .Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync(user);

        _roleManagerMock
            .Setup(x => x.RoleExistsAsync("Admin"))
            .ReturnsAsync(true);

        _userManagerMock
            .Setup(x => x.IsInRoleAsync(user, "Admin"))
            .ReturnsAsync(true);

        // Act
        var result = await _authService.AddRoleAsync("1", "Admin");

        // Assert
        result.Should().Be("User already has this role!");
    }

    [Fact]
    public async Task AddRoleAsync_Should_Add_Role_Successfully()
    {
        // Arrange
        var user = new ApplicationUser();

        _userManagerMock
            .Setup(x => x.FindByIdAsync("1"))
            .ReturnsAsync(user);

        _roleManagerMock
            .Setup(x => x.RoleExistsAsync("Admin"))
            .ReturnsAsync(true);

        _userManagerMock
            .Setup(x => x.IsInRoleAsync(user, "Admin"))
            .ReturnsAsync(false);

        _userManagerMock
            .Setup(x => x.AddToRoleAsync(user, "Admin"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authService.AddRoleAsync("1", "Admin");

        // Assert
        result.Should().Be("Role added successfully!");

        _userManagerMock.Verify(
            x => x.AddToRoleAsync(user, "Admin"),
            Times.Once);
    }


}

