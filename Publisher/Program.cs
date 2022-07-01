using Azure.Messaging.ServiceBus;

string connectionString = "Endpoint=sb://canalhenriquesouza.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kDokrCcLsJHSKtllj6YGK6gb3ogVmpVGExXMItDyZaE=";
string queueName = "demo1";
string sentance = "Sejam todos bem vindos a live do Canal do Henrique Souza";

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