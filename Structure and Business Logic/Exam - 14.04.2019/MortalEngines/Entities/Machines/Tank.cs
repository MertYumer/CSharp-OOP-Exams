namespace MortalEngines.Entities.Machines
{
    using Contracts;
    using System.Text;

    public class Tank: BaseMachine, ITank
    {
        private const double DefaultHealthPoints = 100;

        public Tank(string name, double attackPoints, double defensePoints)
            : base(name, attackPoints, defensePoints, DefaultHealthPoints)
        {
            this.ToggleDefenseMode();
        }

        public bool DefenseMode { get; private set; }

        public void ToggleDefenseMode()
        {
            this.DefenseMode = this.DefenseMode == true
                ? false
                : true;

            if (this.DefenseMode)
            {
                this.AttackPoints -= 40;
                this.DefensePoints += 30;
            }

            else
            {
                this.AttackPoints += 40;
                this.DefensePoints -= 30;
            }
        }

        public override string ToString()
        {
            var mode = this.DefenseMode == true
                ? "ON"
                : "OFF";

            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($" *Defense: {mode}");
            return builder.ToString().TrimEnd();
        }
    }
}
