using AutoMapper;
using Microsoft.EntityFrameworkCore;
using My_Movie.Application.Exceptions;
using My_Movie.DTO;
using My_Movie.Model;

namespace My_Movie.IRepository.Repositiory
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DBContext _dbContext;
        private readonly HttpClient _httpClient;
        public IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(DBContext dBContext, IConfiguration configuration, HttpClient httpClient, IMapper mapper,ILogger<RoleRepository> logger)
        {
            _httpClient = httpClient;
            _dbContext = dBContext;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public RoleRepository(DBContext dBContext)
        {
            
        }
        public async Task<RoleResponse> GetRoleByNameAsync(string role_name)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == role_name);
            if (role == null) throw new BadRequestException("This Role already exists");
            return _mapper.Map<RoleResponse>(role);
        }

        public async Task<RoleResponse> AddRoleAsync(Role newRole)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == newRole.Name);
            if (role != null) throw new BadRequestException("This Role already exists");

            _dbContext.Roles.Add(newRole);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<RoleResponse>(newRole);
        }
        public async Task AddRoleForUser(int userId, string roleName)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null) throw new BadRequestException($"Role {roleName} does not exist.", roleName);
            var user_role = new UserRole
            {
                UserId = userId,
                RoleId = role.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _dbContext.UserRoles.Add(user_role);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<RoleResponse>> GetAllRoleAsync()
        {
            var roles = await _dbContext.Roles.ToListAsync();
            if (roles == null || roles.Count == 0) throw new NotFoundException("Not found");
            _logger.LogInformation("Roles not found");
            return _mapper.Map<List<RoleResponse>>(roles);
        }
    }
}
