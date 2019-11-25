﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCqrs.Seed
{
    public class EventStore :IEventStore
    {
        private readonly IEventPublisher _publisher;

        private struct EventDescriptor
        {
            public readonly Guid Id;
            public readonly int Version;
            public readonly Event EventData;
         
          
            public EventDescriptor(Guid id, Event eventData, int version)
            {
                Id = id;
                EventData = eventData;
                Version = version;
            }
        }

        public EventStore(IEventPublisher publisher)
        {
            _publisher = publisher;
        }
        
        private readonly Dictionary<Guid, List<EventDescriptor>> _current = new Dictionary<Guid, List<EventDescriptor>>();

        public void SaveEvents(Guid aggregateId, IEnumerable<Event>events, int expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;

            // try to get event descriptors list for given aggregate id
            // otherwise -> create empty dictionary
            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }
            // check whether latest event version matches current aggregate version
            // otherwise -> throw exception
            else if(eventDescriptors[eventDescriptors.Count -1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }
            
            var i = expectedVersion;

            // iterate through current aggregate events increasing version with each processed event
            foreach (var @event in events)
            {
                i++;
                @event.Version = i;

                // push event to the event descriptors list for current aggregate
                eventDescriptors.Add(new EventDescriptor(aggregateId, @event, i));
                
                TextEventStoreProvider.SaveEventData(aggregateId, @event, i);

                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        public List<Event> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            if(!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
       
    }
}
