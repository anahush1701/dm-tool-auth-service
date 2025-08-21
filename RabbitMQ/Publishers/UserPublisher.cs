using AuthService.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace AuthService.RabbitMQ.Publishers
{
    public class UserPublisher
    {
        private const string QueueName = "user_created_queue";
        public async Task PublishNewUserAsync(Guid userId, string name)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: QueueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

            var userDto = new UserCreatedDto
            {
                UserId = userId,
                UserName = name
            };

            string message = JsonSerializer.Serialize(userDto);
            var body = System.Text.Encoding.UTF8.GetBytes(message);

            await channel.BasicPublishAsync(exchange: string.Empty,
                                             routingKey: QueueName,
                                             mandatory: false,
                                             basicProperties: new BasicProperties(),
                                             body: new ReadOnlyMemory<byte>(body));

            Console.WriteLine($" [x] Sent user ID: {userDto.UserId} - {userDto.UserName}");
        }
    }
}
