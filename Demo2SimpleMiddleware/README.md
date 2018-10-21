This sample demonstrates a simple echo bot with state with ASP.Net Core 2. The bot maintains a simple counter that increases with each message from the user.

# To try this sample
- Clone the samples repository
```bash
git clone https://github.com/zbahhah/SimpleBot_demo.git
```
- [Optional] Update the `appsettings.json` file under `SimpleBot_demo/demo2SimpleMiddleware` with your botFileSecret.  For Azure Bot Service bots, you can find the botFileSecret under application settings.
# Prerequisites
## Visual Studio
- Navigate to the samples folder (`SimpleBot_demo/demo2SimpleMiddleware`) and open EchoBotWithCounter.csproj in Visual Studio.
- Run the project (press `F5` key).

## Visual Studio Code
- Open `SimpleBot_demo/demo2SimpleMiddleware` sample folder.
- Bring up a terminal, navigate to `SimpleBot_demo/demo2SimpleMiddleware` folder.
- Type `dotnet run`.

## Testing the bot using Bot Framework Emulator
[Microsoft Bot Framework Emulator](https://github.com/microsoft/botframework-emulator) is a desktop application that allows bot 
developers to test and debug their bots on localhost or running remotely through a tunnel.
- Install the Bot Framework emulator from [here](https://aka.ms/botframeworkemulator).

## Connect to bot using Bot Framework Emulator **V4**
- Launch the Bot Framework Emulator.
- File -> Open bot and navigate to `SimpleBot_demo/demo2SimpleMiddleware` folder.
- Select `demo2.bot` file.
# Middleware
Middleware is simply a class that sits between the adapter and your bot logic, added to your adapter's middleware collection during initialization. The SDK allows you to write your own middleware or add reusable components of middleware created by others. Every activity coming into or out of your bot flows through your middleware.

The adapter processes and directs incoming activities in through the bot middleware pipeline to your bot’s logic and then back out again. As each activity flows in and out of the bot, each piece of middleware can inspect or act upon the activity, both before and after the bot logic runs.

Before jumping into middleware, it is important to understand [bots in general](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0) and [how they process activities](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-basics?view=azure-bot-service-4.0#the-activity-processing-stack).

# Deploy this bot to Azure
You can use the [MSBot](https://github.com/microsoft/botbuilder-tools) Bot Builder CLI tool to clone and configure any services this sample depends on. In order to install this and other tools, you can read [Installing CLI Tools](../../../Installing_CLI_tools.md).

To clone this bot, run
```bash
msbot clone services -f deploymentScripts/msbotClone -n <BOT-NAME> -l <Azure-location> --subscriptionId <Azure-subscription-id>
```
# Further reading
- [Azure Bot Service Introduction](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0)
- [Bot State](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-storage-concept?view=azure-bot-service-4.0)
- [Write directly to storage](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-v4-storage?view=azure-bot-service-4.0&tabs=jsechoproperty%2Ccsetagoverwrite%2Ccsetag)
- [Managing conversation and user state](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-v4-state?view=azure-bot-service-4.0&tabs=js)
