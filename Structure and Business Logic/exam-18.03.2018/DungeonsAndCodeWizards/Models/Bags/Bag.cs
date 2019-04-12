namespace DungeonsAndCodeWizards.Models.Bags
{
    using Contracts;
    using Items;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Bag : IBag
    {
        private List<Item> items;

        public Bag(int capacity)
        {
            this.items = new List<Item>();
            this.Capacity = capacity;
        }

        public int Capacity { get; }

        public int Load => this.Items.Sum(i => i.Weight);

        public IReadOnlyCollection<Item> Items
            => this.items.AsReadOnly();

        public void AddItem(Item item)
        {
            if (this.Load + item.Weight > this.Capacity)
            {
                throw new InvalidOperationException("Bag is full!");
            }

            this.items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (this.Items.Count == 0)
            {
                throw new InvalidOperationException("Bag is empty!");
            }

            var item = this.Items
               .FirstOrDefault(i => i.GetType().Name == name);

            if (item == null)
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }

            this.items.Remove(item);
            return item;
        }
    }
}
