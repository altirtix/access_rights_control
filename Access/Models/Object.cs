using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Access.Models
{
    public class Object
    {
        public string Name;
        public string Right;

        private static readonly Random random = new Random();

        public static List<string> rights = new List<string>() { "Top Secret ", "Secret", "Open Data" };

        public static string RandomStrings(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int RandomFlags()
        {
            int index = random.Next(rights.Count);
            return index;
        }

        public Object()
        {
            this.Name = RandomStrings(6);
            this.Right = rights[RandomFlags()];
        }

        public override string ToString()
        {
            return Name + ": " + Right;
        }

    }
}
