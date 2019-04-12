namespace DungeonsAndCodeWizards.Models.Items
{
    using Characters;

    public class PoisonPotion : Item
    {
        private const int DefaultWeight = 5;

        public PoisonPotion()
            : base(DefaultWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            character.CheckIfAlive();
            character.DecreaseHealth(20);
        }
    }
}
