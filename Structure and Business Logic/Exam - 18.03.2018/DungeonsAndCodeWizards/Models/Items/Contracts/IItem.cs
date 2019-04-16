namespace DungeonsAndCodeWizards.Models.Items.Contracts
{
    using Characters;

    public interface IItem
    {
        int Weight { get; }

        void AffectCharacter(Character character);
    }
}
