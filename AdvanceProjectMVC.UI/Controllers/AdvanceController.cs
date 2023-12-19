using AdvanceProjectMVC.ConnectService;
using AdvanceProjectMVC.Dto.Advance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvanceProjectMVC.UI.Extensions;
using AdvanceProjectMVC.Dto.Employee;
using FluentValidation.Results;
using AdvanceProjectMVC.UI.Validation;
using AdvanceProjectMVC.Dto.Project;

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
			var validator = new AdvanceInsertValidator();
			ValidationResult validationResult = validator.Validate(dto);

			if (!validationResult.IsValid)
			{
				foreach (var error in validationResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				ViewBag.Project = await _projectConnectService.GetProject() ?? new List<ProjectSelectDTO>();
				return View();
			}

			var data = await _advanceConnectService.AddAdvance(dto);
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> AddAdvanceHistoryApprove(int advanceId, int statusId, decimal advanceAmount)
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");

			AdvanceHistoryApproveDTO dto = new AdvanceHistoryApproveDTO
			{
				AdvanceID = advanceId,
				AdvanceAmount = advanceAmount,
				StatusID = statusId + 1,
				TransactorID = employee.Id
			};

			var data = await _advanceConnectService.AddAdvanceHistoryApprove(dto);

			return RedirectToAction("GetUserAdvanceList", "Advance");
		}



		public async Task<List<AdvanceOrderConfirmDTO>> GetAdvanceOrderConfirm(int businessUnitId, List<int> titles)
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return new List<AdvanceOrderConfirmDTO>();
			}

			var data = await _advanceConnectService.GetAdvanceOrderConfirm(businessUnitId, titles);

			return data;
		}

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

		public async Task<List<int>> GetTitleID(decimal advanceAmount)
		{
			var data = await _advanceConnectService.GetTitleID(advanceAmount);

			return data;
		}


		public async Task<IActionResult> GetUserAdvanceList()
		{
			var employee = HttpContext.Session.MyGet<EmployeeSelectDTO>("CurrentUser");
			if (employee is null)
			{
				return RedirectToAction("Auth", "Logout");
			}
			var data = await _advanceConnectService.GetUserAdvanceList(employee.Id, Convert.ToInt32(employee.BusinessUnitId));

			return View(data);
		}

		[HttpGet]
		public async Task<IActionResult> AdvanceReject(int advanceId)
		{
			var rejectAdvanceStatus =  await _advanceConnectService.RejectAdvance(advanceId);
			if (rejectAdvanceStatus)
			{
				return RedirectToAction("GetUserAdvanceList", "Advance");
			}

			return RedirectToAction("GetUserAdvanceList", "Advance");
		}

	}
}
