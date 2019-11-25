using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public class Repository<T>: IRepository<T> where T: AggregateRoot, new()
    {
        private readonly IEventStore _storage;
        private readonly string _storagelocation;
        public Repository(IEventStore storage, string storagelocation)
        {
            _storage = storage;
            _storagelocation = storagelocation;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.SaveEvents(aggregate.Id, aggregate.GetUnCommittedChanged(), expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            var e = _storage.GetEventsForAggregate(id);
            obj.LoadFromHistory(e);
            return obj;
        }
    }
}
