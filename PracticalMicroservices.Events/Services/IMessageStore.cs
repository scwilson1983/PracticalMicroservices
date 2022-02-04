using PracticalMicroservices.Events.Entities;
using System;
using System.Collections.Generic;

namespace PracticalMicroservices.Events.Services
{
    public interface IMessageStore
    {
        long WriteMessage<T>(T entity, Guid id, long expectedVersion = -1);
        IEnumerable<Message> ReadMessages(ReadMessageRequest request);
    }

    public class ReadMessageRequest
    {
        
    }
}
