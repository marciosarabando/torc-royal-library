namespace Torc.Domain.Options;

public class RabbitMQOptions
{
    public string Url { get; set; }
    public string Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public QueueProfile PaymentConfirmedQueue { get; set; }
}

public class QueueProfile
{
    public string QueueName { get; set; }
    public string VirtualHost { get; set; }
}
