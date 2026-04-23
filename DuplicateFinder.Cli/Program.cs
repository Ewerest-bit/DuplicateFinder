using DuplicateFinder.Core;
namespace DuplicateFinder.Cli
{
    internal class Program
    {
        
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: DuplicateFinder.Cli <path>");
                return 1;
            }

            string path = args[0];

            try
            {
                var duplicateFinderService = new DuplicateFinderService();
                var files = duplicateFinderService.FindDuplicates(path);

                
                if (!files.Any())
                {
                    Console.WriteLine("Дубликаты не найдены");
                    return 0;
                }

                int num = 1;
                int countGroups = files.Count;
                int countFilesInGroup = 0;
                int extraCopies = 0;
                foreach ( var file in files)
                {
                    Console.WriteLine($"Группа {num++}: ");
                    Console.WriteLine($"Хэш - {file.Key}");
                    extraCopies += file.Value.Count() - 1;
                    foreach (var value in file.Value)
                    {   
                        Console.WriteLine($"\t{value}");
                        countFilesInGroup++;
                    }
                    Console.WriteLine();
                }

                
                Console.WriteLine("Количество групп: " + countGroups);
                Console.WriteLine("Количество файлов в группах: " + countFilesInGroup);
                Console.WriteLine("Количество лишних копий: " + extraCopies);

                    return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return 1;
            }
            
        }
    }
}
