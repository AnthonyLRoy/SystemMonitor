using System.Collections.Generic;
using SimpleConfigSections;
using Configuration = SimpleConfigSections.Configuration;
namespace Dan.monitor.Common
{

    public class ExternalReaderLibrarySection : ConfigurationSection<IExternalReaderLibrary>
    {
    }

    public interface IExternalReaderLibrary
    {
        string StringValue { get; }
        ILibrary library { get; }
        IEnumerable<ILibrary> libraries { get; }
    }

    public interface ILibrary
    {
        string name { get; }
        string location { get; }
    }

}
