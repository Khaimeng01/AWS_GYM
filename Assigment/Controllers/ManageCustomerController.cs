using Assigment.Areas.Identity.Data;
using Assigment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageCustomerController : Controller
    {
        private readonly UserManager<AssigmentUser> _userManager;
        private readonly SignInManager<AssigmentUser> _signInManager;
        private readonly ILogger<ManageCustomerController> _logger;

        public ManageCustomerController(UserManager<AssigmentUser> userManager, 
            SignInManager<AssigmentUser> signInManager, ILogger<ManageCustomerController> logger) // Change this line to use AssigmentUser
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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


        public IActionResult Index()
        {
            var users = _userManager.GetUsersInRoleAsync("Customer").Result.ToList();
            return View(users);
        }

        public IActionResult AddCustomerView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
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
                {   UserName = model.Email,
                    FullName = model.FullName,
                    Email = model.Email, 
                    PhoneNumber = model.PhoneNumber,
                    address = model.address,
                    RegisteredDate = DateTime.Now,
                    Ic = model.Ic,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, "Customer");
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

                if (string.IsNullOrEmpty(model.address))
                {
                    ModelState.AddModelError("address", "Custom address error message.");
                }
                if (string.IsNullOrEmpty(model.Ic))
                {
                    ModelState.AddModelError("Ic", "Custom IC error message.");
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    ModelState.AddModelError("Password", "Custom password error message.");
                }


            }
            return View("AddCustomerView", model);
        }

        public IActionResult EditCustomerView(string id)
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

            var model = new editUserViewModel
            {
                FullName = user.FullName,
                UserName = user.UserName,
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                address = user.address,
                RegisteredDate = user.RegisteredDate,
                Ic = user.Ic,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(editUserViewModel model)
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
                    return View("EditCustomerView", model); ; // Return the view with the error message
                }

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.address = model.address;
                user.Ic = model.Ic;
                user.RegisteredDate = model.RegisteredDate;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
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

                if (string.IsNullOrEmpty(model.address))
                {
                    ModelState.AddModelError("address", "Custom address error message.");
                }
                if (string.IsNullOrEmpty(model.Ic))
                {
                    ModelState.AddModelError("Ic", "Custom IC error message.");
                }
               
            }

            return View("EditCustomerView", model);
        }


        public async Task<IActionResult> ResetPassword(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "GymSession");
            }

            // Find the user by their ID
            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                // Handle the case where the user was not found
                return RedirectToAction("Index", "ManageAdmin");
            }

            // Generate a new password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Define the new password
            string newPassword = user.Ic+"_Reset"; // Make sure to use a strong password

            // Reset the user's password using the token
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                // The password has been reset successfully
                return RedirectToAction("Index", "ManageCustomer");
            }
            else
            {
                return RedirectToAction("Index", "GymSession");
                //Handle the case where the password reset failed
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
                //return View();
            }
        }
    }
}
