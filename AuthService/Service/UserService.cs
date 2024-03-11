using AuthService.DB;
using AuthService.Entities;
using AuthService.Models;
using Core.KafkaClient;
using ContextSupport = Core.Db.ContextSupport;

namespace AuthService.Service;

public class UserService : IUserService
{
    private readonly UserDbContext _userDbContext;
    private readonly IRoleService _roleService;
    private static MessageBus? _msgBus;

    public UserService(
        ContextSupport.IDbContextFactory<UserDbContext> authContextFactory,
        IRoleService roleService)
    {
        _roleService = roleService;
        _msgBus = new MessageBus();
        _userDbContext = authContextFactory.CreateDbContext();
    }

    public User? SignIn(SignInRequest model)
    {
        var userDbo =
            _userDbContext.Users.FirstOrDefault(u => u.Login == model.Username && u.Password == model.Password);
        if (userDbo == null)
            return null;

        var role = _roleService.GetRole(userDbo.RoleId);
        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
            FirstName = userDbo.FirstName,
            SecondName = userDbo.SecondName,
            Role = role?.RoleName,
            RoleId = (int)role?.RoleId
        };
        return user;
    }

    public User SignUp(SignUpRequest model)
    {
        var userDbo = new UserDbo
        {
            Id = Guid.NewGuid(),
            Login = model.Username,
            Password = model.Password,
            RoleId = 0
        };
        _userDbContext.Add(userDbo);
        _msgBus?.SendMessage("sign-up", new MessageContract(userDbo.Id));
        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
        };
        return user;
    }

    public SignInResponseModel ForgotPassword(string username)
    {
        var userDbo = _userDbContext.Users.FirstOrDefault(u => u.Login == username);
        if (userDbo == null)
            return null;

        var role = _roleService.GetRole(userDbo.RoleId);
        var user = new User
        {
            Id = userDbo.Id,
            Login = userDbo.Login,
            Password = userDbo.Password,
            FirstName = userDbo.FirstName,
            SecondName = userDbo.SecondName,
            Role = role?.RoleName,
            RoleId = (int)role?.RoleId
        };
        return new SignInResponseModel(user);
    }

    public IEnumerable<User> GetAll()
    {
        var roles = _roleService.GetRoles().ToDictionary(r => r.RoleId, r => r.RoleName);
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

    public IEnumerable<User> GetAllForAssign()
    {
        var roles = _roleService.GetRoles().ToDictionary(r => r.RoleId, r => r.RoleName);
        return _userDbContext.Users.Select(u => new User
            {
                Id = u.Id,
                Login = u.Login,
                Password = u.Password,
                FirstName = u.FirstName,
                SecondName = u.SecondName,
                Role = roles[u.RoleId]
            })
            .Where(u => u.RoleId != 0 || u.RoleId != 1)
            .ToArray();
    }

    public User? GetById(Guid userId)
    {
        var userDbo = _userDbContext.Users.FirstOrDefault(u => u.Id == userId);
        if (userDbo == null)
            return null;

        var role = _roleService.GetRole(userDbo.RoleId);
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