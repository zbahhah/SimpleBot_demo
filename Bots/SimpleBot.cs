using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace demo1.Bots
{
    public class SimpleBot : IBot
    {
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
