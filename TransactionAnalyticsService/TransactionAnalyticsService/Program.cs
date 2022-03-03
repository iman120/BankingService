using Confluent.Kafka;
using System;

namespace TransactionAnalyticsService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TRANSACTION ANALYTICS SERVICE");

            string _topicName = "LaunderyCheckTopic";
            var config = new ConsumerConfig
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };

            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe(_topicName);
                int acceptedPaymentsCounter = 0;
                int declinedPaymentsCounter = 0;
                while (true)
                {
                    var cr = consumer.Consume();
                    if(cr.Message.Value.ToString().Equals("Payment accepeted."))
                    {
                        acceptedPaymentsCounter++;
                    }else if(cr.Message.Value.ToString().Equals("Payment declined."))
                    {
                        declinedPaymentsCounter++;
                    }

                    string newAnalyticsString = "--- Analytics result at " +
                                                DateTime.Now.ToString("h:mm:ss tt") + "---" + "\n" +
                                                "Accepted payments: " + acceptedPaymentsCounter + "\n" +
                                                "Declined payments: " + declinedPaymentsCounter + "\n";
                    Console.WriteLine(newAnalyticsString);
                }
            }
        }
    }
}
