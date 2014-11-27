namespace CompanyDb.RandomDataGeneration
{
    using System.Collections.Generic;

    using Data;

    public class RandomDepartmentGenerator : RandomDataGenerator
    {
        private const int DepNameMinLength = 10;
        private const int DepNameMaxLength = 50;
        private ICollection<Department> generatedDepartments;
        private ICollection<string> generatedNames;

        public IEnumerable<Department> GenerateDepartments(int number)
        {
            this.generateCount = number;
            generatedDepartments = new List<Department>(number);
            generatedNames = new List<string>(number);

            for (int i = 0; i < number; i++)
            {
                var newDepartment = new Department()
                {
                    Name = GetUniqueDepartmentName()
                };

                this.generatedDepartments.Add(newDepartment);
            }

            return generatedDepartments;
        }

        private string GetUniqueDepartmentName()
        {
            string newDepName = string.Empty;

            do
            {
                newDepName = GetString(
                    DepNameMinLength, DepNameMaxLength, DataType.LowerLetters);
                newDepName = char.ToUpper(newDepName[0]) + newDepName.Substring(1);
            }
            while (generatedNames.Contains(newDepName));

            this.generatedNames.Add(newDepName);
            return newDepName;
        }
    }
}
