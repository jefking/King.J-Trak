namespace King.JTrak.Unit.Test
{
    using King.Azure.Data;
    using King.JTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using NSubstitute;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SynchronizerTests
    {
        [Test]
        public void Constructor()
        {
            var c = new ConfigValues
            {
                FromConnectionString = "UseDevelopmentStorage=true;",
                FromTable = "from",
                ToConnectionString = "UseDevelopmentStorage=true;",
                ToContainer = "to",
            };
            new Synchronizer(c);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorConfigNull()
        {
            new Synchronizer(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorFromNull()
        {
            var to = Substitute.For<IContainer>();
            new Synchronizer(null, to);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorToNull()
        {
            var from = Substitute.For<ITableStorage>();
            new Synchronizer(from, null);
        }

        [Test]
        public async Task Run()
        {
            var random = new Random();
            var count = random.Next(1, 25);
            var entities = new List<IDictionary<string, object>>();
            var to = Substitute.For<IContainer>();
            for (var i = 0; i < count; i++)
            {
                var dic = new Dictionary<string, object>();
                dic.Add(TableStorage.PartitionKey, Guid.NewGuid().ToString());
                dic.Add(TableStorage.RowKey, Guid.NewGuid().ToString());
                entities.Add(dic);
                var name = string.Format("{0}-{1}.json", dic[TableStorage.PartitionKey], dic[TableStorage.RowKey]);
                to.Save(name, dic);
            }
            var from = Substitute.For<ITableStorage>();
            from.Query(Arg.Any<TableQuery>()).Returns(Task.FromResult((IEnumerable<IDictionary<string, object>>)entities));
            to.CreateIfNotExists();

            var s = new Synchronizer(from, to);
            await s.Run();

            from.Received().Query(Arg.Any<TableQuery>());
            to.Received().CreateIfNotExists();
            foreach (var dic in entities)
            {
                var name = string.Format("{0}-{1}.json", dic[TableStorage.PartitionKey], dic[TableStorage.RowKey]);
                to.Received().Save(name, dic);
            }
        }

        [Test]
        public async Task RunNoEntities()
        {
            var from = Substitute.For<ITableStorage>();
            from.Query(Arg.Any<TableQuery>()).Returns(Task.FromResult((IEnumerable<IDictionary<string, object>>)null));
            var to = Substitute.For<IContainer>();
            to.CreateIfNotExists();
            to.Save(Arg.Any<string>(), Arg.Any<IDictionary<string, object>>());

            var s = new Synchronizer(from, to);
            await s.Run();

            from.Received().Query(Arg.Any<TableQuery>());
            to.Received().CreateIfNotExists();
            to.Received(0).Save(Arg.Any<string>(), Arg.Any<IDictionary<string, object>>());
        }
    }
}