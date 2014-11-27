namespace CompanyDb.RandomDataGeneration
{
    using System;
    using System.Collections.Generic;

    using Data;

    public class RandomProjectGenerator : RandomDataGenerator
    {
        private const int ProjectNameMinLength = 5;
        private const int ProjectNameMaxLength = 50;

        private ICollection<string> generatedNames;

        public ICollection<Project> GenerateRandomProjects(int count)
        {
            this.generateCount = count;
            var generatedProjects = new List<Project>(count);
            this.generatedNames = new List<string>(count);

            for (int i = 0; i < generateCount; i++)
            {
                var newProject = new Project()
                {
                    Name = GetUniqueName(),
                };

                generatedProjects.Add(newProject);
            }

            return generatedProjects;
        }

        private string GetUniqueName()
        {
            string newProjectName = string.Empty;

            do
            {
                newProjectName = GetString(
                    ProjectNameMinLength, ProjectNameMaxLength, DataType.LowerLetters);
                newProjectName = 
                    char.ToUpper(newProjectName[0]) + newProjectName.Substring(1);
            }
            while (this.generatedNames.Contains(newProjectName));

            this.generatedNames.Add(newProjectName);
            return newProjectName;
        }
    }
}
