using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace CheckFolders
{
   internal class JsonConfiguration
    {
        public static string ConfigJSON = "./settings.json";

        public static void CheckOrCreate()
        {
            if (!File.Exists(ConfigJSON))
            {
                Console.WriteLine("Write path to needle folder:");
                string path = "";
                PathCheck(ref path);
                using FileStream fs = new FileStream(ConfigJSON, FileMode.OpenOrCreate);
                ConfigModel result = new ConfigModel(path);
                JsonSerializer.Serialize(fs, result);
                Console.WriteLine("Json has been created");
            }
        }
        public static ConfigModel GetJsonValue()
        {
            ConfigModel result = new ConfigModel();
            using (FileStream fs = new FileStream(ConfigJSON, FileMode.OpenOrCreate))
            {
                ConfigModel? tmp = JsonSerializer.Deserialize<ConfigModel>(fs);
                if (tmp != null)
                {
                    result = tmp;
                }
            }

            return result;
        }

        private static void PathCheck(ref string path)
        {
            while (true)
            {
                path = Console.ReadLine();
                if (path == "")
                {
                    Console.WriteLine("The path can't be empty");
                    continue;
                }
                else if (File.Exists(path))
                {
                    Console.WriteLine("You can't monitor a file");
                    continue;
                }
                else if (!Directory.Exists(path))
                {
                    Console.WriteLine("The wrong path");
                    continue;
                }
                break;
            }
        }
    }
}
