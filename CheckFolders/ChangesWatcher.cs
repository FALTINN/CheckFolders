namespace CheckFolders
{
    internal class ChangesWatcher
    {
        private static List<string> SessionCreations = new List<string>{};
        private static List<string> SessionChangesInFiles = new List<string> { };
        private static List<string> SessionDeletions = new List<string> { };

        public static void WatchAndProcessingChanges()
        {
            string path = JsonConfiguration.GetJsonValue().Path;
            using var watcher = new FileSystemWatcher($"{path}");


            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;

            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            PrintSessionChanges();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            string ChangeText = $"Changed: {e.FullPath}";
            Console.WriteLine(ChangeText);
            SessionChangesInFiles.Add(e.FullPath);
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string ChangeText = $"Created: {e.FullPath}";
            Console.WriteLine(ChangeText);
            SessionCreations.Add(e.FullPath);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string ChangeText = $"Deleted: {e.FullPath}";
            Console.WriteLine(ChangeText);
            SessionDeletions.Add(e.FullPath);
        }

        private static void PrintSessionChanges()
        {
            if(SessionChangesInFiles.Count > 0)
            {
                Console.WriteLine("Changes in Files:");
                SessionIterator(SessionChangesInFiles);
            }
            if (SessionCreations.Count > 0)
            {
                Console.WriteLine("Created Files:");
                SessionIterator(SessionCreations);
            }
            if (SessionDeletions.Count > 0)
            {
                Console.WriteLine("Deleted Files:");
                SessionIterator(SessionDeletions);
            }
        }

        private static void SessionIterator(List<string> IterableList)
        {
            var IterableDistinctList = IterableList.Distinct();
            foreach(string Text in IterableDistinctList)
            {
                Console.WriteLine($"-{Text}");
            }
        }
    }
}
