using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PmLab.NetTest
{
    internal class Program
    {
        private const string Url = "https://gist.githubusercontent.com/skalinets/23691610f9bbf590b6fba51e373375b4/raw/0b9cc1e97650f5204edbd7b1906e03435506eaf7/mess.txt";
        private const int TrashCount = 10;

        static void Main(string[] args)
        {
            var inputStr = GetString(Url);

            var result = CleanString(inputStr, TrashCount);

            Console.WriteLine(result);
            Console.ReadKey(true);
        }

        private static string CleanString(string input, int trashCharCount)
        {
            var charDict = new Dictionary<char, int>();

            foreach (var c in input.ToCharArray())
            {
                charDict.AddOrUpdate(c);
            }

            return SpitJoin(input, charDict.Where(x => x.Value >= trashCharCount).Select(x => x.Key));
        }

        private static string SpitJoin(string input, IEnumerable<char> removeList)
        {
            return string.Join(string.Empty, input.Split(removeList.ToArray()));
        }

        private static string GetString(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

    internal static class Extensions
    {
        internal static void AddOrUpdate(this Dictionary<char, int> map, char key)
        {
            if (map.ContainsKey(key))
            {
                var cnt = map[key];

                map[key] = cnt + 1;
            }
            else
            {
                map.Add(key, 1);
            }
        }
    }
}
