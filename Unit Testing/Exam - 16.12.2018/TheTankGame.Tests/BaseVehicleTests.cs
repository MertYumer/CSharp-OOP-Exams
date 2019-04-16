namespace TheTankGame.Tests
{
    using System;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;

    using Entities.Miscellaneous;
    using Entities.Parts;
    using Entities.Vehicles;
    using Entities.Parts.Contracts;
    
    [TestFixture]
    public class VehicleAssemblerTests
    {
        [TestCase(null, 112, 12, 12, 12, 12)]
        [TestCase("Gosho", 0, 12, 12, 12, 12)]
        [TestCase("Gosho", 112, 0, 12, 12, 12)]
        [TestCase("Gosho", 112, 12, -1, 12, 12)]
        [TestCase("Gosho", 112, 12, 12, -1, 12)]
        [TestCase("Gosho", 112, 12, 12, 12, -1)]
        public void Vehicle_InvalidParameters_ShouldThrowArgumentException(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            Assert.Throws<ArgumentException>(() 
                => new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler()));
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void Vehicle_ValidParameter_ShouldInitializeCorrectly(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            Assert.IsNotNull(vehicle);
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void AddArsenalPart_ShouldAddPartCorrectly(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            IPart arsenalPart = new ArsenalPart(model, weight, price, 10);

            vehicle.AddArsenalPart(arsenalPart);

            var expected = 1;
            var actual = vehicle.Parts.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void AddShellPart_ShouldAddPartCorrectly(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            IPart shellPart = new ShellPart(model, weight, price, 10);

            vehicle.AddShellPart(shellPart);

            var expected = 1;
            var actual = vehicle.Parts.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void AddEndurancePart_ShouldAddPartCorrectly(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            IPart endurancePart = new EndurancePart(model, weight, price, 10);

            vehicle.AddEndurancePart(endurancePart);

            var expected = 1;
            var actual = vehicle.Parts.Count();

            Assert.AreEqual(expected, actual);
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void PartsProperty_ShouldReturnCorrectly(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            IPart arsenalPart = new ArsenalPart(model, weight, price, 10);

            vehicle.AddArsenalPart(arsenalPart);

            var expected = arsenalPart;
            var actual = vehicle.Parts.ToList()[0];

            Assert.AreSame(expected, actual);
        }

        [TestCase("Gosho", 112, 12, 12, 12, 12)]
        public void ToString_ShouldReturnTheCorrectString(string model,
            double weight, decimal price, int attack, int defense, int hitPoints)
        {
            var vehicle = new Revenger(model, weight, price, attack, defense,
                hitPoints, new VehicleAssembler());

            StringBuilder result = new StringBuilder();

            result.AppendLine($"{vehicle.GetType().Name} - {vehicle.Model}");
            result.AppendLine($"Total Weight: {vehicle.TotalWeight:F3}");
            result.AppendLine($"Total Price: {vehicle.TotalPrice:F3}");
            result.AppendLine($"Attack: {vehicle.TotalAttack}");
            result.AppendLine($"Defense: {vehicle.TotalDefense}");
            result.AppendLine($"HitPoints: {vehicle.TotalHitPoints}");
            result.Append("Parts: None");

            var expected = result.ToString();
            var actual = vehicle.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
