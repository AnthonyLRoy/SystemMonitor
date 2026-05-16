using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dan.Client.DirectoryReader
{
    using System.Threading.Tasks;

    using Dan.Common.Messages;

    public interface IDirectoryProcessor
    {
       Task<int> ProcessAsync(directoriesDirectory directory);
    }
}
