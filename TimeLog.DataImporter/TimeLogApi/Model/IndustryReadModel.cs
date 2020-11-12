using System;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class IndustryReadModel
    {
        /// <summary>
        /// Gets or sets the industry ID
        /// </summary>
        /// <value>
        /// The industry ID
        /// </value>
        public int IndustryID { get; set; }

        /// <summary>
        /// Gets or sets the industry name
        /// </summary>
        /// <value>
        /// The industry name
        /// </value>
        public string IndustryName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the industry GUID
        /// </summary>
        /// <value>
        /// The industry GUID
        /// </value>
        public Guid IndustryGuid { get; set; }
    }
}