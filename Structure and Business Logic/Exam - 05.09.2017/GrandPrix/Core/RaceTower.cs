using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RaceTower
{
    private int lapsNumber;
    private int trackLength;
    private List<Driver> activeDrivers;
    private Dictionary<Driver, string> disqualifiedDrivers;
    private Weather weather;
    private int currentLap;

    public RaceTower()
    {
        this.weather = Weather.Sunny;
        this.activeDrivers = new List<Driver>();
        this.disqualifiedDrivers = new Dictionary<Driver, string>();
    }

    public bool IsFinished { get; private set; }

    public Driver Winner { get; private set; }

    public void SetTrackInfo(int lapsNumber, int trackLength)
    {
        this.lapsNumber = lapsNumber;
        this.trackLength = trackLength;
    }

    public void RegisterDriver(List<string> commandArgs)
    {
        try
        {
            var tyre = TyreFactory.CreateTyre(commandArgs.Skip(4).ToArray());
            var car = CarFactory.CreateCar(commandArgs.Skip(2).ToArray(), tyre);
            var driver = DriverFactory.CreateDriver(commandArgs.Take(2).ToArray(), car);
            this.activeDrivers.Add(driver);
        }

        catch (ArgumentException ae)
        {
        }
    }

    public void DriverBoxes(List<string> commandArgs)
    {
        string reasonToBox = commandArgs[0];
        string driverName = commandArgs[1];
        var driver = this.activeDrivers.First(d => d.Name == driverName);
        driver.IncreaseTime(20);

        if (reasonToBox == "ChangeTyres")
        {
            var tyre = TyreFactory.CreateTyre(commandArgs.Skip(2).ToArray());
            driver.Car.ChangeTyres(tyre);
        }

        else if (reasonToBox == "Refuel")
        {
            double fuelAmount = double.Parse(commandArgs[2]);
            driver.Car.Refuel(fuelAmount);
        }
    }

    public string CompleteLaps(List<string> commandArgs)
    {
        int numberOfLaps = int.Parse(commandArgs[0]);

        if (numberOfLaps > this.lapsNumber)
        {
            return $"There is no time! On lap {this.currentLap}.";
        }

        var stringBuilder = new StringBuilder();

        for (int i = 0; i < numberOfLaps; i++)
        {
            for (int j = 0; j < this.activeDrivers.Count; j++)
            {
                var driver = this.activeDrivers[j];
                driver.CompleteLap(this.trackLength);

                try
                {
                    driver.Car.ReduceFuel(this.trackLength, driver.FuelConsumptionPerKm);
                    driver.Car.Tyre.ReduceDegradation();
                }

                catch (ArgumentException ae)
                {
                    this.activeDrivers.Remove(driver);
                    j--;
                    this.disqualifiedDrivers.Add(driver, ae.Message);
                }
            }

            this.currentLap++;
            var standings = this.activeDrivers
                .OrderByDescending(d => d.TotalTime)
                .ToList();

            this.Overtaking(standings, stringBuilder);
        }

        if (this.currentLap == this.lapsNumber)
        {
            this.IsFinished = true;
            this.Winner = this.activeDrivers
                .OrderBy(d => d.TotalTime)
                .FirstOrDefault();
        }

        return stringBuilder.ToString().TrimEnd();
    }

    public string GetLeaderboard()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"Lap {currentLap}/{lapsNumber}");

        int position = 1;
        foreach (var driver in activeDrivers.OrderBy(d => d.TotalTime))
        {
            stringBuilder.AppendLine($"{position++} {driver.Name} {driver.TotalTime:F3}");
        }

        foreach (var driver in this.disqualifiedDrivers.Reverse())
        {
            stringBuilder.AppendLine($"{position++} {driver.Key.Name} {driver.Value}");
        }

        return stringBuilder.ToString().TrimEnd();
    }

    public void ChangeWeather(List<string> commandArgs)
    {
        this.weather = (Weather)Enum.Parse(typeof(Weather), commandArgs[0]);
    }

    private void Overtaking(List<Driver> standings, StringBuilder stringBuilder)
    {
        for (int i = 0; i < standings.Count - 1; i++)
        {
            Driver frontDriver = standings[i];
            Driver behindDriver = standings[i + 1];
            double gap = Math.Abs(frontDriver.TotalTime - behindDriver.TotalTime);
            int interval = 2;
            bool crashed = this.CheckConditions(frontDriver, ref interval);

            if (gap <= interval)
            {
                if (crashed)
                {
                    this.disqualifiedDrivers.Add(frontDriver, "Crashed");
                    this.activeDrivers.Remove(frontDriver);
                    continue;
                }

                frontDriver.DecreaseTime(interval);
                behindDriver.IncreaseTime(interval);
                stringBuilder.AppendLine($"{frontDriver.Name} has overtaken {behindDriver.Name} on lap {this.currentLap}.");
            }
        }
    }

    private bool CheckConditions(Driver frontDriver, ref int interval)
    {
        bool crashed = false;
        if (frontDriver.GetType().Name == "AggressiveDriver"
            && frontDriver.Car.Tyre.GetType().Name == "UltrasoftTyre")
        {
            interval = 3;
            if (this.weather == Weather.Foggy)
            {
                crashed = true;
            }
        }

        if (frontDriver.GetType().Name == "EnduranceDriver"
            && frontDriver.Car.Tyre.GetType().Name == "HardTyre")
        {
            interval = 3;
            if (this.weather == Weather.Rainy)
            {
                crashed = true;
            }
        }

        return crashed;
    }
}
