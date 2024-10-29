namespace HomeAnalytica.Web.Bootstrap
{
    /// <summary>
    /// The CompositionRoot class is responsible for setting up the application’s 
    /// dependency injection and service registration.
    /// </summary>
    /// <remarks>
    /// This class serves as the entry point for configuring the services that 
    /// will be used throughout the application. It creates a <see cref="WebApplicationBuilder"/> 
    /// instance and registers services, configuration settings, and environment 
    /// information necessary for the application to function correctly.
    /// </remarks>
    public class CompositionRoot
    {
        /// <summary>
        /// Creates a <see cref="WebApplicationBuilder"/> instance with the specified 
        /// <paramref name="options"/> and configures the application's services.
        /// </summary>
        /// <param name="options">The options to configure the web application.</param>
        /// <returns>A configured <see cref="WebApplicationBuilder"/> instance.</returns>
        public WebApplicationBuilder CreateBuilder(WebApplicationOptions options)
        {
            var builder = WebApplication.CreateBuilder(options);

            ConfigureAndRegisterServices(builder.Services, builder.Configuration, builder.Environment);

            return builder;
        }

        private void ConfigureAndRegisterServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            ConfigureServices(services, configuration);

            RegisterServices(services);
        }

        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

            // Add services to the container.
            services.AddRazorComponents().AddInteractiveServerComponents();
            services.AddHttpClient();

            //services.AddServerSideBlazor()
            //    .AddHubOptions(options =>
            //    {
            //        options.EnableDetailedErrors = true;
            //    });

        }

        private void RegisterServices(IServiceCollection services)
        {
            // Singleton Services


            // Scoped Services


            // Transient
        }
    }
}
