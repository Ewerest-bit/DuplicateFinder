using DuplicateFinder.Core;

namespace DuplicateFinder.Core.Tests
{
    public class FileScannerTests
    {
        [Fact]
        public void FileScanner_ReturnsAllFiles_FromNestedFolders()
        {
            //arrange
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), $"DuplicateFinderTests_{Guid.NewGuid():N}");

            string subFolder1Path = Path.Combine(tempDirectoryPath, "SubFolder1");
            string subFolder2Path = Path.Combine(tempDirectoryPath, "SubFolder2");

            Directory.CreateDirectory(tempDirectoryPath);
            Directory.CreateDirectory(subFolder1Path);
            Directory.CreateDirectory(subFolder2Path);

            string file1Path = Path.Combine(subFolder1Path, "file1.txt");
            string file2Path = Path.Combine(subFolder2Path, "file2.txt");

            File.WriteAllText(file1Path, "Hello");
            File.WriteAllText(file2Path, "world");

            var fileScanner = new FileScanner();
            try
            {
                //act
                var result = fileScanner.FileScan(tempDirectoryPath);

                //assert
                Assert.Equal(2, result.Length);
            }
            finally
            {
                if (Directory.Exists(tempDirectoryPath))
                    Directory.Delete(tempDirectoryPath, recursive: true);
            }

        }

        [Fact]
        public void FileScanner_ThrowsDirectoryNotFoundException_WhenPathDoesNotExist()
        {
            //arrange
            string path = Path.Combine(Path.GetTempPath(), $"NotExistFolder_{Guid.NewGuid()}");
            var fileScanner = new FileScanner();

            //assert + act
            Assert.Throws<DirectoryNotFoundException>(() => fileScanner.FileScan(path));
        }
    }
}
