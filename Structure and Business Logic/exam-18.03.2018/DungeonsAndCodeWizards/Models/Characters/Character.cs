namespace DungeonsAndCodeWizards.Models.Characters
{
    using Bags;
    using Contracts;
    using DungeonsAndCodeWizards.Models.Items;
    using Enums;
    using System;

    public abstract class Character : ICharacter
    {
        private string name;
        private double health;
        private double armor;

        public Character(string name, double health, double armor,
            double abilityPoints, Bag bag, Faction faction)
        {
            this.Name = name;
            this.BaseHealth = health;
            this.Health = health;
            this.BaseArmor = armor;
            this.Armor = armor;
            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
            this.Faction = faction;
            this.IsAlive = true;
            this.RestHealMultiplier = 0.2;
        }

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }

                this.name = value;
            }
        }

        public double BaseHealth { get; private set; }

        public double Health
        {
            get => this.health;

            private set
            {
                if (value > this.BaseHealth)
                {
                    value = this.BaseHealth;
                }

                else if (value < 0)
                {
                    this.IsAlive = false;
                    value = 0;
                }

                this.health = value;
            }
        }

        public double BaseArmor { get; private set; }

        public double Armor
        {
            get => this.armor;

            private set
            {
                if (value > this.BaseArmor)
                {
                    value = this.BaseArmor;
                }

                else if (value < 0)
                {
                    value = 0;
                }

                this.armor = value;
            }
        }

        public double AbilityPoints { get; private set; }

        public Bag Bag { get; private set; }

        public Faction Faction { get; private set; }

        public bool IsAlive { get; set; }

        public double RestHealMultiplier { get; set; }

        public void CheckIfAlive()
        {
            if (!this.IsAlive)
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void DecreaseHealth(double points)
        {
            this.Health -= points;

            if (this.Health == 0)
            {
                this.IsAlive = false;
            }
        }

        public void GiveCharacterItem(Item item, Character character)
        {
            this.CheckIfAlive();
            character.CheckIfAlive();
            character.ReceiveItem(item);
        }

        public void IncreaseHealth(double points)
        {
            this.Health += points;
        }

        public void ReceiveItem(Item item)
        {
            this.CheckIfAlive();
            this.Bag.AddItem(item);
        }

        public void Rest()
        {
            this.CheckIfAlive();

            this.Health += this.BaseHealth * this.RestHealMultiplier;
        }

        public void RestoreArmor()
        {
            this.Armor = this.BaseArmor;
        }

        public void TakeDamage(double hitPoints)
        {
            this.CheckIfAlive();

            double armorPoints = this.Armor;
            this.Armor -= hitPoints;
            hitPoints -= armorPoints;

            if (hitPoints > 0)
            {
                this.Health -= hitPoints;
            }

            if (this.Health == 0)
            {
                this.IsAlive = false;
            }
        }

        public void UseItem(Item item)
        {
            this.CheckIfAlive();
            item.AffectCharacter(this);
        }

        public void UseItemOn(Item item, Character character)
        {
            this.CheckIfAlive();
            character.CheckIfAlive();
            item.AffectCharacter(character);
        }
    }
}
