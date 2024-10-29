using HomeAnalytica.Web.Bootstrap;

WebApplicationOptions options = new WebApplicationOptions { Args = args };

var compRoot = new CompositionRoot();

var builder = compRoot.CreateBuilder(options);

var app = builder.Build();

MiddlewareConfigurator.Configure(app, app.Environment);

app.Run();
