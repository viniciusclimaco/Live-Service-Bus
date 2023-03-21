using Azure.Messaging.ServiceBus;

string connectionString = "";
string queueName = "vssummit2023";
string sentance = "Sejam todos bem vindos ao VS Summit 2023";

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