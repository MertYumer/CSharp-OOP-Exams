using Travel.Core.Controllers;
using Travel.Entities;
using Travel.Entities.Airplanes;
using Travel.Entities.Items;

namespace Travel.UserTests
{
	using NUnit.Framework;
    using System.Text;

    public class FlightControllerTests
	{
		[Test]
		public void SuccessfulTrip()
		{
			var passengers = new[]
			{
				new Passenger("Pesho1"),
				new Passenger("Pesho2"),
				new Passenger("Pesho3"),
				new Passenger("Pesho4"),
				new Passenger("Pesho5"),
				new Passenger("Pesho6"),
			};

			var airplane = new LightAirplane();

			foreach (var passenger in passengers)
			{
				airplane.AddPassenger(passenger);
			}

			var trip = new Trip("Sofia", "London", airplane);

			var airport = new Airport();

			airport.AddTrip(trip);

			var flightController = new FlightController(airport);

			var bag = new Bag(passengers[1], new[] { new Colombian() });

			passengers[1].Bags.Add(bag);

			var completedTrip = new Trip("Sofia", "Varna", new LightAirplane());
			completedTrip.Complete();

			airport.AddTrip(completedTrip);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("SofiaLondon1:");
            stringBuilder.AppendLine("Overbooked! Ejected Pesho2");
            stringBuilder.AppendLine("Confiscated 1 bags ($50000)");
            stringBuilder.AppendLine("Successfully transported 5 passengers from Sofia to London.");
            stringBuilder.AppendLine("Confiscated bags: 1 (1 items) => $50000");

            var expectedResult = stringBuilder.ToString().TrimEnd();
            var actualResult = flightController.TakeOff();

			Assert.AreEqual(expectedResult, actualResult);
			Assert.IsTrue(trip.IsCompleted);
		}
	}
}