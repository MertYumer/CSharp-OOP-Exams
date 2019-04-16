namespace DungeonsAndCodeWizards.Factories
{
    using Models.Characters.Enums;
    using Models.Characters;
    using System;

    public class CharacterFactory
    {
        public Character CreateCharacter(string faction,
            string characterType, string name)
        {
            if (!Enum.TryParse(typeof(Faction), faction, out object tempFaction))
            {
                throw new ArgumentException($"Invalid faction \"{faction}\"!");
            }

            Faction finalFaction = (Faction)tempFaction;

            switch (characterType)
            {
                case "Cleric":
                    return new Cleric(name, finalFaction);

                case "Warrior":
                    return new Warrior(name, finalFaction);

                default:
                    throw new ArgumentException($"Invalid character type \"{characterType}\"!");
            }
        }
    }
}
