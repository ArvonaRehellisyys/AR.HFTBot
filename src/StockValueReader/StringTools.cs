using System;
using System.Linq;

namespace StockValueReader
{
    public class StringTools
    {

        private void ParseTime(string input, out int h, out int m, out int s)
        {
            string[] parts = input.Split(' ');

            h = -1;
            m = -1;
            s = -1;

            if (parts.Count() == 2)
            {
                bool isAM = (parts[1].ToLower() == "am");
                parts = parts[0].Split(':');

                if (!(parts.Count() == 3 && int.TryParse(parts[0], out h) && int.TryParse(parts[1], out m) && int.TryParse(parts[2], out s)))
                {
                    h = -1;
                    m = -1;
                    s = -1;
                }
                else if (!isAM)
                    h += 12;
            }
        }

        private void ParseDate(string input, out int d, out int m, out int y)
        {
            string[] parts = input.Split('/');

            if (!(parts.Count() == 3 && int.TryParse(parts[0], out m) && int.TryParse(parts[1], out d) && int.TryParse(parts[2], out y)))
            {
                d = -1;
                m = -1;
                y = -1;
            }
        }

        public DateTime ParseDateTimeFromString(string input)
        {
            string[] parts = input.Split(' ');
            DateTime result = DateTime.MinValue;

            if (parts.Count() == 3)
            {
                int hour;
                int min;
                int sec;
                int day;
                int month;
                int year;

                ParseDate(parts[0], out day, out month, out year);
                ParseTime(parts[1] + " " + parts[2], out hour, out min, out sec);

                if (year > 0 && month > 0 && day > 0 && hour > 0 && min > 0 && sec > 0)
                    result = new DateTime(year, month, day, hour, min, sec);
            }

            return result;
        }
    }
}
