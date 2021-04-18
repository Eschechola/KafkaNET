using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace KafkaNET.Publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string bootstrapServers = "localhost:9092";
            string topicName = "log-events";

            while (true)
            {
                Console.Write("Digite a mensagem: \n> ");
                string message = Console.ReadLine();

                try
                {
                    var config = new ProducerConfig
                    {
                        BootstrapServers = bootstrapServers
                    };

                    using (var producer = new ProducerBuilder<Null, string>(config).Build())
                    {
                        var result = await producer.ProduceAsync(
                            topicName,
                            new Message<Null, string>
                            {
                                Value = message
                            }
                        );

                        Console.WriteLine("Mensagem: {0}\nStatus: {1}", message, result.Status.ToString());
                    }

                    Console.WriteLine("Concluído o envio de mensagens");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
