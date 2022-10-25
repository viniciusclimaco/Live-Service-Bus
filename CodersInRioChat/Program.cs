using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

string connectionString = "Endpoint=sb://live-service-bus-raffa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mYRMwrYLsYiDnans6mDCqV+pQI3UeZ4xqoue7Vx8t6I=";
string topicName = "chatcoders";

Console.WriteLine("Digite seu nome:");
var userName = Console.ReadLine();

var serviceBusAdministrationClient = new ServiceBusAdministrationClient(connectionString);

// Cria um tópico caso não exista
if (!await serviceBusAdministrationClient.TopicExistsAsync(topicName))
    await serviceBusAdministrationClient.CreateTopicAsync(topicName);

//Cria uma subscrição temporária para o usuário se não existir
if (!await serviceBusAdministrationClient.SubscriptionExistsAsync(topicName, userName))
{
    var options = new CreateSubscriptionOptions(topicName, userName)
    {
        AutoDeleteOnIdle = TimeSpan.FromMinutes(5)
    };
    await serviceBusAdministrationClient.CreateSubscriptionAsync(options);
}

var serviceBusClient = new ServiceBusClient(connectionString);
var serviceBusSender = serviceBusClient.CreateSender(topicName);

//Instancia um Message Processor
var processor = serviceBusClient.CreateProcessor(topicName, userName);
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

await processor.StartProcessingAsync();

var helloMessage = new ServiceBusMessage($"{ userName } entrou na sala.");
await serviceBusSender.SendMessageAsync(helloMessage);

while (true)
{
    var text = Console.ReadLine();
    if (text == "exit")    
        break;
    
    var message = new ServiceBusMessage($"{userName}> { text }");
    await serviceBusSender.SendMessageAsync(message);
}

var goodbyeMessage = new ServiceBusMessage($"{userName} saiu da sala.");
await serviceBusSender.SendMessageAsync(goodbyeMessage);

await processor.StopProcessingAsync();

await processor.CloseAsync();
await serviceBusSender.CloseAsync();

#region "Metodos"

async Task MessageHandler(ProcessMessageEventArgs args)
{
    var test = args.Message.Body.ToString();
    Console.WriteLine(test);

    await args.CompleteMessageAsync(args.Message);
}

async Task ErrorHandler(ProcessErrorEventArgs args)
{
    throw new NotImplementedException();
}

#endregion
