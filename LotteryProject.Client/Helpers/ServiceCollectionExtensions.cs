using FluentValidation;
using LotteryProject.Client.Shared.ViewModels;
using Microsoft.Extensions.Options;


namespace LotteryProject.Client.Helpers
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEntityClientValidations(this IServiceCollection services)
		{
			services.AddScoped<IValidator<GuestViewModel>, GuestViewModelValidator>();
			return services;
		}
	}
}
