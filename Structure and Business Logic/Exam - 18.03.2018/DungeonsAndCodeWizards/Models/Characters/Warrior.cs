namespace DungeonsAndCodeWizards.Models.Characters
{
    using System;
    using Bags;
    using Contracts;
    using Enums;

    public class Warrior : Character, IAttackable
    {
        private const double DefaultBaseHealth = 100;
        private const double DefaultBaseArmor = 50;
        private const double DefaultAbilityPoints = 40;

        public Warrior(string name, Faction faction)
            : base(name, DefaultBaseHealth, DefaultBaseArmor, DefaultAbilityPoints,
                  new Satchel(), faction)
        {
        }

        public void Attack(Character character)
        {
            this.CheckIfAlive();
            character.CheckIfAlive();

            if (this.Name == character.Name)
            {
                throw new InvalidOperationException("Cannot attack self!");
            }

            if (this.Faction == character.Faction)
            {
                throw new ArgumentException($"Friendly fire! Both characters are from" +
                    $" {this.Faction} faction!");
            }

            character.TakeDamage(this.AbilityPoints);
        }
    }
}