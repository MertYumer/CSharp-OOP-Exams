namespace DungeonsAndCodeWizards.Core
{
    using System;
    using System.Linq;

    public class Engine
    {
        private DungeonMaster dungeonMaster;

        public Engine()
        {
            this.dungeonMaster = new DungeonMaster();
        }

        public void Run()
        {
            while (true)
            {
                string input = Console.ReadLine();
                

                if (this.dungeonMaster.IsGameOver() || string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Final stats:");
                    Console.WriteLine(this.dungeonMaster.GetStats());
                    return;
                }

                string[] commands = input.Split();
                var arguments = commands.Skip(1).ToArray();

                try
                {
                    switch (commands[0])
                    {
                        case "JoinParty":
                            Console.WriteLine(this.dungeonMaster.JoinParty(arguments));
                            break;

                        case "AddItemToPool":
                            Console.WriteLine(this.dungeonMaster.AddItemToPool(arguments));
                            break;

                        case "PickUpItem":
                            Console.WriteLine(this.dungeonMaster.PickUpItem(arguments));
                            break;

                        case "UseItem":
                            Console.WriteLine(this.dungeonMaster.UseItem(arguments));
                            break;

                        case "UseItemOn":
                            Console.WriteLine(this.dungeonMaster.UseItemOn(arguments));
                            break;

                        case "GiveCharacterItem":
                            Console.WriteLine(this.dungeonMaster.GiveCharacterItem(arguments));
                            break;

                        case "GetStats":
                            Console.WriteLine(this.dungeonMaster.GetStats());
                            break;

                        case "Attack":
                            Console.WriteLine(this.dungeonMaster.Attack(arguments));
                            break;

                        case "Heal":
                            Console.WriteLine(this.dungeonMaster.Heal(arguments));
                            break;

                        case "EndTurn":
                            Console.WriteLine(this.dungeonMaster.EndTurn(arguments));
                            break;
                    }
                }

                catch (ArgumentException ae)
                {
                    Console.WriteLine($"Parameter Error: {ae.Message}");
                }

                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine($"Invalid Operation: {ioe.Message}");
                }
            }
        }
    }
}
