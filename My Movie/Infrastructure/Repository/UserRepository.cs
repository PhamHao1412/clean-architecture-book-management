using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My_Movie.Domain.Model;
using My_Movie.DTO;
using My_Movie.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using My_Movie.Application.Exceptions;

namespace My_Movie.IRepository.Repositiory;

public class UserRepository(
    DBContext _dbContext,
    IConfiguration _configuration,
    IMapper _mapper,
    IHttpContextAccessor httpContextAccessor,
    ILogger<UserRepository> _logger) : IUserRepository
{
    public UserRepository(DBContext _dbContext) : this(_dbContext, null, null, null, null) { }
    public UserRepository() : this(null, null, null, null, null) { }

    public async Task<User> GetUserByLoginName(string login_name)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.LoginName == login_name);
        if (user == null) throw new BadRequestException("The login name does not exists");
        return user;
    }

    public async Task<User> CheckUserLogin(string login_name, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.LoginName == login_name);
        if (user == null) throw new BadRequestException("The login name does not exists");
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new BadRequestException("Invalid Password");
        user.lastLogged = DateTime.UtcNow;
        return user;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user != null) return user;
        return null;
    }

    public List<string> GetRoleByUserId(int user_id)
    {
        throw new NotImplementedException();
    }

    public List<string> getRoleByUserId(int user_id)
    {
        var roles = _dbContext.UserRoles.Include(x => x.Role).Where(u => u.UserId == user_id).ToList();
        var rs = roles.Select(u => u.Role.Name).ToList();
        return rs;
    }

    public async Task<UserResponse> AddUserAsync(User user)
    {
        var newUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.LoginName == user.LoginName);
        if (newUser == null)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserResponse>(user);
        }
        throw new BadRequestException("The login name has already exists");
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = Encoding.ASCII.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]);
        var roles = getRoleByUserId(user.Id);

        var claims = new List<Claim>
        {
            new("user_id", user.Id.ToString()),
            new("full_name", user.Name),
            new("login_name", user.LoginName)
        };
        foreach (var role in roles) claims.Add(new Claim("roles", role));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }

    public ClaimsPrincipal GetAuthenticatedUser()
    {
        var cur_user = httpContextAccessor.HttpContext?.User;
        if (cur_user == null) throw new UnauthorizedException("User is not authenticated");
        _logger.LogInformation("User is not authenticated");
        return cur_user;
    }

    public static int[] CreateAndShuffleArray(int size)
    {
        var array = Enumerable.Range(1, size).ToArray();

        var rng = new Random();
        var n = array.Length;
        while (n > 1)
        {
            n--;
            var k = rng.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }

        return array;
    }

    public async Task InsertShuffleIDAsync()
    {
        var shuffledArray = CreateAndShuffleArray(999);

        foreach (var number in shuffledArray)
        {
            var random_id = new RandomID
            {
                id = number,
                isUse = false
            };
            _dbContext.Add(random_id);
            await _dbContext.SaveChangesAsync();
        }
        
    }

    public async Task<RandomID?> GetRandomID()
    {
        return await _dbContext.RandomIds
            .Where(id => id.isUse == false)
            .FirstOrDefaultAsync();
    }

    public async Task SaveChange()
    {
        await _dbContext.SaveChangesAsync();
    }
}