using AdvanceProjectMVC.Dto.Advance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.UI.Validation
{
	public class AdvanceInsertValidator:AbstractValidator<AdvanceInsertDTO>
	{
		public AdvanceInsertValidator()
		{
            RuleFor(dto => dto.AdvanceAmount)
                           .NotNull().WithMessage("Avans tutarı gereklidir.")
                           .GreaterThan(0).WithMessage("Avans tutarı sıfırdan büyük olmalıdır.");

            RuleFor(dto => dto.AdvanceDescription)
                .NotEmpty().WithMessage("Avans açıklaması gereklidir.")
                .MaximumLength(255).WithMessage("Avans açıklaması en fazla 255 karakter olmalıdır.");

            RuleFor(dto => dto.ProjectId)
                .NotNull().WithMessage("Proje seçimi gereklidir.");

            RuleFor(dto => dto.DesiredDate)
                .NotNull().WithMessage("İstenen Tarih gereklidir.")
                .GreaterThan(DateTime.UtcNow).WithMessage("İstenen Tarih gelecekte bir tarih olmalıdır.");


        }
	}
}
