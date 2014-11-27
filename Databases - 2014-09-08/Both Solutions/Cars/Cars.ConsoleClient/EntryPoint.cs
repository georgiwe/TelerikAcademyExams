namespace Cars.ConsoleClient
{
    using System;
    using System.Linq;

    using Cars.Data;
    using Cars.Import.JsonImport;
    using Cars.Export.XmlExport;

    public class EntryPoint
    {
        public static void Main()
        {
            // I am using SQL EXPRESS !
            // I am using SQL EXPRESS !
            // I am using SQL EXPRESS !

            // Also using Newtonsoft JSON parser, 
            // so you will need to download that as well - it's in NuGet

            // Messages appear on the console to let you know it's working

            var db = new CarsDbContext();
            
            //string jsonFilesDirPath = @".\json";

            //db.Configuration.AutoDetectChangesEnabled = false;
            //JsonImporter.GetParsedCars(jsonFilesDirPath, db);
            //db.Configuration.AutoDetectChangesEnabled = true;

            string xmlFilePath = @".\Queries.xml";
            string outputFilePath = @".\exports\";

            XmlExporter.ExportQueriesFromFile(xmlFilePath, outputFilePath, db);
        }
    }
}
