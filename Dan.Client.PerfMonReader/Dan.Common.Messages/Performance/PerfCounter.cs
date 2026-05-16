// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerfCounter.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The perf counter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Dan.Common.Messages.Performance
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;

    using Newtonsoft.Json;

    /// <summary>
    /// The perf counter.
    /// </summary>
    public class PerfCounter
    {
        #region Fields

        /// <summary>
        /// The _pcounter.
        /// </summary>
        [NonSerialized]
        private PerformanceCounter _pcounter;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfCounter"/> class.
        /// </summary>
        public PerfCounter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PerfCounter"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="category">
        /// The category.
        /// </param>
        /// <param name="counter">
        /// The counter.
        /// </param>
        /// <param name="instance">
        /// The instance.
        /// </param>
        /// <param name="timer">
        /// The timer.
        /// </param>
        /// <param name="applicationname">
        /// The applicationname.
        /// </param>
        /// <param name="instanceName">
        /// The instance name.
        /// </param>
        public PerfCounter(
            string name, 
            string category, 
            string counter, 
            string instance, 
            int timer, 
            string applicationname, 
            string instanceName)
        {
            this.Name = name;
            this.Category = category;
            this.Counter = counter;
            this.Instance = instance;
            this.Timer = timer;
            this.Applicationname = applicationname;
            this.InstanceName = instanceName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the applicationname.
        /// </summary>
        public string Applicationname { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the counter.
        /// </summary>
        public string Counter { get; set; }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        public string Instance { get; set; }

        /// <summary>
        /// Gets or sets the instance name.
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the pcounter.
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public PerformanceCounter Pcounter
        {
            get
            {
                return this._pcounter;
            }

            set
            {
                this._pcounter = value;
            }
        }

        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        public int Timer { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public float Value { get; set; }

        #endregion
    }
}