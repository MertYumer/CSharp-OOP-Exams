namespace MortalEngines.Entities.Machines
{
    using Contracts;
    using System.Text;

    public class Fighter : BaseMachine, IFighter
    {
        private const double DefaultHealthPoints = 200;

        public Fighter(string name, double attackPoints, double defensePoints) 
            : base(name, attackPoints, defensePoints, DefaultHealthPoints)
        {
            this.ToggleAggressiveMode();
        }

        public bool AggressiveMode { get; private set; }

        public void ToggleAggressiveMode()
        {
            this.AggressiveMode = this.AggressiveMode == true
                ? false
                : true;

            if (this.AggressiveMode)
            {
                this.AttackPoints += 50;
                this.DefensePoints -= 25;
            }

            else
            {
                this.AttackPoints -= 50;
                this.DefensePoints += 25;
            }
        }

        public override string ToString()
        {
            var mode = this.AggressiveMode == true
                ? "ON"
                : "OFF";

            var builder = new StringBuilder();
            builder.AppendLine(base.ToString());
            builder.AppendLine($" *Aggressive: {mode}");
            return builder.ToString().TrimEnd();
        }
    }
}
