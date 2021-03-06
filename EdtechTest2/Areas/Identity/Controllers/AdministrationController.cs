using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EdtechTest2.Areas.Identity.ViewModels;
using EdtechTest2.Models;
using EdtechTest2.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EdtechTest2.Areas.Identity.Controllers
{
    [Area("Identity")]
   // [Authorize(Roles = SD.InstructorUser+SD.StudentUser)]


    public class AdministrationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AdministrationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel obj)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = obj.RoleName
                };
                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {

                    return RedirectToAction("ListRoles", "Administration", new { area = "Identity" });

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }


        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                var users = _userManager.Users.ToList();


                var userNames = new List<string>();

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        userNames.Add(user.UserName);
                    }
                }

                var obj = new EditRoleViewModel
                {
                    Id = role.Id,
                    RoleName = role.Name,
                    Users = userNames

                };




                return View(obj);
            }
            ViewBag.Message = "User With Given Id Not Found";
            return View("NotFound");

        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> obj, string roleId)
        {
            // It Would Be great if we Delete all The Roles of it and Add The roles which are selected……...so it is short and sweet

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ModelState.AddModelError("", "No Role  Exist With This Id");
                return RedirectToAction(nameof(EditUsersInRole));
            }

            for (int i = 0; i < obj.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(obj[i].UserId);


                if (obj[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                    {
                        return RedirectToAction("EditUsersInRole", role.Id);

                    }
                }
                else if (!(obj[i].IsSelected) && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    if (!result.Succeeded)
                    {
                        return RedirectToAction("EditUsersInRole", role.Id);
                    }
                }

                else
                {
                    continue;
                }
            }

            return RedirectToAction("EditRole", new { id = role.Id });
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);



            if (role == null)
            {
                return View();
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,

                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;

                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = _userManager.Users;
            return View(users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {

                return RedirectToAction(nameof(ListUsers));
            }

            var userRoles = await _userManager.GetRolesAsync(user);


            var model = new EditUserViewModel
            {
                Id = user.Id,
                Roles = userRoles,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateofBirth = user.DateofBirth.ToShortDateString(),
                JoinedOn = user.JoinedDate.ToString(),
                 PhoneNumber=user.PhoneNumber,
                  UniqueUserNameId=user.UserName,


            };


            return View(model);
        }

        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            //   ViewBag.userId = userId+"kkk";
            ViewBag.userId = "kkk";

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.Message = "User Not Foud With The Given Id";
                return View("NoFound");
            }

            var model = new List<UserRolesViewModel>();

            foreach (var item in _roleManager.Roles)
            {
                var roleVm = new UserRolesViewModel
                {
                    RoleId = item.Id,
                    RoleName = item.Name,
                };
                if (await _userManager.IsInRoleAsync(user, roleVm.RoleName))
                {
                    roleVm.IsSelected = true;
                }
                else
                {
                    roleVm.IsSelected = false;

                }

                model.Add(roleVm);

            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);


            if (user == null)
            {
                return RedirectToAction("ManageUserRoles", new { userId = userId });
            }


            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            // this is the best way to do that
            foreach (var item in model)
            {
                if (item.IsSelected)
                {
                    var op = await _userManager.AddToRoleAsync(user, item.RoleName);
                    if (!op.Succeeded)
                    {
                        foreach (var error in op.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }

            }




            return RedirectToAction("EditUser", new { id = user.Id });

        }

    }
}
