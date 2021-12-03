using System.Xml.Serialization;

namespace TimeLog.ReportingAPI.SDK;

/// <summary>
///     Placeholder for allocation related constants and properties
/// </summary>
public class Allocation
{
    /// <summary>
    ///     Gets the default parameter value for filtering for all allocations
    /// </summary>
    [XmlIgnore]
    public static int All => 0;

    /// <summary>
    ///     Gets or sets the identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     Gets or sets the project identifier
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    ///     Gets or sets the task identifier
    /// </summary>
    public int TaskId { get; set; }

    /// <summary>
    ///     Gets or sets the employee identifier
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    ///     Gets or sets the hours allocated
    /// </summary>
    public float AllocatedHours { get; set; }

    /// <summary>
    ///     Gets or sets the hourly rate
    /// </summary>
    public float HourlyRate { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the associated task is fixed price
    /// </summary>
    public bool TaskIsFixedPrice { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether the allocation is active
    /// </summary>
    public bool IsActive { get; set; }
}