## Localization Bug

The `IStringLocalizer` is using the `CurrentCulture` to look up resources, instead of using the CurrentUICulture as it does in Server side  aspnetcore/Blazor.

Possibly caused by this line of code?

https://github.com/dotnet/aspnetcore/blob/a4938d07a5127ffad8466ddf703a6b5b21f4b0c9/src/Components/WebAssembly/WebAssembly/src/Hosting/SatelliteResourcesLoader.cs#L29

` var culturesToLoad = GetCultures(CultureInfo.CurrentCulture);`

As you can see it is loading the CurrentCulture satellite assemblies, not CurrentUICulture. 