using System;


namespace CheckFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            JsonConfiguration.CheckOrCreate();
            ConfigModel config = JsonConfiguration.GetJsonValue();
            Console.WriteLine($"Current path is \"{config.Path}\"");
            ChangesWatcher.WatchAndProcessingChanges();
        }
    }
}