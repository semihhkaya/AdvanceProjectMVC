using AdvanceProjectMVC.Dto.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Validation
{
	public class EmployeeRegisterValidator:AbstractValidator<EmployeeRegisterDTO>
	{
		public EmployeeRegisterValidator()
		{
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş bırakılamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası alanı boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakterden oluşmalıdır.");
            RuleFor(x => x.BusinessUnitId).NotEmpty().WithMessage("İş Birimi ID boş bırakılamaz.");
            RuleFor(x => x.TitleId).NotEmpty().WithMessage("Ünvan ID boş bırakılamaz.");
            RuleFor(x => x.UpperEmployeeId).NotEmpty().WithMessage("Üst Çalışan ID boş bırakılamaz.");
        }
	}
}
