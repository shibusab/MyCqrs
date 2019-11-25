using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public class Event :IMessage
    {
        public int Version;
    }
}
