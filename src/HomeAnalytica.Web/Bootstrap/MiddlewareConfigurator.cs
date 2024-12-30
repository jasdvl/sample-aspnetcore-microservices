using HomeAnalytica.Web.Components;

namespace HomeAnalytica.Web.Bootstrap
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
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // Default HSTS value is 30 days
                // May be changed for production scenarios, see https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            // Disabled HTTPS redirection for debugging purposes. Re-enable in production for secure connections.
            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        }
    }
}
