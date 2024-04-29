using FluentValidation;
using LotteryProject.Client.Shared.ViewModels;
using LotteryProject.Models.Entities;
using Microsoft.Extensions.Options;


namespace LotteryProject.Client.Helpers
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEntityClientValidations(this IServiceCollection services)
		{
			services.AddScoped<IValidator<GuestViewModel>, GuestViewModelValidator>();
			services.AddScoped<IValidator<PresentViewModel>, PresentViewModelValidator>();
			return services;
		}
	}
}
