﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Assigment.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assigment.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AssigmentUser> _userManager;
        private readonly SignInManager<AssigmentUser> _signInManager;

        public IndexModel(
            UserManager<AssigmentUser> userManager,
            SignInManager<AssigmentUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "address")]
            public string address { get; set; }

            [Display(Name = "Ic")]
            public string Ic { get; set; }

            [Required(ErrorMessage = " You must key in your full name first!")]
            [Display(Name = "FullName")]
            public string FullName { get; set; }

            [Display(Name = "RegisteredDate")]
            [DataType(DataType.Date)]
            public DateTime RegisteredDate { get; set; }

        }

        private async Task LoadAsync(AssigmentUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FullName = user.FullName,
                address =user.address,
                Ic =user.Ic,
                RegisteredDate=user.RegisteredDate,

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.FullName != user.FullName)
            {
                user.FullName = Input.FullName;
            }
            if (Input.address != user.address)
            {
                user.address = Input.address;
            }
            if (Input.RegisteredDate != user.RegisteredDate)
            {
                user.RegisteredDate = Input.RegisteredDate;
            }
            if (Input.Ic != user.Ic)
            {
                user.Ic = Input.Ic;
            }
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
