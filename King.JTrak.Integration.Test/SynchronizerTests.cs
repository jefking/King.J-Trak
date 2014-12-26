namespace King.JTrak.Integration.Test
{
    using King.Azure.Data;
    using King.JTrak.Models;
    using Microsoft.WindowsAzure.Storage.Table;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class SynchronizerTests
    {
        [Test]
        public async Task Synchronize()
        {
            var random = new Random();
            var count = random.Next(1, 25);
            var c = new ConfigValues
            {
                FromConnectionString = "UseDevelopmentStorage=true;",
                FromTable = "t" + random.Next(),
                ToConnectionString = "UseDevelopmentStorage=true;",
                ToContainer = "c" + random.Next(),
            };

            //generate
            var from = new TableStorage(c.FromTable, c.FromConnectionString);
            await from.CreateIfNotExists();
            var tEntities = new List<TableEntity>(count);
            for (var i = 0; i < count; i++)
            {
                var e = new TableEntity
                {
                    PartitionKey = Guid.NewGuid().ToString(),
                    RowKey = Guid.NewGuid().ToString(),
                };
                tEntities.Add(e);
            }

            await from.Insert(tEntities);

            //Synchronize
            var s = new Synchronizer(c);
            await s.Run();

            //validate
            var to = new Container(c.ToContainer, c.ToConnectionString);
            var entities = to.List();
            Assert.IsNotNull(entities);
            Assert.AreEqual(count, entities.Count());
            foreach (var e in entities)
            {
                var name = e.Uri.Segments.Last();
                var t = await to.GetText(name);
                Assert.IsNotNull(t);
                var entity = JsonConvert.DeserializeObject<TableEntity>(t);
                var returned = from ee in tEntities
                               where entity.PartitionKey == ee.PartitionKey
                                && entity.RowKey == ee.RowKey
                               select ee;
                Assert.IsNotNull(returned);
            }

            await from.Delete();
            await to.Delete();
        }
    }
}