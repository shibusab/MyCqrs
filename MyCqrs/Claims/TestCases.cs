using MyCqrs.Claims.Commands;
using MyCqrs.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCqrs.Claims
{
   public class TestCases
    {
        public void TryClaimsInBus()
        {
            var bus = new FakeBus();
            var command = new ClaimFileCommand("C001", 10, DateTime.Now);
            bus.Send(command);
        }
    }
}
