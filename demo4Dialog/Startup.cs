namespace demo4Dialog
{
    using demo4Dialog.Bots;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.BotFramework;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Integration;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Configuration;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;

    /// <summary>
    /// Startup dotnet core class
    /// </summary>
    /// <created>10/21/2018</created>
    public class Startup
    {
        /// <summary>
        /// Gets or sets the content root path.
        /// </summary>
        /// <value>
        /// The content root path.
        /// </value>
        /// <created>10/21/2018</created>
        public string ContentRootPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <created>10/21/2018</created>
        public Startup(IHostingEnvironment env)
        {
            ContentRootPath = env.ContentRootPath;
        }


        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <created>10/21/2018</created>
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            services.AddSingleton(configuration);

            // add your state bot to your app
            services.AddBot<DialogBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);

                // The Memory Storage used here is for local bot debugging only. When the bot
                // is restarted, everything stored in memory will be gone.
                IStorage dataStore = new MemoryStorage();

                // Create Conversation State object.
                // The Conversation State object is where we persist anything at the conversation-scope.
                var conversationState = new ConversationState(dataStore);

                options.State.Add(conversationState);

            });

            // Create and register state accessors.
            // Accessors created here are passed into the IBot-derived class on every turn.
            // https://github.com/Microsoft/BotBuilder-Samples/blob/master/samples/csharp_dotnetcore/04.simple-prompt/
            services.AddSingleton<BotAccessors>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
                if (options == null)
                {
                    throw new InvalidOperationException("BotFrameworkOptions must be configured prior to setting up the state accessors");
                }

                var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
                if (conversationState == null)
                {
                    throw new InvalidOperationException("ConversationState must be defined and added before adding conversation-scoped state accessors.");
                }

                // The dialogs will need a state store accessor. Creating it here once (on-demand) allows the dependency injection
                // to hand it to our IBot class that is create per-request.
                var accessors = new BotAccessors(conversationState)
                {
                    DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateAccessorName),
                };

                return accessors;

            });
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <created>10/21/2018</created>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();

            // Tell your application to use Bot Framework
            app.UseBotFramework();
        }
    }
}
