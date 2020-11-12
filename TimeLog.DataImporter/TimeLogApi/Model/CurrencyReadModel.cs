namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class CurrencyReadModel
    {
        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public int CurrencyID { get; set; }

        /// <summary>
        /// Gets or sets the currency abb.
        /// </summary>
        /// <value>
        /// The currency abb.
        /// </value>
        public string CurrencyABB { get; set; }

        /// <summary>
        /// Gets or sets the name of the descriptive.
        /// </summary>
        /// <value>
        /// The name of the descriptive.
        /// </value>
        public string DescriptiveName { get; set; }

        /// <summary>
        /// Gets or sets the current rate.
        /// </summary>
        /// <value>
        /// The current rate.
        /// </value>
        public double CurrentRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}