using System;

public static class TyreFactory
{
    public static Tyre CreateTyre(string[] commandArgs)
    {
        string tyreType = commandArgs[0];
        double tyreHardness = double.Parse(commandArgs[1]);
        double grip = 0;

        if (tyreType == "Ultrasoft")
        {
            grip = double.Parse(commandArgs[2]);
        }

        switch (tyreType)
        {
            case "Hard":
                return new HardTyre(tyreHardness);

            case "Ultrasoft":
                return new UltrasoftTyre(tyreHardness, grip);

            default:
                throw new ArgumentException();
        }
    }
}
