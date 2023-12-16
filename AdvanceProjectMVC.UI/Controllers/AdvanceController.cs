using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Advance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvanceProjectMVC.UI.Extensions;
using AdvanceProjectMVC.Dto.Employee;

namespace AdvanceProjectMVC.UI.Controllers
{
	public class AdvanceController : Controller
	{
		AdvanceConnectService _advanceConnectService;
		ProjectConnectService _projectConnectService;
		public AdvanceController(AdvanceConnectService advanceConnectService, ProjectConnectService projectConnectService)
		{
			_advanceConnectService = advanceConnectService;
			_projectConnectService = projectConnectService;
		}
		[HttpGet]
		public async Task<IActionResult> AddAdvance()
		{
			ViewBag.Project = await _projectConnectService.GetProject();
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return BadRequest();
			}

			return View(new AdvanceInsertDTO()
			{
				EmployeeId = employee.Id
			});
		}

		[HttpPost]
		public async Task<IActionResult> AddAdvance(AdvanceInsertDTO dto)
		{

			var data = await _advanceConnectService.AddAdvance(dto);
			return RedirectToAction("Index", "Home");
		}
	}
}
