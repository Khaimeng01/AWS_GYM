using Assigment.Areas.Identity.Data;
using Assigment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
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
            var users = _userManager.Users.ToList();
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
                var user = new AssigmentUser 
                {   UserName = model.Email,
                    FullName = model.FullName,
                    Email = model.Email, 
                    PhoneNumber = model.PhoneNumber,
                    address = model.address,
                    RegisteredDate = DateTime.Now,
                    Ic = model.Ic,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                return RedirectToAction("Index", "GymSession");
            }
            return View(model);
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

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                return RedirectToAction("Index", "GymSession");
            }

            return View(model);
        }
    }
}
