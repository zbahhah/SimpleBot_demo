namespace demo1.Bots
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using System.Threading;
    using System.Threading.Tasks;

    public class SimpleBot : IBot
    {
        /// <summary>
        /// When implemented in a bot, handles an incoming activity.
        /// </summary>
        /// <param name="turnContext">The context object for this turn.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task that represents the work queued to execute.
        /// </returns>
        /// <created>10/20/2018</created>
        /// <remarks>
        /// The <paramref name="turnContext" /> provides information about the
        /// incoming activity, and other data needed to process the activity.
        /// </remarks>
        /// <seealso cref="T:Microsoft.Bot.Builder.ITurnContext" />
        /// <seealso cref="T:Microsoft.Bot.Schema.IActivity" />
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if(turnContext.Activity.Type is ActivityTypes.Message)
            {
                var input = turnContext.Activity.Text;
                await turnContext.SendActivityAsync($"SimpleBot: {input}");
            }
        }
    }
}
