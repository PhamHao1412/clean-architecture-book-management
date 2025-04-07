using Microsoft.EntityFrameworkCore;
using Moq;
using My_Movie;
using My_Movie.Application.Exceptions;
using My_Movie.Application.Features.User.Commands.POST.SignUp;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.Domain.Model;
using My_Movie.DTO;
using My_Movie.IRepository;
using My_Movie.IRepository.Repositiory;
using My_Movie.Model;

// ReSharper disable All

namespace Tests.Users;

public class SignUpCommandTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IRoleRepository> _mockRoleRepository;
    private readonly SignUpCommandHandler _handler;
    private readonly DBContext _dbContext;
    private readonly IUserRepository userRepository;
    private readonly IRoleRepository roleRepository;



    public SignUpCommandTests()
    {
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new DBContext(options);
        userRepository = new UserRepository(_dbContext);
        roleRepository = new RoleRepository(_dbContext);
        
        _mockUserRepository = new Mock<IUserRepository>();
        _mockRoleRepository = new Mock<IRoleRepository>();
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenLoginNameAlreadyExists()
    {
        // Arrange
        var command = new SignUpCommand("HaoPham", "hao", "123", "123");

        var randomId = new RandomID { id = 12345 };
        _mockUserRepository.Setup(repo => repo.GetRandomID())
            .ReturnsAsync(randomId);
        var firstName = command.fullname.Split(' ')[0];
        var user = new User
        {
            Name = command.fullname,
            LoginName = command.username,
            Password = BCrypt.Net.BCrypt.HashPassword(command.password),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            user_id = firstName + "_" + randomId.id.ToString("D5")
        };

        // _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
        //     .ThrowsAsync(new BadRequestException("The login name has already exists"));
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var handle = new SignUpCommandHandler(userRepository,roleRepository);
        // Act & Assert
        var exception = await Assert.ThrowsAsync<BadRequestException>(() => handle.Handle(command, CancellationToken.None));
        Assert.Equal("The login name has already exists", exception.Message);
        
        _mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_CreatesUserSuccessfully()
    {
        // Arrange
        var username = "newUser";
        var password = "password123";
        var fullname = "Test User";
        var command = new SignUpCommand(fullname, username, password, password);
        var randomId = new RandomID { id = 12345 };
        _mockUserRepository.Setup(repo => repo.GetUserByLoginName(username))!.ReturnsAsync((User)null);
        _mockUserRepository.Setup(repo => repo.GetRandomID())
            .ReturnsAsync(randomId);
        var firstName = command.fullname.Split(' ')[0];

        var newUser = new User
        {
            Id = 1,
            Name = command.fullname,
            LoginName = command.username,
            Password = BCrypt.Net.BCrypt.HashPassword(command.password),
            CreatedAt = It.IsAny<DateTime>(),
            UpdatedAt = It.IsAny<DateTime>(),
            user_id = firstName + "_" + randomId.id.ToString("D5")
        };
        var userResponse = new UserResponse { Id = 1, Name = fullname, LoginName = username };

        _mockUserRepository.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
            .ReturnsAsync(userResponse);
        _mockRoleRepository.Setup(repo => repo.AddRoleForUser(newUser.Id, "User"));
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Code);
        Assert.Equal("success", result.Status);
        Assert.Equal(userResponse, result.Data);
        _mockUserRepository.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
        _mockRoleRepository.Verify(repo => repo.AddRoleForUser(It.IsAny<int>(), "User"), Times.Once);
    }
}