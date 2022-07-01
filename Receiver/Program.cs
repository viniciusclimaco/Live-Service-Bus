using Azure.Messaging.ServiceBus;

string connectionString = "Endpoint=sb://canalhenriquesouza.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kDokrCcLsJHSKtllj6YGK6gb3ogVmpVGExXMItDyZaE=";
string queueName = "demo1";

var client = new ServiceBusClient(connectionString);
var receiver = client.CreateReceiver(queueName);

Console.WriteLine("Recebendo Mensagens...");

while (true)
{
    var message = await receiver.ReceiveMessageAsync();
    if (message != null)
    {
        Console.Write(message.Body.ToString());        
        await receiver.CompleteMessageAsync(message);
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("Todas as mensagens foram recebidas.");
        break;
    }
}

await receiver.CloseAsync();