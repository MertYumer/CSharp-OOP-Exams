namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        private Phone phone;

        [SetUp]
        public void SetUp()
        {
            this.phone = new Phone("Samsung", "Galaxy S10");
        }

        [Test]
        public void Constructor_ShouldInitializeCorrectly()
        {
            Assert.IsNotNull(this.phone);
        }

        [Test]
        public void Constructor_ShouldSetValuesCorrectly()
        {
            var expectedMake = "Samsung";
            var actualMake = this.phone.Make;

            var expectedModel = "Galaxy S10";
            var actualModel = this.phone.Model;

            Assert.AreEqual(expectedMake, actualMake);
            Assert.AreEqual(expectedModel, actualModel);
        }

        [TestCase("", "Galaxy S10")]
        [TestCase("Samsung", "")]
        public void Constructor_InvalidParameters_ShouldThrowArgumentException(string make, string model)
        {
            Assert.Throws<ArgumentException>(() => this.phone = new Phone(make, model));
        }

        [TestCase("John", "0897654321")]
        public void AddContactMethod_ValidParameters_ShouldAddCorrectly(string name, string phone)
        {
            this.phone.AddContact(name, phone);

            var expected = 1;
            var actual = this.phone.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestCase("John", "0897654321")]
        public void AddContactMethod_InvalidParameters_ShouldThrowInvalidOperationException(string name, string phone)
        {
            this.phone.AddContact(name, phone);

            Assert.Throws<InvalidOperationException>(() => this.phone.AddContact(name, phone));
        }

        [TestCase("John", "0897654321")]
        public void CallMethod_ValidParameters_ShouldReturnTheCorrectString(string name, string number)
        {
            this.phone.AddContact(name, number);

            var expected = $"Calling {name} - {number}...";
            var actual = this.phone.Call(name);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("John")]
        public void CallMethod_InvalidParameters_ShouldThrowInvalidOperationException(string name)
        {
            Assert.Throws<InvalidOperationException>(() => this.phone.Call(name));
        }
    }
}