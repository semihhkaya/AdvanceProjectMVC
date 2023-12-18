using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Employee;
using AdvanceProjectMVC.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
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
				return RedirectToAction("Auth","Logout");
			}

			var data = await _employeeConnectService.GetAdvanceByEmployeeId(employee.Id);
			
			return View(data);
		}

		[HttpGet]
		public async Task<IActionResult> GetAdvanceDetails(int advanceId)
		{

			var data = await _employeeConnectService.GetAdvanceDetails(advanceId);
			if (data!=null)
			{
				return View(data);
			}

			return BadRequest();
		}

		[HttpGet]//onay bekleyen avans talepleri
		public async Task<IActionResult> GetAdvanceConfirmByEmployee()
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return RedirectToAction("Auth", "Logout");
			}

			var data = await _employeeConnectService.GetAdvanceConfirmByEmployee(employee.Id);

			return View(data);
		}

	}
}
