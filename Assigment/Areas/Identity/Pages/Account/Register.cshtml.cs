using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;
using Assigment.Areas.Identity.Data;
using System.Linq;

namespace Assigment.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<AssigmentUser> _signInManager;
        private readonly UserManager<AssigmentUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<AssigmentUser> userManager,
            SignInManager<AssigmentUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please enter your Full Name first before submitting your form!")]
            [StringLength(256, ErrorMessage = "You must enter the value between 6 - 256 chars", MinimumLength = 6)]
            [Display(Name = "FullName")] //label

            public string FullName { get; set; }

            [Required]
            [Display(Name = "Username")]
            public string CustomerName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [RegularExpression(@"^\d{6}-\d{2}-\d{4}$", ErrorMessage = "The IC number must be in the format of xxxxxx-xx-xxxx.")]
            [Display(Name="Ic")]
            public string Ic { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "ConfirmPassword")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
<<<<<<< Updated upstream
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Range(18,100,ErrorMessage="Invalid AGE")]
            [Display(Name = "Customer Age")]
            public int customerAge { get; set; }





=======
            [Display(Name = "PhoneNumber")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "The phone number must be a 10-digit number.")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "RegisteredDate")]
            [DataType(DataType.Date)]

            public DateTime RegisteredDate{ get; set; }

            [Required]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "The address must be between 5 and 100 characters long.")]
            [RegularExpression(@"^[a-zA-Z0-9\s\-\#]+$", ErrorMessage = "The address can only contain letters, numbers, spaces, dashes, and hash symbols.")]
            [Display(Name = "address")]
            public string address { get; set; }
>>>>>>> Stashed changes

        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
<<<<<<< Updated upstream
                var user = new AssigmentUser 
                { UserName = Input.CustomerName, 
                    Email = Input.Email,
                    PhoneNumber = Input.PhoneNumber,
                };
=======
                var user = new AssigmentUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    RegisteredDate = Input.RegisteredDate,
                    Ic = Input.Ic,
                    PhoneNumber=Input.PhoneNumber,
                    address=Input.address,
                    EmailConfirmed = true
                };

>>>>>>> Stashed changes
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "Customer");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        return RedirectToPage("Login");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
