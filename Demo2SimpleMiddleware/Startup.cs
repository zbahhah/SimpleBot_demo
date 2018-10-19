namespace Demo2SimpleMiddleware
{
    using Demo2SimpleMiddleware.Bots;
    using Demo2SimpleMiddleware.Middleware;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Bot.Builder.BotFramework;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Startup dotnet core class
    /// </summary>
    /// <created>10/20/2018</created>
    public class Startup
    {
        /// <summary>
        /// Gets or sets the content root path.
        /// </summary>
        /// <value>
        /// The content root path.
        /// </value>
        /// <created>10/20/2018</created>
        public string ContentRootPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <created>10/20/2018</created>
        public Startup(IHostingEnvironment env)
        {
            ContentRootPath = env.ContentRootPath;
        }


        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <created>10/20/2018</created>
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            services.AddSingleton(configuration);

            // add your simple bot to your app
            services.AddBot<SimpleBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

                // add middleware
                

                options.Middleware.Add(new SimpleMiddleware1());

                options.Middleware.Add(new SimpleMiddleware2());

            });
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <created>10/20/2018</created>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            app.UseBotFramework();
        }
    }
}
