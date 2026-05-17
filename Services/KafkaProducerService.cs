using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly ProducerConfig _config;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration, ILogger<KafkaProducerService> logger)
        {
            _configuration = configuration;
            _logger = logger;

            
            var bootstrapServers = _configuration["Kafka:BootstrapServers"] ?? "localhost:9092";
            _topic = _configuration["Kafka:OrderTopic"] ?? "order-created-topic";

            _config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                MessageTimeoutMs = 4000, 
                RequestTimeoutMs = 4000
            };
        }

        public async Task ProduceAsync(string message)
        {
            try
            {
                
                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
                    _logger.LogInformation("Successfully published message to Kafka topic: {Topic}", _topic);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send message to Kafka");
                throw;
            }
        }
    }
}