using Assigment.Areas.Identity.Data;
using Assigment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Controllers
{
    public class ManageCustomerController : Controller
    {
        private readonly UserManager<AssigmentUser> _userManager;
        private readonly SignInManager<AssigmentUser> _signInManager; 

        public ManageCustomerController(UserManager<AssigmentUser> userManager, SignInManager<AssigmentUser> signInManager) // Change this line to use AssigmentUser
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                {   UserName = model.Username,
                    Email = model.Email, 
                    PhoneNumber = model.PhoneNumber,
                    address = model.address,
                    RegisteredDate = model.RegisteredDate,
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

                user.Email = model.Email;
                user.UserName = model.UserName;
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

            return View(model);
        }
    }
}
