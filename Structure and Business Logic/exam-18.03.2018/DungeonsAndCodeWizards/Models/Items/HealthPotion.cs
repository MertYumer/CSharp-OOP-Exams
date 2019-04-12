namespace DungeonsAndCodeWizards.Models.Items
{
    using Characters;

    public class HealthPotion : Item
    {
        private const int DefaultWeight = 5;

        public HealthPotion() 
            : base(DefaultWeight)
        {
        }

        public override void AffectCharacter(Character character)
        {
            character.CheckIfAlive();
            character.IncreaseHealth(20);
        }
    }
}
