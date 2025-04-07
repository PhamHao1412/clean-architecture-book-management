using Microsoft.EntityFrameworkCore;
using Moq;
using My_Movie;
using My_Movie.Application.Exceptions;
using My_Movie.Application.Features.User.Commands.POST.SignIn;
using My_Movie.Application.UserFeatures.Commands;
using My_Movie.IRepository;
using My_Movie.IRepository.Repositiory;
using My_Movie.Model;

namespace Tests.Users;

public class SignInCommandTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly SigInCommandHandler _handler;
    private readonly DBContext _dbContext;
    private readonly IUserRepository userRepository;


    public SignInCommandTests()
    {
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new DBContext(options);
        userRepository = new UserRepository(_dbContext);
        _mockUserRepository = new Mock<IUserRepository>();
    }


    [Fact]
    public async Task CheckUserLogin_InvalidLoginName_ThrowsException()
    {
        // Arrange   
        var loginName = "hao1412";
        var password = "123";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("123");

        var user = new User { Name = "Hao Pham", LoginName = "hao", Password = hashedPassword, user_id = "Hao_12372" };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var command = new SignInCommand(loginName, password);
        var handle = new SigInCommandHandler(userRepository);

        // Act
        var exception =
            await Assert.ThrowsAsync<BadRequestException>(() => handle.Handle(command, CancellationToken.None));
        // Assert    
        Assert.Equal("The login name does not exists", exception.Message);
    }

    [Fact]
    public async Task CheckUserLogin_InvalidPassword_ThrowsException()
    {
        // Arrange   
        var loginName = "hao1412";
        var password = "123";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("321");

        var user = new User
        {
            Name = "Hao Pham",
            LoginName = loginName,
            Password = hashedPassword,
            user_id = "Hao_12372"
        };
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        var command = new SignInCommand(loginName, password);
        var handle = new SigInCommandHandler(userRepository);
        // Act
        var exception =
            await Assert.ThrowsAsync<BadRequestException>(() => handle.Handle(command, CancellationToken.None));
        // Assert    
        Assert.Equal("Invalid Password", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldReturnToken_WhenUserExists()
    {
        // Arrange
        var loginName = "validUser";
        var password = "validPassword";
        var jwt_token = "token_example";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new User
        {
            Name = "Hao Pham",
            LoginName = loginName,
            Password = hashedPassword,
            user_id = "Hao_12372"
        };
        var command = new SignInCommand(loginName, password);
        _mockUserRepository.Setup(repo => repo.CheckUserLogin(loginName, password))
            .ReturnsAsync(user);
        _mockUserRepository.Setup(repo => repo.GenerateJwtToken(It.IsAny<User>()))
            .Returns(jwt_token);
        // Act
        var handler = new SigInCommandHandler(_mockUserRepository.Object);
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        Assert.Equal(200, result.Code);
        Assert.Equal("success", result.Status);
        Assert.Equal(jwt_token, result.Data);
    }
}