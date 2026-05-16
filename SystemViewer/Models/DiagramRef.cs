// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagramRef.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The diagram ref.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SystemViewer.Models
{
    /// <summary>
    /// The diagram ref.
    /// </summary>
    public class DiagramRef
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramRef"/> class.
        /// </summary>
        public DiagramRef()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiagramRef"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="creator">
        /// The creator.
        /// </param>
        public DiagramRef(string id, string name, string description, string creator)
        {
            this.ID = id;
            this.Name = name;
            this.Description = description;
            this.Creator = creator;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the creator.
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}