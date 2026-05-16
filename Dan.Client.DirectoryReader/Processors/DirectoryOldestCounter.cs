namespace Dan.Client.DirectoryReader.Processors
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DirectoryOldestCounter : IDirectoryProcessor
    {
        public async Task<int> ProcessAsync(directoriesDirectory directory)
        {
            var local = new DirectoryInfo(directory.counter.uncdirectory);
            var myFile = (from f in local.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).Last();
            if (myFile == null) return -1;
            return (int)(DateTime.Now.ToUniversalTime() - myFile.LastWriteTime.ToUniversalTime()).TotalSeconds;
        }
    }
}
