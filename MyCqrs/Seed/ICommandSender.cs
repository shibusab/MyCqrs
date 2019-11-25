using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Seed
{
    public interface ICommandSender
    {
        void Send<T>(T command) where T : Command;
    }
}
