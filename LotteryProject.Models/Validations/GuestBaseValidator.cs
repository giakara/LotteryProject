using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LotteryProject.Models.Validations
{
	public class GuestBaseValidator<TModel> : AbstractValidator<TModel>
	where TModel : class, IGuestValidationProperties
	{
		public GuestBaseValidator()
		{
			CreateDefaultRules();
		}

		private void CreateDefaultRules()
		{
			RuleSet("GuestItemDetails", () =>
			{
				RuleFor(t => t.GuestName)
				.NotEmpty().WithMessage("Name is Required property and cannot be empty")
				.NotNull().WithMessage("Name is Required property and cannot be null")
				.MaximumLength(100).WithMessage("Maximum Name charachters is 100");

				RuleFor(t => t.GuestSurname)
				.NotEmpty().WithMessage("Surname is Required property and cannot be empty")
				.NotNull().WithMessage("Surname is Required property and cannot be null")
				.MaximumLength(150).WithMessage("Maximum Surname charachters is 150");

				RuleFor(t => t.Email)
				.NotEmpty().WithMessage("Email is Required property and cannot be empty")
				.NotNull().WithMessage("Email is Required property and cannot be null")
				.MaximumLength(150).WithMessage("Maximum Email charachters is 150");


				RuleFor(t => t)
				.Must(x => !x.IsDuplicated).WithMessage("Guest Already Exists");
			});
		}
	}
}
