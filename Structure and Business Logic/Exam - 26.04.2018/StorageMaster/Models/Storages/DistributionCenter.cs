namespace StorageMaster.Models.Storages
{
    using Vehicles;

    public class DistributionCenter : Storage
    {
        private const int DefaultCapacity = 2;
        private const int DefaultGarageSlots = 5;

        public DistributionCenter(string name)
            : base(name, DefaultCapacity, DefaultGarageSlots, 
                  new Vehicle[] { new Van(), new Van(), new Van() })
        {
        }
    }
}
