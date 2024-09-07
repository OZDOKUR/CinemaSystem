using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;

public class UserManager : IUserService
{
    private readonly IUserDal _userDal;

    public UserManager(IUserDal userDal)
    {
        _userDal = userDal;
    }

    public void Add(AppUser entity)
    {
        _userDal.Add(entity);
    }

    public void Delete(AppUser entity)
    {
        _userDal.Delete(entity);
    }

    public void Delete(int id)
    {
        var user = _userDal.GetById(id);
        if (user != null)
        {
            _userDal.Delete(user);
        }
    }

    public List<AppUser> GetAll()
    {
        return _userDal.List();
    }

    public AppUser GetById(int id)
    {
        return _userDal.GetById(id);
    }

    public List<AppUser> GetUsersByRoleId(int roleId)
    {
        return _userDal.GetUsersByRoleId(roleId);
    }

    public void DeleteByRoleUserId(int id)
    {
       
    }

    public void Update(AppUser user)
    {
        var resultUser = _userDal.GetById(user.Id);
        if (resultUser != null)
        {
            resultUser.Name = user.Name;
            resultUser.Surname = user.Surname;
            resultUser.Email = user.Email;
            resultUser.UserName = user.Email;
            resultUser.PhoneNumber = user.PhoneNumber;
            resultUser.BirthDate = user.BirthDate;
            resultUser.NormalizedEmail = user.Email.ToUpper();
            resultUser.NormalizedUserName = user.Email.ToUpper();
            _userDal.Update(resultUser);
        }
    }
}
