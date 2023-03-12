using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Access.Models
{
    public class User
    {
        public string Name;

        public bool isRead;
        public bool isWrite;
        public bool isGrant;

        public string isAccess;

        private static readonly Random random = new Random();

        public static List<string> rights = new List<string>() { "Top Secret ", "Secret", "Open Data" };

        public static string RandomNumbers(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static bool RandomBoolFlags()
        {
            return Convert.ToBoolean(random.Next(2));
        }
        public int RandomIntFlags()
        {
            int index = random.Next(rights.Count);
            return index;
        }

        public User (User previousUser)
        {
            this.Name = previousUser.Name;
            this.isRead = previousUser.isRead;
            this.isWrite = previousUser.isWrite;
            this.isGrant = previousUser.isGrant;
        }

        public User() 
        {
            this.Name = RandomNumbers(6);
            this.isRead = RandomBoolFlags();
            this.isWrite = RandomBoolFlags();
            this.isGrant = RandomBoolFlags();
            this.isAccess = rights[RandomIntFlags()];
        }

        public void SetPerm()
        {
            this.isRead = RandomBoolFlags();
            this.isWrite = RandomBoolFlags();
            this.isGrant = RandomBoolFlags();
            this.isAccess = rights[RandomIntFlags()];
        }

        public string DAC()
        {
            string result = String.Empty;
            result += Name + ": ";
            if (isRead == true)
            {
                result += "R";
            }
            if (isWrite == true)
            {
                result += "W";
            }
            if (isGrant == true)
            {
                result += "G";
            }
            if (isRead != true && isWrite != true && isGrant != true) 
            {
                result = Name + ": " + "none";
            }
            result += Environment.NewLine;
            return result;
        }

        public string MAC()
        {
            string result = String.Empty;
            result += Name + ": " + rights[RandomIntFlags()] + Environment.NewLine;
            return result;
        }
    }
}
