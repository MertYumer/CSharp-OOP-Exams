using System;

public class Car : ICar
{
    private double fuelAmount;

    public Car(int hp, double fuelAmount, Tyre tyre)
    {
        this.Hp = hp;
        this.FuelAmount = fuelAmount;
        this.Tyre = tyre;
    }

    public int Hp { get; private set; }

    public double FuelAmount
    {
        get => this.fuelAmount;

        private set
        {
            if (value > 160)
            {
                value = 160;
            }

            else if (value < 0)
            {
                throw new ArgumentException("Out of fuel");
            }

            this.fuelAmount = value;
        }
    }

    public Tyre Tyre { get; private set; }

    public void ChangeTyres(Tyre tyre)
    {
        this.Tyre = tyre;
    }

    public void Refuel(double fuelAmount)
    {
        this.FuelAmount += fuelAmount;
    }

    public void ReduceFuel(int trackLength, double fuelConsumptionPerKm)
    {
        this.FuelAmount -= (trackLength * fuelConsumptionPerKm);
    }
}
