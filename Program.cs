using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Globalization;

namespace LocalizationBug
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddLocalization(options =>
			{
				options.ResourcesPath = "Resources";
			});

			var app = builder.Build();

			var JS = app.Services.GetRequiredService<IJSRuntime>() as IJSRuntime;
			var ccname = await JS.InvokeAsync<string>("localStorage.getItem","culture");
			var uiname = await JS.InvokeAsync<string>("localStorage.getItem","uiculture");
			if (string.IsNullOrWhiteSpace(ccname))
			{
				ccname = "en";
			}
			if (string.IsNullOrWhiteSpace(uiname))
			{
				uiname = "en";
			}
			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo(ccname);
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.GetCultureInfo(uiname);
			await app.RunAsync();
		}
	}
}
