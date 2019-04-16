using System;

public class UltrasoftTyre : Tyre
{
    private const string DefaultName = "Ultrasoft";

    public UltrasoftTyre(double hardness, double grip)
        : base(DefaultName, hardness)
    {
        this.Grip = grip;
    }

    public double Grip { get; private set; }

    public override double Degradation
    {
        get => base.Degradation;

        protected set
        {
            if (value < 30)
            {
                throw new ArgumentException("Blown Tyre");
            }

            base.Degradation = value;
        }
    }

    public override void ReduceDegradation()
    {
        this.Degradation -= (this.Hardness + this.Grip);
    }
}
