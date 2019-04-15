namespace MortalEngines.Entities.Machines
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Pilot : IPilot
    {
        private string name;
        private List<IMachine> machines;

        public Pilot(string name) 
        {
            this.Name = name;
            this.machines = new List<IMachine>();
        }

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"Pilot name cannot be null or empty string.");
                }

                this.name = value;
            }
        }

        public IReadOnlyCollection<IMachine> Machines => this.machines.AsReadOnly();

        public void AddMachine(IMachine machine)
        {
            if (machine == null)
            {
                throw new NullReferenceException("Null machine cannot be added to the pilot.");
            }

            this.machines.Add(machine);
        }

        public string Report()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{this.Name} - {this.machines.Count} machines");

            foreach (var machine in this.machines)
            {
                builder.AppendLine(machine.ToString());
            }

            return builder.ToString().TrimEnd();
        }
    }
}
