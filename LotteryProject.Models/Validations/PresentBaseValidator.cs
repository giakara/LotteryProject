using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace LotteryProject.Models.Validations
{
	public class PresentBaseValidator<TModel> : AbstractValidator<TModel>
	where TModel : class, IPresentValidationProperties
	{
		public PresentBaseValidator()
		{
			CreateDefaultRules();
		}

		private void CreateDefaultRules()
		{
			RuleSet("PresentItemDetails", () =>
			{
				RuleFor(t => t.Description)
				.NotEmpty().WithMessage("Description is Required property and cannot be empty")
				.NotNull().WithMessage("Description is Required property and cannot be null")
				.MaximumLength(100).WithMessage("Maximum Description charachters is 100");



			});
		}
	}
}
