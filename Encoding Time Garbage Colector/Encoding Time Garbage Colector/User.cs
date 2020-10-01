using System;
using System.Collections.Generic;
using System.Text;

namespace Encoding_Time_Garbage_Colector
{
    public class User
    {
        private static int id = 0;

        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }



        public User(string firstName, string lastName, DateTime birthday)
        {
            Id = GetNextId();
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        // Use string formatting
        public override string ToString()
        {
            string format = "Id: {0, -10:D} Name: {1, -30} Birthday: {2, -20}";
            return string.Format(format, Id, FirstName + " " + LastName, Birthday);
        }

        private int GetNextId()
        {
            return id++;
        }
    }

    
}
