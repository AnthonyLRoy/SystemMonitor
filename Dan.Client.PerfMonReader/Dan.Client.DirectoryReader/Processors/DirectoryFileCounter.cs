namespace Dan.Client.DirectoryReader.Processors
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DirectoryFileCounter : IDirectoryProcessor
    {
        public async Task<int> ProcessAsync(directoriesDirectory directory)
        {
           var di = new DirectoryInfo(directory.counter.uncdirectory);
           var result =  di.GetFiles(directory.counter.filter).Count();
           return result;
        }
    }
}
