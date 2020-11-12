using Newtonsoft.Json;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class CountryReadModel
    {
        /// <summary>
        /// Gets or sets the country ID
        /// </summary>
        /// <value>
        /// The country ID
        /// </value>
        public int CountryID { get; set; }

        /// <summary>
        /// Gets or sets the country name
        /// </summary>
        /// <value>
        /// The country name
        /// </value>
        [JsonProperty("Country")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the ISO of country
        /// </summary>
        /// <value>
        /// The ISO of country
        /// </value>
        public string ISO { get; set; }

        /// <summary>
        /// Gets or sets the default currency ID
        /// </summary>
        /// <value>
        /// The default currency ID
        /// </value>
        public int DefaultCurrencyID { get; set; }
    }
}