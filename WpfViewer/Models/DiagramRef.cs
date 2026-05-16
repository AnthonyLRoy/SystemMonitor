namespace SystemViewer.Models
{
    public class DiagramRef
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }

        public DiagramRef()
        {

        }
        public DiagramRef(string id, string name, string description, string creator)
        {
            ID = id;
            Name = name;
            Description = description;
            Creator = creator;
        }
    }

}
