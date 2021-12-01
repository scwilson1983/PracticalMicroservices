using PracticalMicroservices.Events.Entities;
using PracticalMicroservices.Events.Infrastructure.Db;
using System;
using System.Text.Json;

namespace PracticalMicroservices.Events.Services
{
    public class MessageStore : IMessageStore
    {
        readonly MessageStoreContext _messageStoreContext;

        public MessageStore(MessageStoreContext messageStoreContext)
        {
            _messageStoreContext = messageStoreContext;
        }

        public long WriteMessage<T>(T entity, Guid id, long expectedVersion = -1)
        {
            var entityName = entity.GetType().Name;
            var streamName = $"{entityName}-{id}";
            var data = JsonSerializer.Serialize(entity);
           return _messageStoreContext.WriteMessage(Guid.NewGuid().ToString(), streamName, $"{entityName}Type", data, "{}", expectedVersion);
        }
    }
}
