﻿using Newtonsoft.Json;
using OniHealth.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OniHealth.Worker2.Utils
{
    public static class WorkerSharedFunctions
    {
        private static readonly HttpClient _httpClient;
        private static string _defaultApiUrl;

        static WorkerSharedFunctions()
        {
            _defaultApiUrl = "http://localhost:80/api";
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region RabbitMQ
        private static void Enqueue<T>(T obj, string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
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

        private static void Enqueue<T>(IEnumerable<T> obj, string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
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

        public static Task EnqueueAsync<T>(IEnumerable<T> obj, string queueName)
        {
            return Task.Run(() => Enqueue(obj, queueName));
        }

        private static async Task<T> Dequeue<T>(string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
                DispatchConsumersAsync = true

            };
            T obj = default;
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

        public static async Task<T> DequeueAsync<T>(string queueName)
        {
            var dequeuedObject = await Dequeue<T>(queueName);
            return dequeuedObject;
        }

        private static async Task<IEnumerable<T>> DequeueList<T>(string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
                DispatchConsumersAsync = true

            };
            IEnumerable<T> obj = default;
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
                var tcs = new TaskCompletionSource<bool>();
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    obj.Append(JsonConvert.DeserializeObject<T>(message));
                    channel.BasicAck(ea.DeliveryTag, false);

                    if (IsNotNullOrEmpty(obj))
                        tcs.SetResult(true);
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
                await tcs.Task;
                return obj;
            }
        }

        public static async Task<IEnumerable<T>> DequeueListAsync<T>(string queueName)
        {
            var dequeuedObject = await Dequeue<IEnumerable<T>>(queueName);
            return dequeuedObject;
        }

        private static async Task<T> DequeueAndProcess<T>(string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
                DispatchConsumersAsync = true

            };
            T obj = default;
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
                await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(2)));
                return obj;
            }
        }

        public static async Task<T> DequeueAndProcessAsync<T>(string queueName)
        {
            var dequeuedObject = await DequeueAndProcess<T>(queueName);
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
                return default;
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
            return default;
        }

        public static bool IsDateBetween(DateTime input, DateTime start, DateTime end)
        {
            return input >= start && input <= end;
        }

        public static T ConvertObject<T>(object obj)
        {
            if (obj == null) return default(T);

            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T IsNullOrZero<T>(this T variable, T defaultValue)
        {
            if (defaultValue == null) throw new ArgumentException("default value can't be null", "defaultValue");
            if (variable == null || variable.Equals(default(T)))
                return defaultValue;
            return variable;
        }

        #endregion

        #region HTTP/HTTPS

        public static async Task<object> GetAsync(string apiEndpoint, string parameters = "", string token = "", string apiBaseUrl = "")
        {
            apiBaseUrl = string.IsNullOrEmpty(apiBaseUrl) ? _defaultApiUrl : apiBaseUrl;

            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiBaseUrl}/{apiEndpoint}/{parameters}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(content);
            }

            return null;
        }

        public static async Task<object> PostAsync(string apiEndpoint, object resource, string apiBaseUrl = "")
        {
            apiBaseUrl = string.IsNullOrEmpty(apiBaseUrl) ? _defaultApiUrl : apiBaseUrl;
            var serializedResource = JsonConvert.SerializeObject(resource);
            var content = new StringContent(serializedResource, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{apiBaseUrl}/{apiEndpoint}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(responseContent);
            }

            return null;
        }

        public static async Task<bool> PutAsync(string apiEndpoint, string parameters, object updatedResource, string apiBaseUrl = "")
        {
            apiBaseUrl = string.IsNullOrEmpty(apiBaseUrl) ? _defaultApiUrl : apiBaseUrl;
            var serializedResource = JsonConvert.SerializeObject(updatedResource);
            var content = new StringContent(serializedResource, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{apiBaseUrl}/{apiEndpoint}/{parameters}", content);

            return response.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteAsync(string apiEndpoint, string parameters, string apiBaseUrl = "")
        {
            apiBaseUrl = string.IsNullOrEmpty(apiBaseUrl) ? _defaultApiUrl : apiBaseUrl;
            var response = await _httpClient.DeleteAsync($"{apiBaseUrl}/{apiEndpoint}/{parameters}");

            return response.IsSuccessStatusCode;
        }
        #endregion
    }
}