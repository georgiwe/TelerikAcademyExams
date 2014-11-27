namespace CompanyDb.RandomDataGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CompanyDb.Data;

    public class RandomEmployeeProjectsGenerator : RandomDataGenerator
    {
        private ICollection<EmployeesProject> generated;

        public IEnumerable<EmployeesProject> Generate(int count, IList<Employee> availableEmployees, ICollection<Project> availableProjects)
        {
            this.generateCount = count;
            this.generated = new List<EmployeesProject>(this.generateCount);

            for (int i = 0; i < this.generateCount; i++)
            {
                var projEndDate = this.GetRandomDate(min: new DateTime(2010, 1, 1), max: new DateTime());
                var daysToSubtract = this.GetInt(30, 1000);
                var earliestProjStartDate = projEndDate.AddDays(-daysToSubtract);
                var projStartDate = this.GetRandomDate(earliestProjStartDate, projEndDate);

                var newEmpProj = new EmployeesProject()
                {
                    Employee = this.GetAssignedEmployee(availableEmployees),
                    StartDate = projStartDate,
                    EndDate = projEndDate,
                    Project = this.GetAssignedProject(availableProjects),
                };

                this.generated.Add(newEmpProj);
            }

            return this.generated;
        }

        private Project GetAssignedProject(ICollection<Project> availableProjects)
        {
            var projectsWithLessThan20Employees = availableProjects
                .Where(p => p.EmployeesProjects.Count < 20)
                .ToList();

            var selectedProject = projectsWithLessThan20Employees[ rnd.Next(0, projectsWithLessThan20Employees.Count) ];

            return selectedProject;
        }

        private Employee GetAssignedEmployee(IList<Employee> availableEmployees)
        {
            var result = availableEmployees[rnd.Next(0, availableEmployees.Count)];
            return result;
        }
    }
}
