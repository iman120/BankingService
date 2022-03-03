using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace MoneyLaunderingService
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("MONEY LAUNDERING SERVICE");

            string _consumeTopicName = "paymentsTopic";
            string _produceTopicName = "LaunderyCheckTopic";

            var _consumerConfig = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            var _producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            int amount = 0;
            using (var consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build())
            {
                consumer.Subscribe(_consumeTopicName);
                while (true)
                {
                    var cr = consumer.Consume();
                    //TODO: Check input if number etc? -> eig nicht notwendig da normalerweise bei echtem Service eh nur richtige Eingaben kommen
                    amount = Int32.Parse(cr.Message.Value);
                    Console.OutputEncoding = System.Text.Encoding.Default;
                    Console.WriteLine("Payment recieved: " + amount + "€.");

                    //PRODUCE 
                    string launderingMessage;
                    if(amount <= 1000)
                    {
                        launderingMessage = "Payment accepeted.";
                    }
                    else
                    {
                        launderingMessage = "Payment declined.";
                    }

                    using (var producer = new ProducerBuilder<Null, string>(_producerConfig).Build())
                    {
                        await producer.ProduceAsync(_produceTopicName, new Message<Null, string> { Value = launderingMessage });
                        producer.Flush(TimeSpan.FromSeconds(10));
                    }
                }
            }
        }
    }
}
