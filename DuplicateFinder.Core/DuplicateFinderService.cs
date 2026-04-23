using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFinder.Core
{
    public class DuplicateFinderService
    {
        public Dictionary<string, List<string>> FindDuplicates(string rootPath)
        {
            var fileScanner = new FileScanner();
            var fileHasher = new FileHasher();

            var allFiles = fileScanner.FileScan(rootPath);

            var duplicateGroups = new Dictionary<string, List<string>>();
            foreach (var file in allFiles)
            {
                var hash = fileHasher.GetHash(file);
                if (!duplicateGroups.ContainsKey(hash))
                    duplicateGroups[hash] = new List<string>();

                duplicateGroups[hash].Add(file);
            }

            var filteredGroup = new Dictionary<string, List<string>>();
            foreach (var group in duplicateGroups)
            {
                if (group.Value.Count > 1)
                {
                    filteredGroup[group.Key] = group.Value;
                }
            }

            return filteredGroup;   

        }
    }
}
