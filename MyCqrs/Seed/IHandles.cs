using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public interface IHandles<T>
    {
        void Handle(T message);
    }
}
