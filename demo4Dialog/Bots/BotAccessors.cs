namespace demo4Dialog.Bots
{
    using System;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;

    /// <summary>
    /// This class is created as a Singleton and passed into the IBot-derived constructor.
    ///  - See <see cref="DialogBot"/> constructor for how that is injected.
    ///  - See the Startup.cs file for more details on creating the Singleton that gets
    ///    injected into the constructor.
    /// </summary>
    /// <created>10/21/2018</created>
    public class BotAccessors
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BotAccessors"/> class.
        /// Contains the <see cref="ConversationState"/> and associated <see cref="IStatePropertyAccessor{T}"/>.
        /// </summary>
        /// <param name="conversationState">State of the conversation.</param>
        /// <created>10/21/2018</created>
        /// <exception cref="ArgumentNullException">conversationState</exception>
        public BotAccessors(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }

        /// <summary>
        /// Gets the state of the conversation.
        /// </summary>
        /// <value>
        /// The state of the conversation.
        /// </value>
        /// <created>10/21/2018</created>
        public ConversationState ConversationState { get; }


        /// <summary>
        /// Gets or sets the dialog state accessor.
        /// </summary>
        /// <value>
        /// The dialog state accessor.
        /// </value>
        /// <created>10/21/2018</created>
        public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }

        /// <summary>
        /// Gets the name of the dialog state accessor.
        /// </summary>
        /// <value>
        /// The name of the dialog state accessor.
        /// </value>
        /// <created>10/21/2018</created>
        public static string DialogStateAccessorName { get; } = $"{nameof(BotAccessors)}.DialogStateAccessor";
    }
}
