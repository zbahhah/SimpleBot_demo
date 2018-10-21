This sample demonstrates the use of prompts with ASP.Net Core 2.

# To try this sample
- Clone the samples repository
```bash
git clone https://github.com/zbahhah/SimpleBot_demo.git
```
- [Optional] Update the `appsettings.json` file under `SimpleBot_demo/demo4Dialog` with your botFileSecret.  For Azure Bot Service bots, you can find the botFileSecret under application settings.
# Prerequisites
## Visual Studio
- Navigate to the samples folder (`SimpleBot_demo/demo4Dialog`) and open SimplePromptBot.csproj in Visual Studio.
- Hit F5.

## Visual Studio Code
- Open `SimpleBot_demo/demo4Dialog` sample folder.
- Bring up a terminal, navigate to `SimpleBot_demo/demo4Dialog` folder.
- Type `dotnet run`.

## Update packages
- In Visual Studio right click on the solution and select "Restore NuGet Packages".
- In Visual Studio Code type `dotnet restore`
## Testing the bot using Bot Framework Emulator
[Microsoft Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot 
developers to test and debug their bots on localhost or running remotely through a tunnel.
- Install the [Bot Framework emulator](https://aka.ms/botframeworkemulator).

## Connect to bot using Bot Framework Emulator **V4**
- Launch the Bot Framework Emulator.
- File -> Open bot and navigate to `SimpleBot_demo/demo4Dialog` folder.
- Select `demo4.bot` file.
# Deploy this bot to Azure
You can use the [MSBot](https://github.com/microsoft/botbuilder-tools) Bot Builder CLI tool to clone and configure any services this sample depends on. Ensure you have [Node.js](https://nodejs.org/) version 8.5 or higher.

To install all Bot Builder tools

```bash
npm i -g msbot chatdown ludown qnamaker luis-apis botdispatch luisgen
```
To clone this bot, run
```
msbot clone services -f deploymentScripts/msbotClone -n <BOT-NAME> -l <Azure-location> --subscriptionId <Azure-subscription-id>
```
# Further reading
- [Azure Bot Service](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Bot Storage](https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-state?view=azure-bot-service-3.0&viewFallbackFrom=azure-bot-service-4.0)
