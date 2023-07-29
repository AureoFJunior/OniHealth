﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OniHealth.Domain.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace OniHealth.Domain.Utils
{
    public static class SharedFunctions
    {
        private static readonly HttpClient _httpClient;
        private static readonly IConfiguration _configuration;
        private static string _defaultApiUrl;

        static SharedFunctions()
        {
            _configuration = new ConfigurationBuilder().Build();
            _httpClient = new HttpClient();
            _defaultApiUrl = _configuration.GetSection("HttpConfig")["DefaultApiUrl"];
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region RabbitMQ
        private static void Enqueue<T>(T obj, string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration(_configuration);
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
                DispatchConsumersAsync = true

            };
            var connection = factory.CreateConnection();

            var channel = GetChannel(queueName);

            string message = JsonConvert.SerializeObject(obj);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            channel.Close();
            connection.Close();

        }

        private static void Enqueue<T>(IEnumerable<T> obj, string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration(_configuration);
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
            var channel = GetChannel(queueName);
            T obj = default(T);
            var consumer = new AsyncEventingBasicConsumer(channel);
            var tcs = new TaskCompletionSource<T>();

            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    obj = JsonConvert.DeserializeObject<T>(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                    tcs.SetResult(obj);
                }
                catch (Exception ex)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
            return await tcs.Task;
        }

        private static IModel? GetChannel(string queueName)
        {
            RabbitMQConfiguration rabbitMQConfiguration = new RabbitMQConfiguration(_configuration);
            var factory = new ConnectionFactory()
            {
                HostName = rabbitMQConfiguration.QueueHostName,
                DispatchConsumersAsync = true

            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.ExchangeDeclare();
            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
            arguments: null);
            channel.ExchangeBind();
            channel.ConfirmSelect();

            return channel;
        }

        public static async Task<T> DequeueAsync<T>(string queueName)
        {
            var dequeuedObject = await SharedFunctions.Dequeue<T>(queueName);
            return dequeuedObject;
        }

        private static async Task<IEnumerable<T>> DequeueList<T>(string queueName)
        {

            IEnumerable<T> obj = default(IEnumerable<T>);
            var channel = GetChannel(queueName);

            var consumer = new AsyncEventingBasicConsumer(channel);
            var tcs = new TaskCompletionSource<bool>();
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    obj.Append(JsonConvert.DeserializeObject<T>(message));
                    channel.BasicAck(ea.DeliveryTag, false);

                    if (SharedFunctions.IsNotNullOrEmpty(obj))
                        tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };

            channel.BasicConsume(queueName, true, consumer);
            await tcs.Task;
            return obj;

        }

        public static async Task<IEnumerable<T>> DequeueListAsync<T>(string queueName)
        {
            var dequeuedObject = await SharedFunctions.Dequeue<IEnumerable<T>>(queueName);
            return dequeuedObject;
        }

        private static async Task<T> DequeueAndProcess<T>(string queueName)
        {

            T obj = default(T);
            var channel = GetChannel(queueName);

            var consumer = new AsyncEventingBasicConsumer(channel);
            var tcs = new TaskCompletionSource<T>();
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    obj = JsonConvert.DeserializeObject<T>(message);
                    channel.BasicAck(ea.DeliveryTag, false);
                    tcs.SetResult(obj);
                }
                catch (Exception ex)
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }

            };

            channel.BasicConsume(queueName, true, consumer);

            await Task.WhenAny(tcs.Task, Task.Delay(TimeSpan.FromSeconds(2)));
            return obj;

        }

        public static async Task<T> DequeueAndProcessAsync<T>(string queueName)
        {
            var dequeuedObject = await SharedFunctions.DequeueAndProcess<T>(queueName);
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

        public static Dictionary<string, string> GetFieldValues<T>(T obj)
        {
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                if (value != null)
                {
                    string fieldName = property.Name;
                    string fieldValue = value.ToString();
                    fieldValues.Add(fieldName, fieldValue);
                }
            }

            return fieldValues;
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
