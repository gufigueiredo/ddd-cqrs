using System.Collections.Generic;
using LiteDB;
using Localiza.LocalRental.Infrastructure.DataAccess;
using Localiza.LocalRental.Infrastructure.DataModel.Collections;
using Newtonsoft.Json;
using SC.SDK.NetStandard.DomainCore;
using SC.SDK.NetStandard.DomainCore.Events;

namespace Localiza.LocalRental.Infrastructure.Events
{
    public class EventStream : IEventStream
    {
        private readonly ILiteDbContext _context;

        public EventStream(ILiteDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EventStreamDataModel> GetStream()
        {
            using (var db = new LiteRepository(_context.Context))
            {
                var dbModel = db.Query<EventStreamDataModel>().ToList();
                return dbModel;
            }
        }

        public void AddToStream<T>(T entity) where T : Entity, IAggregateRoot
        {
            var events = new List<EventStreamDataModel>();
            if (entity.DomainEvents != null)
            {
                foreach (var item in entity.DomainEvents)
                {
                    var eventObj = (Event)item;
                    var eventData = JsonConvert.SerializeObject(eventObj);

                    events.Add(new EventStreamDataModel
                    {
                        TimeStamp = eventObj.TimeStamp,
                        EventType = eventObj.GetType().Name,
                        Data = eventData,
                        Id = eventObj.Id.ToString()
                    });
                }
                using (var db = new LiteRepository(_context.Context))
                {
                    events.ForEach(e => db.Insert(e));
                }
            }
        }
    }
}
