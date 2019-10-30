using System;
namespace Localiza.LocalRental.Infrastructure.DataModel.Collections
{
    public class EventStreamDataModel
    {
        public string Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EventType { get; set; }
        public string Data { get; set; }
    }
}
