using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.Services.Domain.Implementations;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(string userType)
    {
        var items = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });
        //continue as normal if Show All is selected
        if (userType == "All") { }
        //if Active Only button is clicked, call FilterByActive and select only active accounts
        else if (userType == "Active")
        {
            items = _userService.FilterByActive(true).Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                IsActive = p.IsActive
            });
        }
        //if Non Active button is clicked, call FilterByActive and select only inactive accounts
        else if (userType == "Inactive")
        {
            items = _userService.FilterByActive(false).Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                DateOfBirth = p.DateOfBirth,
                IsActive = p.IsActive
            });
        }


        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    [Route("ViewUser")]
    [HttpGet]
    public ViewResult ViewUser(long userId)
    {
        //call FilterById to select user account with matching ID
        var items = _userService.FilterById(userId).Select(p => new UserListItemViewModel
        {
            Id = userId,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });
        var model = new UserListViewModel{Items = items.ToList()};
        return View(model);
    }

    [Route("AddUser")]
    public ViewResult AddUser()
    {        
        //change to AddUser page
        return View();
    }

    
    [HttpPost]
    public ActionResult CreateUser(User user)
    {
        //add user and return to user list page
        _userService.AddUser(user);
        return RedirectToAction();
    }
    [Route("DeleteUser")]
    public ActionResult DeleteUser(long userId)
    {
        _userService.DeleteUser(userId);
        return RedirectToAction("");
    }

    [Route("EditUser")]
    [HttpGet]
    public ActionResult EditUser(long userId)
    {
        //call FilterById to select user account with matching ID
        var items = _userService.FilterById(userId).Select(p => new UserListItemViewModel
        {
            Id = userId,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive
        });

        var user = items.First();

        //Populate EditUser page with user that has been selected
        return View(user);
    }
    [Route("ApplyEditUser")]
    [HttpPost]
    public ActionResult ApplyEditUser(User user)
    {
        _userService.UpdateUser(user);
        return RedirectToAction("");
    }

}
