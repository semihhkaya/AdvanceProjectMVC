using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Employee;
using AdvanceProjectMVC.UI.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Controllers
{
	public class AuthController : Controller
	{
		EmployeeConnectService _employeeApi;
		TitleConnectService _titleConnectService;
		BusinessUnitConnectService _businessUnitConnectService;

		//GenelSayfaBaglantiServis _genelApi;
		public AuthController(EmployeeConnectService employeeApi,TitleConnectService titleConnectService,BusinessUnitConnectService businessUnitConnectService)
		{
			_employeeApi = employeeApi;
			_titleConnectService = titleConnectService;
			_businessUnitConnectService = businessUnitConnectService;
			//_genelApi = genelApi;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(EmployeeLoginDTO employeeLoginDto)
		{
			var dto = await _employeeApi.Login(employeeLoginDto);
			if (dto == null)
			{
				// Giriş başarısızsa, hata mesajını göster
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

			var authProp = new AuthenticationProperties() { ExpiresUtc = DateTimeOffset.Now.AddMinutes(1) };
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userpri,authProp);

			//Sessiona değer atadım (extension)
			HttpContext.Session.MySet("CurrentUser", dto);

			TempData["EmployeeFullName"] = dto.Name + " " + dto.Surname;
			TempData["EmployeeTitle"] = dto.Title.TitleName;

			return RedirectToAction("Index", "Home");


		}


		[HttpGet]
		public async Task<IActionResult> Register()
		{
			ViewBag.Title = await _titleConnectService.GetTitle();
			ViewBag.BusinessUnit = await _businessUnitConnectService.GetBusinessUnit();
			ViewBag.Employee = await _employeeApi.GetEmployee();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(EmployeeRegisterDTO dto)
		{
			var donendeger = await _employeeApi.Register(dto);
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
