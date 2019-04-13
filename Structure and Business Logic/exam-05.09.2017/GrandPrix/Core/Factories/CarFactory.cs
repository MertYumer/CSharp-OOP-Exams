public static class CarFactory
{
    public static Car CreateCar(string[] commandArgs, Tyre tyre)
    {
        int hp = int.Parse(commandArgs[0]);
        double fuelAmount = double.Parse(commandArgs[1]);

        return new Car(hp, fuelAmount, tyre);
    }
}
