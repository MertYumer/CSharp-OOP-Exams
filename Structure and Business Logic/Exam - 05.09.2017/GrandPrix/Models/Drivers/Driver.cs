public abstract class Driver : IDriver
{
    protected Driver(string name, Car car)
    {
        this.Name = name;
        this.Car = car;
    }

    public string Name { get; private set; }

    public double TotalTime { get; private set; }

    public Car Car { get; private set; }

    public double FuelConsumptionPerKm { get; protected set; }

    public virtual double Speed
        => (this.Car.Hp + this.Car.Tyre.Degradation) / this.Car.FuelAmount;

    public void CompleteLap(int trackLength)
    {
        this.TotalTime += 60 / (trackLength / this.Speed);
    }

    public void IncreaseTime(int seconds)
    {
        this.TotalTime += seconds;
    }

    public void DecreaseTime(int seconds)
    {
        this.TotalTime -= seconds;
    }
}
