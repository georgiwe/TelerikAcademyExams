namespace CompanyDb.DataGeneration
{
    using System;
    using System.Linq;

    using CompanyDb.Data;
    using CompanyDb.RandomDataGeneration;

    public class EntryPoint
    {
        public static void Main()
        {
            var depCount = 100;
            var projectCount = 1000;
            var reportCount = 250000;
            var empCount = 5000;

            var db = new CompanyDbEntities();
            db.Configuration.AutoDetectChangesEnabled = false;

            var depGenerator = new RandomDepartmentGenerator();
            var departments = depGenerator.GenerateDepartments(depCount);
            db.Departments.AddRange(departments);
            db.SaveChanges();

            var projGenerator = new RandomProjectGenerator();
            var projects = projGenerator.GenerateRandomProjects(projectCount);

            var employeeGeneratr = new RandomEmployeeGenerator();
            var employees = employeeGeneratr.GenerateRandomEmployees(empCount, db);

            var reportGenerator = new RandomReportGenerator();
            var reports = reportGenerator.GenerateReports(reportCount, employees);

            var emplProjGenerator = new RandomEmployeeProjectsGenerator();
            var emplProjects = emplProjGenerator.Generate(projectCount, employees, projects);

            db.Reports.AddRange(reports);
            db.EmployeesProjects.AddRange(emplProjects);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            db.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
