namespace DungeonsAndCodeWizards.Models.Characters.Contracts
{
    using Bags;
    using Items;

    public interface ICharacter
    {
        string Name { get; }

        double BaseHealth { get; }

        double Health { get; }

        double BaseArmor { get; }

        double Armor { get; }

        double AbilityPoints { get; }

        Bag Bag { get; }

        bool IsAlive { get; }

        double RestHealMultiplier { get; set; }

        void TakeDamage(double hitPoints);

        void Rest();

        void CheckIfAlive();

        void IncreaseHealth(double points);

        void DecreaseHealth(double points);

        void RestoreArmor();

        void UseItem(Item item);

        void UseItemOn(Item item, Character character);

        void GiveCharacterItem(Item item, Character character);

        void ReceiveItem(Item item);
    }
}
