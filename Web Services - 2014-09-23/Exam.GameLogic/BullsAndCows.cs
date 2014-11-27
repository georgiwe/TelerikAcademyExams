namespace Exam.GameLogic
{
    using System.Collections.Generic;

    public static class BullsAndCows
    {
        public static bool IsNumberValid(string number)
        {
            var hasAnythingButDigits = false;
            var uniqueDigits = new HashSet<char>();

            foreach (var digit in number)
            {
                uniqueDigits.Add(digit);

                if (char.IsDigit(digit) == false)
                {
                    hasAnythingButDigits = true;
                    break;
                }
            }

            var hasRepeatingDigits = uniqueDigits.Count != 4;

            if (string.IsNullOrWhiteSpace(number) ||
                number.Length != 4 ||
                hasAnythingButDigits ||
                hasRepeatingDigits)
            {
                return false;
            }

            return true;
        }
    }
}
