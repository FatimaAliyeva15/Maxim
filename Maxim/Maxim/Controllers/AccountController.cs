using Maxim.ViewModel;
using Maxim_Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Maxim.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		
		public async Task<IActionResult> CreateRole()
		{
			IdentityRole role1 = new IdentityRole("Admin");
			IdentityRole role2 = new IdentityRole("Member");

			await _roleManager.CreateAsync(role1);
			await _roleManager.CreateAsync(role2);

			return Ok("Rollar yarandi");
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if(!ModelState.IsValid) return View();

			AppUser user = new AppUser()
			{
				FullName = registerVM.Name,
				Email = registerVM.Email,
				UserName = registerVM.UserName,
			};

            var result = await _userManager.CreateAsync(user, registerVM.Password);


            if (!result.Succeeded)
			{
				foreach(var item in result.Errors)
				{
					ModelState.AddModelError("", item.Description);
				}
				return View();
			}

			await _userManager.AddToRoleAsync(user, "Admin");

			return RedirectToAction(nameof(Login));
		}

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if(!ModelState.IsValid) return NotFound();

			AppUser user;

			if (loginVM.UserNameOrEmail.Contains("@"))
			{
				user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
			}
			else
			{
				user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
			}

			if (user == null)
			{
				ModelState.AddModelError("", "Username or password is not valid");
				return View();
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginVM.Password, true);

			if (result.IsLockedOut)
			{
                ModelState.AddModelError("", "Try again shortly");
                return View();
            }

			if(!result.Succeeded)
			{
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

			await _signInManager.SignInAsync(user, loginVM.IsPersistent);

			var role = await _userManager.GetRolesAsync(user);

			if (role.Contains("Admin"))
			{
				return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
        }
	}
}
