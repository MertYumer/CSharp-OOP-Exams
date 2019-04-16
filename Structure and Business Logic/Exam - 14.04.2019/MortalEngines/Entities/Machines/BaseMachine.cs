namespace MortalEngines.Entities.Machines
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class BaseMachine : IMachine
    {
        private string name;
        private IPilot pilot;

        public BaseMachine(string name, double attackPoints, 
            double defensePoints, double healthPoints)
        {
            this.Name = name;
            this.AttackPoints = attackPoints;
            this.DefensePoints = defensePoints;
            this.HealthPoints = healthPoints;
            this.Targets = new List<string>();
        }

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Machine name cannot be null or empty.");
                }

                this.name = value;
            }
        }

        public IPilot Pilot
        {
            get => this.pilot;

            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Pilot cannot be null.");
                }

                this.pilot = value;
            }
        }

        public double HealthPoints { get; set; }

        public double AttackPoints { get; protected set; }

        public double DefensePoints { get; protected set; }

        public IList<string> Targets { get; private set; }

        public void Attack(IMachine target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

            target.HealthPoints -= (this.AttackPoints - target.DefensePoints);
            this.Targets.Add(target.Name);

            if (target.HealthPoints < 0)
            {
                target.HealthPoints = 0;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            string targetList = this.Targets.Count == 0
                ? "None"
                : string.Join(",", this.Targets);

            builder.AppendLine($"- {this.Name}");
            builder.AppendLine($" *Type: {this.GetType().Name}");
            builder.AppendLine($" *Health: {this.HealthPoints:f2}");
            builder.AppendLine($" *Attack: {this.AttackPoints:f2}");
            builder.AppendLine($" *Defense: {this.DefensePoints:f2}");
            builder.AppendLine($" *Targets: {targetList}");

            return builder.ToString().TrimEnd();
        }
    }
}
