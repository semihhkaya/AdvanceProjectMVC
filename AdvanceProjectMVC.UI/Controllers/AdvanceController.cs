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

		[HttpGet]
		public async Task<IActionResult> AddAdvanceHistoryApprove(int advanceId, int statusId,decimal advanceAmount)
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");

			AdvanceHistoryApproveDTO dto = new AdvanceHistoryApproveDTO
			{
				AdvanceID = advanceId,
				AdvanceAmount = advanceAmount,
				StatusID = statusId +1,
				TransactorID = employee.Id
				
			};


			var data = await _advanceConnectService.AddAdvanceHistoryApprove(dto);
			return RedirectToAction("GetAdvanceConfirmByEmployee", "Employee");
		}


		//Bu avansı sırası ile kimler onaylayacak?
		public async Task<List<AdvanceOrderConfirmDTO>> GetAdvanceOrderConfirm(int businessUnitId, List<int> titles)
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return new List<AdvanceOrderConfirmDTO>();
			}

			var data = await _advanceConnectService.GetAdvanceOrderConfirm(businessUnitId,titles);

			return data;
		}
		//  -- Bir avansı hangi kişilerin onayladığını gösterir 
		public async Task<List<AdvanceApprovedEmployeeDTO>> GetAdvanceApproveEmployee(int advanceId, List<int> titles)
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return new List<AdvanceApprovedEmployeeDTO>();
			}

			var data = await _advanceConnectService.GetAdvanceApproveEmployee(advanceId, titles);

			return data;
		}
		//  -- 5000 TL'lik gelen avans tutarını hangi TitleId'ler onaylayacak? avans tutarı ile birlikte rule gittim hangi rule id'lerin bu miktarı onaylayacağını buldum 
		// Example Output : TitleId[4, 3]
		public async Task<List<int>> GetTitleID(decimal advanceAmount)
		{
			var data = await _advanceConnectService.GetTitleID(advanceAmount);

			return data;
		}
	}
}
