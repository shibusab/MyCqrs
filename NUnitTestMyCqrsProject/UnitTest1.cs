using MyCqrs.Claims.Commands;
using MyCqrs.Claims.Domain;
using MyCqrs.Claims.Events;
using MyCqrs.Persistent;
using MyCqrs.Seed;
using NUnit.Framework;
using System;

namespace NUnitTestMyCqrsProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

      
        [Test]
        public void TestUnitOfWorkWithCommit()
        {
            var bus = new FakeBus();
            var events = new ClaimsEventHandler(bus);
            var storage = new EventStore(bus);

            var eventStoreRepository = new Repository<Claim>(storage, @"c:\shibutemp\eventstore.txt");
            var emailProvider = new TextEmailProvider(@"c:\shibutemp\emaillog.txt");

            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MyTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                connection.Open();
                var unitOfWork = new UnitOfWork(connection).WithTransaction();
                //var unitOfWork = new UnitOfWork(connection);

                var queryService = new MyCqrs.Persistent.ClaimsQueryService(unitOfWork);
                var commandService = new MyCqrs.Persistent.ClaimsCommandService(unitOfWork);

                var commands = new ClaimsCommandHandler(bus, queryService, commandService, eventStoreRepository, emailProvider);
                RegisterCommandHandles(bus, commands);
                RegisterEventHandles(bus, events);


                var claimNo = "C009";
                var command = new ClaimFileCommand(claimNo, 10, DateTime.Now);
                // bus.Send(command);

                var claimRejectedCommand = new ClaimRejectedCommand(claimNo, "r_user1", DateTime.Now);
                bus.Send(claimRejectedCommand);

                //var claimApprovedCommand = new ClaimApprovedCommand(claimNo, "a_user1", DateTime.Now);
                //bus.Send(claimApprovedCommand);

                unitOfWork.Commit();
                //unitOfWork.Rollback();

            }

       var eventDatas= TextEventStoreProvider.GetEventData("4eba9d9f-d64b-4d98-9fa6-2bced2695143");














            Assert.Pass();
        }

        private static void RegisterEventHandles(FakeBus bus, ClaimsEventHandler events)
        {
            bus.RegisterHandle<ClaimFiledEvent>(events.Handle);
            bus.RegisterHandle<ClaimRejectedEvent>(events.Handle);
            bus.RegisterHandle<ClaimApprovedEvent>(events.Handle);
        }

        private static void RegisterCommandHandles(FakeBus bus, ClaimsCommandHandler commands)
        {
            bus.RegisterHandle<ClaimFileCommand>(commands.Handle);
            bus.RegisterHandle<SendEmailCommand>(commands.Handle);
            bus.RegisterHandle<ClaimRejectedCommand>(commands.Handle);
            bus.RegisterHandle<ClaimApprovedCommand>(commands.Handle);
        }
    }

}