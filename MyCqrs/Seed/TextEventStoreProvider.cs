using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using static MyCqrs.Seed.EventStore;
using System.Linq;

namespace MyCqrs.Seed
{
    public static class TextEventStoreProvider
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        private static string path = @"c:\shibutemp\eventstore.txt";
        public static void SaveEventData(Guid aggregateId, Event @event, int expectedVersion)
        {
            var metadata = JsonConvert.SerializeObject(@event);
            var eventData = new EventData { AggregateId = aggregateId, AggregateType = "Claims", Created = DateTime.Now, Event = @event.GetType().FullName, Id = Guid.NewGuid(), Metadata = metadata, Version = expectedVersion };
            
            var fileData = JsonConvert.SerializeObject(eventData);

            _readWriteLock.EnterWriteLock();
            try
            {
                // Append text to the file
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(fileData);
                    sw.Close();
                }
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }
        }

        public static List<EventData> GetEventData(string aggregateId)
        {
            var eventDatas= new List<EventData>();
            try
            {

                var lines = File.ReadAllLines(path).Where(l => l.Contains(aggregateId));
                   
                foreach(var line in lines)
                {
                    eventDatas.Add(JsonConvert.DeserializeObject<EventData>(line));
                }
                
            }
            finally
            {
                // Release lock
                
            }
            return eventDatas;
        }
    }
}
