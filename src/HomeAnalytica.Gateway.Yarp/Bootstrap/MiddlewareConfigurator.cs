namespace HomeAnalytica.Gateway.Yarp.Bootstrap
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
            app.MapReverseProxy();
        }
    }
}
