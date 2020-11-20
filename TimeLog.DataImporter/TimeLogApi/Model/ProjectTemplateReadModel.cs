using System;

namespace TimeLog.DataImporter.TimeLogApi.Model
{
    public class ProjectTemplateReadModel
    {
        /// <summary>
        /// Gets or sets the project template GUID
        /// </summary>
        /// <value>
        /// The project template GUID
        /// </value>
        public Guid ProjectTemplateGuid { get; set; }

        /// <summary>
        /// Gets or sets the project template ID
        /// </summary>
        /// <value>
        /// The project template ID
        /// </value>
        public int ProjectTemplateID { get; set; }

        /// <summary>
        /// Gets or sets the project template name
        /// </summary>
        /// <value>
        /// The project template name
        /// </value>
        public string ProjectTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the project template description
        /// </summary>
        /// <value>
        /// The project template description
        /// </value>
        public string ProjectTemplateDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }
    }
}