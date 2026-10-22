using GarageConsoleApp.Models;

namespace GarageConsoleApp
{
    internal static class VehicleRegistry
    {
        internal static List<Vehicle> CreateSeed()
        {
            List<Vehicle> vehicles =
            [
                // Cars (default: 1 space)
                new Car("ABC123", "Volvo V70", "Black", FuelType.Diesel),
                new Car("DEF456", "Tesla Model 3", "Red", FuelType.Electric),
                new Car("GHJ789", "Volkswagen Golf", "Black", FuelType.Gasoline),
                new Car("KLM012", "Toyota Prius", "White", FuelType.Hybrid),
                new Car("NOP345", "Smart Fortwo", "Red", FuelType.Electric, sizeInUnits: 2),

                // Motorcycles (default: a third of a space)
                new Motorcycle("CDE12F", "Honda CB500F", "Black", TransmissionType.Manual),
                new Motorcycle("HJK34L", "Yamaha MT-07", "Blue", TransmissionType.Manual),
                new Motorcycle("MNP56R", "Vespa GTS 300", "White", TransmissionType.Automatic),
                new Motorcycle("STU78X", "Piaggio MP3", "Pink", TransmissionType.Automatic, numberOfWheels: 3),
                new Motorcycle("YZA90B", "Harley-Davidson Road King", "Black", TransmissionType.Manual, sizeInUnits: 2),

                // Buses (default: 3 spaces)
                new Bus("BUS001", "Volvo 8900", "Red", numberOfSeats: 50, numberOfWheels: 6),
                new Bus("BUS002", "Scania Citywide", "Blue", numberOfSeats: 40, numberOfWheels: 6),
                new Bus("BUS003", "Mercedes-Benz Sprinter", "White", numberOfSeats: 19, numberOfWheels: 4,
                    sizeInUnits: ParkingUnits.FromSpaces(2)),

                // Boats (default: 3 spaces)
                new Boat("BOT001", "Nimbus 305", "White", length: 9.5),
                new Boat("BOT002", "Buster XL", "Gray", length: 5.7, sizeInUnits: ParkingUnits.FromSpaces(2)),
                new Boat("BOT003", "Sunseeker Predator 50", "Red", length: 15.2, sizeInUnits: ParkingUnits.FromSpaces(6)),

                // Airplanes (default: 8 spaces)
                new Airplane("SE-ABC", "Cessna 172", "White", numberOfWheels: 3, numberOfEngines: 1),
                new Airplane("SE-DEF", "Piper PA-28 Cherokee", "Red", numberOfWheels: 3, numberOfEngines: 1),
                new Airplane("SE-GHJ", "Beechcraft Baron 58", "Blue", numberOfWheels: 3, numberOfEngines: 2,
                    sizeInUnits: ParkingUnits.FromSpaces(10)),
            ];

            EnsureUniqueRegistrationNumbers(vehicles);
            return vehicles;
        }

        private static void EnsureUniqueRegistrationNumbers(List<Vehicle> vehicles)
        {
            for (int i = 0; i < vehicles.Count; i++)
            {
                for (int j = i + 1; j < vehicles.Count; j++)
                {
                    if (vehicles[i].RegistrationNumber.Equals(
                            vehicles[j].RegistrationNumber, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new DuplicateRegistrationNumberException(vehicles[i].RegistrationNumber);
                    }
                }
            }
        }
    }
}