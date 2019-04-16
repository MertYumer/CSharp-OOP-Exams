namespace DungeonsAndCodeWizards.Models.Items
{
    using Contracts;
    using Characters;

    public abstract class Item : IItem
    {
        public Item(int weight)
        {
            this.Weight = weight;
        }

        public int Weight { get; private set; }

        public abstract void AffectCharacter(Character character);       
    }
}
