namespace MortalEngines.Core
{
    using Contracts;
    using System;

    public class Engine : IEngine
    {
        private MachinesManager machinesManager;

        public Engine()
        {
            this.machinesManager = new MachinesManager();
        }

        public void Run()
        {
            while (true)
            {
                var input = Console.ReadLine().Split();
                string command = input[0];

                try
                {
                    switch (command)
                    {
                        case "HirePilot":
                            Console.WriteLine(this.machinesManager
                                .HirePilot(input[1]));
                            break;

                        case "PilotReport":
                            Console.WriteLine(this.machinesManager
                                .PilotReport(input[1]));
                            break;

                        case "ManufactureTank":
                            Console.WriteLine(this.machinesManager
                                .ManufactureTank(input[1], double.Parse(input[2]), double.Parse(input[3])));
                            break;

                        case "ManufactureFighter":
                            Console.WriteLine(this.machinesManager
                                .ManufactureFighter(input[1], double.Parse(input[2]), double.Parse(input[3])));
                            break;

                        case "MachineReport":
                            Console.WriteLine(this.machinesManager
                                .MachineReport(input[1]));
                            break;

                        case "AggressiveMode":
                            Console.WriteLine(this.machinesManager
                                .ToggleFighterAggressiveMode(input[1]));
                            break;

                        case "DefenseMode":
                            Console.WriteLine(this.machinesManager
                                .ToggleTankDefenseMode(input[1]));
                            break;

                        case "Engage":
                            Console.WriteLine(this.machinesManager
                                .EngageMachine(input[1], input[2]));
                            break;

                        case "Attack":
                            Console.WriteLine(this.machinesManager
                                .AttackMachines(input[1], input[2]));
                            break;

                        case "Quit":
                            return;
                    }
                }

                catch (ArgumentNullException ane)
                {
                    Console.WriteLine($"Error: {ane.Message}");
                }

                catch (NullReferenceException nre)
                {
                    Console.WriteLine($"Error: {nre.Message}");
                }
            }
        }
    }
}
