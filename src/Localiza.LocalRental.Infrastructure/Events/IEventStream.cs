using System.Collections.Generic;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;
using SC.SDK.NetStandard.DomainCore;

namespace Localiza.LocalRental.Infrastructure.Events
{
    public interface IEventStream
    {
        void AddToStream<T>(T entity) where T : Entity, IAggregateRoot;
        IEnumerable<EventStreamDataModel> GetStream();
    }
}
