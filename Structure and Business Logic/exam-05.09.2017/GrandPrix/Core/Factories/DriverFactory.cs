using System;

public static class DriverFactory
{
    public static Driver CreateDriver(string[] commandArgs, Car car)
    {
        string type = commandArgs[0];
        string name = commandArgs[1];

        switch (type)
        {
            case "Aggressive":
                return new AggressiveDriver(name, car);

            case "Endurance":
                return new EnduranceDriver(name, car);

            default:
                throw new ArgumentException();
        }
    }
}
