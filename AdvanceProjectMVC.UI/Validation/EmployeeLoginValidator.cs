using AdvanceProjectMVC.Dto.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Validation
{
	public class EmployeeLoginValidator:AbstractValidator<EmployeeLoginDTO>
	{
		public EmployeeLoginValidator()
		{
			RuleFor(dto => dto.Email)
				.NotEmpty().WithMessage("E-posta adresi boş olamaz.")
				.EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

			RuleFor(dto => dto.Password)
				.NotEmpty().WithMessage("Şifre boş olamaz.")
				.MinimumLength(3).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.");
		}
	}
}
