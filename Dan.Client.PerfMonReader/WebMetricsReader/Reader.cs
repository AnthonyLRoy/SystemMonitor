// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Reader.cs" company="DanSys">
//   
// </copyright>
// <summary>
//   The events reader.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace WebMetricsReader
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Dan.Common.Messages;
    using Dan.monitor.Common;

    using Newtonsoft.Json;

    /// <summary>
    /// The events reader.
    /// </summary>
    [WebEventsReaderAttribue(true)]
    public class EventsReader : IWebMetricsReader<MetricValue>
    {
        #region Fields

        /// <summary>
        /// The _name.
        /// </summary>
        private readonly string _name;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsReader"/> class.
        /// </summary>
        public EventsReader()
        {
            this._name = "TestReader";
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get groups async.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<GroupName>> GetGroupsAsync(string filter)
        {
            var results = await this.Get(string.Format(LocalResources.webapigroups, filter));
            var groupnames = JsonConvert.DeserializeObject<List<GroupName>>(results);
            foreach (var group in groupnames)
            {
                group.Id = this._name;
            }

            return groupnames;
        }

        /// <summary>
        /// The get metrics async.
        /// </summary>
        /// <param name="eventIds">
        /// The event ids.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task<List<MetricValue>> GetMetricsAsync(List<string> eventIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get metrics by group name async.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<List<MetricValue>> GetMetricsByGroupNameAsync(string groupName)
        {
            var results = await this.Get(string.Format(LocalResources.webapibygroup, groupName));
            return JsonConvert.DeserializeObject<List<MetricValue>>(results);
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create web connection.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestmethod">
        /// The requestmethod.
        /// </param>
        /// <returns>
        /// The <see cref="HttpRequestMessage"/>.
        /// </returns>
        private HttpRequestMessage CreateWebConnection(string url, string requestmethod)
        {
            var request = new System.Net.Http.HttpRequestMessage();
            request.Headers.Clear();
            var authHeader = this.GetAutenticationToken();
            request.Method = new HttpMethod(requestmethod);
            request.Headers.Add("Authorization", authHeader);
            request.RequestUri = new Uri(url);
            return request;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<string> Get(string url)
        {
            string content = null;
            var request = this.CreateWebConnection(url, WebRequestMethods.Http.Get);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
            }
            else
            {
                return null;
            }

            return content;
        }

        /// <summary>
        /// The get autentication token.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetAutenticationToken()
        {
            return string.Format("WRAP access_token=\"{0}\"", LocalResources.acstoken);
        }

        /// <summary>
        /// The valid data.
        /// </summary>
        /// <param name="metricCounters">
        /// The metric counters.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool ValidData(List<string> metricCounters)
        {
            return true;
        }

        #endregion
    }
}