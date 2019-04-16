namespace StorageMaster.Models.Storages
{
    using Contracts;
    using Products;
    using Vehicles;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Storage : IStorage
    {
        private Vehicle[] garage;
        private List<Product> products;

        public Storage(string name, int capacity, int garageSlots,
            IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;
            this.garage = new Vehicle[this.GarageSlots];
            this.products = new List<Product>();

            for (int i = 0; i < vehicles.ToArray().Length; i++)
            {
                this.garage[i] = vehicles.ToArray()[i];
            }
        }

        public string Name { get; private set; }

        public int Capacity { get; private set; }

        public int GarageSlots { get; private set; }

        public bool IsFull
            => this.Products.Sum(p => p.Weight) >= this.Capacity;

        public IReadOnlyCollection<Vehicle> Garage 
            => this.garage;

        public IReadOnlyCollection<Product> Products
            => this.products.AsReadOnly();

        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.GarageSlots)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }

            if (this.garage[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }

            var vehicle = this.garage[garageSlot];
            return vehicle;
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            var vehicle = this.GetVehicle(garageSlot);
            int destinationGarageSlot = -1;

            for (int i = 0; i < deliveryLocation.GarageSlots; i++)
            {
                if (deliveryLocation.garage[i] == null)
                {
                    destinationGarageSlot = i;
                    break;
                }
            }

            if (destinationGarageSlot == -1)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            this.garage[garageSlot] = null;
            deliveryLocation.garage[destinationGarageSlot] = vehicle;
            return destinationGarageSlot;
        }

        public int UnloadVehicle(int garageSlot)
        {
            if (this.IsFull)
            {
                throw new InvalidOperationException("Storage is full!");
            }

            var vehicle = this.GetVehicle(garageSlot);
            int unloadedProductsCount = 0;

            foreach (var product in vehicle.Trunk)
            {
                if (!this.IsFull)
                {
                    this.products.Add(product);
                    unloadedProductsCount++;
                }
            }

            return unloadedProductsCount;
        }
    }
}
