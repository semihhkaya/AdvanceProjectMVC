using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Employee;
using AdvanceProjectMVC.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Controllers
{
	public class EmployeeController : Controller
	{
		EmployeeConnectService _employeeConnectService;

		public EmployeeController(EmployeeConnectService employeeConnectService)
		{
			_employeeConnectService = employeeConnectService;

		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> GetAdvanceByEmployeeId()
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return BadRequest();
			}


			var data = await _employeeConnectService.GetAdvanceByEmployeeId(employee.Id);
			
			return View(data);
		}
	}
}
