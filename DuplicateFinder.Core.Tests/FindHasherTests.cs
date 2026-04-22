
namespace DuplicateFinder.Core.Tests
{
    public class FindHasherTests
    {
        [Theory]
        [InlineData("Hello", "Hello", true)]
        [InlineData("Hello", "world", false)]
        public void GetHash_ReturnsExpectedEquality_BasedOnFileContent(string content1, string content2, bool shouldBeEqual)
        {
            //arrange
            string folder1Path = Path.Combine(Path.GetTempPath(), $"SameFolder1_{Guid.NewGuid():N}");
            string folder2Path = Path.Combine(Path.GetTempPath(), $"SameFolder2_{Guid.NewGuid():N}");

            Directory.CreateDirectory( folder1Path );
            Directory.CreateDirectory( folder2Path );

            string file1Path = Path.Combine(folder1Path, "file1.txt");
            string file2Path = Path.Combine(folder2Path, "file2.txt");

            File.WriteAllText(file1Path, content1);
            File.WriteAllText(file2Path, content2);

            FindHasher hasher = new FindHasher();
            try
            {
                //act
                string hash1 = hasher.GetHash( file1Path );
                string hash2 = hasher.GetHash( file2Path );

                if (shouldBeEqual == true)
                {
                    Assert.Equal( hash1, hash2 );
                }
                else
                {
                    Assert.NotEqual(hash1, hash2 );
                }
            }
            finally
            {
                if (Directory.Exists(folder1Path))
                    Directory.Delete(folder1Path, recursive: true);
                if (Directory.Exists(folder2Path))
                    Directory.Delete(folder2Path, recursive: true);
            }
        }
      
        [Fact]
        public void GetHash_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            //arrange
            string path = Path.Combine(Path.GetTempPath(), $"FileNotFound_{Guid.NewGuid()}");


            FindHasher hasher = new FindHasher();
            //act + assert
            Assert.Throws<FileNotFoundException>(() =>  hasher.GetHash(path));

        }
    }
}
