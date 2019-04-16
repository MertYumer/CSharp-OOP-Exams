namespace DungeonsAndCodeWizards.Factories
{
    using Models.Items;
    using System;

    public class ItemFactory
    {
        public Item CreateItem(string itemName)
        {
            switch (itemName)
            {
                case "ArmorRepairKit":
                    return new ArmorRepairKit();

                case "HealthPotion":
                    return new HealthPotion();

                case "PoisonPotion":
                    return new PoisonPotion();

                default:
                    throw new ArgumentException($"Invalid item \"{itemName}\"!");
            }
        }
    }
}
