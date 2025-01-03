using HomeAnalytica.DataRegistry.Bootstrap;

WebApplicationOptions options = new WebApplicationOptions { Args = args };

var compRoot = new CompositionRoot();

var builder = compRoot.CreateBuilder(options);

var serviceProvider = builder.Services.BuildServiceProvider();

// Initialize database (migration and seeding)
var dbInitializer = new DatabaseInitializer(serviceProvider);
dbInitializer.Initialize();

var app = builder.Build();

MiddlewareConfigurator.Configure(app, app.Environment);

app.Run();
