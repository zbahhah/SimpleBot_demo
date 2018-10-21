namespace demo2SimpleMiddleware.Middleware
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using System.Threading;
    using System.Threading.Tasks;

    public class SimpleMiddleware2 : IMiddleware
    {
        /// <summary>
        /// Called when [turn asynchronous].
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="next">The next.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <created>10/20/2018</created>
        public async Task OnTurnAsync(ITurnContext context, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await context.SendActivityAsync($"[SimpleMiddleware2] {context.Activity.Type}/OnTurnAsync/Before");

            if (context.Activity.Type == ActivityTypes.Message && context.Activity.Text == "secret pwd")
            {
                // calling next is optional
                await next(cancellationToken);
            }

            await context.SendActivityAsync($"[SimpleMiddleware2] {context.Activity.Type}/OnTurnAsync/After");
        }
    }
}
