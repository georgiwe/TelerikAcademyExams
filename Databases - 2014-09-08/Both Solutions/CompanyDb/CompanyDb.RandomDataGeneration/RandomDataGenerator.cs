namespace CompanyDb.RandomDataGeneration
{
    using System;

    public abstract class RandomDataGenerator
    {
        protected static Random rnd = new Random();

        protected const string allAlphaNumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        protected const string smallLetters = "abcdefghijklmnopqrstuvwxyz";
        protected const string bigLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        protected const string numerics = "1234567890";
        protected const string allLeters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        protected int generateCount;

        protected virtual string GetStringExact(int length, DataType type)
        {
            string dataSource = GetDataSource(type);

            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = dataSource[rnd.Next(0, dataSource.Length)];
            }

            return new string(result);
        }

        protected virtual string GetString(int min, int max, DataType type)
        {
            return GetStringExact(rnd.Next(min, max + 1), type);
        }

        protected virtual int GetInt(int min, int max)
        {
            return rnd.Next(min, max + 1);
        }

        protected virtual double GetDouble()
        {
            return rnd.NextDouble();
        }

        protected virtual bool RollDice(int percent)
        {
            return rnd.Next(0, 101) <= percent;
        }

        protected virtual string GetDataSource(DataType type)
        {
            switch (type)
            {
                case DataType.Alphanumeric:
                    return allAlphaNumeric;
                case DataType.CapitalLetters:
                    return bigLetters;
                case DataType.LowerLetters:
                    return smallLetters;
                case DataType.Numbers:
                    return numerics;
                case DataType.CapitalAndLowerLetters:
                    return allLeters;
                default:
                    throw new ArgumentException();
            }
        }

        protected virtual DateTime GetRandomDate(DateTime min, DateTime max)
        {
            if (min == (new DateTime()))
            {
                min = new DateTime(2013, 1, 1);
            }

            if (max == (new DateTime()))
            {
                max = new DateTime(2025, 1, 1);
            }

            int rangeOfDays = (max - min).Days;
            var result = min.AddDays(rnd.Next(rangeOfDays));
            return result;
        }
    }
}
