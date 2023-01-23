using Confluent.Kafka;
using Domain.DTO;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace n5_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly ILogger<PermissionController> _logger;
        private readonly IUsersPermissionsService _service;
        private readonly string bootstrapServers = "localhost:9092";
        private readonly string topic = "permissions";
        public PermissionController(ILogger<PermissionController> logger, IUsersPermissionsService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                SendPermissionsMessage(topic, new MessageKafkaDTO("get"));
                return Ok(await _service.GetPermissions());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
           
        }

        [HttpPut]
        public async Task<IActionResult> RequestPermission(PermissionDTOAssign model)
        {
            try
            {
                SendPermissionsMessage(topic, new MessageKafkaDTO("request"));
                var result = await _service.RequestPermission(model);

                return result != false?  Ok() : StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ModifyPermission(PermissionDTOModify model)
        {
            try
            {
                SendPermissionsMessage(topic, new MessageKafkaDTO("modify"));
                var result = await _service.ModifyPermission(model);

                return result != false ? Ok() : StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        private async Task<bool> SendPermissionsMessage
       (string topic, MessageKafkaDTO messageInput)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build()){

                    messageInput.Id = Guid.NewGuid();
                    string message = JsonSerializer.Serialize(messageInput);
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value =  message
                    });

                    Debug.WriteLine($"Delivery Timestamp: { result.Timestamp.UtcDateTime}");

                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
