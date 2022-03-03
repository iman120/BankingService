using Confluent.Kafka;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBankingService
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private string _topicName = "paymentsTopic";

        private ProducerConfig _config;
        public ProducerController(ProducerConfig config)
        {
            this._config = config;
        }
        [HttpPost("send")]
        public async Task<ActionResult> Get(int amount)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                await producer.ProduceAsync(_topicName, new Message<Null, string> { Value = amount.ToString() });
                producer.Flush(TimeSpan.FromSeconds(10));
                return Ok(true);
            }
        }
    }
}
