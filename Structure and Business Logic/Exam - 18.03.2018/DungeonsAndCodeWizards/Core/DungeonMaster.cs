namespace DungeonsAndCodeWizards.Core
{
    using Models.Characters;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models.Items;
    using Factories;
    using System.Linq;
    using Models.Characters.Contracts;

    public class DungeonMaster
    {
        private List<Character> characterParty;
        private List<Item> itemPool;
        private CharacterFactory characterFactory;
        private ItemFactory itemFactory;
        private int lastSurvivalRounds;

        public DungeonMaster()
        {
            this.characterParty = new List<Character>();
            this.itemPool = new List<Item>();
            this.characterFactory = new CharacterFactory();
            this.itemFactory = new ItemFactory();
            this.lastSurvivalRounds = 0;
        }

        public string JoinParty(string[] args)
        {
            string faction = args[0];
            string characterType = args[1];
            string name = args[2];
            var character = this.characterFactory
                .CreateCharacter(faction, characterType, name);

            this.characterParty.Add(character);
            return $"{name} joined the party!";
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];
            var item = this.itemFactory.CreateItem(itemName);
            this.itemPool.Add(item);

            return $"{itemName} added to pool.";
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];
            var character = this.characterParty
                .FirstOrDefault(c => c.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }

            if (this.itemPool.Count == 0)
            {
                throw new InvalidOperationException("No items left in pool!");
            }

            var item = this.itemPool.Last();
            character.ReceiveItem(item);
            this.itemPool.RemoveAt(this.itemPool.Count - 1);

            return $"{characterName} picked up {item.GetType().Name}!";
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            var character = this.characterParty
                .FirstOrDefault(c => c.Name == characterName);

            if (character == null)
            {
                throw new ArgumentException($"Character {characterName} not found!");
            }

            var item = character.Bag.GetItem(itemName);
            character.UseItem(item);

            return $"{character.Name} used {itemName}.";
        }

        public string UseItemOn(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            var giver = this.characterParty
                .FirstOrDefault(c => c.Name == giverName);

            var receiver = this.characterParty
                .FirstOrDefault(c => c.Name == receiverName);

            if (giver == null)
            {
                throw new ArgumentException($"Character {giverName} not found!");
            }

            if (receiver == null)
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }

            var item = giver.Bag.GetItem(itemName);
            giver.UseItemOn(item, receiver);

            return $"{giverName} used {itemName} on {receiverName}.";
        }

        public string GiveCharacterItem(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            var giver = this.characterParty
                .FirstOrDefault(c => c.Name == giverName);

            var receiver = this.characterParty
                .FirstOrDefault(c => c.Name == receiverName);

            if (giver == null)
            {
                throw new ArgumentException($"Character {giverName} not found!");
            }

            if (receiver == null)
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }

            var item = giver.Bag.GetItem(itemName);
            giver.GiveCharacterItem(item, receiver);

            return $"{giverName} gave {receiverName} {itemName}.";
        }

        public string GetStats()
        {
            var orderedCharacters = this.characterParty
                .OrderByDescending(c => c.IsAlive)
                .ThenByDescending(c => c.Health);

            var stringBuilder = new StringBuilder();

            foreach (var character in orderedCharacters)
            {
                string status = character.IsAlive ? "Alive" : "Dead";

                stringBuilder.AppendLine($"{character.Name} - " +
                    $"HP: {character.Health}/{character.BaseHealth}, " +
                    $"AP: {character.Armor}/{character.BaseArmor}, " +
                    $"Status: {status}");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            var attacker = this.characterParty
                .FirstOrDefault(c => c.Name == attackerName);

            var receiver = this.characterParty
                .FirstOrDefault(c => c.Name == receiverName);

            if (attacker == null)
            {
                throw new ArgumentException($"Character {attackerName} not found!");
            }

            if (receiver == null)
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }

            if (!(attacker is IAttackable))
            {
                throw new ArgumentException($"{attacker.Name} cannot attack!");
            }

            ((IAttackable)attacker).Attack(receiver);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{attackerName} attacks {receiverName} " +
                $"for {attacker.AbilityPoints} hit points!");

            stringBuilder.AppendLine($" {receiverName} has {receiver.Health}/{receiver.BaseHealth} HP " +
            $"and {receiver.Armor}/{receiver.BaseArmor} AP left!");

            if (!receiver.IsAlive)
            {
                stringBuilder.AppendLine($"{receiver.Name} is dead!");
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string receiverName = args[1];

            var healer = this.characterParty
                .FirstOrDefault(c => c.Name == healerName);

            var receiver = this.characterParty
                .FirstOrDefault(c => c.Name == receiverName);

            if (healer == null)
            {
                throw new ArgumentException($"Character {healerName} not found!");
            }

            if (receiver == null)
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }

            if (!(healer is IHealable))
            {
                throw new ArgumentException($"{healerName} cannot heal!");
            }

            ((IHealable)healer).Heal(receiver);

            return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}!" +
                $" {receiver.Name} has {receiver.Health} health now!";
        }

        public string EndTurn(string[] args)
        {
            var stringBuilder = new StringBuilder();
            var aliveCharacters = this.characterParty
                .Where(c => c.IsAlive)
                .ToList();

            foreach (var character in aliveCharacters)
            {
                var healthBeforeRest = character.Health;
                character.Rest();
                stringBuilder.AppendLine($"{character.Name} rests ({healthBeforeRest} " +
                    $"=> {character.Health})");
            }

            if (aliveCharacters.Count <= 1)
            {
                this.lastSurvivalRounds++;
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public bool IsGameOver()
        {
            return this.lastSurvivalRounds == 2;
        }
    }
}
