using System;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class LegalEntityReadModel
    {
        /// <summary>
        /// Gets or sets the Legal Entity identifier.
        /// </summary>
        /// <value>
        /// The Legal Entity identifier.
        /// </value>
        public int LegalEntityID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets the Currency ISO.
        /// </summary>
        /// <value>
        /// The Currency ISO.
        /// </value>
        public string CurrencyISO { get; set; }

        /// <summary>
        /// Gets or sets the Country ISO.
        /// </summary>
        /// <value>
        /// The Country ISO.
        /// </value>
        public string CountryISO { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is the system default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is system default; otherwise, <c>false</c>.
        /// </value>
        public bool IsSystemLegalEntity { get; set; }
    }
}