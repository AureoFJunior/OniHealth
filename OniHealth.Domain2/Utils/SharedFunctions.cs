using Newtonsoft.Json;
using OniHealth.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Domain.Utils
{
    public static class SharedFunctions
    {
        #region RabbitMQ
        private static void Enqueue<T>(T obj, string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.HostName,
                DispatchConsumersAsync = true

            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(obj);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);

                channel.Close();
                connection.Close();
            }
        }

        public static Task EnqueueAsync<T>(T obj, string queueName)
        {
            return Task.Run(() => Enqueue(obj, queueName));
        }

        private static async Task<T> DequeueAsync<T>(string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.HostName,
                DispatchConsumersAsync = true

            };
            T obj;
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                var consumer = new AsyncEventingBasicConsumer(channel);
                var tcs = new TaskCompletionSource<T>();
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    obj = JsonConvert.DeserializeObject<T>(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                    tcs.SetResult(obj);
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
                return await tcs.Task;
            }
        }

        public static async Task<T> DequeueAndProcessAsync<T>(string queueName)
        {
            var dequeuedObject = await SharedFunctions.DequeueAsync<T>(queueName);
            return dequeuedObject;
        }
        #endregion

        #region Utils
        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        public static T SafeConvertToNumber<T>(string input) where T : struct
        {
            if (string.IsNullOrEmpty(input))
                return default(T);
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(input, out int result))
                    return (T)(object)result;
            }
            else if (typeof(T) == typeof(short))
            {
                if (short.TryParse(input, out short result))
                    return (T)(object)result;
            }
            else if (typeof(T) == typeof(long))
            {
                if (long.TryParse(input, out long result))
                    return (T)(object)result;
            }
            return default(T);
        }

        public static bool IsDateBetween(DateTime input, DateTime start, DateTime end)
        {
            return input >= start && input <= end;
        }

        #endregion
        //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlRlc3RlIiwibmJmIjoxNjc1MjYwMDU0LCJleHAiOjE2NzUyNjcyNTQsImlhdCI6MTY3NTI2MDA1NH0.ZWgw62RcKVeWi8S3BUi5I6SYFrebQY2VZsNeLApA-kw

        //        {
        //  "id": 0,
        //  "title": "teste",
        //  "consulttimeid": null,
        //  "consulttypeid": null,
        //  "customerid": null,
        //  "doctorid": null,
        //  "examid": null,
        //  "customerispresent": null,
        //  "doctorispresent": null,
        //  "isactive": true
        //}
}
}
