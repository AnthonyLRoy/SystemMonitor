namespace Dan.Client.DirectoryReader.Processors
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DirectorySizeCounter : IDirectoryProcessor
    {
        public async Task<int> ProcessAsync(directoriesDirectory directory)
        {
            var result = new DirectoryInfo(directory.counter.uncdirectory).GetFiles(directory.counter.filter);
            return result.ToList().Sum(file => (int)file.Length);
        }
    }
}
