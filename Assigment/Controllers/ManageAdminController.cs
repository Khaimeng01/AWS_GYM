using Assigment.Areas.Identity.Data;
using Assigment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageAdminController : Controller
    {
        private readonly UserManager<AssigmentUser> _userManager;

        public ManageAdminController(UserManager<AssigmentUser> userManager) // Change this line to use AssigmentUser
        {
            _userManager = userManager;
        }


        public IActionResult Index()
        {
            var users = _userManager.GetUsersInRoleAsync("Admin").Result.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                // Handle the case where deleting the user failed
                ModelState.AddModelError("", "Failed to delete the user");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AddAdminView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _userManager.Users.SingleOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email has already been registered.");
                    return View("AddAdminView", model); ; // Return the view with the error message
                }

                var user = new AssigmentUser
                {
                    UserName = model.Email,
                    FullName = model.FullName,
                    Email = model.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction(nameof(Index));
                }

            }
            else
            {
                if (string.IsNullOrEmpty(model.FullName))
                {
                    ModelState.AddModelError("FullName", "Custom full name error message.");
                }
                if (string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("Email", "Custom email error message.");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Custom password error message.");
                }
            }

            return View("AddAdminView", model);
        }


        public IActionResult EditAdminView(string id)
        {

                if (string.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                var model = new EditAdminModel
                {
                    FullName = user.FullName,
                    Id = user.Id,
                    Email = user.Email,
                };

                return View(model);
        }

        public async Task<IActionResult> Edit(EditAdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                var existingUser = _userManager.Users.SingleOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email has already been registered.");
                    return View("EditAdminView", model); ; // Return the view with the error message
                }

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Email;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.FullName))
                {
                    ModelState.AddModelError("FullName", "Custom full name error message.");
                }
                if (string.IsNullOrEmpty(model.Email))
                {
                    ModelState.AddModelError("Email", "Custom email error message.");
                }
            }

            return View("EditAdminView", model);
        }


        public async Task<IActionResult> updatePasswordView(string id) // Add the id parameter here
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(id); // Use FindByIdAsync() to search by id
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{id}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return View();
        }

    }
}
