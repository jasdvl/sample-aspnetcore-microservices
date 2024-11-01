using HomeAnalytica.DataCollection.Bootstrap;

// WARNING: This project uses a pre-generated certificate (HomeAnalytica.DataCollection.pfx) for demo purposes only.
// NEVER use this in a production environment.

WebApplicationOptions options = new WebApplicationOptions { Args = args };

var compRoot = new CompositionRoot();

var builder = compRoot.CreateBuilder(options);

var app = builder.Build();

MiddlewareConfigurator.Configure(app, app.Environment);

app.Run();
