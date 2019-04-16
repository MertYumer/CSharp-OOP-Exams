using System;
using System.Linq;

public class Engine
{
    private RaceTower raceTower;

    public Engine()
    {
        this.raceTower = new RaceTower();
    }

    public void Run()
    {
        int numberOfLaps = int.Parse(Console.ReadLine());
        int trackLength = int.Parse(Console.ReadLine());
        this.raceTower.SetTrackInfo(numberOfLaps, trackLength);

        while (true)
        {
            try
            {
                var input = Console.ReadLine().Split().ToList();
                string command = input[0];
                input.RemoveAt(0);

                switch (command)
                {
                    case "RegisterDriver":
                        this.raceTower.RegisterDriver(input);
                        break;

                    case "Leaderboard":
                        Console.WriteLine(this.raceTower.GetLeaderboard());
                        break;

                    case "CompleteLaps":
                        var result = this.raceTower.CompleteLaps(input);

                        if (result != string.Empty)
                        {
                            Console.WriteLine(result);
                        }

                        break;

                    case "Box":
                        this.raceTower.DriverBoxes(input);
                        break;

                    case "ChangeWeather":
                        this.raceTower.ChangeWeather(input);
                        break;
                }

                if (this.raceTower.IsFinished)
                {
                    var winner = this.raceTower.Winner;
                    Console.WriteLine($"{winner.Name} wins the race for {winner.TotalTime:f3} seconds.");
                    return;
                }
            }

            catch (ArgumentException ae)
            {
            }
        }
    }
}
