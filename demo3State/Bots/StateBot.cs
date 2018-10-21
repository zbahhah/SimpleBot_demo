namespace demo3State.Bots
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using System.Threading;
    using System.Threading.Tasks;

    //https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/02.echo-with-counter
    public class StateBot : IBot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EchoWithCounterBot"/> class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        public StateBot(BotAccessors botAccessors)
        {
            _accessors = botAccessors ?? throw new System.ArgumentNullException(nameof(botAccessors));
        }

        private readonly BotAccessors _accessors;

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            var converstationId = $"Conversation.Id: {turnContext.Activity?.Conversation?.Id}\n";

            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                // Get the conversation state from the turn context.
                var oldState = await _accessors.CounterStatePropertyAccessor.GetAsync(turnContext, () => new CounterState());

                // Bump the turn count for this conversation.
                var newState = new CounterState { TurnCount = oldState.TurnCount + 1 };

                // Set the property using the accessor.
                await _accessors.CounterStatePropertyAccessor.SetAsync(turnContext, newState);

                // Save the new turn count into the conversation state 
                //(on conversation level or saveing a state per each conversation ).
                await _accessors.ConversationState.SaveChangesAsync(turnContext);                

                // Echo back to the user whatever they typed.
                var responseMessage = $"{converstationId}Turn {newState.TurnCount}: You sent '{turnContext.Activity.Text}'\n";

                await turnContext.SendActivityAsync(responseMessage);
            }
            else
            {                
                await turnContext.SendActivityAsync($"{converstationId}Non-Message {turnContext.Activity.Type} activity type event detected");
            }
        }
    }
}
