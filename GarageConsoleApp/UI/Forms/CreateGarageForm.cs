using GarageConsoleApp.Models;
using static GarageConsoleApp.UI.ConsoleUI;

namespace GarageConsoleApp.UI.Forms
{
    internal static class CreateGarageForm
    {
        private const int MinCapacity = 1;
        private const int MaxCapacity = 100;

        internal static Garage Run(List<Vehicle> vehicles)
        {
            Title("Create garage");
            string name = ReadString("What do you want to call your garage: ");
            int capacity = ReadInt($"How many parking spaces do you want? ({MinCapacity}-{MaxCapacity}): ", MinCapacity, MaxCapacity);
            int selected = Choose("Do you want an empty or populated garage?", ["Empty garage", "Populated garage"]);

            var garage = new Garage(name, capacity);

            Console.WriteLine();
            Text($"Garage \"{name}\" created with {capacity} parking spaces.");

            if (selected == 1)
            {
                int parked = Populate(garage, vehicles);
                Text($"{parked} of {vehicles.Count} vehicles from the registry were parked.");
            }

            PressAnyKey();
            return garage;
        }

        // Parks vehicles from the registry in random order, skipping any that do not fit
        private static int Populate(Garage garage, List<Vehicle> vehicles)
        {
            // Shuffle a copy so the registry keeps its own order
            List<Vehicle> shuffled = [.. vehicles];
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = Random.Shared.Next(i + 1);
                (shuffled[j], shuffled[i]) = (shuffled[i], shuffled[j]);
            }

            int parked = 0;
            foreach (Vehicle vehicle in shuffled)
            {
                if (garage.Park(vehicle) == ParkResult.Parked)
                    parked++;
            }
            return parked;
        }
    }
}