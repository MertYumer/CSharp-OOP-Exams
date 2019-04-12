namespace DungeonsAndCodeWizards.Models.Items
{
    using Characters;

    public class ArmorRepairKit : Item
    {
        private const int DefaultWeight = 10;

        public ArmorRepairKit()
            : base(DefaultWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            character.CheckIfAlive();
            character.RestoreArmor();
        }
    }
}
