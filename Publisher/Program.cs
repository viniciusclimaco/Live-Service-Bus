using Azure.Messaging.ServiceBus;

string connectionString = "Endpoint=sb://live-service-bus-raffa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mYRMwrYLsYiDnans6mDCqV+pQI3UeZ4xqoue7Vx8t6I=";
string queueName = "live-raffa";
string sentance = "Sejam todos bem vindos a live do Canal do ShareBook";

var client = new ServiceBusClient(connectionString);
var sender = client.CreateSender(queueName);

Console.WriteLine("Enviando Mensagens...");
foreach (var character in sentance)
{
    var message = new ServiceBusMessage(character.ToString());
    await sender.SendMessageAsync(message);
    Console.WriteLine($"    Enviado: { character }");

}

await sender.CloseAsync();

Console.WriteLine("Mensagens Enviadas...");
Console.ReadLine();