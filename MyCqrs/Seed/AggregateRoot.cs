using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>();

        public abstract Guid Id { get; }
        public int Version { get; internal set; }

        public IEnumerable<Event>GetUnCommittedChanged()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        public void LoadFromHistory(IEnumerable<Event> history)
        {
            foreach(var e in history)
            {
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(Event @event)
        {
            ApplyChange(@event, false);
        }

        protected void ApplyChange(Event @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);
            
            if (isNew)
            {
                _changes.Add(@event);
            }
        }
    }
}
