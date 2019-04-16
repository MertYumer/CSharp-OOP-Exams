namespace MortalEngines.Core
{
    using Contracts;
    using Entities.Contracts;
    using Entities.Machines;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private List<Pilot> pilots;
        private List<BaseMachine> machines;

        public MachinesManager()
        {
            this.pilots = new List<Pilot>();
            this.machines = new List<BaseMachine>();
        }

        public string HirePilot(string name)
        {
            if (this.pilots.Any(p => p.Name == name))
            {
                return $"Pilot {name} is hired already";
            }

            else
            {
                var pilot = new Pilot(name);
                this.pilots.Add(pilot);
                return $"Pilot {name} hired";
            }
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.Any(t => t.Name == name))
            {
                return $"Machine {name} is manufactured already";
            }

            else
            {
                var tank = new Tank(name, attackPoints, defensePoints);
                this.machines.Add(tank);
                return $"Tank {tank.Name} manufactured - attack: {tank.AttackPoints:F2};" +
                    $" defense: {tank.DefensePoints:F2}";
            }
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            if (this.machines.Any(t => t.Name == name))
            {
                return $"Machine {name} is manufactured already";
            }

            else
            {
                var fighter = new Fighter(name, attackPoints, defensePoints);
                this.machines.Add(fighter);
                return $"Fighter {fighter.Name} manufactured - attack: {fighter.AttackPoints:F2};" +
                    $" defense: {fighter.DefensePoints:F2}; aggressive: ON";
            }
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            var pilot = this.pilots.FirstOrDefault(p => p.Name == selectedPilotName);
            var machine = this.machines.FirstOrDefault(p => p.Name == selectedMachineName);

            if (pilot == null)
            {
                return $"Pilot {selectedPilotName} could not be found";
            }

            if (machine == null)
            {
                return $"Machine {selectedMachineName} could not be found";
            }

            if (this.pilots.Any(p => p.Machines.Any(m => m.Name == machine.Name)))
            {
                return $"Machine {machine.Name} is already occupied";
            }

            else
            {
                machine.Pilot = pilot;
                pilot.AddMachine(machine);
                return $"Pilot {pilot.Name} engaged machine {machine.Name}";
            }
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            var attacker = this.machines.FirstOrDefault(m => m.Name == attackingMachineName);
            var defender = this.machines.FirstOrDefault(m => m.Name == defendingMachineName);

            if (attacker == null)
            {
                return $"Machine {attackingMachineName} could not be found";
            }

            if (defender == null)
            {
                return $"Machine {defendingMachineName} could not be found";
            }

            if (attacker.HealthPoints == 0)
            {
                return $"Dead machine {attacker.Name} cannot attack or be attacked";
            }

            if (defender.HealthPoints == 0)
            {
                return $"Dead machine {defender.Name} cannot attack or be attacked";
            }

            else
            {
                attacker.Attack(defender);
                return $"Machine {defender.Name} was attacked by machine" +
                    $" {attacker.Name} - current health: {defender.HealthPoints:f2}";
            }
        }

        public string PilotReport(string pilotReporting)
        {
            IPilot pilot = this.pilots.First(p => p.Name == pilotReporting);
            return pilot.Report();
        }

        public string MachineReport(string machineName)
        {
            var machine = this.machines.First(m => m.Name == machineName);
            return machine.ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            Fighter fighter = (Fighter)this.machines
                .FirstOrDefault(t => t.Name == fighterName);

            if (fighter == null)
            {
                return $"Machine {fighterName} could not be found";
            }

            fighter.ToggleAggressiveMode();
            return $"Fighter {fighter.Name} toggled aggressive mode";
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            Tank tank = (Tank)this.machines
                .FirstOrDefault(t => t.Name == tankName);

            if (tank == null)
            {
                return $"Machine {tankName} could not be found";
            }

            tank.ToggleDefenseMode();
            return $"Tank {tank.Name} toggled defense mode";
        }
    }
}