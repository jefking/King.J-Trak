namespace King.JTrak.Unit.Test.Models
{
    using King.JTrak.Models;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ConfigValuesTests
    {
        [Test]
        public void Constructor()
        {
            new ConfigValues();
        }

        [Test]
        public void IsIConfigValues()
        {
            Assert.IsNotNull(new ConfigValues() as IConfigValues);
        }

        [Test]
        public void FromTable()
        {
            var expected = Guid.NewGuid().ToString();
            var c = new ConfigValues
            {
                FromTable = expected,
            };

            Assert.AreEqual(expected, c.FromTable);
        }

        [Test]
        public void ToContainer()
        {
            var expected = Guid.NewGuid().ToString();
            var c = new ConfigValues
            {
                ToContainer = expected,
            };

            Assert.AreEqual(expected, c.ToContainer);
        }

        [Test]
        public void FromConnectionString()
        {
            var expected = Guid.NewGuid().ToString();
            var c = new ConfigValues
            {
                FromConnectionString = expected,
            };

            Assert.AreEqual(expected, c.FromConnectionString);
        }

        [Test]
        public void ToConnectionString()
        {
            var expected = Guid.NewGuid().ToString();
            var c = new ConfigValues
            {
                ToConnectionString = expected,
            };

            Assert.AreEqual(expected, c.ToConnectionString);
        }
    }
}