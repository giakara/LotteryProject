using LotteryProject.Client;
using LotteryProject.Client.Shared.Services.Interfaces;
using LotteryProject.Client.Shared;
using LotteryProject.Client.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using LotteryProject.Client.Shared.ViewModels;
using LotteryProject.Client.Helpers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IGuestService, GuestService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7292/") });
builder.Services.AddEntityClientValidations();
await builder.Build().RunAsync();
