namespace Cars.Export.XmlExport
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    using Cars.Data;
    using Cars.Models;

    public static class XmlExporter
    {
        private static CarsDbContext db;

        public static void ExportQueriesFromFile(string inputFilePath, string outputDirPath, CarsDbContext context)
        {
            XmlExporter.db = context;

            var file = XDocument.Load(inputFilePath);

            foreach (var queryNode in file.Descendants().Where(d => d.Name.LocalName == "Query"))
            {
                IQueryable<Car> result = db.Cars.Select(x => x);

                var outputFileName = queryNode.Attribute("OutputFileName").Value;

                var whereClauses = queryNode.Element("WhereClauses").Elements("WhereClause").Select(c =>
                    new
                    {
                        PropertyName = c.Attribute("PropertyName").Value, // City, Year, and so on
                        Type = c.Attribute("Type").Value,                 // Greater than, Equals, and so on
                    });

                foreach (var clause in whereClauses)
                {
                    var clauseValue = queryNode.Value;

                    result = AddWhereClause(result, c =>
                    {
                        object value = GetPropValue(c, clause.PropertyName);
                        string type = GetType(value).Name;

                        switch (type.ToLower())
                        {
                            case "int32":
                                switch (clause.Type)
                                {
                                    case "GreaterThan":
                                        return ((int)value).CompareTo(clauseValue) > 0;
                                    case "LessThan":
                                        return ((int)value).CompareTo(clauseValue) < 0;
                                    case "Equals":
                                        return ((int)value).CompareTo(clauseValue) == 0;
                                    default:
                                        break;
                                }
                                break;
                            case "decimal":
                                switch (clause.Type)
                                {
                                    case "GreaterThan":
                                        return ((decimal)value).CompareTo(clauseValue) > 0;
                                    case "LessThan":
                                        return ((decimal)value).CompareTo(clauseValue) < 0;
                                    case "Equals":
                                        return ((decimal)value).CompareTo(clauseValue) == 0;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                switch (clause.Type)
                                {
                                    case "GreaterThan":
                                        return ((string)value).CompareTo(clauseValue) > 0;
                                    case "LessThan":
                                        return ((string)value).CompareTo(clauseValue) < 0;
                                    case "Equals":
                                        return ((string)value).CompareTo(clauseValue) == 0;
                                    default:
                                        break;
                                }
                                break;
                        }

                        throw new ArgumentException();

                    });
                }


                string orderBy = queryNode.Element("OrderBy") != null ? queryNode.Element("OrderBy").Value : null;

                var resultList = result.ToList(); // THese are the filtered Cars, no ordering though
            }
        }

        private static Type GetType(object clauseValue)
        {
            try
            {
                var type = GetNumberType(clauseValue);
            }
            catch (Exception ex)
            {
                return typeof(string);
            }

            return typeof(string);
        }

        private static Type GetNumberType(object value)
        {
            bool parsed = false;
            decimal tDecimal = -4m;

            parsed = decimal.TryParse((string)value, out tDecimal);

            if (parsed)
            {
                return typeof(decimal);
            }

            int tInt = -1;

            parsed = int.TryParse((string)value, out tInt);

            if (parsed)
            {
                return typeof(int);
            }

            throw new ArgumentException();
        }

        private static IQueryable<Car> AddWhereClause(IQueryable<Car> expressionSoFar, Func<Car, bool> func)
        {
            return expressionSoFar.Where(x => func(x));
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
