using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dan.Client.DirectoryReader
{
    using Dan.Client.DirectoryReader.Processors;

    public static class DirectoryProcessorFactory
    {
        internal static IDirectoryProcessor GetProcessor(directoriesDirectoryCounterType directoriesDirectoryCounterType)
        {
            switch (directoriesDirectoryCounterType)
            {
                   case directoriesDirectoryCounterType.count:
                    return new DirectoryFileCounter();
                   case directoriesDirectoryCounterType.lastmodified:
                    return new DirectoryLastModifiedCounter();
                   case directoriesDirectoryCounterType.size:
                    return new DirectorySizeCounter();
                case directoriesDirectoryCounterType.oldest:
                    return new DirectoryOldestCounter();
                default:
                    return null;
            }
        }
    }
}
