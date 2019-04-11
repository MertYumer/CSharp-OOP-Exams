namespace CosmosX.Tests
{
    using CosmosX.Entities.Containers;
    using CosmosX.Entities.Containers.Contracts;
    using CosmosX.Entities.Modules.Absorbing;
    using CosmosX.Entities.Modules.Absorbing.Contracts;
    using CosmosX.Entities.Modules.Energy;
    using CosmosX.Entities.Modules.Energy.Contracts;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ModuleContainerTests
    {
        private IContainer moduleContainer;

        [SetUp]
        public void SetUp()
        {
            this.moduleContainer = new ModuleContainer(3);
        }

        [Test]
        public void Constructor_InitializeCorrectly()
        {
            var expected = 0;
            var actual = this.moduleContainer.ModulesByInput.Count;

            Assert.IsNotNull(this.moduleContainer);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddEnergyModule_ValidModule_ShouldAddCorrectly()
        {
            IEnergyModule module = new CryogenRod(2, 3);
            this.moduleContainer.AddEnergyModule(module);

            var expected = 1;
            var actual = this.moduleContainer.ModulesByInput.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddEnergyModule_InvalidModule_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => 
            this.moduleContainer.AddEnergyModule(null));
        }

        [Test]
        public void AddEnergyModule_RemovesOldestModule()
        {
            IEnergyModule firstModule = new CryogenRod(1, 2);
            IEnergyModule secondModule = new CryogenRod(2, 3);
            IEnergyModule thirdModule = new CryogenRod(3, 4);
            IEnergyModule fourthModule = new CryogenRod(4, 5);

            this.moduleContainer.AddEnergyModule(firstModule);
            this.moduleContainer.AddEnergyModule(secondModule);
            this.moduleContainer.AddEnergyModule(thirdModule);
            this.moduleContainer.AddEnergyModule(fourthModule);

            Assert.AreEqual(3, this.moduleContainer.ModulesByInput.Count);
        }

        [Test]
        public void AddAbsorbingModule_ValidModule_ShouldAddCorrectly()
        {
            IAbsorbingModule module = new HeatProcessor(1, 2);
            this.moduleContainer.AddAbsorbingModule(module);

            var expected = 1;
            var actual = this.moduleContainer.ModulesByInput.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AddAbsorbingModule_InvalidModule_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            this.moduleContainer.AddAbsorbingModule(null));
        }

        [Test]
        public void AddAbsorbingModule_RemovesOldestModule()
        {
            IAbsorbingModule firstModule = new HeatProcessor(1, 2);
            IAbsorbingModule secondModule = new HeatProcessor(2, 3);
            IAbsorbingModule thirdModule = new HeatProcessor(3, 4);
            IAbsorbingModule fourthModule = new HeatProcessor(4, 5);

            this.moduleContainer.AddAbsorbingModule(firstModule);
            this.moduleContainer.AddAbsorbingModule(secondModule);
            this.moduleContainer.AddAbsorbingModule(thirdModule);
            this.moduleContainer.AddAbsorbingModule(fourthModule);

            Assert.AreEqual(3, this.moduleContainer.ModulesByInput.Count);
        }

        [Test]
        public void TotalEnergyOutput_ShouldReturnCorrectValue()
        {
            IEnergyModule firstModule = new CryogenRod(1, 2);
            IEnergyModule secondModule = new CryogenRod(2, 3);
            this.moduleContainer.AddEnergyModule(firstModule);
            this.moduleContainer.AddEnergyModule(secondModule);

            var expected = 5;
            var actual = this.moduleContainer.TotalEnergyOutput;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TotalHeatAbsorbing_ShouldReturnCorrectValue()
        {
            IAbsorbingModule firstModule = new HeatProcessor(1, 2);
            IAbsorbingModule secondModule = new HeatProcessor(2, 3);
            this.moduleContainer.AddAbsorbingModule(firstModule);
            this.moduleContainer.AddAbsorbingModule(secondModule);

            var expected = 5;
            var actual = this.moduleContainer.TotalHeatAbsorbing;

            Assert.AreEqual(expected, actual);
        }
    }
}