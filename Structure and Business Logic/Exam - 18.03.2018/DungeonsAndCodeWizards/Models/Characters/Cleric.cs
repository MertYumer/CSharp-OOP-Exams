namespace DungeonsAndCodeWizards.Models.Characters
{
    using Bags;
    using Contracts;
    using Enums;
    using System;

    public class Cleric : Character, IHealable
    {
        private const double DefaultBaseHealth = 50;
        private const double DefaultBaseArmor = 25;
        private const double DefaultAbilityPoints = 40;

        public Cleric(string name, Faction faction)
            : base(name, DefaultBaseHealth, DefaultBaseArmor, DefaultAbilityPoints,
                  new Backpack(), faction)
        {
            this.RestHealMultiplier = 0.5;
        }

        public void Heal(Character character)
        {
            this.CheckIfAlive();
            character.CheckIfAlive();

            if (this.Faction != character.Faction)
            {
                throw new InvalidOperationException("Cannot heal enemy character!");
            }

            character.IncreaseHealth(this.AbilityPoints);
        }
    }
}
