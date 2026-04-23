
namespace DuplicateFinder.Core.Tests
{
    public class DuplicateFinderServiceTests
    {
        [Fact]
        public void FindDuplicates_ReturnsOneGroup_WhenTwoFilesHaveSameContent()
        {
            //arrange
            var dir = Create_DirectoryWithFiles(
                ("folder1", "file1.txt", "Hello"),
                ("folder2", "file2.txt", "Hello")
                );

            var duplicateFinderService = new DuplicateFinderService();

            try
            {
                var duplicateGroup = duplicateFinderService.FindDuplicates(dir);

                var filesInGroup = Assert.Single(duplicateGroup.Values);
                Assert.Equal(2, filesInGroup.Count);
            }
            finally
            {
                DeleteDirectory(dir);
            }
        }
        [Fact]
        public void FindDuplicates_ReturnsNoGroups_WhenAllFilesAreUnique()
        {
            var dir = Create_DirectoryWithFiles(
                ("folder1", "file1.txt", "Hello"),
                ("folder2", "file2.txt", "world")
                );
            
            var duplicateFinderService = new DuplicateFinderService();

            try
            {
                var duplicateGroup = duplicateFinderService.FindDuplicates(dir);

                Assert.Empty(duplicateGroup);
            }
            finally
            {
                DeleteDirectory(dir);
            }
        }
        [Fact]
        public void FindDuplicates_ReturnsEmptyResult_WhenDirectoryIsEmpty()
        {
            var dir = Create_DirectoryWithFiles();

            var duplicateFinderService = new DuplicateFinderService();

            try
            {
                var duplicateGroup = duplicateFinderService.FindDuplicates(dir);

                Assert.Empty(duplicateGroup);
            }
            finally
            {
                DeleteDirectory(dir);
            }
        }
        [Fact]
        public void FindDuplicates_ReturnsEmptyResult_WhenOnlyOneUniqueFileExists()
        {
            var dir = Create_DirectoryWithFiles(
               ("folder1", "file1.txt", "Hello")
               );

            var duplicateFinderService = new DuplicateFinderService();

            try
            {
                var duplicateGroup = duplicateFinderService.FindDuplicates(dir);

                Assert.Empty(duplicateGroup);
            }
            finally
            {
                DeleteDirectory(dir);
            }
        }
        [Fact]
        public void FindDuplicates_ReturnsOnlyDuplicateGroups_WhenMixedFilesExist()
        {
            var dir = Create_DirectoryWithFiles(
                ("folder1", "file1.txt", "Hello"),
                ("folder2", "file2.txt", "Hello"),
                ("folder3", "file3.txt", "!"),
                ("folder4", "file4.txt", "world")
                );

            var duplicateFinderService = new DuplicateFinderService();

            try
            {
                var duplicateGroup = duplicateFinderService.FindDuplicates(dir);


                var filesInGroup = Assert.Single(duplicateGroup.Values);
                Assert.Equal(2, filesInGroup.Count);
            }
            finally
            {
                DeleteDirectory(dir);
            }
        }

        private string Create_DirectoryWithFiles(params (string subFolder, string fileName, string content)[] files)
        {
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), $"DuplicateFinderTests_{Guid.NewGuid():N}");
            
            Directory.CreateDirectory(tempDirectoryPath);

            foreach (var (subFolder, fileName, content) in files)
            {
                string folderPath = Path.Combine(tempDirectoryPath, subFolder);
                Directory.CreateDirectory(folderPath);
                string filePath = Path.Combine(folderPath, fileName);
                File.WriteAllText(filePath, content);
            }

            return tempDirectoryPath;
        }

        private static void DeleteDirectory(string dir)
        {
            if (Directory.Exists(dir))
                Directory.Delete(dir, recursive: true);
        }
    }
}
