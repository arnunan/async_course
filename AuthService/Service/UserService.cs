using AuthService.Authorization;
using AuthService.DB;
using AuthService.Entities;
using AuthService.Models;
using ContextSupport = Core.Db.ContextSupport;

namespace AuthService.Service;

public class UserService : IUserService
{
    private readonly UserDbContext _userDbContext;
    private readonly RoleDbContext _roleDbContext;

    private readonly IJwtUtils _jwtUtils;

    public UserService(IJwtUtils jwtUtils,
        ContextSupport.IDbContextFactory<UserDbContext> authContextFactory,
        ContextSupport.IDbContextFactory<RoleDbContext> roleContextFactory)
    {
        _jwtUtils = jwtUtils;
        _userDbContext = authContextFactory.CreateDbContext();
        _roleDbContext = roleContextFactory.CreateDbContext();
    }

    public SignInResponseModel? SignIn(SignInRequest model)
    {
        var userDbo =
            _userDbContext.Users.FirstOrDefault(u => u.Login == model.Username && u.Password == model.Password);
        if (userDbo == null)
            return null;

        var role = _roleDbContext.Roles.FirstOrDefault(u => u.RoleId == userDbo.RoleId);
        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
            FirstName = userDbo.FirstName,
            SecondName = userDbo.SecondName,
            Role = role?.RoleName
        };
        var token = _jwtUtils.GenerateJwtToken(user);
        return new SignInResponseModel(user, token);
    }

    public SignUpResponseModel SignUp(SignUpRequest model)
    {
        var userDbo = new UserDbo
        {
            Id = Guid.NewGuid(),
            Login = model.Username,
            Password = model.Password,
            RoleId = 0
        };
        _userDbContext.Users.Add(userDbo);

        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
        };
        var token = _jwtUtils.GenerateJwtToken(user);
        return new SignUpResponseModel(user, token);
    }

    public SignInResponseModel ForgotPassword(string username)
    {
        var userDbo = _userDbContext.Users.FirstOrDefault(u => u.Login == username);
        if (userDbo == null)
            return null;

        var role = _roleDbContext.Roles.FirstOrDefault(u => u.RoleId == userDbo.RoleId);
        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
            FirstName = userDbo.FirstName,
            SecondName = userDbo.SecondName,
            Role = role?.RoleName
        };
        var token = _jwtUtils.GenerateJwtToken(user);
        return new SignInResponseModel(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        var roles = _roleDbContext.Roles.ToDictionary(r => r.RoleId, r => r.RoleName);
        return _userDbContext.Users.Select(u => new User
        {
            Id = u.Id,
            Login = u.Login,
            Password = u.Password,
            FirstName = u.FirstName,
            SecondName = u.SecondName,
            Role = roles[u.RoleId]
        }).ToArray();
    }

    public User? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public User? GetById(Guid userId)
    {
        var userDbo = _userDbContext.Users.FirstOrDefault(u => u.Id == userId);
        if (userDbo == null)
            return null;

        var role = _roleDbContext.Roles.FirstOrDefault(u => u.RoleId == userDbo.RoleId);
        return new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
            FirstName = userDbo.FirstName,
            SecondName = userDbo.SecondName,
            Role = role?.RoleName
        };
    }
}