namespace Cars.Import.JsonImport
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Data.Entity;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Cars.Models;
    using Cars.Data;

    public static class JsonImporter
    {
        private static CarsDbContext db;
        private static HashSet<City> citiesImported;
        private static HashSet<Dealer> dealersImported;
        private static HashSet<Manufacturer> manufacturersImported;
        private static HashSet<Car> carsImported;

        public static void GetParsedCars(string jsonsDir, CarsDbContext context)
        {
            citiesImported = new HashSet<City>();
            dealersImported = new HashSet<Dealer>();
            manufacturersImported = new HashSet<Manufacturer>();
            carsImported = new HashSet<Car>();
            JsonImporter.db = context;

            var filePaths = Directory.GetFiles(jsonsDir);

            foreach (var path in filePaths)
            {
                ImportCarsInFile(path);
                Console.WriteLine("\nFILE IMPORTED\n");
            }
        }

        private static void ImportCarsInFile(string filePath)
        {
            var jsonArrayStr = File.ReadAllText(filePath);
            var parsedObjects = JsonConvert.DeserializeObject<JsonItemPlaceholder[]>(jsonArrayStr);

            int importedCarsCount = 0;

            foreach (var obj in parsedObjects)
            {
                var newCar = ParseCar(obj);

                db.Cars.Add(newCar);
                importedCarsCount++;

                carsImported.Add(newCar);

                if (importedCarsCount % 200 == 0)
                {
                    db.SaveChanges();
                    Console.WriteLine("\nSent 200 to db\n");
                }
            }
        }

        private static Car ParseCar(JsonItemPlaceholder jsonItem)
        {
            var newCar = new Car()
            {
                Model = jsonItem.Model,
                Year = jsonItem.Year,
                Transmission = jsonItem.Transmission,
                Price = jsonItem.Price,
                Manufacturer = GetManufacturer(jsonItem.ManufacturerName),
                Dealer = GetDealer(jsonItem.Dealer.Name, jsonItem.Dealer.Name),
            };

            return newCar;
        }

        private static Dealer GetDealer(string dealerName, string dealerCityName)
        {
            var dealer = db.Dealers.Include("Cities")
                .FirstOrDefault(d => d.Name == dealerName);

            if (dealer == null)
            {
                var dealerCity = GetCity(dealerCityName);
                var dealerCities = new HashSet<City>();
                dealerCities.Add(dealerCity);

                dealer = new Dealer()
                {
                    Cities = dealerCities,
                    Name = dealerName
                };

                dealersImported.Add(dealer);
            }

            return dealer;
        }

        private static Manufacturer GetManufacturer(string manufName)
        {
            var manufacturer = db.Manufacturers.FirstOrDefault(m => m.Name == manufName);

            if (manufacturer == null)
            {
                manufacturer = manufacturersImported.FirstOrDefault(m => m.Name == manufName);
            }

            if (manufacturer == null)
            {
                manufacturer = new Manufacturer()
                {
                    Name = manufName,
                };

                manufacturersImported.Add(manufacturer);
            }

            return manufacturer;
        }

        private static City GetCity(string cityName)
        {
            var city = db.Cities.FirstOrDefault(c => c.Name == cityName);

            if (city == null)
            {
                city = citiesImported.FirstOrDefault(c => c.Name == cityName);
            }

            if (city == null)
            {
                city = new City()
                {
                    Name = cityName,
                };

                citiesImported.Add(city);
            }

            return city;
        }
    }
}
