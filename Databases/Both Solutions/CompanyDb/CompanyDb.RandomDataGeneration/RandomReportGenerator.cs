namespace CompanyDb.RandomDataGeneration
{
    using System;
    using System.Collections.Generic;

    using Data;

    public class RandomReportGenerator : RandomDataGenerator
    {
        public IEnumerable<Report> GenerateReports(int count, IList<Employee> availableEmployees)
        {
            this.generateCount = count;
            var generatedReports = new List<Report>(count);

            for (int i = 0; i < this.generateCount; i++)
            {
                var newReport = new Report()
                {
                    Time = this.GetRandomDate(new DateTime(), DateTime.Now),
                    Employee = this.GetAssignedEmployee(availableEmployees),
                };

                generatedReports.Add(newReport);
            }

            return generatedReports;
        }

        private Employee GetAssignedEmployee(IList<Employee> availableEmployees)
        {
            var result = availableEmployees[rnd.Next(0, availableEmployees.Count)];
            return result;
        }
    }
}
