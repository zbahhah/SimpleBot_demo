namespace demo4Dialog.Bots
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Microsoft.Bot.Builder.Dialogs;
    using System.Threading;
    using System.Threading.Tasks;
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <created>10/21/2018</created>
    /// <seealso cref="Microsoft.Bot.Builder.IBot" />
    public class DialogBot : IBot
    {
        private readonly DialogSet dialogSet;
        private readonly BotAccessors botAccessors;
        private const string WelcomeText = "Welcome to Simple Prompt Bot. This bot will introduce you to prompts. Type anything to get started.";


        /// <summary>
        /// Initializes a new instance of the <see cref="DialogBot"/> class.
        /// </summary>
        /// <created>10/21/2018</created>
        public DialogBot(BotAccessors accessors)
        {
            // compose dialog
            botAccessors = accessors ?? throw new ArgumentNullException(nameof(accessors));
            dialogSet = new DialogSet(accessors.DialogStateAccessor);
            dialogSet.Add(new TextPrompt("name"));
        }

        /// <summary>
        /// When implemented in a bot, handles an incoming activity.
        /// </summary>
        /// <param name="turnContext">The context object for this turn.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task that represents the work queued to execute.
        /// </returns>
        /// <created>10/21/2018</created>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <remarks>
        /// The <paramref name="turnContext" /> provides information about the
        /// incoming activity, and other data needed to process the activity.
        /// </remarks>
        /// <seealso cref="T:Microsoft.Bot.Builder.ITurnContext" />
        /// <seealso cref="T:Microsoft.Bot.Schema.IActivity" />
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext == null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var converstationId = $"Conversation.Id: {turnContext.Activity?.Conversation?.Id}\n";

            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            if (turnContext.Activity.Type is ActivityTypes.Message)
            {
                // Run the DialogSet - let the framework identify the current state of the dialog from
                // the dialog stack and figure out what (if any) is the active dialog.
                var dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);
                var results = await dialogContext.ContinueDialogAsync(cancellationToken);

                // If the DialogTurnStatus is Empty we should start a new dialog.
                if (results.Status == DialogTurnStatus.Empty)
                {
                    // A prompt dialog can be started directly on the DialogContext. The prompt text is given in the PromptOptions.
                    await dialogContext.PromptAsync(
                        "name",
                        new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") },
                        cancellationToken);
                }

                // We had a dialog run (it was the prompt). Now it is Complete.
                else if (results.Status == DialogTurnStatus.Complete)
                {
                    // Check for a result.
                    if (results.Result != null)
                    {
                        // Finish by sending a message to the user. Next time ContinueAsync is called it will return DialogTurnStatus.Empty.
                        await turnContext.SendActivityAsync(MessageFactory.Text($"Thank you, I have your name as '{results.Result}'."));
                    }
                }
            }
            // Processes ConversationUpdate Activities to welcome the user.
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded != null)
                {
                    await SendWelcomeMessageAsync(turnContext, cancellationToken);
                }
            }
            else
            {
                await turnContext.SendActivityAsync($"{converstationId}Non-Message {turnContext.Activity.Type} activity type event detected");
            }

            // Save the new turn count into the conversation state.
            // it's a very important line of code to save 
            await botAccessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        /// <summary>
        /// Sends a welcome message to the user.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <created>10/21/2018</created>
        private static async Task SendWelcomeMessageAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var reply = turnContext.Activity.CreateReply();
                    reply.Text = WelcomeText;
                    await turnContext.SendActivityAsync(reply, cancellationToken);
                }
            }
        }
    }
}
