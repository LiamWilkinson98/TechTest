using System;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive)
    {
        //find all users that are either active or inactive, depending on how it's called
        var allUsers = _dataAccess.GetAll<User>().Where(p => p.IsActive == isActive);
        return allUsers;
  
    }

    public IEnumerable<User> FilterById(long userId)
    {
        var user = _dataAccess.GetAll<User>().Where(p => p.Id == userId);
        return user;
    }

    public void AddUser(User user)
    {
        _dataAccess.Create(user);
    }

    public void DeleteUser(long userId)
    {
        User user = new User();
        user.Id = userId;
        _dataAccess.Delete(user);
    }

    public void UpdateUser(User user)
    {
        _dataAccess.Update(user);
    }

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();
}
