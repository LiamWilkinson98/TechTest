﻿using System.Collections.Generic;
using UserManagement.Models;

namespace UserManagement.Services.Domain.Interfaces;

public interface IUserService 
{
    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    IEnumerable<User> FilterByActive(bool isActive);
    IEnumerable<User> FilterById(long userId);
    void AddUser(User user);
    void DeleteUser(long userId);
    void UpdateUser(User user);
    IEnumerable<User> GetAll();
}
