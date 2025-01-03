namespace HomeAnalytica.DataRegistry.Bootstrap
{
    /// <summary>
    /// Configures the middleware components for the application.
    /// </summary>
    /// <remarks>
    /// This class is responsible for setting up the middleware pipeline, including routing, 
    /// authentication, authorization, logging, and other middleware components that handle 
    /// incoming requests and outgoing responses. It should be called after the dependency 
    /// injection configuration to ensure that all services are available for the middleware.
    /// </remarks>
    public class MiddlewareConfigurator
    {
        public static void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureRoutes();

            // HTTPS is not required for services behind the reverse proxy,
            // as the proxy handles HTTPS termination and forwards requests via HTTP within the internal network.
            //app.UseHttpsRedirection();
        }
    }
}
