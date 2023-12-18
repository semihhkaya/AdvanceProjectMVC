using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Employee;
using AdvanceProjectMVC.UI.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Controllers
{
	[Authorize]
	public class AuthController : Controller
	{
		EmployeeConnectService _employeeService;
		TitleConnectService _titleConnectService;
		BusinessUnitConnectService _businessUnitConnectService;

		public AuthController(EmployeeConnectService employeeService,
							  TitleConnectService titleConnectService,
							  BusinessUnitConnectService businessUnitConnectService)
		{
			_employeeService = employeeService;
			_titleConnectService = titleConnectService;
			_businessUnitConnectService = businessUnitConnectService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Login(EmployeeLoginDTO employeeLoginDto)
		{
			var dto = await _employeeService.Login(employeeLoginDto);
			if (dto == null)
			{
				ModelState.AddModelError(string.Empty, "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");

				return View();
			}

			HttpContext.Response.Cookies.Append("token", dto.Token, new CookieOptions
			{
				Expires = DateTimeOffset.Now.AddSeconds(20),
			});

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier,dto.Id.ToString()),
				new Claim(ClaimTypes.Name,dto.Name),
				new Claim(ClaimTypes.Surname,dto.Surname),
				new Claim(ClaimTypes.Email,dto.Email),
				new Claim(ClaimTypes.Role, dto.Title.TitleName)
			};

			var userIdentity = new ClaimsIdentity(claims, "login");
			var userpri = new ClaimsPrincipal(userIdentity);

			var authProp = new AuthenticationProperties() { ExpiresUtc = DateTimeOffset.Now.AddMinutes(10) };

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userpri, authProp);

			EmployeeSelectDTO employeeSession = new EmployeeSelectDTO()
			{
				Id = dto.Id,
				Email = dto.Email,
				Name = dto.Name,
				Surname = dto.Surname,
				Title = dto.Title,
				PhoneNumber = dto.PhoneNumber,
				TitleId = dto.TitleId,
				BusinessUnitId = dto.BusinessUnitId,
			};
			
			HttpContext.Session.MySet("CurrentUser", employeeSession);

			TempData["EmployeeFullName"] = dto.Name + " " + dto.Surname;
			TempData["EmployeeTitle"] = dto.Title.TitleName;

			return RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		[HttpGet]
		public async Task<IActionResult> Register()
		{
			ViewBag.Title = await _titleConnectService.GetTitle();
			ViewBag.BusinessUnit = await _businessUnitConnectService.GetBusinessUnit();
			ViewBag.Employee = await _employeeService.GetEmployee();

			return View();
		}

		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Register(EmployeeRegisterDTO dto)
		{
			var donendeger = await _employeeService.Register(dto);
			if (donendeger)
			{
				TempData["kullanicidurumu"] = "Kullanıcı başarıyla kaydedildi";
				return RedirectToAction("Login");
			}

			return View();
		}


		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			HttpContext.Session.Clear();

			return RedirectToAction("Login", "Auth");
		}
	}
}
