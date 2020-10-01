using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

namespace Encoding_Time_Garbage_Colector
{
    class Program
    {
        public const string path = @"C:\Eugen Files\HomeWorks\Encoding Time Garbage Colector"; // put your path here

        static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User ("Eugeniu", "Ciolacu", new DateTime (1990, 01, 01)),
                new User ("Alexandr", "Racovschi", new DateTime (1990, 01, 01)),
                new User ("Test", "Test", new DateTime (1990, 01, 01))
            };



            Helper helper = new Helper();
            helper.WriteToFileAllUsersAsUTF8(users, path);



            Console.WriteLine(helper.FindInFileUserWithId(path, 2));
            Console.WriteLine("\n\n\n");




            if (users.Count >= 2)
            {
                helper.BubbleSortByLastName(users);
            }

            foreach (User item in users)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n\n\n");



            string age = helper.GetUserAgeAsString(users[0]);
            Console.WriteLine($"{users[0].FirstName} age is: {age} years");
            Console.WriteLine("\n\n\n");



            helper.UselessExample();
            Console.WriteLine("\n\n\n");



            FileStreamAndDisposePatternExample example = new FileStreamAndDisposePatternExample(path);
            example.AddDateToFile();
            Console.WriteLine(example.GetDateFromFile());
            example.Dispose(true);
            example = null;

            Console.ReadKey();

        }
    }
}
