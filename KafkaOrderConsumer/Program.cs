using System;
using System.Threading;
using Confluent.Kafka;

class Program
{
    static void Main(string[] args)
    {
     
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "order-loggers-group",    
            AutoOffsetReset = AutoOffsetReset.Earliest 
        };

        const string topic = "order-created-topic";
        using var cts = new CancellationTokenSource();

        Console.CancelKeyPress += (_, e) => {
            e.Cancel = true;
            cts.Cancel();
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

        
        consumer.Subscribe(topic);

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[Consumer] Listening for messages on topic: '{topic}'... Press Ctrl+C to exit.");
        Console.ResetColor();

        try
        {
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    
                    var consumeResult = consumer.Consume(cts.Token);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[New Order Trapped!] Time: {DateTime.Now}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Message Data: {consumeResult.Message.Value}");
                    Console.ResetColor();
                }
                catch (ConsumeException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error occurred during consume: {ex.Error.Reason}");
                    Console.ResetColor();
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            consumer.Close();
            Console.WriteLine("[Consumer] Connection closed cleanly.");
        }
    }
}