using PracticalMicroservices.Events.Entities;
using System;

namespace PracticalMicroservices.Events.Services
{
    public interface IMessageStore
    {
        long WriteMessage<T>(T entity, Guid id, long expectedVersion = -1);
    }
}
