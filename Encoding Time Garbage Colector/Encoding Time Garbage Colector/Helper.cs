using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Encoding_Time_Garbage_Colector
{
    public class Helper
    {
        // Use encodings in writing to file stream
        public void WriteToFileAllUsersAsUTF8 (List<User> users, string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            using (FileStream fileStream = new FileStream(@$"{path}\all users.txt", FileMode.OpenOrCreate))
            {
                foreach (User item in users)
                {
                    string user = item.ToString();
                    byte[] utf8 = Encoding.UTF8.GetBytes(user + "\n");
                    fileStream.Write(utf8);
                }
            }
        }

        // string searching, encoding
        public string FindInFileUserWithId (string path, int id)
        {
            using (StreamReader file = new StreamReader (@$"{path}\all users.txt"))
            {
                string line = "User not found";

                while ((line = file.ReadLine()) != null)
                {
                    byte[] tmp = Encoding.Default.GetBytes(line);
                    line = Encoding.Default.GetString(tmp);

                    line = line.Replace("Id:", "").TrimStart();

                    if (line.StartsWith(id.ToString()))
                    {
                        return line;
                    }
                }

                return line;
            }
        }

        // string comparing
        public void BubbleSortByLastName (List<User> users)
        {
            CultureInfo currentCultureInfo = CultureInfo.CurrentCulture;

            for (int i = 0; i < users.Count - 1; i++)
            {
                for (int j = 0; j < users.Count - 1; j++)
                {
                    if (string.Compare(users[j].LastName, users[j + 1].LastName, currentCultureInfo, CompareOptions.StringSort) > 0)
                    {
                        User u = users[j];
                        users[j] = users[j + 1];
                        users[j + 1] = u;
                    }
                }
            }
        }

        // DateTime, TimeSpan
        public string GetUserAgeAsString(User user)
        {
            DateTime now = DateTime.Now;

            TimeSpan age = now - user.Birthday;

            double ageInDays = age.TotalDays;

            double daysInYear = 365.2425;

            double ageInYears = ageInDays / daysInYear;

            return string.Format("{0:f1}", ageInYears);
        }

        // Use datetimeoffset in your application
        // Use timezone in your application
        // Use cultureinfo when working with strings and numbers
        public void UselessExample ()
        {
            Console.WriteLine("Formated DateTimeNow: {0}", DateTime.Now.ToString("hh : mm : ss"));

            Console.WriteLine("After 10 hours will be: {0}", (DateTime.Now + TimeSpan.FromHours(10)));
            Console.WriteLine("This is the same as {0} ", DateTime.Now.AddHours(10));
            Console.WriteLine("\n\n\n");

            Console.WriteLine("Current local time {0} ", DateTime.Now);
            Console.WriteLine("Current time as Universal Time {0} ", DateTime.Now.ToUniversalTime());
            Console.WriteLine("\n\n\n");

            DateTime currentTimeAsUtc = DateTime.UtcNow;
            DateTime currentTimeLocal = DateTime.Now;
            TimeSpan difference = currentTimeLocal - currentTimeAsUtc;
            Console.WriteLine("{0} - {1} = {2}", currentTimeLocal, currentTimeAsUtc, difference);
            DateTimeOffset dateTimeOffset1 = DateTimeOffset.Now;
            DateTimeOffset dateTimeOffset2 = DateTimeOffset.UtcNow;
            difference = dateTimeOffset1 - dateTimeOffset2;
            Console.WriteLine("{0} - {1} = {2}", dateTimeOffset1, dateTimeOffset2, difference);
            Console.WriteLine("\n\n\n");

            TimeZone localZone = TimeZone.CurrentTimeZone;
            Console.WriteLine(localZone.StandardName);
            DateTime utcTime = localZone.ToUniversalTime(DateTime.Now);
            Console.WriteLine(utcTime);
            Console.WriteLine("\n\n\n");

            var tzis = TimeZoneInfo.GetSystemTimeZones();
            foreach (var tzi in tzis)
            {
                Console.WriteLine(string.Format("{0}:::{1}", tzi.Id, tzi.DisplayName));
            }
            Console.WriteLine("\n\n\n");

            CultureInfo thisMachineCultureInfo = CultureInfo.CurrentCulture;
            Console.WriteLine("This machine culture: " + thisMachineCultureInfo.TextInfo);


            CultureInfo invariant = CultureInfo.InvariantCulture;
            string str1 = string.Format("I have {0:C} of money", 10).ToString(invariant);
            string str2 = string.Format("I have {0:C} of money", 10).ToString(thisMachineCultureInfo);
            Console.WriteLine(str1 + "\n" + str2);
        }
    }
}
