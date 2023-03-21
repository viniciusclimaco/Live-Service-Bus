using Azure.Messaging.ServiceBus;

string connectionString = "";
string queueName = "vssummit2023";

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