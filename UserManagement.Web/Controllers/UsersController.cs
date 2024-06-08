using System;
using System.Linq;
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
        if (userType == "All")
        {

        }
        //if Active Only button is clicked, recreate IEnumerable with only active accounts
        else if (userType == "Active")
        {
            items = items.Where(p => p.IsActive == true);
        }
        //if Non Active button is clicked, recreate IEnumerable with only inactive accounts
        else if (userType == "Inactive")
        {
            items = items.Where(p => p.IsActive == false);
        }


        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
