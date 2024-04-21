namespace Torc.Domain.Util;

public static class LogMessages
{
    private const string _prefixLog = "[TORC SERVICE WORKER]";

    public static string QueueSubscriptionStartedLog(string queueName) => $"{_prefixLog} - RabbitMQ queue subscription initialized: {queueName}";

    public static string ErrorProcessingMessageReceivedLog(string queueName, string ex) => $"{_prefixLog} - ERROR Processing message from queue: {queueName}. Exception: {ex}";

    public static string MessageReceivedLog(string queueName, string json) => $"{_prefixLog} - Message from queue: {queueName} received. Message: {json}";
}
