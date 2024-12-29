using HomeAnalytica.Web.Bootstrap;
using System.Globalization;

WebApplicationOptions options = new WebApplicationOptions { Args = args };

var compRoot = new CompositionRoot();

var builder = compRoot.CreateBuilder(options);

var app = builder.Build();

MiddlewareConfigurator.Configure(app, app.Environment);

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

app.Run();
