using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Employee;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public async Task<IActionResult> Login(EmployeeLoginDTO dto)
		{
			var result = await _employeeApi.Login(dto);

			if (result.Success && result.Data != null)
			{


				// TempData["kullanicidurumu"] = "Kullanıcı başarıyla giriş yaptı";

				return RedirectToAction("Index","Home"); // veya başka bir sayfaya yönlendirme yapabilirsiniz.
			}

			// Giriş başarısızsa, hata mesajını göster
			ModelState.AddModelError(string.Empty, "Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
			return View();
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
	}
}
