namespace CompanyDb.RandomDataGeneration
{
    using System.Collections.Generic;
    using System.Linq;

    using CompanyDb.Data;

    public class RandomEmployeeGenerator : RandomDataGenerator
    {
        private const int ChanceToHaveManager = 95;
        private const decimal MinSalary = 50000m;
        private const decimal MaxSalary = 200000m;
        private CompanyDbEntities db;
        private IList<Employee> generatedEmployees;
        private IList<Employee> managers;
        private int alreadyGeneratedCount;

        public IList<Employee> GenerateRandomEmployees(int count, CompanyDbEntities db)
        {
            this.alreadyGeneratedCount = 0;
            this.generateCount = count;
            this.db = db;
            this.generateCount = count;
            this.generatedEmployees = new List<Employee>(count);
            this.managers = new List<Employee>();

            for (int i = 0; i < this.generateCount; i++)
            {
                var newEmployee = new Employee()
                {
                    FirstName = this.GetString(5, 20, DataType.LowerLetters),
                    LastName = this.GetString(5, 20, DataType.LowerLetters),
                    DepartmentID = GetAssignedDepartmentId(),
                    Employee1 = GetAssignedManager(),
                    YearlySalary = this.GetSalary(),
                };

                if (newEmployee.Employee1 == null)
                {
                    this.managers.Add(newEmployee);
                }

                this.generatedEmployees.Add(newEmployee);
                this.alreadyGeneratedCount++;
            }

            return this.generatedEmployees;
        }

        private decimal GetSalary()
        {
            var salary = MinSalary + (decimal)this.GetDouble() * (MaxSalary - MinSalary);
            return salary;
        }

        private Employee GetAssignedManager()
        {
            var shouldHaveManager = base.RollDice(ChanceToHaveManager);

            if (shouldHaveManager == false || this.managers.Count == 0)
            {
                return null;
            }

            var assignedManager = this.managers[rnd.Next(0, this.managers.Count)];
            return assignedManager;
        }

        private int GetAssignedDepartmentId()
        {
            // Single responsibility :P
            int assignedDepartmentId = this.AssignDepartment();
            return assignedDepartmentId;
        }

        private int AssignDepartment()
        {
            var availableDepartments = this.db.Departments
                .Select(d => d.DepartmentID)
                .ToList(); // Check to switch back to IQueriable if needed

            int assignedDepartment = 0;

            if (availableDepartments.Count > 0)
            {
                assignedDepartment =
                    availableDepartments[
                        RandomDataGenerator.rnd.Next(0, availableDepartments.Count)];
            }

            return assignedDepartment;
        }
    }
}
