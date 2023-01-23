using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Domain.Services
{
    public class KafkaProducerService : IHostedService, IDisposable
    {
        private IProducer<string, string> _producer;
        private readonly ILogger<KafkaProducerService> _logger;
        private readonly string topic = "test";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";


        public KafkaProducerService(ILogger<KafkaProducerService> logger)
        {
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            Init();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Kafka Producer Service has started.");
                    await Produce(cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Kafka Producer Service is stopping.");

            _producer.Flush(cancellationToken);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        private void Init()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = bootstrapServers,
                ClientId = "Kafka.Dotnet.Sample",
                SecurityProtocol = SecurityProtocol.Plaintext,
                EnableDeliveryReports = false,
                QueueBufferingMaxMessages = 10000000,
                QueueBufferingMaxKbytes = 100000000,
                BatchNumMessages = 500,
                Acks = Acks.None,
                DeliveryReportFields = "none"
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        private async Task Produce(CancellationToken cancellationToken)
        {
            try
            {
                using (_logger.BeginScope("Kafka App Produce Sample Data"))
                {
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        for (int i = 0; i < 1001; i++)
                        {
                            var msg = new Message<string, string>
                            {
                                Key = "permissions",
                                Value = "exampleValue"
                            };
                            await _producer.ProduceAsync("permissions", msg).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
            }
        }
    }
}
