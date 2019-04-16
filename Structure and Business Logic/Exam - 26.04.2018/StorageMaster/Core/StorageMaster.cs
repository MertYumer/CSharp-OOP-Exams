namespace StorageMaster.Core
{
    using Factories;
    using Models.Products;
    using Models.Storages;
    using Models.Vehicles;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StorageMaster
    {
        private ProductFactory productFactory;
        private StorageFactory storageFactory;
        private List<Storage> storageRegistry;
        private List<Product> productsPool;
        private Vehicle currentVehicle;

        public StorageMaster()
        {
            this.productFactory = new ProductFactory();
            this.storageFactory = new StorageFactory();
            this.storageRegistry = new List<Storage>();
            this.productsPool = new List<Product>();
        }

        public string AddProduct(string type, double price)
        {
            var product = productFactory.CreateProduct(type, price);

            this.productsPool.Add(product);
            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            var storage = storageFactory.CreateStorage(type, name);

            this.storageRegistry.Add(storage);
            return $"Registered {name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            this.currentVehicle = this.storageRegistry
                .FirstOrDefault(s => s.Name == storageName)
                .GetVehicle(garageSlot);

            return $"Selected {this.currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            foreach (var productName in productNames)
            {
                var product = this.productsPool
                    .LastOrDefault(p => p.GetType().Name == productName);

                if (product == null)
                {
                    throw new InvalidOperationException($"{productName} is out of stock!");
                }

                this.productsPool.Remove(product);

                if (this.currentVehicle.IsFull)
                {
                    break;
                }

                this.currentVehicle.LoadProduct(product);
            }

            return $"Loaded {this.currentVehicle.Trunk.Count}/{productNames.Count()}" +
                $" products into {this.currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot,
            string destinationName)
        {
            var sourceStorage = this.storageRegistry
                .FirstOrDefault(s => s.Name == sourceName);

            var destinationStorage = this.storageRegistry
                .FirstOrDefault(s => s.Name == destinationName);

            if (sourceStorage == null)
            {
                throw new InvalidOperationException("Invalid source storage!");
            }

            if (destinationStorage == null)
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            var vehicle = sourceStorage.GetVehicle(sourceGarageSlot);
            int destinationGarageSlot = sourceStorage
                .SendVehicleTo(sourceGarageSlot, destinationStorage);

            return $"Sent {vehicle.GetType().Name} to {destinationName}" +
                $" (slot {destinationGarageSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var storage = this.storageRegistry
                .FirstOrDefault(s => s.Name == storageName);

            var vehicle = storage.GetVehicle(garageSlot);
            var unloadedProductsCount = storage.UnloadVehicle(garageSlot);

            return $"Unloaded {unloadedProductsCount}/{vehicle.Trunk.Count} " +
                $"products at {storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            var storage = this.storageRegistry.First(s => s.Name == storageName);
            double productsWeight = storage.Products.Sum(p => p.Weight);

            var products = storage.Products
                .GroupBy(p => p.GetType().Name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(p => p.Count)
                .ThenBy(p => p.Name)
                .ToList();

            var listedProducts = new List<string>();

            foreach (var product in products)
            {
                listedProducts.Add($"{product.Name} ({product.Count})");
            }

            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Stock ({productsWeight}/{storage.Capacity}):" +
                $" [{string.Join(", ", listedProducts)}]");

            var listedVehicles = new List<string>();

            foreach (var vehicle in storage.Garage)
            {
                string vehicleName = vehicle == null
                    ? "empty"
                    : vehicle.GetType().Name;

                listedVehicles.Add(vehicleName);
            }

            stringBuilder.AppendLine($"Garage: [{string.Join("|", listedVehicles)}]");

            return stringBuilder.ToString().TrimEnd();
        }

        public string GetSummary()
        {
            var orderedStorages = this.storageRegistry
                .OrderByDescending(s => s.Products.Sum(p => p.Price));

            var stringBuilder = new StringBuilder();

            foreach (var storage in orderedStorages)
            {
                double totalMoney = storage.Products.Sum(p => p.Price);
                stringBuilder.AppendLine($"{storage.Name}:");
                stringBuilder.AppendLine($"Storage worth: ${totalMoney:f2}");
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
